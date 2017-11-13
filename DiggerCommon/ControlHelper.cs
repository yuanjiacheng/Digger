using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DiggerCommon
{
    public class ControlHelper
    {
        public class BindComboxEnumType<T>
        {
            public string Name
            {
                get;
                set;
            }
            public string TypeValue
            {
                get;
                set;
            }
            private static readonly List<BindComboxEnumType<T>> bindTypes;
            static BindComboxEnumType()
            {
                bindTypes = new List<BindComboxEnumType<T>>();

                Type t = typeof(T);

                foreach (FieldInfo field in t.GetFields())
                {
                    if (field.IsSpecialName == false)
                    {
                        BindComboxEnumType<T> bind = new BindComboxEnumType<T>();
                        bind.Name = field.Name;
                        bind.TypeValue = field.GetRawConstantValue().ToString();
                        bindTypes.Add(bind);
                    }

                }
            }
            public static List<BindComboxEnumType<T>> BindTypes
            {
                get
                {
                    return bindTypes;
                }
            }
        }
        /// <summary>
        /// 选择保存文件位置
        /// </summary>
        /// <param name="Filter">文件后缀 Excel 文件|*.xls|csv|*.csv</param>
        /// <returns></returns>
        public string GetSavePath(string Filter)
        {
            string result = string.Empty;
            try
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = Filter;
                sfd.FilterIndex = 0;

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    result = sfd.FileName;
                }

                sfd.Dispose();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            return result;
        }
        public string GetReadPath(string Fifter)
        {
            string result = string.Empty;
            try
            {
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Filter = Fifter;
                ofd.FilterIndex = 0;

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    result = ofd.FileName;
                }

                ofd.Dispose();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            return result;
        }
    }
}
