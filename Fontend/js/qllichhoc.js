ungDung.controller("QuanLyLichHocCtrl", function ($scope, $http) {

    $scope.dsLichHoc = [];
    $scope.loc = {};

    function load() {
        $http.get(API_URL + "/LichHoc/GetAllFull")
            .then(res => $scope.dsLichHoc = res.data);
    }
    load();

    $scope.locFilter = function (item) {
        if ($scope.loc.ngayHoc &&
            new Date(item.NgayHoc).toDateString() !== new Date($scope.loc.ngayHoc).toDateString())
            return false;

        if ($scope.loc.caHoc && item.CaHoc !== $scope.loc.caHoc)
            return false;

        if ($scope.loc.giangVien &&
            !item.TenGiangVien.toLowerCase().includes($scope.loc.giangVien.toLowerCase()))
            return false;

        return true;
    };
});
