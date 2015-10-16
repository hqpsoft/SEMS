
sems.service("LoginService", function ($http) {
    //登录校验
    this.post = function (login) {
        return $http.post("/api/LoginAPI", login);
    }
});



sems.controller("LoginController", function ($scope, $http, LoginService) {
    //声明表单提交事件
    $scope.SubmitLogin = function (login) {
        LoginService.post(login).success(function (data) {
            formSubmitSuccessClick();
        }).error(function (data) {
            formSubmitFailClick(data);
        });
    };

});