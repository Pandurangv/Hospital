HospitalApp.controller("DailyCollectionController", ['$scope', '$http', '$filter', '$rootScope', function ($scope, $http, $filter, $rootScope) {
    $scope.ListDailyCollection = [];
    $scope.ListIPDCollection = [];
    $scope.ListOPDCollection = [];
    $scope.SearchIPDCollectionList = [];
    $scope.SearchOPDCollectionList = [];
    $scope.Paging = 10;
    $scope.CurruntIndex = 0;
    $scope.PagingIPD = 10;
    $scope.CurruntIndexIPD = 0;
    $scope.DoctorsList = [];
    var objdatehelper = new datehelper({ format: "dd/MM/yyyy", cdate: new Date() });
    $scope.SelectDoctor = function () {
        var doctorid = $("#ddldoctor").val();
        if (doctorid > 0) {
            $scope.FilterDate();
        }
    }


    $scope.Reset = function () {
        $scope.Start();
    }


    $scope.TotalAmount = 0;
    $scope.TotalAmountIPD = 0;
    $scope.FilterDate = function () {
        var dtFrom = undefined;
        var dtTo = undefined;
        $scope.TotalAmount = 0;
        $scope.TotalAmountIPD = 0;
        var dtFromtxt = $("#txtFromdate").val();
        var dtTotxt = $("#txtTODate").val();
        if (dtFromtxt != "") {
            dtFrom = new Date(dtFromtxt);
        }
        if (dtTotxt != "") {
            dtTo = new Date(dtTotxt);
        }
        var totalamount = 0;
        var doctorid = $("#ddldoctor").val();
        if (doctorid > 0) {
            $scope.ListOPDCollection = [];
            angular.forEach($scope.ListDailyCollection, function (actype, key) {
                var admitdate = objdatehelper.getFormatteddate($filter('mydate')(actype.AdmitDate), "yyyy-MM-dd");
                if (dtFrom !== undefined && dtTo !== undefined) {
                    dtFrom = objdatehelper.getFormatteddate(dtFrom, "yyyy-MM-dd");
                    dtTo = objdatehelper.getFormatteddate(dtTo, "yyyy-MM-dd");
                    var test1 = objdatehelper.compareDates(admitdate, dtFrom);
                    var test2 = objdatehelper.compareDates(dtTo, admitdate);
                    if (actype.DeptDoctorId == doctorid && actype.PatientType == "OPD" && (test1 == 0 || test1 == 1) && (test2 == 0 || test2 == 1)) {
                        $scope.ListOPDCollection.push(actype);
                    }
                }
                else if (dtFrom !== undefined && dtTo === undefined) {
                    dtFrom = objdatehelper.getFormatteddate(dtFrom, "yyyy-MM-dd");
                    var test1 = objdatehelper.compareDates(admitdate, dtFrom);
                    if (actype.DeptDoctorId == doctorid && actype.PatientType == "OPD" && (test1 == 0 || test1 == 1)) {
                        $scope.ListOPDCollection.push(actype);
                    }
                }
                else if (dtFrom === undefined && dtTo !== undefined) {
                    dtTo = objdatehelper.getFormatteddate(dtTo, "yyyy-MM-dd");
                    var test2 = objdatehelper.compareDates(dtTo, admitdate);
                    if (actype.DeptDoctorId == doctorid && actype.PatientType == "OPD" && (test2 == 0 || test2 == 1)) {
                        $scope.ListOPDCollection.push(actype);
                    }
                }
                else if (dtFrom === undefined && dtTo === undefined) {
                    if (actype.DeptDoctorId == doctorid && actype.PatientType == "OPD") {
                        $scope.ListOPDCollection.push(actype);
                    }
                }
            });
            $scope.ListIPDCollection = [];
            angular.forEach($scope.ListDailyCollection, function (actype, key) {
                var admitdate = objdatehelper.getFormatteddate($filter('mydate')(actype.AdmitDate), "yyyy-MM-dd");
                if (dtFrom !== undefined && dtTo !== undefined) {
                    dtFrom = objdatehelper.getFormatteddate(dtFrom, "yyyy-MM-dd");
                    dtTo = objdatehelper.getFormatteddate(dtTo, "yyyy-MM-dd");
                    var test1 = objdatehelper.compareDates(admitdate, dtFrom);
                    var test2 = objdatehelper.compareDates(dtTo, admitdate);
                    if (actype.DeptDoctorId == doctorid && actype.PatientType == "IPD" && (test1 == 0 || test1 == 1) && (test2 == 0 || test2 == 1)) {
                        $scope.ListIPDCollection.push(actype);
                    }
                }
                else if (dtFrom !== undefined && dtTo === undefined) {
                    dtFrom = objdatehelper.getFormatteddate(dtFrom, "yyyy-MM-dd");
                    var test1 = objdatehelper.compareDates(admitdate, dtFrom);
                    if (actype.DeptDoctorId == doctorid && actype.PatientType == "IPD" && (test1 == 0 || test1 == 1)) {
                        $scope.ListIPDCollection.push(actype);
                    }
                }
                else if (dtFrom === undefined && dtTo !== undefined) {
                    dtTo = objdatehelper.getFormatteddate(dtTo, "yyyy-MM-dd");
                    var test2 = objdatehelper.compareDates(dtTo, admitdate);
                    if (actype.DeptDoctorId == doctorid && actype.PatientType == "IPD" && (test2 == 0 || test2 == 1)) {
                        $scope.ListIPDCollection.push(actype);
                    }
                }
                else if (dtFrom === undefined && dtTo === undefined) {
                    if (actype.DeptDoctorId == doctorid && actype.PatientType == "IPD") {
                        $scope.ListIPDCollection.push(actype);
                    }
                }
            });
        }
        else {
            $scope.ListOPDCollection = [];
            angular.forEach($scope.ListDailyCollection, function (actype, key) {
                var admitdate = objdatehelper.getFormatteddate($filter('mydate')(actype.AdmitDate), "yyyy-MM-dd");
                if (dtFrom !== undefined && dtTo !== undefined) {
                    dtFrom = objdatehelper.getFormatteddate(dtFrom, "yyyy-MM-dd");
                    dtTo = objdatehelper.getFormatteddate(dtTo, "yyyy-MM-dd");
                    var test1 = objdatehelper.compareDates(admitdate, dtFrom);
                    var test2 = objdatehelper.compareDates(dtTo, admitdate);
                    if (actype.PatientType == "OPD" && (test1 == 0 || test1 == 1) && (test2 == 0 || test2 == 1)) {
                        $scope.ListOPDCollection.push(actype);
                    }
                }
                else if (dtFrom !== undefined && dtTo === undefined) {
                    dtFrom = objdatehelper.getFormatteddate(dtFrom, "yyyy-MM-dd");
                    var test1 = objdatehelper.compareDates(admitdate, dtFrom);
                    if (actype.PatientType == "OPD" && (test1 == 0 || test1 == 1)) {
                        $scope.ListOPDCollection.push(actype);
                    }
                }
                else if (dtFrom === undefined && dtTo !== undefined) {
                    dtTo = objdatehelper.getFormatteddate(dtTo, "yyyy-MM-dd");
                    var test2 = objdatehelper.compareDates(dtTo, admitdate);
                    if (actype.PatientType == "OPD" && (test2 == 0 || test2 == 1)) {
                        $scope.ListOPDCollection.push(actype);
                    }
                }
                else if (dtFrom === undefined && dtTo === undefined) {
                    if (actype.PatientType == "OPD") {
                        $scope.ListOPDCollection.push(actype);
                    }
                }
            });
            $scope.ListIPDCollection = [];
            angular.forEach($scope.ListDailyCollection, function (actype, key) {
                var admitdate = objdatehelper.getFormatteddate($filter('mydate')(actype.AdmitDate), "yyyy-MM-dd");
                if (dtFrom !== undefined && dtTo !== undefined) {
                    dtFrom = objdatehelper.getFormatteddate(dtFrom, "yyyy-MM-dd");
                    dtTo = objdatehelper.getFormatteddate(dtTo, "yyyy-MM-dd");
                    var test1 = objdatehelper.compareDates(dtFrom, admitdate);
                    var test2 = objdatehelper.compareDates(dtTo, admitdate);
                    if (actype.PatientType == "IPD" && (test1 == 0 || test1 == 1) && (test2 == 0 || test2 == 1)) {
                        $scope.ListIPDCollection.push(actype);
                    }
                }
                else if (dtFrom !== undefined && dtTo === undefined) {
                    dtFrom = objdatehelper.getFormatteddate(dtFrom, "yyyy-MM-dd");
                    var admitdate = objdatehelper.getFormatteddate($filter('mydate')(actype.AdmitDate), "yyyy-MM-dd");
                    var test1 = objdatehelper.compareDates(admitdate, dtFrom);
                    if (actype.PatientType == "IPD" && (test1 == 0 || test1 == 1)) {
                        $scope.ListIPDCollection.push(actype);
                    }
                }
                else if (dtFrom === undefined && dtTo !== undefined) {
                    dtTo = objdatehelper.getFormatteddate(dtTo, "yyyy-MM-dd");
                    var test2 = objdatehelper.compareDates(dtTo, admitdate);
                    if (actype.PatientType == "IPD" && (test2 == 0 || test2 == 1)) {
                        $scope.ListIPDCollection.push(actype);
                    }
                }
                else if (dtFrom === undefined && dtTo === undefined) {
                    if (actype.PatientType == "IPD") {
                        $scope.ListIPDCollection.push(actype);
                    }
                }
            });

        }
        var ipdtotalAmount = 0;
        angular.forEach($scope.ListIPDCollection, function (actype, key) {
            ipdtotalAmount += actype.NetAmount;
        });
        $scope.TotalAmountIPD = ipdtotalAmount;
        angular.forEach($scope.ListOPDCollection, function (actype, key) {
            totalamount += actype.NetAmount;
        });
        //        $scope.First();
        //        $scope.FirstIPD();

        $scope.TotalAmount = totalamount;
        setTimeout(function () {
            $scope.$apply();
        }, 1000);

    }

    //    $scope.First = function () {
    //        $scope.CurruntIndex = 0;
    //        $scope.SearchOPDCollectionList = $filter('limitTo')($scope.ListOPDCollection, $scope.Paging, 0);
    //    }

    //    $scope.Prev = function () {
    //        $scope.CurruntIndex = $scope.CurruntIndex - $scope.Paging;
    //        if ($scope.CurruntIndex >= 0) {
    //            $scope.SearchOPDCollectionList = $filter('limitTo')($scope.ListOPDCollection, $scope.Paging, $scope.CurruntIndex);
    //        }
    //        else {
    //            $scope.CurruntIndex = 0;
    //        }
    //    }

    //    $scope.Next = function () {
    //        $scope.CurruntIndex = $scope.CurruntIndex + $scope.Paging;
    //        if ($scope.CurruntIndex <= $scope.ListOPDCollection.length) {
    //            $scope.SearchOPDCollectionList = $filter('limitTo')($scope.ListOPDCollection, $scope.Paging, $scope.CurruntIndex);
    //        }
    //        else {
    //            $scope.Last();
    //        }
    //    }

    //    $scope.Last = function () {
    //        var total = $scope.ListOPDCollection.length;
    //        var rem = parseInt($scope.ListOPDCollection.length) % parseInt($scope.Paging);
    //        var position = $scope.ListOPDCollection.length - $scope.Paging;
    //        if (rem > 0) {
    //            position = $scope.ListOPDCollection.length - rem;
    //        }
    //        $scope.CurruntIndex = position;
    //        $scope.SearchOPDCollectionList = $filter('limitTo')($scope.ListOPDCollection, $scope.Paging, position);
    //    }

    //    $scope.FirstIPD = function () {
    //        $scope.CurruntIndexIPD = 0;
    //        $scope.SearchIPDCollectionList = $filter('limitTo')($scope.ListIPDCollection, $scope.PagingIPD, 0);
    //    }

    //    $scope.PrevIPD = function () {
    //        $scope.CurruntIndexIPD = $scope.CurruntIndexIPD - $scope.PagingIPD;
    //        if ($scope.CurruntIndexIPD >= 0) {
    //            $scope.SearchIPDCollectionList = $filter('limitTo')($scope.ListIPDCollection, $scope.PagingIPD, $scope.CurruntIndexIPD);
    //        }
    //        else {
    //            $scope.CurruntIndexIPD = 0;
    //        }
    //    }

    //    $scope.NextIPD = function () {
    //        $scope.CurruntIndexIPD = $scope.CurruntIndexIPD + $scope.PagingIPD;
    //        if ($scope.CurruntIndexIPD <= $scope.ListIPDCollection.length) {
    //            $scope.SearchIPDCollectionList = $filter('limitTo')($scope.ListIPDCollection, $scope.PagingIPD, $scope.CurruntIndexIPD);
    //        }
    //        else {
    //            $scope.LastIPD();
    //        }
    //    }

    //    $scope.LastIPD = function () {
    //        var total = $scope.ListIPDCollection.length;
    //        var rem = parseInt($scope.ListIPDCollection.length) % parseInt($scope.PagingIPD);
    //        var position = $scope.ListIPDCollection.length - $scope.PagingIPD;
    //        if (rem > 0) {
    //            position = $scope.ListIPDCollection.length - rem;
    //        }
    //        $scope.CurruntIndexIPD = position;
    //        $scope.SearchIPDCollectionLists = $filter('limitTo')($scope.ListIPDCollection, $scope.PagingIPD, position);
    //    }

    $scope.Start = function () {
        $scope.ListDailyCollection = data;
        $scope.DoctorsList = doctorslist;
        $scope.TotalAmount = 0;
        $scope.ListIPDCollection = $scope.ListDailyCollection.filter(function (actype) {
            if (actype.PatientType == "IPD") {
                $scope.TotalAmountIPD += actype.NetAmount;
            }
            return (actype.PatientType == "IPD");
        });
        $scope.ListOPDCollection = $scope.ListDailyCollection.filter(function (actype) {
            if (actype.PatientType == "OPD") {
                $scope.TotalAmount += actype.NetAmount;
            }
            return (actype.PatientType == "OPD");
        });
        //        $scope.First();
        //        $scope.FirstIPD();
        var html = "<option value='0'>---Select Doctor---</option>";
        angular.forEach($scope.DoctorsList, function (value, key) {
            html += "<option value='" + value.PKId + "'>" + value.FullName + "</option>";
        });
        $("#ddldoctor").html(html);
        if ($scope.DoctorsList.length == 1) {
            $("#ddldoctor").prop('selectedIndex', 1);
        }
    }

    $scope.init = function () {
        $scope.Start();
        $(document).ready(function () {
            $('#txtFromdate').datetimepicker({
                timepicker: false,
                format: 'Y/m/d'
            });
            $('#txtFromdate').on('changeDate', function (ev) {
                $('#txtFromdate').val(ev.date);
                $('#txtFromdate').datepicker("hide");
                $scope.FilterDate();
            });
            $('#txtTODate').datetimepicker({
                timepicker: false,
                format: 'Y/m/d'
            });
            $('#txtTODate').on('changeDate', function (ev) {
                $('#txtTODate').val(ev.date);
                $('#txtTODate').datepicker("hide");
                $scope.FilterDate();
            });
        });
    }

    $scope.init();

} ]);