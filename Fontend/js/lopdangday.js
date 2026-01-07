ungDung.controller("LopDangDayCtrl", function ($scope, $http) {

    const tk = JSON.parse(localStorage.getItem("taiKhoanDangNhap"));

    $scope.dsLop = [];

    $http.get(API_URL + "/LopHoc/GetByGiangVien/" + tk.IdGiangVien)
        .then(res => {
            $scope.dsLop = res.data;
        });

    $scope.xemHocVien = function (idLopHoc) {
        window.location.href = "hocvienlop.html?idLopHoc=" + idLopHoc;
    };
    $scope.xemKyThi = function (idLopHoc) {
    window.location.href = "/giangvien/xemkythi.html?idLopHoc=" + idLopHoc;
};

});
