ungDung.controller("AdminKhoaHocCtrl", function ($scope, $http) {

    $scope.dsKhoaHoc = [];
    $scope.dangMo = false;
    $scope.isSua = false;
    $scope.kh = {};

    function load() {
        $http.get(API_URL + "/KhoaHoc/GetAll")
            .then(res => $scope.dsKhoaHoc = res.data);
    }
    load();

    $scope.timKiem = function () {
        if (!$scope.tuKhoa) {
            load();
            return;
        }

        const kw = $scope.tuKhoa.toLowerCase();
        $scope.dsKhoaHoc = $scope.dsKhoaHoc.filter(kh =>
            kh.TenKhoaHoc.toLowerCase().includes(kw) ||
            kh.LoaiTieng.toLowerCase().includes(kw)
        );
    };

    $scope.moThem = function () {
        $scope.kh = {};
        $scope.isSua = false;
        $scope.dangMo = true;
    };

    $scope.moSua = function (kh) {
        $scope.kh = angular.copy(kh);
        $scope.isSua = true;
        $scope.dangMo = true;
    };

    $scope.luu = function () {
        if ($scope.isSua) {
            $http.put(
                API_URL + "/KhoaHoc/Update/" + $scope.kh.IdKhoaHoc,
                $scope.kh
            ).then(() => {
                load();
                $scope.dangMo = false;
            });
        } else {
            $http.post(
                API_URL + "/KhoaHoc/Create",
                $scope.kh
            ).then(() => {
                load();
                $scope.dangMo = false;
            });
        }
    };

    $scope.xoa = function (id) {
        if (!confirm("Xóa khóa học này?")) return;

        $http.delete(API_URL + "/KhoaHoc/Delete/" + id)
            .then(() => load());
    };

    $scope.dong = function () {
        $scope.dangMo = false;
    };

});
