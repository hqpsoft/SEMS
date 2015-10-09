/*
  定义全局module
*/
var sems;
(function () {
    sems = angular.module("SEMS", []);
})();

//表单提交成功事件通用操作
var formSubmitSuccessClick = function () {
    bootbox.alert("提交成功", function (data) {
        window.location.href = document.referrer;//返回列表页,并刷新
    });
}

//表单提交失败事件通用操作
var formSubmitFailClick = function (data) {
    var msgInfo = new Array();
    if (data.ErrorInfo == undefined) {
        angular.forEach(data.ModelState, function (data) {
            msgInfo.push(data);
        });
    } else {
        msgInfo.push(data.Message);
    }
    for (var i = 0; i < msgInfo.length; i++) {
        Metronic.alert({
            container: "#bootstrap_alerts_demo",
            message: msgInfo[i],
            icon: "warning",
            type: "warning",
            reset: false,
        });
    }
}