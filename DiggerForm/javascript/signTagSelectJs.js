//为A标签绑定事件
function signTagSelect() {
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
    var diggerTemplateDiv = document.createElement('input');
    diggerTemplateDiv.id = 'diggerTemplateDiv';
    diggerTemplateDiv.type = 'hidden';
    diggerTemplateDiv.value = '';
    //增加存储控件
    document.getElementsByTagName('BODY').item(0).appendChild(diggerNextPage);
    document.getElementsByTagName('BODY').item(0).appendChild(diggerTemplate);
    document.getElementsByTagName('BODY').item(0).appendChild(diggerTemplateDiv);
    var tagAs = document.getElementsByTagName('a');
    for (var i = 0; i < tagAs.length; i++) {
        tagAs[i].href = "javascript:void(0)";
    }
}

signTagSelect();
//委托事件
document.onmousedown = function (e) {
    //左键点击
    if (e.button == 0) {
        //没有被标记过
        if (!hasClass(e.target, "diggerSigned") && !hasClass(e.target, "diggerWillSelect")) {
            //没有被标记为下一页,是叶节点
            if (!hasClass(e.target, "diggerNextPage")) {
                var tagNameList = getTagNames(e.target);
                var tagClass = e.target.className == undefined ? "" : e.target.className;
                var tagId = e.target.id ? "" : e.target.id;
                if (GetTemplateDiv([{ TagNameList: tagNameList, TagClass: tagClass, TagId: tagId }])) {
                    //添加类和样式
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
                else {
                    alert("挖掘多列数据必须在同一个数据模板内");
                }
            }
            else {
                alert("数据标记不能和下一页标记相同")
            }
        }
            //被标记过
        else {
            var tagNameList = getTagNames(e.target);
            var tagClass = e.target.className == undefined ? "" : e.target.className;
            var tagId = e.target.id ? "" : e.target.id;
            //删除类和样式
            var eleSelected = getElementsByTagNames(tagNameList);
            for (var j = 0; j < eleSelected.length; j++) {
                if (hasClass(eleSelected[j], "diggerWillSelect"))
                    removeClass(eleSelected[j], "diggerWillSelect");
                if (hasClass(eleSelected[j], "diggerSigned"))
                    removeClass(eleSelected[j], "diggerSigned");
            }
            var signedDataNum = document.getElementsByClassName("diggerSigned");
            //如果没有被标记挖掘的数据了
            if(signedDataNum.length==0)
            {
                var diggerTemplateDivSigned = document.getElementsByClassName("diggerTemplateDiv");
                for(var i=0;i<diggerTemplateDivSigned.length;i++)
                {
                    removeClass(diggerTemplateDivSigned[i], "diggerTemplateDiv");
                }
            }
        }
    }
        //右键点击
    else if (e.button == 2) {
        if (!(hasClass(e.target, "diggerSigned") && hasClass(e.target, "diggerWillSelect"))) {
            removeClass(document.getElementsByClassName("diggerNextPage").item(0), "diggerNextPage")
            addClass(e.target, 'diggerNextPage');
        }
        else {
            alert("下一页标记不能和数据标记相同")
        }
    };
}

//识别标记项所在的模板div;
function GetTemplateDiv(tagNameList) {
    var tree = getElementsTreesByTagNames(tagNameList);
    var spiltNode = getSpiltNode(tree);
    var signTemplateDiv = document.getElementsByClassName("diggerTemplateDiv");

    if (signTemplateDiv.length > 0) {
        for (var i = 0; i < spiltNode.length; i++)
        {
            if(hasClass(spiltNode[i].node, "diggerTemplateDiv"))
            {
                return true;
            }
        }
        return false;
    }
    else {
        for (var i = 0; i < spiltNode.length; i++) {
            addClass(spiltNode[i].node, "diggerTemplateDiv");
        }
    }
    return true;
}