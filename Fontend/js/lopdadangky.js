ungDung.controller("LopDaDangKyCtrl", function ($scope, $http) {

    var taiKhoan = JSON.parse(localStorage.getItem("taiKhoanDangNhap"));

    if (!taiKhoan || !taiKhoan.IdHocVien) {
        window.location.href = "/html/login.html";
        return;
    }

    $scope.dsLop = [];

    $http.get(API_URL + "/LopHoc/TheoHocVien/" + taiKhoan.IdHocVien)
        .then(function (res) {
            $scope.dsLop = res.data;
        }, function () {
            alert("Không tải được danh sách lớp");
        });

    $scope.xemLichHoc = function (idLopHoc) {
        window.location.href = "xemlichhoc.html?idLopHoc=" + idLopHoc;
    };
});
