ungDung.controller("LichDayCtrl", function ($scope, $http) {

    const tk = JSON.parse(localStorage.getItem("taiKhoanDangNhap"));
    $scope.dsLichHoc = [];

    $http.get(API_URL + "/GiangVien/LichDay/" + tk.IdGiangVien)
        .then(function (res) {
            $scope.dsLichHoc = res.data;
        }, function () {
            alert("Không lấy được lịch dạy");
        });
});
