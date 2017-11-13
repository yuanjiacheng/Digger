var getElementsByTagName = function (eles, tagName) {
    var result = [];
    if (eles == document) {
        result = eles.getElementsByTagName(tagName);
    }
    else {
        for (var i = 0; i < eles.length; i++) {
            var node = eles[i].firstChild;
            while (node != null) {
                if (node.tagName == tagName) {
                    result.push(node);
                }
                node = node.nextSibling;
            }
        }
        if(eles.length==undefined)
        {      
            var node = eles.firstChild;
            while (node != null) {
                if (node.tagName == tagName) {
                    result.push(node);
                }
                node = node.nextSibling;
            }
        }
    }
    return result;
}
//获得html模板标识数据,obj:选中的元素
var getTagNames = function (obj) {
    var result = [];
    var lastTagName = obj.tagName;
    result.push(obj.tagName);
    while (lastTagName.toLowerCase() != "body") {
        obj = obj.parentNode;
        result.push(obj.tagName);
        lastTagName = obj.tagName;
    }
    return result;
}
//根据标签序列获得获得节点
var getElementsByTagNames = function (tagNames, eles) {
    var result = document;
    if (arguments[1] != undefined) {
        result = eles;
    }
    for (var i = tagNames.length - 1; i >= 0; i--) {
        result = getElementsByTagName(result, tagNames[i]);
    }
    return result;
}
//去空格
String.prototype.trim = function () {
    return this.replace(/^\s\s*/, '').replace(/\s\s*$/, '');
}
//是否存在类型
function hasClass(ele, cls) {
    var result = false;
    if (cls == undefined || cls.trim() == "") {
        return true;
    }
    if (ele.className == undefined) {
        return false;
    }
    var clss = cls.split(' ');
    for (var i = 0; i < clss.length; i++) {
        var className = clss[i].trim();
        if (className != "" && !ele.className.match(new RegExp('(\\s|^)' + className + '(\\s|$)'))) {
            result = false;
            break;
        }
        else {
            result = true;
        }
    }
    return result;
}

//增加类型
function addClass(ele, cls) {
    if (ele != undefined) {
        if (!this.hasClass(ele, cls)) {
            if (ele.className != undefined) {
                ele.className += ' ' + cls;
            }
            else {
                ele.className = cls;
            }
        }
    }
}

//删除类型
function removeClass(ele, cls) {
    if (ele != undefined) {
        if (hasClass(ele, cls)) {
            var reg = new RegExp('(\\s|^)' + cls + '(\\s|$)');
            ele.className = ele.className.replace(reg, ' ');
        }
    }
}


//获得树的分裂点(树节点按id正序排序
var getSpiltNode = function (tree) {
    var spiltNode = [];
    var iterator = 0;
    //标记各级根节点下有多少叶节点
    for (var i = tree.length - 1; i >= 0; i--) {
        if (tree[i].ifRoot) {
            break;
        }
        var parentId = -1;
        var nodeId = tree[i].nodeId;
        do {
            parentId = tree[nodeId - 1].parentId;
            nodeId = tree[parentId - 1].nodeId;
            tree[nodeId - 1].leafCount++;
        }
        while (parentId != 1)
    }
    //获得分裂节点
    var differ = 0;
    for (var i = tree.length - 1; i >= 0; i--) {
        if (tree[i].ifRoot) {
            break;
        }
        var parentId = -1;
        var nodeId = tree[i].nodeId;
        do {
            var newDiffer = tree[tree[nodeId - 1].parentId - 1].leafCount - tree[nodeId - 1].leafCount;
            if (newDiffer > differ) {
                spiltNode = [];
                spiltNode.push({ node: tree[nodeId - 1].ele, parentNode: tree[tree[nodeId - 1].parentId - 1].ele });
                differ = newDiffer;
            }
            else if (newDiffer == differ) {
                spiltNode.push({ node: tree[nodeId - 1].ele, parentNode: tree[tree[nodeId - 1].parentId - 1].ele });
            }
            parentId = tree[nodeId - 1].parentId;
            nodeId = tree[parentId - 1].nodeId;
        }
        while (parentId != 1)
    }
    return spiltNode;
}
//获得结构树
var getElementsTreesByTagNames = function (obj) {
    var result = [];
    var nodes = [];
    for (var i = obj[0].TagNameList.length - 1; i >= 0; i--) {
        var tagName = obj[0].TagNameList[i];
        if (result.length == 0) {
            nodes = getNodesByTagName(nodes, tagName, 0, true);
            for (var j = 0; j < nodes.length; j++) {
                result.push(nodes[j]);
            }
        }
        else if (i == 0) {
            nodes = getNodesByTagName(nodes, tagName, nodes[nodes.length - 1].nodeId, false, obj[0].TagClass == undefined ? "" : obj[0].TagClass, obj[0].TagId == undefined ? "" : obj[0].TagId);
            for (var j = 0; j < nodes.length; j++) {
                result.push(nodes[j]);
            }
        }
        else {
            nodes = getNodesByTagName(nodes, tagName, nodes[nodes.length - 1].nodeId, true);
            for (var j = 0; j < nodes.length; j++) {
                result.push(nodes[j]);
            }
        }
    }
    return result;
}

var getNodesByTagName = function (nodes, tagName, id, ifRoot, tagClass, tagId) {
    var result = [];
    //根节点
    if (id == 0) {
        var eles = document.getElementsByTagName(tagName);
        for (var i = 0; i < eles.length; i++) {
            id++;
            result.push({ nodeId: id, ele: eles[i], parentId: 0, ifRoot: ifRoot, leafCount: 0 });
        }
    }
    else {
        for (var i = 0; i < nodes.length; i++) {
            var ele = nodes[i].ele.firstChild;
            while (ele != null) {
                if (ele.tagName == tagName) {
                    if (arguments[4] != undefined && arguments[5] != undefined) {
                        if (tagClass == "" || ele.className == tagClass) {
                            if (tagId == "" || ele.id == tagId) {
                                id++;
                                result.push({ nodeId: id, ele: ele, parentId: nodes[i].nodeId, ifRoot: ifRoot, leafCount: 0 });
                            }
                        }
                    }
                    else {
                        id++;
                        result.push({ nodeId: id, ele: ele, parentId: nodes[i].nodeId, ifRoot: ifRoot, leafCount: 0 });
                    }
                }
                ele = ele.nextSibling;
            }
        }
    }
    return result;
}
