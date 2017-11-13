using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Collections;
using System.Data;
using System.Web.Script.Serialization;
using System.Reflection;
using System.Text.RegularExpressions;

namespace DiggerCommon
{
    public static class DataHelper
    {
        /// <summary>
        /// 获得config中的key值
        /// </summary>
        /// <param name="key">key名称</param>
        /// <returns>key值</returns>
        public static string GetConfig(string key)
        {
            var rvl = string.Empty;
            try
            {
                rvl = System.Configuration.ConfigurationManager.AppSettings[key].ToString();
            }
            catch { }
            return rvl;
        }
        /// <summary>
        /// 获得要执行的脚本
        /// </summary>
        /// <param name="javaScriptFilePathList">脚本地址数组</param>
        /// <returns>要执行的脚本</returns>
        public static string GetJavaScript(List<string> javaScriptFilePathList)
        {
            var res = string.Empty;
            try
            {
                foreach (var item in javaScriptFilePathList)
                {
                    res += File.ReadAllText(item) + "\r\n";
                }
            }
            catch(Exception ex) { throw ex; }
            return res;
        }
        /// <summary>
        /// 字符串转对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="json"></param>
        /// <returns></returns>
        public static T JsonToObject<T>(string json)
        {
            return JsonConvert.DeserializeObject<T>(json);
        }
        /// <summary>
        /// 对象转字符串
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ObjectToJson<T>(T obj)
        {
            return JsonConvert.SerializeObject(obj);
        }

        #region Json 字符串 转换为 DataTable数据集合
        /// <summary>
        /// Json 字符串 转换为 DataTable数据集合
        /// </summary>
        /// <param name="json">json数据</param>
        /// <param name="dataTable">存入的datatable</param>
        /// <returns></returns>
        public static DataTable JsonToDataTable(string json, DataTable dataTable = null)
        {
            if (dataTable == null)
            {
                dataTable = new DataTable();  //实例化
            }
            DataTable result;
            try
            {
                JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
                javaScriptSerializer.MaxJsonLength = Int32.MaxValue; //取得最大数值
                ArrayList arrayList = javaScriptSerializer.Deserialize<ArrayList>(json);
                if (arrayList.Count > 0)
                {
                    foreach (Dictionary<string, object> dictionary in arrayList)
                    {
                        if (dictionary.Keys.Count<string>() == 0)
                        {
                            result = dataTable;
                            return result;
                        }
                        if (dataTable.Columns.Count == 0)
                        {
                            foreach (string current in dictionary.Keys)
                            {
                                dataTable.Columns.Add(current, dictionary[current].GetType());
                            }
                        }
                        DataRow dataRow = dataTable.NewRow();
                        foreach (string current in dictionary.Keys)
                        {
                            dataRow[current] = dictionary[current];
                        }

                        dataTable.Rows.Add(dataRow); //循环添加行到DataTable中
                    }
                }
            }
            catch
            {
            }
            result = dataTable;
            return result;
        }
        #endregion

        #region 将一个实体集合转换为Datatable
        /// <summary>
        /// 将一个实体集合转换为Datatable
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="items">实体集合</param>
        /// <param name="dt">要合并的datatable</param>
        /// <returns></returns>
        public static DataTable ToDataTable<T>(List<T> items, DataTable dt = null)
        {
            if (dt == null)
                dt = new DataTable(typeof(T).Name);

            PropertyInfo[] props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (PropertyInfo prop in props)
            {
                Type t = GetCoreType(prop.PropertyType);
                dt.Columns.Add(prop.Name, t);
            }

            foreach (T item in items)
            {
                var values = new object[props.Length];

                for (int i = 0; i < props.Length; i++)
                {
                    values[i] = props[i].GetValue(item, null);
                }

                dt.Rows.Add(values);
            }

            return dt;
        }
        private static Type GetCoreType(Type t)
        {
            if (t != null && IsNullable(t))
            {
                if (!t.IsValueType)
                {
                    return t;
                }
                else
                {
                    return Nullable.GetUnderlyingType(t);
                }
            }
            else
            {
                return t;
            }
        }
        private static bool IsNullable(Type t)
        {
            return !t.IsValueType || (t.IsGenericType && t.GetGenericTypeDefinition() == typeof(Nullable<>));
        }
        #endregion

