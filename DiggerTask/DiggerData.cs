using DiggerCommon;
using DiggerModel;
using Sashulin;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DiggerTask
{
    public partial class DiggerData : Form
    {
        #region 定义变量
        DiggerTemplate diggerTemplate = new DiggerTemplate();
        bool firstBatchData = true;
        DataTable dtDiggerData = new DataTable();
        string diggerDataSavePath = "";
        int documentComplate = 0;
        string json2 = DataHelper.GetConfig("json2Path");
        string common = DataHelper.GetConfig("commonPath");
        string getData = DataHelper.GetConfig("getDataJsPath");
        string removeElements = DataHelper.GetConfig("removeElementsPath");
        string saveHashCodesPath = DataHelper.GetConfig("");
        ChromeWebBrowser browser = new ChromeWebBrowser();
        string getUrlDataJs = "";
        string removeElementsJs = "";
        int invertal = 4096; //挖掘间隔
        bool lastTimeSuccess = true;//上次挖掘是否成功
        int intertalAvailableTimes = 0; //间隔时间可用次数
        System.Timers.Timer timer = new System.Timers.Timer();
        List<int> hashCodes = new List<int>();//缓存一系列下载数据的哈希值，用于验证数据是否重复
        int hashCodesCount = Convert.ToInt32(DataHelper.GetConfig("hashCodesCount"));
        int judgeRepeatThreshold = Convert.ToInt32(DataHelper.GetConfig("judgeRepeatThreshold"));
        int maxRepeatTime = Convert.ToInt32(DataHelper.GetConfig("maxRepeatTime")); //最大连续失败时间
        DateTime dtLastTime = DateTime.Now;
        #endregion
        public DiggerData()
        {
            InitializeComponent();
        }
        public DiggerData(DiggerTemplate DiggerTemplate, string DiggerDataSavePath)
        {
            
            diggerTemplate = DiggerTemplate;
            diggerDataSavePath = DiggerDataSavePath;
            CSharpBrowserSettings settings = new CSharpBrowserSettings();
            browser.Initialize(false);  //无图模式
            getUrlDataJs = loadGetDataJs();
            if (string.IsNullOrEmpty(getUrlDataJs)) return;
            removeElementsJs = loadRemoveDataJs();
            if (string.IsNullOrEmpty(removeElementsJs)) return;
            switch (diggerTemplate.LoadMoreDataType)
            {
                #region 单url操作
                case 1: //点击加载更多按钮
                case 2: //页面下拉到底部
                    {
                        try
                        {
                            browser.OpenUrl(diggerTemplate.LoadDataUrl.First());//打开网址
                            browser.BrowserDocumentCompleted += new EventHandler(DocumentComplated);    //文档第一次加载
                            browser.ExecuteScript("document.ready(function(){window.CallCSharpMethod('DocumentComplated')});");
                            this.Controls.Add(browser);
                            this.WindowState = FormWindowState.Maximized;
                            browser.Dock = DockStyle.Fill;
                        }
                        catch (Exception ex)
                        {
                            LogHelper.WriteLog("Main:页面下拉到底部" + ex.Message, ex);
                            System.Diagnostics.Process.GetCurrentProcess().Kill(); ;
                        }
                        break;
                    }
                #endregion
                #region 多url操作
                case 0://点击下一页
                    {
                        try
                        {
                            browser.OpenUrl(diggerTemplate.LoadDataUrl.First());//打开网址
                            browser.BrowserDocumentCompleted += new EventHandler(DocumentComplated);    //文档第一次加载
                            browser.ExecuteScript("document.ready(function(){window.CallCSharpMethod('DocumentComplated')});");
                            this.Controls.Add(browser);
                            this.WindowState = FormWindowState.Maximized;
                            browser.Dock = DockStyle.Fill;
                        }
                        catch (Exception ex)
                        {
                            LogHelper.WriteLog("Main:点击下一页" + ex.Message, ex);
                            System.Diagnostics.Process.GetCurrentProcess().Kill(); ;
                        }
                        break;
                    }
                case 3:
                    {
                        break;
                    }
                    #endregion
            }
        }
        /// <summary>
        /// 加载获取数据脚本
        /// </summary>
        /// <returns>获取数据脚本</returns>
        string loadGetDataJs()
        {
            string result = "";
            try
            {
                List<string> jsFiles = new List<string>();
                jsFiles.Add(json2);
                jsFiles.Add(common);
                jsFiles.Add(getData);
                result = DataHelper.GetJavaScript(jsFiles);
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog("loadGetDataJs" + ex.Message, ex);
            }
            return result;
        }
        /// <summary>
        /// 加载删除数据脚本
        /// </summary>
        /// <returns>删除数据脚本</returns>
        string loadRemoveDataJs()
        {
            string result = "";
            try
            {
                List<string> jsFiles = new List<string>();
                jsFiles.Add(json2);
                jsFiles.Add(common);
                jsFiles.Add(removeElements);
                result = DataHelper.GetJavaScript(jsFiles);
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog("loadRemoveDataJs" + ex.Message, ex);
            }
            return result;
        }

        #region 浏览器文档加载完毕，会加载两次,第二次才作数
        private void DocumentComplated(object sender, EventArgs e)
        {
            try
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
                                timer.Elapsed += timer_Elapsed;
                                timer.Enabled = true;
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
            catch (Exception ex)
            {
                LogHelper.WriteLog("DocumentComplated" + ex.Message, ex);
                System.Diagnostics.Process.GetCurrentProcess().Kill();
            }
        }
        #endregion

        private void timer_Elapsed(object sender, EventArgs e)
        {
            try
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
                    if (!ifRepeat(dtDiggerData))
                    {
                        DataHelper.DataTableToCSV(dtDiggerData, diggerDataSavePath, firstBatchData);
                        #region 调节下载速度
                        firstBatchData = false;
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
                        timer.Interval = invertal;
                    }
                    #endregion
                }
                else
                {
                    if ((DateTime.Now - dtLastTime).Seconds > maxRepeatTime)
                    {
                        timer.Elapsed -= timer_Elapsed;
                        System.Diagnostics.Process.GetCurrentProcess().Kill(); ;
                    }
                }

            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex.Message, ex);
                File.WriteAllText()
                System.Diagnostics.Process.GetCurrentProcess().Kill(); 
            }
        }

        private void getBrowerData()
        {
            try
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
                            if (!ifRepeat(dtDiggerData))
                            {
                                DataHelper.DataTableToCSV(dtDiggerData, diggerDataSavePath, firstBatchData);
                                //获得下一页url
                                browser.ExecuteScript(getUrlDataJs + "\r\n" + string.Format("getNextPageUrl('{0}')", DataHelper.ObjectToJson(diggerTemplate.NextPageElementTemplate)));
                                var nextPageUrl = browser.EvaluateScript("document.getElementById('nextPageUrl').value").ToString();
                                Regex regAbsoluteUrlPath = new Regex(@"((http|ftp|https)://)(([a-zA-Z0-9\._-]+\.[a-zA-Z]{2,6})|([0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}))(:[0-9]{1,4})*(/[a-zA-Z0-9\&%_\./-~-]*)?");
                                if (!regAbsoluteUrlPath.IsMatch(nextPageUrl))
                                {
                                    System.Diagnostics.Process.GetCurrentProcess().Kill();
                                }
                                browser.OpenUrl(nextPageUrl);
                                browser.BrowserDocumentCompleted += new EventHandler(DocumentComplated);    //文档第一次加载
                                browser.ExecuteScript("document.ready(function(){window.CallCSharpMethod('DocumentComplated')});");
                            }
                            else
                            {

                            }
                            break;
                        }
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog("getBrowerData" + ex.Message);
                System.Diagnostics.Process.GetCurrentProcess().Kill(); 
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
                timer.Interval = invertal;
                intertalAvailableTimes = 10;
            }
            else if (!ifSuccess && lastTimeSuccess)//这次失败，上次成功
            {
                invertal *= 2;
                timer.Interval = invertal;
                intertalAvailableTimes = 10;
            }
            else//这次失败，上次失败
            {
                invertal *= 2;
            }
            if (invertal < 2)
            {
                invertal = 2;
            }
        }
        /// <summary>
        /// 数据是否重复
        /// </summary>
        /// <param name="dt">存入的数据</param>
        /// <returns></returns>
        private bool ifRepeat(DataTable dt)
        {

            int repeatDataCount = 0;
            foreach (DataRow r in dt.Rows)
            {
                string str = "";
                foreach (var item in r.ItemArray)
                {
                    str += item.ToString();
                }
                var hashCode = str.GetHashCode();
                bool repeat = false;
                foreach (var s in hashCodes)
                {
                    if (hashCode == s)
                    {
                        repeatDataCount++;
                        repeat = true;
                        break;
                    }
                }
                if (!repeat)
                {
                    if (hashCodes.Count > hashCodesCount)
                    {
                        hashCodes.Remove(0);
                    }
                    hashCodes.Add(hashCode);
                }
            }
            var res = (float)repeatDataCount / (float)dt.Rows.Count * 100.00 > judgeRepeatThreshold;
            if (res)
            {
                if ((DateTime.Now - dtLastTime).Seconds >= maxRepeatTime)
                {
                    System.Diagnostics.Process.GetCurrentProcess().Kill();
                }
            }
            else
            {
                dtLastTime = DateTime.Now;
            }
            return res;
        }

        private void initHashCode()
        {

        }
    }
}
