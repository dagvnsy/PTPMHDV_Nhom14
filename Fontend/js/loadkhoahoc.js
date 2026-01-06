ungDung.controller("KhoaHocCtrl", function ($scope, $http, $location) {

    $scope.dsKhoaHoc = [];
    $scope.tenNgonNgu = "";
    $scope.moTaNgonNgu = "";

    $scope.dangKyHoc = function (idKhoaHoc) {
    window.location.href = "dangkyhoc.html?idKhoaHoc=" + idKhoaHoc;
    };


    function layLoaiTieng() {
        var query = window.location.search.substring(1);
        var params = query.split("&");
        for (var i = 0; i < params.length; i++) {
            var pair = params[i].split("=");
            if (pair[0] === "loaiTieng") {
                return pair[1];
            }
        }
        return "";
    }

    var loaiTieng = layLoaiTieng();


    if (loaiTieng === "TIENG_ANH") {
        $scope.tenNgonNgu = "Khóa học Tiếng Anh";
        $scope.moTaNgonNgu = "Chương trình tiếng Anh từ cơ bản đến luyện thi IELTS.";
    }
    else if (loaiTieng === "TIENG_TRUNG") {
        $scope.tenNgonNgu = "Khóa học Tiếng Trung";
        $scope.moTaNgonNgu = "Lộ trình học tiếng Trung và luyện thi HSK.";
    }
    else if (loaiTieng === "TIENG_NHAT") {
        $scope.tenNgonNgu = "Khóa học Tiếng Nhật";
        $scope.moTaNgonNgu = "Khóa học tiếng Nhật từ N5 đến N3.";
    }
    else if (loaiTieng === "TIENG_HAN") {
        $scope.tenNgonNgu = "Khóa học Tiếng Hàn";
        $scope.moTaNgonNgu = "Đào tạo tiếng Hàn giao tiếp và TOPIK.";
    }

    // Gọi API lấy khóa học theo loại tiếng
    $http.get(API_URL + "/KhoaHoc/GetByLoaiTieng?loaiTieng=" + loaiTieng)
        .then(function (response) {
            $scope.dsKhoaHoc = response.data;
        }, function () {
            alert("Không tải được danh sách khóa học");
        });
});
