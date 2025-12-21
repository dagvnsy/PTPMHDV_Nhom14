ungDung.controller("dangNhapController", function ($scope, $http) {

    $scope.taiKhoan = {
        tenDangNhap: "",
        matKhau: ""
    };

    $scope.thongBao = "";
    $scope.dangXuLy = false;


    $scope.dangNhap = function () {

        if (!$scope.taiKhoan.tenDangNhap || !$scope.taiKhoan.matKhau) {
            $scope.thongBao = "Vui lòng nhập đầy đủ tài khoản và mật khẩu";
            return;
        }

        $scope.dangXuLy = true;
        $scope.thongBao = "";

        var duLieuGui = {
            TenDangNhap: $scope.taiKhoan.tenDangNhap,
            MatKhau: $scope.taiKhoan.matKhau
        };

        $http.post(API_URL + "/Auth/Login", duLieuGui)
            .then(function (res) {

                var taiKhoan = res.data;

                localStorage.setItem(
                    "taiKhoanDangNhap",
                    JSON.stringify(taiKhoan)
                );

                switch (taiKhoan.LoaiNguoiDung) {
                    case "ADMIN":
                        window.location.href = "admin/trangchu.html";
                        break;
                    case "GIANGVIEN":
                        window.location.href = "giangvien/trangchugv.html";
                        break;
                    case "HOCVIEN":
                        window.location.href = "hocvien/trangchuhv.html";
                        break;
                    default:
                        $scope.thongBao = "Không xác định được quyền";
                }

            }, function (err) {

                if (err.status === 401) {
                    $scope.thongBao = "Sai tên đăng nhập hoặc mật khẩu";
                } else {
                    $scope.thongBao = "Không kết nối được máy chủ";
                }

            })
            .finally(function () {
                $scope.dangXuLy = false;
            });
    };
});
