ungDung.controller("AdminChungChiCtrl", function ($scope, $http) {

    $scope.dsKhoaHoc = [];
    $scope.dsHocVien = [];
    $scope.idKhoaHoc = null;

    $http.get(API_URL + "/KhoaHoc/GetAll")
        .then(function (res) {
            console.log("DS KHÓA HỌC:", res.data);
            $scope.dsKhoaHoc = res.data;
        }, function (err) {
            alert("Không load được khóa học");
            console.error(err);
        });

    $scope.loadHocVien = function () {
    if (!$scope.idKhoaHoc) return;

    $http.get(API_URL + "/ChungChi/GetByKhoaHoc/" + $scope.idKhoaHoc)
        .then(function (res) {
            $scope.dsHocVien = res.data;

            angular.forEach($scope.dsHocVien, function (hv) {
                $http.get(API_URL + "/ChungChi/Check", {
                    params: {
                        idHocVien: hv.IdHocVien,
                        idKhoaHoc: $scope.idKhoaHoc
                    }
                }).then(res => {
                    hv.daCap = res.data.daCap;
                    hv.idChungChi = res.data.idChungChi;
                });

            });

            
        });
};



    $scope.capChungChi = function (hv) {

        const data = {
            IdHocVien: hv.IdHocVien,
            IdKhoaHoc: $scope.idKhoaHoc,
            NgayCap: new Date(),
            XepLoai: hv.XepLoai
        };

        $http.post(API_URL + "/ChungChi/Create", data)
    .then(res => {
        alert("Cấp chứng chỉ thành công");

        hv.daCap = true;
        hv.idChungChi = res.data.idChungChi; // nếu backend trả
    });

    };


    $scope.xemChungChi = function (idChungChi) {
    window.open(API_URL + "/ChungChi/XuatPDF/" + idChungChi, "_blank");
        };


});
