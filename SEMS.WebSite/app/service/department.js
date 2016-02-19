
//创建一个CompapyService
sems.service("DepartmentService", function ($http) {
    //列表分页
    this.getByPage = function (param) {
        return $http.get("/api/Department/" + param + "");
    };

    //新增
    this.post = function (company) {
        return $http.post("/api/Department", company);
    };
});



//创建一个CompanyController
sems.controller("DepartmentController", function ($scope, $http, DepartmentService) {
    $scope.isShowForm = false;
    //分页配置
    $scope.tableOptions = {
        url: '/api/Department',
        columns: [
            { field: 'Id', title: 'Id', align: 'center', width: 280 },
            { field: 'CompanyName', title: '部门名称', align: 'center' },
            { field: 'Remark', title: '备注', align: 'center' },
            {
                title: '操作', align: 'center', width: 200, formatter: function (value, row, index) {
                    return [
                        '<a class="btn btn-primary editor" ng-click="edit(' + row.Id + ')" title="编辑">',
                            '编辑',
                        '</a>',

                        '<a class="btn btn-primary delete" ng-click="delete(' + row.Id + ')" title="删除">',
                            '删除',
                        '</a>'].join('');
                }
            },
        ],
        search: true,
        showRefresh: true,
        showToggle: true,
        showColumns: true,
        showExport: true,
        minimumCountColumns: 2,
        showPaginationSwitch: true,
        pagination: true,
        idField: true,
        pageList: [10, 25, 50, 100],
        sidePagination: 'server',
    };

    //编辑事件
    $scope.edit = function (id) {
        DepartmentService.getById(id).success(function (data) {
            $scope.formTile = "公司编辑";
            $scope.isShowForm = true;
            $scope.company = data;
        }).error(function (data) {
            formSubmitFailClick(data);
        });
    }

    //删除事件
    $scope.delete = function (id) {
        DepartmentService.delete(id).success(function (data) {
            formSubmitSuccessClick();
        }).error(function (data) {
            formSubmitFailClick(data);
        });
    }

    //声明表单提交事件
    $scope.SubmitCompany = function (company) {
        if (typeof company.Id == "undefined") {
            DepartmentService.post(company).success(function (data) {
                formSubmitSuccessClick();
            }).error(function (data) {
                formSubmitFailClick(data);
            });
        }
        else {
            DepartmentService.put(company.Id, company).success(function (data) {
                formSubmitSuccessClick();
            }).error(function (data) {
                formSubmitFailClick(data);
            });
        }
    };

    $scope.OpenForm = function () {
        $scope.formTile = "公司新增";
        $scope.isShowForm = true;
    }

    $scope.CancelForm = function () {
        $scope.isShowForm = false;
    }

});


