HospitalApp.controller("DoctorTreatmentController", ['$scope', '$http', '$filter', '$rootScope', function ($scope, $http, $filter, $rootScope) {
    
    $scope.DoctorList = [];
    $scope.PatientList = [];
    $scope.MainTreatmentList = [];

    $scope.TreatmentList = [];
    $scope.SearchTreatmentList = [];

    $scope.ProductList=[];

    

    $scope.AddedProductList=[];

    $scope.SelectedDoctor=0;

    $scope.SelectedPatient=0;
   
    $scope.Details = true;
    $scope.ErrorModel = { IsPatientName: false,IsDoctorName:false,IsTreatment:false,IsProductName:false,IsProductQuantity:false,IsProductPrice:false,IsProductAmount:false };
    $scope.ErrorMessage = ""
    $scope.Add = false;
    $scope.Edit = false;

    $scope.AddProduct=true;
    $scope.EditProduct=false;

    $scope.TreatId = 0;
    $scope.AdmitId = 0;
    $scope.Paging = 10;
    $scope.CurruntIndex = 0;
    $scope.TreatmentModel = { TreatId: 0, TreatmentDate: "", DoctorId: 0,AdmitId:0,TreatmentDetails:"",FollowUpDate:"",Procedures:"",AdmitDate:"" };

    $scope.ProductModel = { BillDetailsId: 0, BillNo: 0, ProductId: 0,ProductName:"",Quantity:0,Price:0,Amount:0,ExpiryDate:"" };

    $scope.Prefix = "";

    $scope.EditProduct=function(productmodel)
    {
        $scope.AddProduct=false;
        $scope.EditProduct=true;
        $("#ddlProduct").val(productmodel.ProductId);
        $scope.ProductChange(productmodel);

    }

    $scope.CheckBatchNo=false;

    $scope.ProductChange=function(productmodel)
    {
        if($("#ddlProduct").val()>0)
        {
           var url = GetVirtualDirectory() + '/Store/DoctorTreatment.aspx?RequestFor=GetProductBatch&ProductId='+$("#ddlProduct").val();
           $http({
                method: 'GET',
                url: url,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                processData: false ,
            }).then(function (response) {
                 var data=response.data;
                 if (data.ProductBatchList.length>0) {
                    $scope.CheckBatchNo=true;
                    var html="";
                    var html1="";
                    angular.forEach($scope.ProductBatchList, function(value, key) {
                        html+='<option value="' + value.BatchNo +'">'+ value.BathcNo +'</option>';
                        html1+='<option value="' + value.ExpiryDate +'">'+ value.ExpiryDate +'</option>';
                    });
                    $("#ddlBatch").html(html);
                    $("#ddlExpiry").html(html1);
                    if (productmodel!==undefined) {
                        $("#ddlBatch").val(productmodel.BatchNo);
                        $("#ddlExpiry").val(productmodel.ExpiryDate);
                    }
                }
            },
            function (response) {
           
            }); 
        }
    }

    $scope.SaveProduct=function(IsEdit)
    {
        $scope.AddProduct=true;
        $scope.EditProduct=false;
        if (IsEdit) {
            
        }
        else {
    
        }
    }

    $scope.AddNewUI = function (isedit) {
        $scope.AddProduct=true;
        $scope.EditProduct=false;
        $("#ddlPType").val(0);
        $scope.TreatmentModel = { TreatId: 0, TreatmentDate: "", DoctorId: 0,AdmitId:0,TreatmentDetails:"",FollowUpDate:"",Procedures:"",AdmitDate:"" };
        $scope.Details = false;
        $scope.Add = true;
        $scope.Edit = false;
    }


    function GetPatientList()
    {
        var url = GetVirtualDirectory() + '/Store/DoctorTreatment.aspx?RequestFor=GetDetails';
        $http({
            method: 'GET',
            url: url,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            processData: false ,
        }).then(function (response) {
                var data=response.data;
                $scope.PatientList=response.data.PatientList;
                $scope.DoctorList=response.data.DoctorList;
                $scope.MainTreatmentList=response.data.DoctorTreatmentList;
                $scope.TreatmentList=response.data.DoctorTreatmentList;
                $scope.ProductList=response.data.ProductList;
                $scope.First();
                var html="";
                angular.forEach($scope.PatientList, function(value, key) {
                    html+='<option value="' + value.AdmitId +'">'+ value.PatientFirstName +'</option>';
                });
                $("#ddlPType").html(html);
                $("#ddlPType").val(0);
                html="";
                angular.forEach($scope.DoctorList, function(value, key) {
                    html+='<option value="' + value.PKId +'">'+ value.FullName +'</option>';
                });
                $("#ddlDoctors").html(html);
                $("#ddlDoctors").val(0);

                html="";
                angular.forEach($scope.ProductList, function(value, key) {
                    html+='<option value="' + value.ProductId +'">'+ value.ProductName +'</option>';
                });
                $("#ddlProduct").html(html);
                $("#ddlProduct").val(0);

        },
        function (response) {
           
        });
    }

    $scope.FilterList = function () {
        var reg = new RegExp($scope.Prefix.toLowerCase());
        $scope.TreatmentList = $scope.MainTreatmentList.filter(function (actype) {
            return (reg.test(actype.PatientName.toLowerCase()));
        });
        $scope.First();
    }

    $scope.Reset = function () {
        $scope.TreatmentList = $scope.MainTreatmentList;
        $scope.SearchTreatmentList = $scope.TreatmentList;
        $scope.First();
    }

    $scope.CancelClick = function () {
        $scope.ProductModel = { BillDetailsId: 0, BillNo: 0, ProductId: 0,ProductName:"",Quantity:0,Price:0,Amount:0,ExpiryDate:"" };
        $scope.AddedProductList=[];
        $scope.Details = true;
        $scope.Add = false;
        $scope.Edit = false;
    }

    $scope.EditClick = function (ProductTypeModel) {
        $scope.TreatmentModel = { 
                                    TreatId: ProductTypeModel.TreatId, 
                                    TreatmentDate: ProductTypeModel.TreatmentDate, 
                                    DoctorId: ProductTypeModel.DoctorId,
                                    AdmitId:ProductTypeModel.AdmitId,
                                    TreatmentDetails:ProductTypeModel.TreatmentDetails,
                                    FollowUpDate:ProductTypeModel.FollowUpDate,
                                    Procedures:ProductTypeModel.Procedures,
                                    AdmitDate:ProductTypeModel.AdmitDate   
                                };
        $("#ddlPType").val(ProductTypeModel.AdmitId);
        $("#ddlDoctors").val(ProductTypeModel.DoctorId);
        $scope.TreatmentModel.TreatmentDetails=ProductTypeModel.TreatmentDetails;
        $scope.TreatmentModel.Procedures=ProductTypeModel.Procedures;
        $scope.Details = false;
        $scope.Add = false;
        $scope.Edit = true;
    }

    $scope.Print=function(TreatmentModel)
    {
        var url = GetVirtualDirectory() + '/PathalogyReport/Reports.aspx?ReportType=DoctorTreatmentChart&AdmitId='+ TreatmentModel.AdmitId;
        window.location=url;
    }

    $scope.Save = function (isEdit) {
        if ($("#ddlPType").val()=="0") {
            $scope.ErrorModel.IsPatientName = true;
            $scope.ErrorMessage = "Patient name should be selected.";
            return false;
        }
        else {
            $scope.ErrorModel.IsPatientName = false;
        }
        if ($("#ddlDoctors").val()=="0") {
            $scope.ErrorModel.IsDoctorName = true;
            $scope.ErrorMessage = "Doctor name should be selected.";
            return false;
        }
        else {
            $scope.ErrorModel.IsDoctorName = false;
        }

        if ($scope.TreatmentModel.TreatmentDetails=="") {
            $scope.ErrorModel.IsTreatment = true;
            $scope.ErrorMessage = "Treatment Details should be selected.";
            return false;
        }
        else {
            $scope.ErrorModel.IsTreatment = false;
        }
        
        var url = GetVirtualDirectory() + '/Store/DoctorTreatment.aspx/Save';
        if (isEdit == false) {
            url = GetVirtualDirectory() + '/Store/DoctorTreatment.aspx/Update';
        }
        var model={};

        $scope.TreatmentModel.DoctorId=$("#ddlDoctors").val();
        $scope.TreatmentModel.AdmitId=$("#ddlPType").val();
        $scope.TreatmentModel.FollowUpDate=$('#txtFollowUpDate').val();
        $scope.TreatmentModel.TreatmentDate=$('#txtTreatmentDate').val();
        var req = {
            method: 'POST',
            url: url,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            processData: false ,
            data: {model: JSON.stringify($scope.TreatmentModel)},
        };
        $http(req).then(function (response) {
             $scope.AddedProductList=[];
             GetPatientList();
        },
        function (response) {
           
        });
    }

    $scope.First = function () {
        $scope.CurruntIndex = 0;
        $scope.SearchTreatmentList = $filter('limitTo')($scope.TreatmentList, $scope.Paging, 0);
    }

    $scope.Prev = function () {
        $scope.CurruntIndex = $scope.CurruntIndex - $scope.Paging;
        if ($scope.CurruntIndex >= 0) {
            $scope.SearchTreatmentList = $filter('limitTo')($scope.TreatmentList, $scope.Paging, $scope.CurruntIndex);
        }
        else {
            $scope.CurruntIndex = 0;
        }
    }

    $scope.Next = function () {
        $scope.CurruntIndex = $scope.CurruntIndex + $scope.Paging;
        if ($scope.CurruntIndex <= $scope.TreatmentList.length) {
            $scope.SearchTreatmentList = $filter('limitTo')($scope.TreatmentList, $scope.Paging, $scope.CurruntIndex);
        }
        else {
            $scope.Last();
        }
    }

    $scope.Last = function () {
        var total = $scope.TreatmentList.length;
        var rem = parseInt($scope.TreatmentList.length) % parseInt($scope.Paging);
        var position = $scope.TreatmentList.length - $scope.Paging;
        if (rem > 0) {
            position = $scope.TreatmentList.length - rem;
        }
        $scope.CurruntIndex = position;
        $scope.SearchTreatmentList = $filter('limitTo')($scope.TreatmentList, $scope.Paging, position);
    }

    $scope.init = function () {
        $(document).ready(function () {
            $('#txtTreatmentDate').datepicker({
                format: "yyyy-mm-dd"
            });
            $('#txtTreatmentDate').datepicker()
             .on('changeDate', function (ev) {
                 $('#txtTreatmentDate').val(ev.date);
                 $('#txtTreatmentDate').datepicker("hide");
             });
        });
        GetPatientList();
    }

    $scope.init();

}]);