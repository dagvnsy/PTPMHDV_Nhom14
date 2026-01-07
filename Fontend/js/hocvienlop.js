ungDung.controller("HocVienLopCtrl", function ($scope, $http) {

    const idLopHoc = new URLSearchParams(window.location.search)
                        .get("idLopHoc");

    $scope.dsHocVien = [];

    $http.get(API_URL + "/LopHoc/GetByLopHoc/" + idLopHoc)
        .then(res => {
            $scope.dsHocVien = res.data;
        });
});
