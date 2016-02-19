
sems.service("LoginService", function ($http) {
    //登录校验
    this.post = function (login) {
        return $http.post("/api/Login", login);
    }
});



sems.controller("LoginController", function ($scope, $http, LoginService) {
    //声明表单提交事件
    $scope.SubmitLogin = function (login) {
        LoginService.post(login).success(function (data) {
            window.location.href = "/Home/Index";

        }).error(function (data) {
            formSubmitFailClick(data);
        });
    };

});