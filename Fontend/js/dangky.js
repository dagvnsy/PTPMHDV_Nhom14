ungDung.controller("DangKyCtrl", function ($scope, $http) {

    $scope.hocVien = {
        tenDangNhap: "",
        matKhau: "",
        tenHocVien: "",
        ngaySinh: "",
        gioiTinh: true,
        email: "",
        soDienThoai: "",
        diaChi: ""
    };

    $scope.thongBao = "";

    $scope.dangKy = function () {

        if (!$scope.hocVien.tenDangNhap || !$scope.hocVien.matKhau) {
            $scope.thongBao = "Vui lòng nhập đầy đủ thông tin";
            return;
        }

        $http({
            method: "POST",
            url: API_URL + "/DangKy/HocVien",
            data: $scope.hocVien,
            headers: {
                "Content-Type": "application/json"
            }
        }).then(function (res) {

            if (res.data === true) {
                alert("Đăng ký thành công");
                window.location.href = "login.html";
            } else {
                $scope.thongBao = "Đăng ký thất bại";
            }

        }, function (err) {
            console.log(err);
            $scope.thongBao = "Lỗi server (500)";
        });
    };
});
