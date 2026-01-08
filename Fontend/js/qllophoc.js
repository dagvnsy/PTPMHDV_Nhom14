ungDung.controller("AdminLopHocCtrl", function ($scope, $http) {

    $scope.dsLopHoc = [];
    $scope.dsKhoaHoc = [];
    $scope.dsGiangVien = [];

    $scope.dangMo = false;
    $scope.isSua = false;
    $scope.lop = {};

    // LOAD DATA
    function load() {
        $http.get(API_URL + "/LopHoc/GetAllTen")
            .then(res => $scope.dsLopHoc = res.data);

        $http.get(API_URL + "/KhoaHoc/GetAll")
            .then(res => $scope.dsKhoaHoc = res.data);

        $http.get(API_URL + "/GiangVien/GetAll")
            .then(res => $scope.dsGiangVien = res.data);
    }
    load();

    // TÌM KIẾM
    $scope.timKiem = function () {
        if (!$scope.tuKhoa) {
            load();
            return;
        }

        const kw = $scope.tuKhoa.toLowerCase();
        $scope.dsLopHoc = $scope.dsLopHoc.filter(l =>
            l.TenLopHoc.toLowerCase().includes(kw)
        );
    };

    // THÊM
    $scope.moThem = function () {
        $scope.lop = {};
        $scope.isSua = false;
        $scope.dangMo = true;
    };

    $scope.xemLichHoc = function (idLopHoc) {
    window.location.href = "/admin/xeplichhoc.html?idLopHoc=" + idLopHoc;
};


    // SỬA
    $scope.moSua = function (lop) {
        $scope.lop = angular.copy(lop);
        $scope.isSua = true;
        $scope.dangMo = true;
    };

    // LƯU
    $scope.luu = function () {

        if ($scope.isSua) {
            $http.put(
                API_URL + "/LopHoc/Update/" + $scope.lop.IdLopHoc,
                $scope.lop
            ).then(() => {
                load();
                $scope.dangMo = false;
            });
        } else {
            $http.post(
                API_URL + "/LopHoc/Create",
                $scope.lop
            ).then(() => {
                load();
                $scope.dangMo = false;
            });
        }
    };

    // XÓA
    $scope.xoa = function (id) {
        if (!confirm("Xóa lớp học này?")) return;

        $http.delete(API_URL + "/LopHoc/Delete/" + id)
            .then(
                () => load(),
                err => alert(err.data)
            );
    };

    // ĐÓNG
    $scope.dong = function () {
        $scope.dangMo = false;
    };

});
