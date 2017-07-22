HospitalApp.controller("ProductController", ['$scope', '$http', '$filter', '$rootScope', function ($scope, $http, $filter, $rootScope) {

    $scope.MainProductList = [];

    $scope.ProductList = [];
    $scope.SearchProductList = [];
   
    $scope.Details = true;
    $scope.ErrorModel = { IsProductName: false,IsUOM:false,IsPrice:false,IsProductType:false };
    $scope.ErrorMessage = ""
    $scope.Add = false;
    $scope.Edit = false;
    $scope.ProductId = 0;
    $scope.ProductTypeId = 0;
    $scope.Paging = 10;
    $scope.CurruntIndex = 0;
    $scope.ProductModel = { ProductId: 0, ProductName: "", UOM: "",SubUOM:"",Price:0,Content:"",ProductTypeId:0 };

    $scope.Prefix = "";

    $scope.AddNewUI = function (isedit) {
        $scope.Details = false;
        $scope.Add = true;
        $scope.Edit = false;
    }


    function GetReportTypes()
    {
        var url = GetVirtualDirectory() + '/Store/ProductType.aspx/GetProductType?RequestFor=GetDetails';
        $http({
            method: 'GET',
            url: url,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            processData: false ,
        }).then(function (response) {
                $scope.MainProductList=response.data;
                $scope.ProductList=$scope.MainProductList;
                $scope.First();
        },
        function (response) {
           
        });
    }

    function GetProducts()
    {
        var url = GetVirtualDirectory() + '/Store/StoreProduct.aspx/GetProducts?RequestFor=GetDetails';
        $http({
            method: 'GET',
            url: url,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            processData: false ,
        }).then(function (response) {
                $scope.MainProductList=response.data;
                $scope.ProductList=$scope.MainProductList;
                $scope.First();
        },
        function (response) {
           
        });
    }


    $scope.FilterList = function () {
        //$scope.ProductTypeList = $filter('filter')(JSON.parse($("#ProductTypeList").val()), { ProductType: $scope.Prefix })
        var reg = new RegExp($scope.Prefix.toLowerCase());
        $scope.ProductList = $scope.MainProductList.filter(function (actype) {
            return (reg.test(actype.ProductName.toLowerCase()));
        });
        $scope.First();
    }

    $scope.Reset = function () {
        $scope.ProductList = $scope.MainProductList;
        $scope.SearchProductList = $scope.ProductTypeList;
        $scope.First();
    }

    $scope.CancelClick = function () {
        
        $scope.Details = true;
        $scope.Add = false;
        $scope.Edit = false;
    }

    $scope.EditClick = function (ProductTypeModel) {
        $scope.ProductModel = { ProductId: ProductTypeModel.ProductId, ProductName:  ProductTypeModel.ProductName, UOM: ProductTypeModel.UOM,SubUOM:ProductTypeModel.SubUOM,Price:ProductTypeModel.Price,Content:ProductTypeModel.Content,ProductTypeId:0 };
        $scope.Details = false;
        $scope.Add = false;
        $scope.Edit = true;
    }

    $scope.Save = function (isEdit) {
        if ($scope.ProductModel.ProductName=="") {
            $scope.ErrorModel.IsProductName = true;
            $scope.ErrorMessage = "Product name should be filled.";
            return false;
        }
        else {
            $scope.ErrorModel.IsProductName = false;
        }
        if ($scope.ProductModel.ProductTypeId=="") {
            $scope.ErrorModel.IsProductType = true;
            $scope.ErrorMessage = "Product type should be selected.";
            return false;
        }
        else {
            $scope.ErrorModel.IsProductType = false;
        }

        if ($scope.ProductModel.UOM=="") {
            $scope.ErrorModel.IsUOM = true;
            $scope.ErrorMessage = "U. O. M. should be selected.";
            return false;
        }
        else {
            $scope.ErrorModel.IsUOM = false;
        }

        if ($scope.ProductModel.Price=="") {
            $scope.ErrorModel.IsPrice = true;
            $scope.ErrorMessage = "Price should be selected.";
            return false;
        }
        else {
            $scope.ErrorModel.IsPrice = false;
        }
        
        var url = GetVirtualDirectory() + '/Store/StoreProducts.aspx/Save';
        if (isEdit == false) {
            url = GetVirtualDirectory() + '/Store/StoreProducts.aspx/Update';
        }
        var model={};
//        if (isEdit == false) {
//            model= {Product: $("#Product").val(),Description:$("#Description").val(),ProcutTypeId:$("#Productid").val()};
//        }
//        else {
//           model= {Product: $("#Product").val(),Description:$("#Description").val(),ProcutTypeId:0};
//        }
        var req = {
            method: 'POST',
            url: url,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            processData: false ,
            data: {model: JSON.stringify($scope.ProductModel)},
        };

        $http(req).then(function (response) {
            if (isEdit==true) {
                    $scope.ProductModel.ProductId=response.data.Id;
                    $scope.ProductList.push($scope.ProductModel);
                }
                else {
                    for (var i = 0; i < $scope.ProductList.length; i++) {
                        if($scope.ProductList[i].ProcutId==$scope.ProductModel.ProductId)
                        {
                            $scope.ProductList[i].ProductName=$scope.ProductModel.ProductName;
                            $scope.ProductList[i].Content=$scope.ProductModel.Content;
                        }
                    }
                }
                setTimeout(function () {
                    $scope.$apply(function () {
                        $scope.MainProductList = $scope.ProductList;
                        $scope.SearchProductList = $scope.ProductList;
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
        $scope.SearchProductList = $filter('limitTo')($scope.ProductList, $scope.Paging, 0);
    }

    $scope.Prev = function () {
        $scope.CurruntIndex = $scope.CurruntIndex - $scope.Paging;
        if ($scope.CurruntIndex >= 0) {
            $scope.SearchProductList = $filter('limitTo')($scope.ProductList, $scope.Paging, $scope.CurruntIndex);
        }
        else {
            $scope.CurruntIndex = 0;
        }
    }

    $scope.Next = function () {
        $scope.CurruntIndex = $scope.CurruntIndex + $scope.Paging;
        if ($scope.CurruntIndex <= $scope.ProductList.length) {
            $scope.SearchProductList = $filter('limitTo')($scope.ProductList, $scope.Paging, $scope.CurruntIndex);
        }
        else {
            $scope.Last();
        }
    }

    $scope.Last = function () {
        var total = $scope.ProductList.length;
        var rem = parseInt($scope.ProductList.length) % parseInt($scope.Paging);
        var position = $scope.ProductList.length - $scope.Paging;
        if (rem > 0) {
            position = $scope.ProductList.length - rem;
        }
        $scope.CurruntIndex = position;
        $scope.SearchProductList = $filter('limitTo')($scope.ProductList, $scope.Paging, position);
    }

    $scope.init = function () {
        GetReportTypes();
        GetProducts();
//        $scope.MainProductTypeList=JSON.parse(data);
//        $scope.ProductTypeList=$scope.MainProductTypeList;
//        $scope.First();
        
    }

    $scope.init();

}]);