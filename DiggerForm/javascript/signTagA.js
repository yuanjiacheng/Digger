//为A标签绑定事件
function signTagA() {
    var style = document.createElement('style');
    style.type = 'text/css';
    style.innerHTML = ".diggerSigned{ background-color:red !important; } .diggerWillSelect{background-color:pink !important;} .diggerNextPage{background-color:yellow !important;}";
    //增加选中样式，和将会被下载数据的样式
    document.getElementsByTagName('HEAD').item(0).appendChild(style);
    var diggerTemplate = document.createElement('input');
    diggerTemplate.id = 'diggerTemplate';
    diggerTemplate.type = 'hidden';
    diggerTemplate.value = '';
    var diggerNextPage = document.createElement('input');
    diggerNextPage.id = 'diggerNextPage';
    diggerNextPage.type = 'hidden';
    diggerNextPage.value = '';
    //增加存储控件
    document.getElementsByTagName('BODY').item(0).appendChild(diggerNextPage);
    document.getElementsByTagName('BODY').item(0).appendChild(diggerTemplate);
    var tagAs = document.getElementsByTagName('a');
    for (var i = 0; i < tagAs.length; i++) {
        tagAs[i].href = "javascript:void(0)";
    }
}

signTagA();
//委托事件
document.onmousedown=function(e)
{
    //左键点击
    if (e.button == 0)
    {
        if (!hasClass(e.target, "diggerNextPage")) {
            if (e.target.tagName.toLowerCase() == "a") {
                var tagNameList = getTagNames(e.target);
                var tagClass = e.target.className == undefined ? "" : e.target.className;
                var tagId = e.target.id ? "" : e.target.id;
                //清除老的样式
                var oldDiggerSignedNode = document.getElementsByClassName("diggerSigned");
                removeClass(oldDiggerSignedNode[0], "diggerSigned");
                var oldDiggerWillSelectNode = document.getElementsByClassName("diggerWillSelect");
                for (var j = 0; j < oldDiggerWillSelectNode.length; j++) {
                    removeClass(oldDiggerWillSelectNode[j], "diggerWillSelect");
                }
                //设置新的样式
                var eleSelected = getElementsByTagNames(tagNameList);
                for (var j = 0; j < eleSelected.length; j++) {
                    //类比较
                    if (tagClass == "" || hasClass(eleSelected[j], tagClass)) {
                        //id比较
                        if (tagId == "" || eleSelected[j].id == tagId) {
                            if (!hasClass(eleSelected[j], "diggerWillSelect")) {
                                addClass(eleSelected[j], "diggerWillSelect");
                            }
                        }
                    }
                }
                removeClass(e.target, "diggerWillSelect");
                addClass(e.target, "diggerSigned");
            }
        }
        else
        {
            alert("链接数据标记不能和下一页标记相同")
        }
    }
    //右键点击
    else if (e.button == 2) {
        if (!(hasClass(e.target, "diggerSigned") && hasClass(e.target, "diggerWillSelect"))) {
            removeClass(document.getElementsByClassName("diggerNextPage").item(0), "diggerNextPage")
            addClass(e.target, 'diggerNextPage');
        }
        else
        {
            alert("下一页标记不能和链接数据标记相同")
        }
    };
}