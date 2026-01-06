ungDung.controller("ThongTinCaNhanCtrl", function ($scope, $http) {

    const taiKhoan = JSON.parse(localStorage.getItem("taiKhoanDangNhap"));
    if (!taiKhoan || !taiKhoan.IdHocVien) {
        window.location.href = "/html/login.html";
        return;
    }

    $scope.hocVien = {};
    $scope.thongBao = "";
    $scope.dsChungChi = [];

    /* Load chứng chỉ */
    $http.get(API_URL + "/ChungChi/GetByHocVien/" + taiKhoan.IdHocVien)
        .then(function (res) {
            console.log("DS CHUNG CHI:", res.data);
            $scope.dsChungChi = res.data;
        }, function () {
            console.error("Không load được chứng chỉ");
        });

    /* Xem chứng chỉ PDF */
    $scope.xemChungChi = function (idChungChi) {
        window.open(
            API_URL + "/ChungChi/XuatPDF/" + idChungChi,
            "_blank"
        );
    };

    // ===== LẤY THÔNG TIN HỌC VIÊN =====
    $http.get(API_URL + "/HocVien/GetById/" + taiKhoan.IdHocVien)
        .then(function (res) {
            $scope.hocVien = res.data;
        }, function () {
            $scope.thongBao = "Không lấy được thông tin học viên";
        });


        const menuUser = document.getElementById("menu-user");

    
    // Đổi mật khẩu
    $scope.doiMatKhau = function () {

        if ($scope.mk.MatKhauMoi !== $scope.mk.XacNhan) {
            $scope.thongBao = "Xác nhận mật khẩu không khớp";
            return;
        }

        var duLieu = {
            MatKhauCu: $scope.mk.MatKhauCu,
            MatKhauMoi: $scope.mk.MatKhauMoi
        };

        $http.put(API_URL + "/TaiKhoan/DoiMatKhau/" + taiKhoan.IdTaiKhoan, duLieu)
            .then(function () {
                $scope.thongBao = "Đổi mật khẩu thành công";
                $scope.mk = {};
            }, function (err) {
                $scope.thongBao = err.data || "Đổi mật khẩu thất bại";
            });
    };

    $scope.dsHoaDon = [];

    // ===== LẤY HÓA ĐƠN HỌC VIÊN =====
    $http.get(API_URL + "/HoaDon/GetByHocVien/" + taiKhoan.IdHocVien)
        .then(function (res) {
            $scope.dsHoaDon = res.data;
        }, function () {
            $scope.thongBao = "Không lấy được danh sách hóa đơn";
        });

});
