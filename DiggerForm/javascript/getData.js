//获得url数据
function getUrlData(str) {
    var result = [];
    try {
        var jsonData = eval("(" + str + ")");
        var eleSelected = getElementsByTagNames(jsonData[0].TagNameList);
        for (var j = 0; j < eleSelected.length; j++) {
            //类比较
            if (jsonData[0].TagClass == "" || hasClass(eleSelected[j], jsonData[0].TagClass)) {
                //id比较
                if (jsonData[0].TagId == "" || eleSelected[j].Id == jsonData[0].TagId) {
                    result.push({ row1: eleSelected[j].getAttribute("href") });
                }
            }
        }
        document.getElementById("diggeredData").value = JSON.stringify(result);
    }
    catch (e) {
        console.log(e.message);
    }
}
//获得标记数据strData:标记的数据，strTemplate:标记的数据模板
function getData(strData, strTemplate) {
    var result = [];
    try {
        //获得模板div
        var template = eval("(" + strTemplate + ")");
        var eleTemplates = getElementsByTagNames(template.TagNameList);
        var jsonData = eval("(" + strData + ")");
        for (var i = 0; i < eleTemplates.length; i++) {
            if (template.TagClass != "" && !hasClass(eleTemplates[i], template.TagClass)) {
                eleTemplates.splice(i,1);
                i--;
                continue;
            }
            if (template.TagId != "" && eleTemplates[i].Id != template.TagId) {
                eleTemplates.splice(i,1);
                i--;
                continue;
            }
        }
        for (var i = 0; i < eleTemplates.length; i++)
        {
            var dataRow = {};
            for(var j=0;j<jsonData.length;j++)
            {
                var eleSelected = getElementsByTagNames(jsonData[j].TagNameList, eleTemplates[i])
                {
                    for(var k=0;k<eleSelected.length;k++)
                    {
                        if (jsonData[j].TagClass == "" || hasClass(eleSelected[k], jsonData[j].TagClass)) {
                            //id比较
                            if (jsonData[j].TagId == "" || eleSelected[k].Id == jsonData[j].TagId) {
                                if (dataRow["row" + j] != "" || dataRow["row" + j]!= undefined) {
                                    dataRow["row" + j] += "<>" + eleSelected[k].innerHTML;
                                }
                                else {
                                    dataRow["row" + j] = eleSelected[k].innerHTML;
                                }
                            }
                        }
                    }
                }
            }
            result.push(dataRow);
        }
        document.getElementById("diggeredData").value = JSON.stringify(result);
    }
    catch (e) {
        console.log(e.message);
    }
}
function innerSaveDataContorl() {
    if (document.getElementById("diggeredData") == undefined) {
        var diggerTemplate = document.createElement('input');
        diggerTemplate.id = 'diggeredData';
        diggerTemplate.type = 'hidden';
        diggerTemplate.value = '';
        document.getElementsByTagName('BODY').item(0).appendChild(diggerTemplate);
    }
}

//点击加载更多数据
function cilckLoadMoreData() {
    var str = arguments[0] ? arguments[0] : "";
    //点击加载更多按钮
    if (str != "") {
        var jsonData = eval("(" + str + ")");
        var eleSelected = getElementsByTagNames(jsonData.TagNameList);
        for (var j = 0; j < eleSelected.length; j++) {
            //类比较
            if (jsonData.TagClass == "" || hasClass(eleSelected[j], jsonData.TagClass)) {
                //id比较
                if (jsonData.TagId == "" || eleSelected[j].Id == jsonData.TagId) {
                    eleSelected[j].click();
                    return;
                }
            }
        }
    }

}



