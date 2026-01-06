ungDung.controller("KyThiCtrl", function ($scope, $http) {

    $scope.dsKyThi = [];
    $scope.coDiem = false;

    const taiKhoan = JSON.parse(localStorage.getItem("taiKhoanDangNhap"));

    if (!taiKhoan || !taiKhoan.IdHocVien) {
        window.location.href = "/html/login.html";
        return;
    }

    $http.get(API_URL + "/KyThi/GetByHocVien/" + taiKhoan.IdHocVien)
        .then(function (res) {
            $scope.dsKyThi = res.data;
             $scope.coDiem = $scope.dsKyThi.some(kt => kt.Diem != null);
        }, function () {
            alert("Không tải được danh sách kỳ thi");
        });

});
