

//获取json数据
function getJSON(url, data) {

    var deffed = $.Deferred();

    $.ajax({
        url: url,
        type: "POST",
        dataType: "json",
        data: JSON.stringify(data),
        timeout:35*1000,
        cache: false,
        success: function (res) {
            deffed.resolve(res);
        },
        error: function (xhr, textstatus, errorThrown) {
            deffed.reject(xhr.responseText);
        }
    });

    return deffed;

}


//回车查询
function keySelect() {
    if (event.keyCode == 13)  //回车键的键值为13

        app.getDemandList(1); //调用登录按钮的登录事件
}

//获取所有菜单
function getAllMenuConfig() {
    return [
        {
            id: 1,
            menuId: "1",
            title: "测试案例",
            menuId: [7, 9],
            index: "1-1",
            name: "测试案例",
            router: "/post.html"
        },
        {
            id: 2,
            menuId: "2",
            title: "需求看板",
            menuId: [7, 9],
            index: "2-1",
            name: "需求看板",
            router: "/post.html"

        },
        {
            id: 3,
            menuId: "3",
            title: "缺陷看板",
            menuId: [7, 9],
            index: "3-1",
            name: "缺陷看板",
            router: "/post.html"
        },
        {
            id: 4,
            menuId: "7",
            title: "测试报告",
            menuId: [7, 9],
            index: "4-1",
            name: "测试报告",
            router: "/post.html"

        },
        {
            id: 5,
            menuId: "5",
            title: "网银论坛",
            menuId: [7, 9],
            index: "5-1",
            name: "网银论坛",
            router: "/post.html"
        }
    ];
}

//全选，全不选
var checkboxes = document.getElementsByName('select_item');
function handSelect_all(t) {

    for (var i = 0; i < checkboxes.length; i++) {
        var checkbox = checkboxes[i];
        if (!$(t).get(0).checked) {
            checkbox.checked = false;
        } else {
            checkbox.checked = true;
        }
    }


    var casePush = [];
    $('.el-checkbox__input:checked').each(function (index, item) {
        if ($(this).next("span").html() != undefined) {
            casePush.push($(this).next("span").html());
        }
    });

    var arrayData = "";

    console.log(casePush)
    if (casePush.length > 0) {

        for (var i = 0; i < casePush.length; i++) {
            if (i == casePush.length - 1) {
                arrayData += casePush[i].toString();
            }
            else {
                arrayData += casePush[i].toString() + "|";
            }
        }
    }

    app.downloadurl = "Handler/ExportFile.ashx?arrayData=" + arrayData;
    console.log(app.downloadurl)

}

//单个选中
function handSelect_item(t) {
    var casePush = [];
    $('.el-checkbox__input:checked').each(function (index, item) {
        if ($(this).next("span").html() != undefined) {
            casePush.push($(this).next("span").html());
        }
    });

    var arrayData = "";

    console.log(casePush)
    if (casePush.length > 0) {

        for (var i = 0; i < casePush.length; i++) {
            if (i == casePush.length - 1) {
                arrayData += casePush[i].toString();
            }
            else {
                arrayData += casePush[i].toString() + "|";
            }
        }
    }

    app.downloadurl = "Handler/ExportFile.ashx?arrayData=" + arrayData;
    console.log(app.downloadurl)
}


window.createNewTab = function (tabName, tabUrl, isIframe) {
    var parentObj = {};
    if (parent === window) {
        parentObj = $(document);

    } else {
        parentObj = parent.$(parent.document);
    }

    parentObj.data("multitabs").create({
        iframe: isIframe || true,
        title: tabName,
        url: tabUrl
    }, true);
}


//去除左右两边的空格
function trim(str) {
    return str.replace(/(^\s*)|(\s*$)/g, "");
}

//获取Url参数
function getUrlParam(name) {
    //构造一个含有目标参数的正则表达式对象  
    var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)");
    //匹配目标参数  
    var r = window.location.search.substr(1).match(reg);
    //返回参数值  
    if (r != null) return unescape(r[2]);
    return null;
}