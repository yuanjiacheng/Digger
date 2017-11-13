using DiggerCommon;
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
using DiggerModel;
using Sashulin;

namespace DiggerForm
{
    public partial class DiggerData : Form
    {
        private DiggerTemplate diggerTemplate = new DiggerTemplate();
        private bool FirstBatchData = true;
        private DataTable dtDiggerData = new DataTable();
        private string DiggerDataSavePath = "";
        private int documentComplate = 0;
        private string json2 = DataHelper.GetConfig("json2Path");
        private string common = DataHelper.GetConfig("commonPath");
        private string getData = DataHelper.GetConfig("getDataJsPath");
        private string removeElements = DataHelper.GetConfig("removeElementsPath");
        ChromeWebBrowser browser = new ChromeWebBrowser();
        private string getUrlDataJs = "";
        private string removeElementsJs = "";
        private int invertal = 4096; //挖掘间隔
        private bool lastTimeSuccess = true;//上次挖掘是否成功
        private int intertalAvailableTimes = 0; //间隔时间可用次数
        public DiggerData()
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
                    diggerTemplate = DataHelper.JsonToObject<DiggerTemplate>(json);
                    if (diggerTemplate == null)
                    {
                        MessageBox.Show("模板无法识别");
                    }
                    else
                    {
                        Lab_DiggerType.Text = "挖掘类型：" + ((DiggerModel.Enum.DiggerType)diggerTemplate.DiggerType).ToString();
                        Lab_GetMoreDataType.Text = "获得更多数据方式：" + ((DiggerModel.Enum.LoadMoreDataType)diggerTemplate.LoadMoreDataType).ToString();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void Btn_BeginDig_Click(object sender, EventArgs e)
        {
            ControlHelper CH = new ControlHelper();
            DiggerDataSavePath = CH.GetSavePath("Csv文件|*.Csv");
            if (string.IsNullOrEmpty(DiggerDataSavePath))
            {
                MessageBox.Show("请选择导出文件地址");
                return;
            }
            else
            {
                if (File.Exists(DiggerDataSavePath))
                {
                    File.Delete(DiggerDataSavePath);
                }
            }
            CSharpBrowserSettings settings = new CSharpBrowserSettings();
            browser.Initialize(false);  //无图模式
            #region 加载获取数据脚本
            List<string> jsFiles = new List<string>();
            jsFiles.Add(json2);
            jsFiles.Add(common);
            jsFiles.Add(getData);
            getUrlDataJs = DataHelper.GetJavaScript(jsFiles);
            #endregion
            switch (diggerTemplate.LoadMoreDataType)
            {
                #region 单url操作
                case 1: //点击加载更多按钮
                case 2: //页面下拉到底部
                    {
                        #region 加载删除数据脚本
                        jsFiles.Clear();
                        jsFiles.Add(json2);
                        jsFiles.Add(common);
                        jsFiles.Add(removeElements);
                        removeElementsJs = DataHelper.GetJavaScript(jsFiles);
                        #endregion
                        browser.OpenUrl(diggerTemplate.LoadDataUrl.First());//打开网址
                        browser.BrowserDocumentCompleted += new EventHandler(DocumentComplated);    //文档第一次加载
                        browser.ExecuteScript("document.ready(function(){window.CallCSharpMethod('DocumentComplate')});");
                        #region 测试用
                        this.Controls.Add(browser);
                        this.WindowState = FormWindowState.Maximized;
                        browser.Dock = DockStyle.Fill;
                        browser.ShowDevTool();
                        #endregion
                        break;
                    }
                #endregion
                #region 多url操作
                case 0://点击下一页
                    {
                        browser.OpenUrl(diggerTemplate.LoadDataUrl.First());//打开网址
                        browser.BrowserDocumentCompleted += new EventHandler(DocumentComplated);    //文档第一次加载
                        browser.ExecuteScript("document.ready(function(){window.CallCSharpMethod('DocumentComplate')});");
                        #region 测试用
                        this.Controls.Add(browser);
                        this.WindowState = FormWindowState.Maximized;
                        browser.Dock = DockStyle.Fill;
                        browser.ShowDevTool();
                        #endregion
                        break;
                    }
                case 3:
                    {
                        #region 测试用
                        this.Controls.Add(browser);
                        this.WindowState = FormWindowState.Maximized;
                        browser.Dock = DockStyle.Fill;
                        browser.ShowDevTool();
                        #endregion
                        break;
                    }
                    #endregion
            }
        }
        #region 浏览器文档加载完毕，会加载两次,第二次才作数
        private void DocumentComplated(object sender, EventArgs e)
        {
            ChromeWebBrowser browser = sender as ChromeWebBrowser;
            documentComplate++;
            if (documentComplate == 2)
            {
                switch (diggerTemplate.LoadMoreDataType)
                {
                    case 1: //点击加载更多按钮
                    case 2: //页面下拉到底部
                        {
                            browser.BrowserDocumentCompleted -= new EventHandler(DocumentComplated);
                            browser.ExecuteScript(getUrlDataJs + "\r\n innerSaveDataContorl()");   //添加id为diggeredData的控件来存储数据
                            timer1.Tick += new EventHandler(timer1_Tick);
                            timer1.Enabled = true;
                            documentComplate = 0;
                            break;
                        }
                    case 0://点击下一页
                        {
                            browser.ExecuteScript(getUrlDataJs + "\r\n innerSaveDataContorl()");
                            browser.BrowserDocumentCompleted -= new EventHandler(DocumentComplated);
                            getBrowerData();
                            documentComplate = 1;
                            break;
                        }
                }
            
            }
        }
        #endregion

        private void timer1_Tick(object sender, EventArgs e)
        {
            try {
                #region 挖数据
                switch (diggerTemplate.DiggerType)
                {
                    case 0:
                        {
                            //执行脚本加获得的数据存到id为diggeredData的input中
                            browser.ExecuteScript(getUrlDataJs + "\r\n" + string.Format("getData('{0}','{1}')", DataHelper.ObjectToJson(diggerTemplate.SelectedElementTemplate),DataHelper.ObjectToJson(diggerTemplate.TemplateDiv)));
                            //获取数据
                            var data = browser.EvaluateScript("document.getElementById('diggeredData').value").ToString();
                            dtDiggerData = DataHelper.JsonToDataTable(data);
                            break;
                        }
                    case 1: //挖链接
                        {
                            //执行脚本加获得的url数据存到id为diggeredData的input中
                            browser.ExecuteScript(getUrlDataJs + "\r\n" + string.Format("getUrlData('{0}')", DataHelper.ObjectToJson(diggerTemplate.SelectedElementTemplate)));
                            //获取数据
                            var data = browser.EvaluateScript("document.getElementById('diggeredData').value").ToString();
                            dtDiggerData = DataHelper.JsonToDataTable(data);
                            foreach (DataRow dr in dtDiggerData.Rows)
                            {
                                dr[0] = DataHelper.relativeUrlPathToAbsoluteUrlPath(dr[0].ToString(), diggerTemplate.LoadDataUrl.First());
                            }
                            break;
                        }
                }

                #endregion
                //挖到的数据数量
                var newDataCount = dtDiggerData.Rows.Count;
                //有挖到新的数据
                if (newDataCount > 0)
                {
                    #region 删除旧数据
                    switch (diggerTemplate.DiggerType)
                    {
                        case 1:
                            {
                                browser.ExecuteScript(removeElementsJs + "\r\n" + string.Format(" removeElements('{0}')", DataHelper.ObjectToJson(diggerTemplate.SelectedElementTemplate)));
                                break;
                            }
                        case 0:
                            {
                                //删除旧数据,挖数据需要所有类型的数据都在一个div块里
                                browser.ExecuteScript(removeElementsJs + "\r\n" + string.Format(" removeTemplate('{0}')", DataHelper.ObjectToJson(diggerTemplate.TemplateDiv)));
                                break;
                            }
                    }
                    #endregion
                    //触发下载更多数据
                    browser.ExecuteScript(getUrlDataJs + "\r\n" + string.Format("cilckLoadMoreData('{0}')", DataHelper.ObjectToJson(diggerTemplate.NextPageElementTemplate)));
                    DataHelper.DataTableToCSV(dtDiggerData, DiggerDataSavePath, FirstBatchData);
                    #region 调节下载速度
                    FirstBatchData = false;
                    if (intertalAvailableTimes == 0)
                    {
                        changTimeTick(true);
                    }
                    else
                    {
                        intertalAvailableTimes--;
                    }
                    lastTimeSuccess = true;
                }
                else
                {
                    if (intertalAvailableTimes == 0)
                    {
                        changTimeTick(false);
                    }
                    else
                    {
                        intertalAvailableTimes--;
                    }
                    lastTimeSuccess = false;
                }
                if (intertalAvailableTimes == 0)
                {
                    timer1.Interval = invertal;
                }
                #endregion
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                timer1.Tick -= timer1_Tick;
            }
        }
        /// <summary>
        /// 动态修改挖掘时间间隔
        /// </summary>
        /// <param name="ifSuccess">这次挖掘是否成功</param>
        private void changTimeTick(bool ifSuccess)
        {
            if (ifSuccess && lastTimeSuccess)//这次成功，上次成功
            {
                invertal /= 2;
            }
            else if (ifSuccess && !lastTimeSuccess)//这次成功，上次失败
            {
                invertal *= 2;
                timer1.Interval = invertal;
                intertalAvailableTimes = 10;
            }
            else if (!ifSuccess && lastTimeSuccess)//这次失败，上次成功
            {
                invertal *= 2;
                timer1.Interval = invertal;
                intertalAvailableTimes = 10;
            }
            else//这次失败，上次失败
            {
                invertal *= 2;
            }
        }

        private void getBrowerData()
        {
            #region 挖数据
            switch (diggerTemplate.DiggerType)
            {
                case 0:
                    {
                        //执行脚本加获得的数据存到id为diggeredData的input中
                        browser.ExecuteScript(getUrlDataJs + "\r\n" + string.Format("getData('{0}','{1}')", DataHelper.ObjectToJson(diggerTemplate.SelectedElementTemplate), DataHelper.ObjectToJson(diggerTemplate.TemplateDiv)));
                        //获取数据
                        var data = browser.EvaluateScript("document.getElementById('diggeredData').value").ToString();
                        dtDiggerData = DataHelper.JsonToDataTable(data);
                        break;
                    }
                case 1: //挖链接
                    {
                        //执行脚本加获得的url数据存到id为diggeredData的input中
                        browser.ExecuteScript(getUrlDataJs + "\r\n" + string.Format("getUrlData('{0}')", DataHelper.ObjectToJson(diggerTemplate.SelectedElementTemplate)));
                        //获取数据
                        var data = browser.EvaluateScript("document.getElementById('diggeredData').value").ToString();
                        dtDiggerData = DataHelper.JsonToDataTable(data);
                        foreach (DataRow dr in dtDiggerData.Rows)
                        {
                            dr[0] = DataHelper.relativeUrlPathToAbsoluteUrlPath(dr[0].ToString(), diggerTemplate.LoadDataUrl.First());
                        }
                        break;
                    }
            }

            #endregion
            switch (diggerTemplate.LoadMoreDataType)
            {

                case 0://点击下一页
                    {
                        DataHelper.DataTableToCSV(dtDiggerData, DiggerDataSavePath, FirstBatchData);
                        //获得下一页url
                        browser.ExecuteScript(getUrlDataJs + "\r\n" + string.Format("getNextPageUrl('{0}')", DataHelper.ObjectToJson(diggerTemplate.NextPageElementTemplate)));
                        var nextPageUrl= browser.EvaluateScript("document.getElementById('nextPageUrl').value").ToString();
                        browser.OpenUrl(nextPageUrl);
                        browser.BrowserDocumentCompleted += new EventHandler(DocumentComplated);    //文档第一次加载
                        browser.ExecuteScript("document.ready(function(){window.CallCSharpMethod('DocumentComplate')});");
                        break;
                    }
            }
        }
    }
}
