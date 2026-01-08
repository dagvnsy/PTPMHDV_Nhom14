ungDung.controller("AdminKyThiCtrl", function ($scope, $http) {

    $scope.dsKyThi = [];
    $scope.dsLopHoc = [];
    $scope.dsKhoaHoc = [];

    $scope.kt = {};
    $scope.dangMo = false;
    $scope.isSua = false;

    function load() {
        $http.get(API_URL + "/KyThi/GetAll")
            .then(res => $scope.dsKyThi = res.data);

        $http.get(API_URL + "/LopHoc/GetAll")
            .then(res => $scope.dsLopHoc = res.data);

        $http.get(API_URL + "/KhoaHoc/GetAll")
            .then(res => $scope.dsKhoaHoc = res.data);
    }
    load();

    $scope.moThem = function () {
        $scope.kt = {};
        $scope.isSua = false;
        $scope.dangMo = true;
    };

    $scope.moSua = function (kt) {
        $scope.kt = angular.copy(kt);
        $scope.isSua = true;
        $scope.dangMo = true;
    };

    $scope.luu = function () {
        if ($scope.isSua) {
            $http.put(
                API_URL + "/KyThi/Update/" + $scope.kt.IdKyThi,
                $scope.kt
            ).then(() => {
                load();
                $scope.dangMo = false;
            });
        } else {
            $http.post(
                API_URL + "/KyThi/Create",
                $scope.kt
            ).then(() => {
                load();
                $scope.dangMo = false;
            });
        }
    };

    $scope.capNhatNhapDiem = function (kt, trangThai) {
    let msg = trangThai ? "Mở nhập điểm?" : "Khóa nhập điểm?";
    if (!confirm(msg)) return;

    $http.put(API_URL + "/KyThi/CapNhatTrangThaiNhapDiem", {
        IdKyThi: kt.IdKyThi,
        IsMoNhapDiem: trangThai
    }).then(function () {
        kt.IsMoNhapDiem = trangThai;
        alert("Cập nhật thành công");
    });
};

    $scope.xoa = function (id) {
        if (!confirm("Xóa kỳ thi này?")) return;

        $http.delete(API_URL + "/KyThi/Delete/" + id)
            .then(() => load());
    };

    $scope.dong = function () {
        $scope.dangMo = false;
    };
});
