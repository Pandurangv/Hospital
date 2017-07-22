HospitalApp.controller("ProductTypeController", ['$scope', '$http', '$filter', '$rootScope', function ($scope, $http, $filter, $rootScope) {
    $scope.MainProductTypeList = [];

    $scope.ProductTypeList = [];
    $scope.SearchProductTypeList = [];
   
    $scope.Details = true;
    $scope.ErrorModel = { IsSelectProductType: false };
    $scope.ErrorMessage = ""
    $scope.Add = false;
    $scope.Edit = false;
    $scope.ProductTypeId = 0;
    $scope.Paging = 10;
    $scope.CurruntIndex = 0;
    $scope.ProductTypeModel = { ProductTypeId: 0, ProductType: "", Description: "" };

    $scope.Prefix = "";

    $scope.AddNewUI = function (isedit) {
        $scope.Details = false;
        $scope.Add = true;
        $scope.Edit = false;
    }


    function GetReportTypes()
    {
        var url = GetVirtualDirectory() + '/Store/ProductType.aspx/GetProductType?RequestFor=GetDetails';
        //var url = 'http://localhost/Hospital/Store/ProductType.aspx/GetProductType?RequestFor=GetDetails';
        $http({
            method: 'GET',
            url: url,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            processData: false ,
        }).then(function (response) {
                $scope.MainProductTypeList=response.data;
                $scope.ProductTypeList=$scope.MainProductTypeList;
                $scope.First();
        },
        function (response) {
           
        });
    }

    $scope.FilterList = function () {
        //$scope.ProductTypeList = $filter('filter')(JSON.parse($("#ProductTypeList").val()), { ProductType: $scope.Prefix })
        var reg = new RegExp($scope.Prefix.toLowerCase());
        $scope.ProductTypeList = $scope.MainProductTypeList.filter(function (actype) {
            return (reg.test(actype.ProductType.toLowerCase()));
        });
        $scope.First();
    }

    $scope.Reset = function () {
        $scope.ProductTypeList = $scope.MainProductTypeList;
        $scope.SearchProductTypeList = $scope.ProductTypeList;
        $scope.First();
    }

    $scope.CancelClick = function () {
        $("#ProductType").val("");
        $("#Description").val("");
        $scope.Details = true;
        $scope.Add = false;
        $scope.Edit = false;
    }

    $scope.EditClick = function (ProductTypeModel) {
        $("#producttypeid").val(ProductTypeModel.ProcutTypeId);
        $("#ProductType").val(ProductTypeModel.ProductType);
        $("#Description").val(ProductTypeModel.Description);
        $scope.Details = false;
        $scope.Add = false;
        $scope.Edit = true;
    }

    $scope.Save = function (isEdit) {
        if ($("#ProductType").val() == "") {
            $scope.ErrorModel.IsSelectProductType = true;
            $scope.ErrorMessage = "Product type should be filled.";
            return false;
        }
        else {
            $scope.ErrorModel.IsSelectSubledger = false;
        }
        
        var url = GetVirtualDirectory() + '/Store/ProductType.aspx/Save';
        if (isEdit == false) {
            url = GetVirtualDirectory() + '/Store/ProductType.aspx/Update';
        }
        var model={};
        if (isEdit == false) {
            model= {ProductType: $("#ProductType").val(),Description:$("#Description").val(),ProcutTypeId:$("#producttypeid").val()};
        }
        else {
           model= {ProductType: $("#ProductType").val(),Description:$("#Description").val(),ProcutTypeId:0};
        }
        var req = {
            method: 'POST',
            url: url,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            processData: false ,
            data: {model: JSON.stringify(model)},
        };

        $http(req).then(function (response) {
            if (isEdit==true) {
                    $scope.ProductTypeModel = { ProductType: $("#ProductType").val(), ProductTypeId: response.data.Id,Description:$("#Description").val() };
                    $scope.ProductTypeList.push(model);
                }
                else {
                    for (var i = 0; i < $scope.ProductTypeList.length; i++) {
                        if($scope.ProductTypeList[i].ProcutTypeId==parseInt($("#producttypeid").val()))
                        {
                            $scope.ProductTypeList[i].ProductType=$("#ProductType").val();
                            $scope.ProductTypeList[i].Description=$("#Description").val();
                        }
                    }
                }
                setTimeout(function () {
                    $scope.$apply(function () {
                        $scope.MainProductTypeList = $scope.ProductTypeList;
                        $scope.SearchProductTypeList = $scope.ProductTypeList;
                        $scope.First();
                        $scope.CancelClick();
                    });
                }, 1000);
        },
        function (response) {
           
        });
    }

    $scope.First = function () {
        $scope.CurruntIndex = 0;
        $scope.SearchProductTypeList = $filter('limitTo')($scope.ProductTypeList, $scope.Paging, 0);
    }

    $scope.Prev = function () {
        $scope.CurruntIndex = $scope.CurruntIndex - $scope.Paging;
        if ($scope.CurruntIndex >= 0) {
            $scope.SearchProductTypeList = $filter('limitTo')($scope.ProductTypeList, $scope.Paging, $scope.CurruntIndex);
        }
        else {
            $scope.CurruntIndex = 0;
        }
    }

    $scope.Next = function () {
        $scope.CurruntIndex = $scope.CurruntIndex + $scope.Paging;
        if ($scope.CurruntIndex <= $scope.ProductTypeList.length) {
            $scope.SearchProductTypeList = $filter('limitTo')($scope.ProductTypeList, $scope.Paging, $scope.CurruntIndex);
        }
        else {
            $scope.Last();
        }
    }

    $scope.Last = function () {
        var total = $scope.ProductTypeList.length;
        var rem = parseInt($scope.ProductTypeList.length) % parseInt($scope.Paging);
        var position = $scope.ProductTypeList.length - $scope.Paging;
        if (rem > 0) {
            position = $scope.ProductTypeList.length - rem;
        }
        $scope.CurruntIndex = position;
        $scope.SearchProductTypeList = $filter('limitTo')($scope.ProductTypeList, $scope.Paging, position);
    }

    $scope.init = function () {
        GetReportTypes();
//        $scope.MainProductTypeList=JSON.parse(data);
//        $scope.ProductTypeList=$scope.MainProductTypeList;
//        $scope.First();
        
    }

    $scope.init();

} ]);