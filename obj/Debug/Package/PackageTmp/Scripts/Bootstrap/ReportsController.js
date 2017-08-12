HospitalApp.controller("ReportsController", ['$scope', '$http', '$filter', '$rootScope', function ($scope, $http, $filter, $rootScope) {
    $scope.data = [];
    $scope.ReportType = "";
    $scope.PatientInfo = {};
    $scope.LanTypeValues = { Morning: "Morning", Afternoon: "Afternoon", Evening: "Evening" };
    $scope.init = function () {
        $scope.ReportType = reportType;
        $scope.data = data;
        if ($scope.data.length > 0) {
            $scope.PatientInfo = $scope.data[0];
            switch ($scope.PatientInfo.LanType) {
                case "Eng":
                    $scope.LanTypeValues = { Morning: "Morning", Afternoon: "Afternoon", Evening: "Evening" };
                    break;
                case "Mar":
                    $scope.LanTypeValues = { Morning: "सकाळी", Afternoon: "दुपारी", Evening: "संध्याकाळी" };
                    break;
                case "Guj":
                    $scope.LanTypeValues = { Morning: "સવારે", Afternoon: "બપોરે", Evening: "સાંજ" };
                    break;
                case "Tam":
                    $scope.LanTypeValues = { Morning: "காலை", Afternoon: "பிற்பகல்", Evening: "சாயங்காலம்" };
                    break;
                case "Tel":
                    $scope.LanTypeValues = { Morning: "ఉదయం", Afternoon: "మధ్యాహ్నం", Evening: "సందర్భంగా" };
                    break;
                default:
                    $scope.LanTypeValues = { Morning: "Morning", Afternoon: "Afternoon", Evening: "Evening" };
                    break;

            }
        }
    }

    $scope.init();

} ]);