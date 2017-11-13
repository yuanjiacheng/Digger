using DiggerCommon;
using DiggerModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DiggerForm
{
    public partial class ScheduledTask : Form
    {
        public string DiggerTemplatePath = "";
        public string ExePath = DataHelper.GetConfig("exePath");
        public string BatPath = DataHelper.GetConfig("batPath");
        public ScheduledTask()
        {
            InitializeComponent();
        }

        private void Btn_ImportDiggerTemplate_Click(object sender, EventArgs e)
        {
            ControlHelper CH = new ControlHelper();
            var path = CH.GetReadPath("Json文件|*.Json");
            if (!string.IsNullOrEmpty(path))
            {
                Lab_TamplatePath.Text = path.Split('\\').Last();
                var json = File.ReadAllText(path);
                try
                {
                    var diggerTemplate = DataHelper.JsonToObject<DiggerTemplate>(json);
                    if (diggerTemplate == null)
                    {
                        DiggerTemplatePath = "";
                        MessageBox.Show("模板无法识别");
                    }
                    else
                    {
                        DiggerTemplatePath = path;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void btn_Save_Click(object sender, EventArgs e)
        {
            #region 检测
            var taskName = tb_Taskname.Text;
            if(string.IsNullOrEmpty(DiggerTemplatePath))
            {
                MessageBox.Show("请选择正确的模板地址");
                return;
            }
            if(string.IsNullOrEmpty(taskName))
            {
                MessageBox.Show("任务名不能为空");
                return;
            }
            var startDate = tb_StartTime.Text;
            if(!IsDate(startDate))
            {
                MessageBox.Show("不是正确的日期格式");
                return;
            }
            var interval = tb_Interval.Text;
            if (!IsInt(interval))
            {
                MessageBox.Show("不是正确的整数");
                return;
            }
            interval = "PT" + interval + "M";
            #endregion
            if (SchTaskExt.IsExists(taskName))
            {
                DialogResult dr = MessageBox.Show ("任务已存在,是否替换","提示",MessageBoxButtons.OKCancel);
                if (dr == DialogResult.OK)
                {
                    SchTaskExt.DeleteTask(taskName);
                    #region 生成bat文件,并新建计划任务
                    string batContext = ExePath + " " + DiggerTemplatePath + " " + taskName;
                    File.WriteAllText(BatPath + taskName + ".bat", batContext,Encoding.GetEncoding("gb2312"));
                    SchTaskExt.CreateTaskScheduler("", taskName, BatPath + taskName + ".bat", interval, startDate);
                    MessageBox.Show("计划任务创建成功");
                    #endregion
                }
                else if (dr == DialogResult.Cancel)
                {
                    return;
                }
            }
            else
            {
                #region 生成bat文件,并新建计划任务
                string batContext = ExePath + " " + DiggerTemplatePath + " " + taskName;
                File.WriteAllText(BatPath + taskName + ".bat", batContext, Encoding.GetEncoding("gb2312"));
                SchTaskExt.CreateTaskScheduler("", taskName, BatPath + taskName + ".bat", interval, startDate);
                MessageBox.Show("计划任务创建成功");
                #endregion
            }

        }
        public bool IsDate(string strDate)
        {
            try
            {
                DateTime.Parse(strDate);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool IsInt(string strInt)
        {
            try
            {
                Int32.Parse(strInt);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
