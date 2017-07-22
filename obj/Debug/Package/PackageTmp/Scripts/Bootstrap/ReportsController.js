HospitalApp.controller("ReportsController", ['$scope', '$http', '$filter', '$rootScope', function ($scope, $http, $filter, $rootScope) {
    $scope.data = [];
    $scope.ReportType = "";
    $scope.PatientInfo = {};

    $scope.init = function () {
        $scope.ReportType = reportType;
        $scope.data = data;
        if ($scope.data.length > 0) {
            $scope.PatientInfo = $scope.data[0];
        }
    }

    $scope.init();

} ]);