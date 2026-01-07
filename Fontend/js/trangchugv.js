ungDung.controller("TrangChuGVCtrl", function ($scope, $http) {

    const taiKhoan = JSON.parse(localStorage.getItem("taiKhoanDangNhap"));

    if (!taiKhoan || !taiKhoan.IdGiangVien) {
        window.location.href = "/dangnhap.html";
        return;
    }

    $scope.gv = {};
    $scope.thongBao = "";

    // loading flags
    $scope.dangTai = true;
    $scope.dangXuLyMK = false;

    // ===== LOAD THÔNG TIN GIẢNG VIÊN =====
    $http.get(API_URL + "/GiangVien/GetById/" + taiKhoan.IdGiangVien)
        .then(function (res) {
            $scope.gv = res.data;
        }, function () {
            $scope.thongBao = "Không lấy được thông tin giảng viên";
        })
        .finally(function () {
            $scope.dangTai = false;
        });

    $scope.doiMatKhau = function () {

    if (!$scope.mkCu || !$scope.mkMoi || !$scope.mkMoi2) {
        $scope.thongBao = "Vui lòng nhập đầy đủ thông tin";
        return;
    }

    if ($scope.mkMoi !== $scope.mkMoi2) {
        $scope.thongBao = "Mật khẩu xác nhận không khớp";
        return;
    }

    var duLieu = {
        MatKhauCu: $scope.mkCu,
        MatKhauMoi: $scope.mkMoi
    };

    $scope.dangXuLyMK = true;
    $scope.thongBao = "";

    $http.put(
        API_URL + "/TaiKhoan/DoiMatKhau/" + taiKhoan.IdTaiKhoan,
        duLieu
    )
    .then(function () {
        $scope.thongBao = "Đổi mật khẩu thành công";
        $scope.mkCu = $scope.mkMoi = $scope.mkMoi2 = "";
    }, function (err) {
        $scope.thongBao = err.data || "Mật khẩu cũ không đúng";
    })
    .finally(function () {
        $scope.dangXuLyMK = false;
    });
};


});