        #region datatable数据导出到指定file中
        /// <summary>
        /// datatable数据导出到指定file中
        /// </summary>
        /// <param name="table">datatable</param>
        /// <param name="file">文件地址</param>
        /// <param name="firstLine">第一行数据</param>
        public static void DataTableToCSV(DataTable table, string file, bool firstBatchData)
        {
            bool inUse = true;
            while (inUse)
            {
                try
                {
                    if(File.Exists(file)&&firstBatchData)//替换文件
                    {
                        File.Move(file, file.Split('.')[0] + DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + '.' + file.Split('.')[1]);
                    }
                    FileStream fs = new FileStream(file, FileMode.Append);
                    inUse = false;
                    StreamWriter sw = new StreamWriter(new BufferedStream(fs), System.Text.Encoding.Default);
                    if (firstBatchData)
                    {
                        string title = "";
                        for (int i = 0; i < table.Columns.Count; i++)
                        {
                            title += table.Columns[i].ColumnName + ",";
                        }
                        title = title.Substring(0, title.Length - 1) + "\n";
                        sw.Write(title);
                    }
                    foreach (DataRow row in table.Rows)
                    {
                        string line = "";
                        for (int i = 0; i < table.Columns.Count; i++)
                        {
                            line += ReplaceChar(row[i].ToString()) + ",";
                        }
                        line = line.Substring(0, line.Length - 1) + "\n";
                        sw.Write(line);
                    }
                    sw.Close();
                    fs.Close();
                }
                catch(Exception ex)
                {
                    if(!inUse)
                    {
                        throw ex;
                    }
                    else
                    {
                        System.Threading.Thread.Sleep(100);
                    }
                }
             }
          
        }
        #endregion

        #region 将相对地址转换成绝对地址
        /// <summary>
        /// 将相对地址转换成绝对地址
        /// </summary>
        /// <param name="relativeUrlPath">相对地址</param>
        /// <param name="domianUrlPath">域名</param>
        /// <returns></returns>
        public static string relativeUrlPathToAbsoluteUrlPath(string relativeUrlPath, string domianUrlPath)
        {
            string result = "";
            Regex regRelativeUrlPath = new Regex(@"(/[a-zA-Z0-9\&%_\./-~-]*)?");
            if (regRelativeUrlPath.IsMatch(relativeUrlPath))
            {
                result = new Uri(domianUrlPath).Host + relativeUrlPath;
            }
            else
            {
                Regex regAbsoluteUrlPath = new Regex(@"((http|ftp|https)://)(([a-zA-Z0-9\._-]+\.[a-zA-Z]{2,6})|([0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}))(:[0-9]{1,4})*(/[a-zA-Z0-9\&%_\./-~-]*)?");
                if (regAbsoluteUrlPath.IsMatch(relativeUrlPath))
                {
                    result = relativeUrlPath;
                }
            }
            return result;
        }
        #endregion

        //防止串列
        private static string ReplaceChar(string str)
        {
            string field = str;
            if (field.IndexOf("+") == 0 || field.IndexOf("-") == 0 || field.IndexOf("=") == 0 || field.IndexOf("'") == 0)
            {
                if (IsNum(field))
                {
                    return field;
                }
                return string.Format("\t{0}", field);
            }
            if (field.IndexOf(',') >= 0)
            {
                return string.Format("\"{0}\"", field);
            }
            if (field.IndexOf('"') >= 0)
            {
                return string.Format("\t{0}", field);

            }
            if (field.IndexOf('\r') >= 0)
            {
                return string.Format("\"{0}\"", field);

            }
            if (field.IndexOf('\n') >= 0)
            {
                return string.Format("\"{0}\"", field);

            }
            if (field.IndexOf('\"') >= 0)
            {
                return string.Format("\"{0}\"", field);

            }
            if (field != field.Trim())
            {
                return string.Format("\"{0}\"", field);
            }
            return field;

        }
        private static bool IsNum(object num)
        {
            try
            {
                Convert.ToDecimal(num);
                return true;
            }
            catch
            {

                return false;
            }
        }
    }
}
