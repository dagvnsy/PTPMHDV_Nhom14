ungDung.controller("AdminCtrl", function ($scope) {

    $scope.page = "/admin/qlhocvien.html";

    $scope.loadPage = function (tenTrang) {
        $scope.page = "/admin/" + tenTrang + ".html";
    };
});
