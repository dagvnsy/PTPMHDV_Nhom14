ungDung.controller("AdminHoaDonCtrl", function ($scope, $http) {

    $scope.dsHoaDon = [];
    $scope.dangMo = false;
    $scope.hd = {};
    $scope.trangThaiLoc = "";
    $scope.dsHoaDonGoc = [];


    function load() {
    $http.get(API_URL + "/HoaDon/GetAll")
        .then(function (res) {
            $scope.dsHoaDonGoc = res.data;
            $scope.dsHoaDon = res.data;
        }, function () {
            alert("Không tải được danh sách hóa đơn");
        });
}

$scope.locTrangThai = function () {
    if (!$scope.trangThaiLoc) {
        $scope.dsHoaDon = $scope.dsHoaDonGoc;
        return;
    }

    $scope.dsHoaDon = $scope.dsHoaDonGoc.filter(hd =>
        hd.TrangThai === $scope.trangThaiLoc
    );
};


    load();

    $scope.taiLai = function () {
    $scope.trangThaiLoc = "";
    load();
};


    // ===== MỞ MODAL SỬA =====
    $scope.moSua = function (hd) {
        $scope.hd = angular.copy(hd);
        $scope.dangMo = true;
    };

    // ===== ĐÓNG MODAL =====
    $scope.dong = function () {
        $scope.dangMo = false;
        $scope.hd = {};
    };

    // ===== LƯU SỬA =====
    $scope.luu = function () {
        $http.put(API_URL + "/HoaDon/Sua", $scope.hd)
            .then(function () {
                alert("Cập nhật hóa đơn thành công");
                load();
                $scope.dong();
            }, function () {
                alert("Cập nhật thất bại");
            });
    };

    // ===== XÓA =====
    $scope.xoa = function (id) {
        if (!confirm("Xóa hóa đơn này?")) return;

        $http.delete(API_URL + "/HoaDon/Xoa/" + id)
            .then(function () {
                alert("Xóa thành công");
                load();
            }, function () {
                alert("Xóa thất bại");
            });
    };

});
