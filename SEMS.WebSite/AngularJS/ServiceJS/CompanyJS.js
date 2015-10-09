
//创建一个CompapyService
sems.service("CompapyService", function ($http) {
    //列表分页
    this.getByPage = function (param) {
        return $http.get("/api/CompanyAPI/" + param + "");
    }

    //根据Id获取表单信息
    this.getById = function (id) {
        return $http.get("/api/CompanyAPI/" + id + "");
    }

    //新增
    this.post = function (company) {
        return $http.post("/api/CompanyAPI", company);
    }

    //修改
    this.put = function (id, company) {
        return $http.put("/api/CompanyAPI/" + id + "", company);
    }

    //删除
    this.delete = function (id) {
        return $http.delete("/api/CompanyAPI/" + id + "");
    }
});



//创建一个CompanyController
sems.controller("CompanyController", function ($scope, $http, CompapyService) {
    //给变量赋值
    CompapyService.getById(1).success(function (data) {
        $scope.company = data;
    }).error(function (data) {
        formSubmitFailClick(data);
    });

    //声明表单提交事件
    $scope.SubmitCompany = function (company) {
        CompapyService.post(company).success(function (data) {
            formSubmitSuccessClick();
        }).error(function (data) {
            formSubmitFailClick(data);
        });

    };

});