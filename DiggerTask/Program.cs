using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DiggerCommon;
using DiggerModel;
using System.Data;
using Sashulin;
using System.IO;
using System.Timers;
using System.Threading;
using System.Windows.Forms;
/*

*/
namespace DiggerTask
{
    class Program
    {
        #region 定义变量
        static DiggerTemplate diggerTemplate = new DiggerTemplate();
        static string DiggerDataSavePath = "";
        #endregion
        [STAThread]
        static void Main(string[] args)
        {
                //string TaskName = args[1];
            if (args.Length == 2)
            {
                string DiggerTemplatePath = args[0];
                string TaskName = args[1];
                //string DiggerTemplatePath = "C:\\Users\\Administrator\\Desktop\\同花顺行业数据模板-双数.Json";
                //string TaskName = "ths";
                if (!initDiggerTemplate(DiggerTemplatePath)) return;    //初始化挖掘模板
                if (!initSaveDataPath(TaskName)) return;                //初始化下载数据保存地址
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new DiggerData(diggerTemplate, DiggerDataSavePath));
                return;
            }
            else
            {
                return;
            }
        }
        /// <summary>
        /// 初始化挖掘模板
        /// </summary>
        /// <param name="path">挖掘模板地址</param>
        /// <returns>是否成功</returns>
        static bool initDiggerTemplate(string path)
        {
            bool result = false;
            try
            {
                if (!string.IsNullOrEmpty(path))
                {
                    var json = File.ReadAllText(path);
                    diggerTemplate = DataHelper.JsonToObject<DiggerTemplate>(json);
                    if (diggerTemplate == null)
                    {
                        throw new Exception("模板无法识别");
                    }
                    result = true;
                }
                else
                {
                    throw new Exception("地址不能为空");
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog("loadDiggerTemplate:" + ex.Message, ex);
            }
            return result;
        }
        /// <summary>
        /// 初始化下载数据保存地址
        /// </summary>
        /// <param name="taskName"></param>
        /// <returns></returns>
        static bool initSaveDataPath(string taskName)
        {
            bool result = false;
            try
            {
                if (!string.IsNullOrEmpty(taskName))
                {
                    //DiggerTempDataSavePath = DataHelper.GetConfig("DataSavePath") + taskName + "_temp.cvs";
                    DiggerDataSavePath = DataHelper.GetConfig("DataSavePath") + taskName + ".csv";
                    result = true;
                }
                else
                {
                    throw new Exception("任务名不能为空");
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog("loadSaveDataPath:" + ex.Message, ex);
            }
            return result;
        }
   

    }
}
