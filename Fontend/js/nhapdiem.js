ungDung.controller("NhapDiemCtrl", function ($scope, $http) {

    const idKyThi = new URLSearchParams(window.location.search)
        .get("idKyThi");

    $scope.dsHocVien = [];

    $http.get(API_URL + "/KetQuaThi/GetByLopHoc/" + idKyThi)
        .then(res => {
            $scope.dsHocVien = res.data;
        });

    $scope.luuDiem = function () {
    $scope.dsHocVien.forEach(hv => {
        $http.post(API_URL + "/KetQuaThi/Luu", {
            IdKyThi: idKyThi,
            IdHocVien: hv.IdHocVien,
            Diem: hv.Diem,
            KetQua: hv.Diem >= 5 ? "Đạt" : "Rớt"
        });
    });
    alert("Lưu điểm thành công");
};

});
