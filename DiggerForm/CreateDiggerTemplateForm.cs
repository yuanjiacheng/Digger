using Sashulin;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using DiggerModel;
using DiggerCommon;
using System.IO;

namespace DiggerForm
{
    public partial class CreateDiggerTemplateForm : Form
    {
        #region 变量
        private DiggerModel.Enum.DiggerType diggerType = DiggerModel.Enum.DiggerType.挖链接;
        private DiggerModel.Enum.LoadMoreDataType loadMoreDataType = DiggerModel.Enum.LoadMoreDataType.点击下一页按钮;
        private string LoadDataUrlDataPath = "";
        private string json2 = DataHelper.GetConfig("json2Path");
        private string common = DataHelper.GetConfig("commonPath");
        private string signTagAJs = DataHelper.GetConfig("signTagAJsPath");
        private string signTagSelectJs = DataHelper.GetConfig("signTagSelectJsPath");
        private string saveTagJs = DataHelper.GetConfig("saveTagJsPath");
        #endregion
        public CreateDiggerTemplateForm()
        {
            InitializeComponent();
            System.Windows.Forms.Control.CheckForIllegalCrossThreadCalls = false;
        }

        #region 控件事件
        #region 主窗口加载事件
        private void Main_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            CSharpBrowserSettings settings = new CSharpBrowserSettings();
            #region 浏览器初始测试
            settings.DefaultUrl = "http://www.html5test.com";
            settings.CachePath = @"C:\temp\caches";
            chromeWebBrowser1.Initialize(settings);
            chromeWebBrowser1.AddPluginDir(@"\plugins");
            #endregion
            #region comboBox绑定数据,默认选中第一行
            Cb_DiggerType.DataSource = ControlHelper.BindComboxEnumType<DiggerModel.Enum.DiggerType>.BindTypes;
            Cb_DiggerType.DisplayMember = "Name";
            Cb_DiggerType.ValueMember = "TypeValue";
            Cb_LoadMoreDataType.DataSource = ControlHelper.BindComboxEnumType<DiggerModel.Enum.LoadMoreDataType>.BindTypes;
            Cb_LoadMoreDataType.DisplayMember = "Name";
            Cb_LoadMoreDataType.ValueMember = "TypeValue";
            Cb_DiggerType.SelectedIndex = 0;
            Cb_LoadMoreDataType.SelectedIndex = 0;
            #endregion

        }
        #endregion
        #region 浏览器按钮事件
        private void chromeWebBrowser1_BrowserPreviewKeyDown(object sender, BrowserKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.F12)
            {
                chromeWebBrowser1.ShowDevTool();
            }
        }
        #endregion
        #region 前往页面点击事件
        private void Btn_Go_Click(object sender, EventArgs e)
        {
            string url = Txt_Url.Text;
            string pattern = @"(http|ftp|https):\/\/[\w\-_]+(\.[\w\-_]+)+([\w\-\.,@?^=%&amp;:/~\+#]*[\w\-\@?^=%&amp;/~\+#])?"; //网址正则表达式
            Regex regex = new Regex(pattern);
            if (regex.IsMatch(url))
            {
                chromeWebBrowser1.OpenUrl(url);
            }
            else
            {
                MessageBox.Show("输入的网址格式不正确(ps:开头要有http://或https://");
            }
        }
        #endregion
        #region 挖掘数据类型修改
        private void Cb_DiggerType_SelectedValueChanged(object sender, EventArgs e)
        {
            if ((int)Cb_DiggerType.SelectedIndex == (int)DiggerModel.Enum.DiggerType.挖链接)
            {
                diggerType = DiggerModel.Enum.DiggerType.挖链接;
            }
            else if ((int)Cb_DiggerType.SelectedIndex == (int)DiggerModel.Enum.DiggerType.挖数据)
            {
                diggerType = DiggerModel.Enum.DiggerType.挖数据;
            }
        }
        #endregion
        #region 加载更多数据方法类型修改
        private void Cb_LoadMoreDataType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((int)Cb_LoadMoreDataType.SelectedIndex == (int)DiggerModel.Enum.LoadMoreDataType.点击下一页按钮)
            {
                loadMoreDataType = DiggerModel.Enum.LoadMoreDataType.点击下一页按钮;
                btn_SelectUrlDataPath.Enabled = false;
            }
            else if ((int)Cb_LoadMoreDataType.SelectedIndex == (int)DiggerModel.Enum.LoadMoreDataType.点击加载更多按钮)
            {
                loadMoreDataType = DiggerModel.Enum.LoadMoreDataType.点击加载更多按钮;
                btn_SelectUrlDataPath.Enabled = false;
            }
            else if ((int)Cb_LoadMoreDataType.SelectedIndex == (int)DiggerModel.Enum.LoadMoreDataType.页面下拉到底部)
            {
                loadMoreDataType = DiggerModel.Enum.LoadMoreDataType.页面下拉到底部;
                btn_SelectUrlDataPath.Enabled = false;
            }
            else if ((int)Cb_LoadMoreDataType.SelectedIndex == (int)DiggerModel.Enum.LoadMoreDataType.无更多数据)
            {
                loadMoreDataType = DiggerModel.Enum.LoadMoreDataType.无更多数据;
                btn_SelectUrlDataPath.Enabled = true;
            }
        }
        #endregion
        #region 开始标记按钮点击事件
        private void Btn_StartSign_Click(object sender, EventArgs e)
        {
            if (diggerType == DiggerModel.Enum.DiggerType.挖链接)
            {
                List<string> jsFiles = new List<string>();
                jsFiles.Add(json2);
                jsFiles.Add(common);
                jsFiles.Add(signTagAJs);
                var js = DataHelper.GetJavaScript(jsFiles);
                chromeWebBrowser1.SetPopupMenuVisible(false);   //禁用右键弹出
                chromeWebBrowser1.ExecuteScript(js);
            }
            if (diggerType == DiggerModel.Enum.DiggerType.挖数据)
            {
                List<string> jsFiles = new List<string>();
                jsFiles.Add(json2);
                jsFiles.Add(common);
                jsFiles.Add(signTagSelectJs);
                var js = DataHelper.GetJavaScript(jsFiles);
                chromeWebBrowser1.SetPopupMenuVisible(false);   //禁用右键弹出
                chromeWebBrowser1.ExecuteScript(js);
            }
        }
        #endregion

        #endregion

        #region
        #endregion
        /// <summary>
        /// 获得标记结果事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_GetSignResult_Click(object sender, EventArgs e)
        {
            DiggerTemplate diggerTemplate = new DiggerTemplate();
            try
            {
                List<string> jsFiles = new List<string>();
                jsFiles.Add(json2);
                jsFiles.Add(common);
                jsFiles.Add(saveTagJs);
                var js = DataHelper.GetJavaScript(jsFiles);
                chromeWebBrowser1.ExecuteScript(js);
                var diggerTemplateData = chromeWebBrowser1.EvaluateScript("document.getElementById('diggerTemplate').value").ToString();
                var diggerNextPage = chromeWebBrowser1.EvaluateScript("document.getElementById('diggerNextPage').value").ToString();
                object diggerTemplateDiv = null;
                if (diggerType == DiggerModel.Enum.DiggerType.挖数据)
                {
                    diggerTemplateDiv = chromeWebBrowser1.EvaluateScript("document.getElementById('diggerTemplateDiv').value");
                }
                diggerTemplate.DiggerType = Cb_DiggerType.SelectedIndex;
                diggerTemplate.LoadDataUrl.Add(Txt_Url.Text);
                if (!string.IsNullOrEmpty(diggerTemplateData))
                {
                    diggerTemplate.SelectedElementTemplate = DataHelper.JsonToObject<List<HtmlElementIdentification.SignHtmlElement>>(diggerTemplateData);
                    if (diggerTemplateDiv != null)
                    {
                        diggerTemplate.TemplateDiv = DataHelper.JsonToObject<HtmlElementIdentification.SignHtmlElement>(diggerTemplateDiv.ToString());
                    }
                    diggerTemplate.LoadMoreDataType = Cb_LoadMoreDataType.SelectedIndex;
                    if (diggerTemplate.LoadMoreDataType != (int)DiggerModel.Enum.LoadMoreDataType.无更多数据 && diggerTemplate.LoadMoreDataType != (int)DiggerModel.Enum.LoadMoreDataType.页面下拉到底部)
                    {
                        if (!string.IsNullOrEmpty(diggerNextPage))
                        {
                            diggerTemplate.NextPageElementTemplate = DataHelper.JsonToObject<HtmlElementIdentification.SignHtmlElement>(diggerNextPage);
                        }
                        else
                        {
                            MessageBox.Show("请右键点击选择下一页按钮");
                            return;
                        }
                    }
                }
                else
                {
                    MessageBox.Show("请左键点击选择待挖取数据");
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
            ControlHelper CH = new ControlHelper();
            var path = CH.GetSavePath("Json文件|*.Json");
            if (!string.IsNullOrEmpty(path))
            {
                File.WriteAllText(path, DataHelper.ObjectToJson(diggerTemplate));
            }
            else
            {
                MessageBox.Show("请选择导出模板地址");
            }
        }

        private void btn_SelectUrlDataPath_Click(object sender, EventArgs e)
        {
            //加载
            ControlHelper CH = new ControlHelper();
            var path = CH.GetReadPath("Csv文件|*.Csv");
            LoadDataUrlDataPath = path;
        }
    }
}
