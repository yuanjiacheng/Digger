using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiggerModel
{
    public class DiggerTemplate
    {
        /// <summary>
        /// 记录挖掘数据的模板div标签(只允许存在一个)
        /// </summary>
        public HtmlElementIdentification.SignHtmlElement TemplateDiv;
        /// <summary>
        /// 记录挖掘链接的模板（当为挖掘url模式时，只有一条）
        /// </summary>
        public List<HtmlElementIdentification.SignHtmlElement> SelectedElementTemplate;
        /// <summary>
        /// 记录下一页元素模板
        /// </summary>
        public HtmlElementIdentification.SignHtmlElement NextPageElementTemplate;
        /// <summary>
        /// 下载数据的Url集合（当为挖掘url模式时，只有一条）
        /// </summary>
        public List<string> LoadDataUrl;
        /// <summary>
        /// 获得下一页的类型
        /// </summary>
        public int LoadMoreDataType;
        /// <summary>
        /// 挖掘数据类型
        /// </summary>
        public int DiggerType;
        public DiggerTemplate()
        {
            SelectedElementTemplate = new List<HtmlElementIdentification.SignHtmlElement>();
            TemplateDiv = new HtmlElementIdentification.SignHtmlElement();
            LoadDataUrl = new List<string>();
        }
        /// <summary>
        /// 下载数据的url集合文件
        /// </summary>
        public string LoadDataUrlDataPath;
    }
}
