ungDung.controller("DangKyHocCtrl", function ($scope, $http) {

    $scope.dsLop = [];

    const taiKhoan = JSON.parse(localStorage.getItem("taiKhoanDangNhap"));
    if (!taiKhoan || !taiKhoan.IdHocVien) {
        alert("Vui lòng đăng nhập");
        window.location.href = "/html/login/.html";
        return;
    }

    const idHocVien = taiKhoan.IdHocVien;

    function layIdKhoaHoc() {
        const url = new URL(window.location.href);
        return url.searchParams.get("idKhoaHoc");
    }

    const idKhoaHoc = layIdKhoaHoc();

    // Load lớp theo khóa học
    $http.get(API_URL + "/LopHoc/GetByKhoaHoc/" + idKhoaHoc)
        .then(function (res) {
            $scope.dsLop = res.data;

            // Kiểm tra từng lớp đã đăng ký hay chưa
            angular.forEach($scope.dsLop, function (lop) {
                $http.get(API_URL + "/DangKyHoc/DaDangKy", {
                    params: {
                        idHocVien: idHocVien,
                        idLopHoc: lop.IdLopHoc
                    }
                }).then(function (resCheck) {
                    lop.DaDangKy = resCheck.data === true;
                });
            });
        });

    // Đăng ký lớp
    $scope.dangKy = function (lop) {

    if (!confirm("Xác nhận đăng ký lớp " + lop.TenLopHoc + "?"))
        return;

    const dangKyData = {
        IdHocVien: idHocVien,
        IdLopHoc: lop.IdLopHoc,
        TrangThai: "DA_DANG_KY"
    };

    // 1. Đăng ký lớp
    $http.post(API_URL + "/DangKyHoc/Create", dangKyData)
        .then(function () {
            alert("Đăng ký thành công. Hóa đơn đã được tạo.");
            lop.DaDangKy = true;
        })
        .catch(function () {
            alert("Có lỗi khi đăng ký");
        });

};

});
