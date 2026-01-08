ungDung.controller("AdminGiangVienCtrl", function ($scope, $http) {

    // ===== BIẾN =====
    $scope.dsGiangVien = [];
    $scope.dangMo = false;
    $scope.isSua = false;

    $scope.form = {}; // dùng cho THÊM
    $scope.gv = {};   // dùng cho SỬA

    // ===== LOAD DANH SÁCH =====
    function load() {
        $http.get(API_URL + "/GiangVien/GetAll")
            .then(function (res) {
                $scope.dsGiangVien = res.data;
            }, function () {
                alert("Không tải được danh sách giảng viên");
            });
    }
    load();

    // ===== TÌM KIẾM (FRONTEND) =====
    $scope.timKiem = function () {
        if (!$scope.tuKhoa) {
            load();
            return;
        }

        const kw = $scope.tuKhoa.toLowerCase();
        $scope.dsGiangVien = $scope.dsGiangVien.filter(g =>
            g.TenGiangVien.toLowerCase().includes(kw) ||
            (g.Email && g.Email.toLowerCase().includes(kw)) ||
            (g.ChuyenMon && g.ChuyenMon.toLowerCase().includes(kw))
        );
    };

    // ===== MỞ MODAL THÊM =====
    $scope.moThem = function () {
        $scope.form = {
            GioiTinh: true // mặc định Nam
        };
        $scope.isSua = false;
        $scope.dangMo = true;
    };

    // ===== MỞ MODAL SỬA =====
    $scope.moSua = function (gv) {
        $scope.gv = angular.copy(gv);
        $scope.isSua = true;
        $scope.dangMo = true;
    };

    // ===== LƯU =====
    $scope.luu = function () {

        if ($scope.isSua) {
            // ===== SỬA =====
            $http.put(
                API_URL + "/GiangVien/Update/" + $scope.gv.IdGiangVien,
                $scope.gv
            ).then(function () {
                alert("Cập nhật giảng viên thành công");
                load();
                $scope.dong();
            }, function () {
                alert("Cập nhật thất bại");
            });

        } else {
            // ===== THÊM (ĐĂNG KÝ GIẢNG VIÊN) =====
            console.log("FORM GUI LEN:", $scope.form);

            $http.post(
                API_URL + "/GiangVien/DangKy",
                $scope.form
            ).then(function () {
                alert("Thêm giảng viên thành công");
                load();
                $scope.dong();
            }, function (err) {
                alert("Thêm giảng viên thất bại");
                console.error(err);
            });
        }
    };

    // ===== XÓA =====
    $scope.xoa = function (id) {
        if (!confirm("Xóa giảng viên này?")) return;

        $http.delete(API_URL + "/GiangVien/Delete/" + id)
            .then(function () {
                alert("Xóa thành công");
                load();
            }, function () {
                alert("Xóa thất bại");
            });
    };

    // ===== ĐÓNG MODAL =====
    $scope.dong = function () {
        $scope.dangMo = false;
        $scope.form = {};
        $scope.gv = {};
    };

});
