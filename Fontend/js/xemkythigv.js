ungDung.controller("ChonKyThiCtrl", function ($scope, $http) {

    const idLopHoc = new URLSearchParams(window.location.search)
        .get("idLopHoc");

    if (!idLopHoc) {
        alert("Thiếu id lớp học");
        return;
    }

    $scope.dsKyThi = [];

    // Lấy danh sách kỳ thi theo lớp
    $http.get(API_URL + "/KyThi/GetByLopHoc/" + idLopHoc)
        .then(function (res) {
            $scope.dsKyThi = res.data;
        }, function () {
            alert("Không tải được danh sách kỳ thi");
        });

    // Vào trang nhập điểm
    $scope.vaoNhapDiem = function (idKyThi) {
        window.location.href =
            "/giangvien/nhapdiem.html?idKyThi=" + idKyThi;
    };
});
