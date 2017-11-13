using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiggerModel
{
    public class Enum
    {
        public enum DiggerType
        {
            挖数据 = 0,
            挖链接 = 1
        }
        public enum LoadMoreDataType
        {
            点击下一页按钮 = 0,
            点击加载更多按钮 = 1,
            页面下拉到底部 = 2,
            无更多数据=3
        }
    }
}
