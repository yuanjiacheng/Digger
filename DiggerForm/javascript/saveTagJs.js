function saveSignData() {
    //存储数据模板路径
    var Data = [];
    var diggerDivTemplateInput = document.getElementById("diggerTemplateDiv");
    var diggerDivTemplateTagNameList = [];
    if (diggerDivTemplateInput != undefined)
    {
        var diggerDivTemplate = document.getElementsByClassName("diggerTemplateDiv")[0];
        diggerDivTemplateTagNameList = getTagNames(diggerDivTemplate);
        var tagId = diggerDivTemplate.id == undefined ? "" : diggerDivTemplate.id;
        var tagClass = diggerDivTemplate.className == undefined ? "" : diggerDivTemplate.className;
        var diggerDivTemplateData = { tagNameList: diggerDivTemplateTagNameList, tagClass: tagClass.replace(/diggerTemplateDiv/, ''), tagId: tagId };
        document.getElementById("diggerTemplateDiv").value = JSON.stringify(diggerDivTemplateData);
    }
    //存储待挖掘数据路径
    var diggerSignedNode = document.getElementsByClassName("diggerSigned");
    for (var i = 0; i < diggerSignedNode.length; i++) {
        var tagNameList = getTagNames(diggerSignedNode[i]);
        tagNameList.splice(tagNameList.length - diggerDivTemplateTagNameList.length, tagNameList.length - 1);
        var tagClass = diggerSignedNode[i].className == undefined ? "" : diggerSignedNode[i].className;
        var tagId = diggerSignedNode[i].id ? "" : diggerSignedNode[i].id;
        var diggerTemplateData = { tagNameList: tagNameList, tagClass: tagClass.replace(/diggerSigned/, ''), tagId: tagId };
        Data.push(diggerTemplateData);
    }
    document.getElementById('diggerTemplate').value = JSON.stringify(Data);
    //存储下一页标签路径
    var diggerNextPageNode = document.getElementsByClassName("diggerNextPage").item(0);
    var tagNameList = getTagNames(diggerNextPageNode);
    var tagClass = diggerNextPageNode.className == undefined ? "" : diggerNextPageNode.className;
    var tagId = diggerNextPageNode.id==undefined ? "" : diggerNextPageNode.id;
    var diggerNextPageData = { tagNameList: tagNameList, tagClass: tagClass.replace(/diggerNextPage/, ''), tagId: tagId };
    document.getElementById('diggerNextPage').value = JSON.stringify(diggerNextPageData);
}

saveSignData();