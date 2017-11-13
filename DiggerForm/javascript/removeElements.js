//删除已读取的数据
function removeElements(str) {
    var jsonData = eval("(" + str + ")");
    var tree = getElementsTreesByTagNames(jsonData);
    var spiltNode = getSpiltNode(tree);
    //删除分裂节点
    for (var i = 0; i < spiltNode.length; i++)
    {
        spiltNode[i].parentNode.removeChild(spiltNode[i].node);
    }
}
//删除数据模板
function removeTemplate(str) {
    var jsonData = eval("(" + str + ")");
    var eleTemplate = getElementsByTagNames(jsonData.TagNameList);
    for(var i=0;i<eleTemplate.length;i++)
    {
        if (jsonData.TagClass == "" || hasClass(eleTemplate[i], jsonData.TagClass)) {
            if (jsonData.TagId == "" || jsonData.TagId == eleTemplate[i].Id) {
                var parentNode = eleTemplate[i].parentNode;
                parentNode.removeChild(eleTemplate[i]);
            }
        }
    }
}
