HospitalApp.controller("ReportsController", ['$scope', '$http', '$filter', '$rootScope', function ($scope, $http, $filter, $rootScope) {
    $scope.data = [];
    $scope.ReportType = "";
    $scope.PatientInfo = {};
    $scope.LanTypeValues = { Morning: "Morning", Afternoon: "Afternoon", Evening: "Evening", BeforeLunch: "Before Eating", AfterLunch: "After Eating" };
    $scope.init = function () {
        $scope.ReportType = reportType;
        $scope.data = data;
        if ($scope.ReportType == "DoctorTreatmentChart") {
            $scope.PatientInfo = $scope.data.TreatmentList[0];
        }
        if ($scope.data.length > 0) {
            $scope.PatientInfo = $scope.data[0];
            switch ($scope.PatientInfo.LanType) {
                case "Eng":
                    $scope.LanTypeValues = { Morning: "Morning", Afternoon: "Afternoon", Evening: "Evening", BeforeLunch: "Before Eating", AfterLunch: "After Eating" };
                    break;
                case "Mar":
                    $scope.LanTypeValues = { Morning: "सकाळी", Afternoon: "दुपारी", Evening: "संध्याकाळी", BeforeLunch: "जेवणाआधी", AfterLunch: "जेवणानंतर" };
                    break;
                case "Guj":
                    $scope.LanTypeValues = { Morning: "સવારે", Afternoon: "બપોરે", Evening: "સાંજ", BeforeLunch: "ખાવું પહેલાં", AfterLunch: "ખાવું પછી" };
                    break;
                case "Tam":
                    $scope.LanTypeValues = { Morning: "காலை", Afternoon: "பிற்பகல்", Evening: "சாயங்காலம்", BeforeLunch: "சாப்பிடுவதற்கு முன்", AfterLunch: "சாப்பிட்ட பிறகு" };
                    break;
                case "Tel":
                    $scope.LanTypeValues = { Morning: "ఉదయం", Afternoon: "మధ్యాహ్నం", Evening: "సందర్భంగా", BeforeLunch: "తినడానికి ముందు", AfterLunch: "తినడం తరువాత" };
                    break;
                default:
                    $scope.LanTypeValues = { Morning: "Morning", Afternoon: "Afternoon", Evening: "Evening", BeforeLunch: "Before Eating", AfterLunch: "After Eating" };
                    break;

            }
        }
    }

    $scope.init();

} ]);