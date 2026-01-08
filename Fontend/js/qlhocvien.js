ungDung.controller("AdminHocVienCtrl", function ($scope, $http) {

    $scope.dsHocVien = [];
    $scope.dangMo = false;
    $scope.isSua = false;

    $scope.hv = {}; // sửa
    $scope.dk = {}; // thêm (đăng ký)

    function load() {
        $http.get(API_URL + "/HocVien/GetAll")
            .then(res => $scope.dsHocVien = res.data);
    }
    load();

    // ===== TÌM KIẾM =====
    $scope.timKiem = function () {
        if (!$scope.tuKhoa) {
            load();
            return;
        }

        const kw = $scope.tuKhoa.toLowerCase();
        $scope.dsHocVien = $scope.dsHocVien.filter(h =>
            h.TenHocVien.toLowerCase().includes(kw) ||
            (h.Email && h.Email.toLowerCase().includes(kw))
        );
    };

    // ===== THÊM =====
    $scope.moThem = function () {
        $scope.dk = {};
        $scope.isSua = false;
        $scope.dangMo = true;
    };

    // ===== SỬA =====
    $scope.moSua = function (hv) {
        $scope.hv = angular.copy(hv);
        $scope.isSua = true;
        $scope.dangMo = true;
    };

    // ===== LƯU =====
    $scope.luu = function () {

        // ===== SỬA =====
        if ($scope.isSua) {
            $http.put(
                API_URL + "/HocVien/Update/" + $scope.hv.IdHocVien,
                $scope.hv
            ).then(function () {
                load();
                $scope.dong();
            });
        }
        // ===== THÊM (ĐĂNG KÝ HỌC VIÊN) =====
        else {
            $http.post(
                API_URL + "/DangKy/HocVien",
                $scope.dk
            ).then(function () {
                alert("Thêm học viên thành công");
                load();
                $scope.dong();
            }, function (err) {
                alert(err.data || "Thêm học viên thất bại");
            });
        }
    };

    // ===== XÓA =====
    $scope.xoa = function (id) {
        if (!confirm("Xóa học viên này?")) return;

        $http.delete(API_URL + "/HocVien/Delete/" + id)
            .then(load);
    };

    $scope.dong = function () {
        $scope.dangMo = false;
    };
});
