ungDung.controller("AdminTaiKhoanCtrl", function ($scope, $http) {

    $scope.dsTaiKhoan = [];
    $scope.dangMo = false;
    $scope.tk = {};

    // LOAD (nếu có API GetAll)
    function load() {
        $http.get(API_URL + "/TaiKhoan/GetAll")
            .then(res => $scope.dsTaiKhoan = res.data);
    }
    load();

    // TÌM KIẾM
    $scope.timKiem = function () {
        if (!$scope.tuKhoa) {
            load();
            return;
        }

        const kw = $scope.tuKhoa.toLowerCase();
        $scope.dsTaiKhoan = $scope.dsTaiKhoan.filter(tk =>
            tk.TenDangNhap.toLowerCase().includes(kw)
        );
    };

    // MỞ FORM THÊM
    $scope.moThem = function () {
        $scope.tk = {};
        $scope.dangMo = true;
    };

    // THÊM
    $scope.them = function () {
        $http.post(
            API_URL + "/TaiKhoan/Them",
            $scope.tk
        ).then(() => {
            load();
            $scope.dangMo = false;
        });
    };



    // ĐỔI TRẠNG THÁI
    $scope.doiTrangThai = function (tk) {
        $http.put(
            API_URL + "/TaiKhoan/TrangThai/" + tk.IdTaiKhoan,
            null,
            { params: { trangThai: !tk.TrangThai } }
        ).then(() => {
            load();
        });
    };

    // ĐÓNG
    $scope.dong = function () {
        $scope.dangMo = false;
    };

});
