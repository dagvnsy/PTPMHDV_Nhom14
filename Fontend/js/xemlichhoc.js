ungDung.controller("LichHocCtrl", function ($scope, $http) {

    $scope.dsLichHoc = [];

    function getIdLopHoc() {
        const params = new URLSearchParams(window.location.search);
        return params.get("idLopHoc");
    }

    const idLopHoc = getIdLopHoc();

    if (!idLopHoc) {
        alert("Không xác định được lớp học");
        return;
    }

    $http.get(API_URL + "/LichHoc/GetByLopHoc/" + idLopHoc)
        .then(function (res) {
            $scope.dsLichHoc = res.data;
        }, function () {
            alert("Không lấy được lịch học");
        });
});
