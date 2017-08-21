HospitalApp.controller("DoctorTreatmentController", ['$scope', '$http', '$filter', '$rootScope', function ($scope, $http, $filter, $rootScope) {
    // Declaration of arrays
    $scope.DoctorList = [];
    $scope.PatientList = [];
    $scope.MainTreatmentList = [];
    $scope.TreatmentList = [];
    $scope.SearchTreatmentList = [];
    $scope.ProductList=[];
    $scope.AddedProductList=[];

    // Declaration of Selected Id's

    $scope.SelectedDoctor=0;
    
    $scope.SelectedTempId=0;

    $scope.SelectedPatient=0;
    
    $scope.SelectedBillNo = 0;

    $scope.AdmitId = 0;
    
    /// Declaration of Models
    
    $scope.TreatmentModel = { TreatId: 0, TreatmentDate: "", DoctorId: 0,AdmitId:0,TreatmentDetails:"",FollowUpDate:"",Procedures:"",AdmitDate:"" };

    $scope.ProductModel = { BillDetailId: 0, BillNo: 0, ProductId: 0,ProductName:"",Quantity:0,Price:0,Amount:0,ExpiryDate:"",TempId:0 };
    $scope.ErrorModel = { IsPatientName: false,IsDoctorName:false,IsTreatment:false,IsProductName:false,IsProductQuantity:false,IsProductPrice:false,IsProductAmount:false,ProductExist:false };
    $scope.ErrorMessage = ""
    
    /// Declaration of Flags

    $scope.ProductError=false;
    $scope.Details = true;
    $scope.Add = false;
    $scope.Edit = false;

    $scope.AddProduct=true;
    $scope.EditProduct=false;

    $scope.CheckBatchNo=false;
    
    $scope.Paging = 10;
    $scope.CurruntIndex = 0;

    $scope.Prefix = "";

    $scope.EditProduct=function(productmodel)
    {
        $scope.AddProduct=false;
        $scope.EditProduct=true;
        $scope.SelectedTempId=productmodel.TempId;
        $("#ddlProduct").val(productmodel.ProductId);
        $scope.ProductChange(productmodel);
        $scope.ProductModel = { 
                                TempId:productmodel.TempId,
                                BillDetailId: productmodel.BillDetailId, 
                                BillNo: productmodel.BillNo, 
                                ProductId: productmodel.ProductId,
                                ProductName:productmodel.ProductName,
                                Quantity:productmodel.Quantity,
                                Price:productmodel.Price,
                                Amount:productmodel.Amount,
                                ExpiryDate:productmodel.ExpiryDate 
                              };
    }

    

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
                        if (value.BatchNo!=null) {
                            html+='<option value="' + value.BatchNo +'">'+ value.BathcNo +'</option>';
                        }
                        if (value.ExpiryDate!=null) {
                            html1+='<option value="' + value.ExpiryDate +'">'+ value.ExpiryDate +'</option>';
                        }
                    });
                    $("#ddlBatch").html(html);
                    if ($('select#ddlBatch option').length==0) {
                        $scope.CheckBatchNo=false;
                    }
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
        var product = $scope.AddedProductList.filter(function (pr) {
            return (pr.ProductId===$("#ddlProduct").val());
        });
        if (IsEdit) {
            if (product.length==0) {
                $scope.ProductError=false;
                var tempid=$scope.AddedProductList.length==0?1:$scope.AddedProductList.length + 1;
                $scope.ProductModel = { 
                                TempId:tempid,
                                BillDetailId: 0, 
                                BillNo: 0, 
                                ProductId: $("#ddlProduct").val(),
                                ProductName:$("#ddlProduct option:selected").text(),
                                Quantity:$scope.ProductModel.Quantity,
                                Price:$scope.ProductModel.Price,
                                Amount:parseFloat($scope.ProductModel.Quantity) * parseFloat($scope.ProductModel.Price),
                                ExpiryDate: $scope.CheckBatchNo==false? $("#txtExpiryDate").val():$("#ddlExpiry").val(),
                                BatchNo: $scope.CheckBatchNo==false? $("#txtBatchNo").val():$("#ddlBatch").val(),
                                IsDelete:false, 
                              };
                $scope.AddedProductList.push($scope.ProductModel);
            }
            else {
                $scope.ProductError=true;
                $scope.ErrorMessage="Product already exist.";
            }
        }
        else {
            angular.forEach($scope.AddedProductList, function(value, key) {
                    if (value.TempId==$scope.SelectedTempId) {
                        $scope.AddedProductList[key].BillDetailsId=value.BillDetailsId;
                        $scope.AddedProductList[key].BillNo=$scope.SelectedBillNo;
                        $scope.AddedProductList[key].ProductId=$("#ddlProduct").val();
                        $scope.AddedProductList[key].ProductName=$("#ddlProduct option:selected").text();
                        $scope.AddedProductList[key].Quantity=$scope.ProductModel.Quantity;
                        $scope.AddedProductList[key].Price=$scope.ProductModel.Price;
                        $scope.AddedProductList[key].Amount=parseFloat($scope.ProductModel.Quantity) * parseFloat($scope.ProductModel.Price);
                        $scope.AddedProductList[key].ExpiryDate=$scope.CheckBatchNo==false? $("#txtExpiryDate").val():$("#ddlExpiry").val();
                        $scope.AddedProductList[key].BatchNo=$scope.CheckBatchNo? $("#txtBatchNo").val():$("#ddlBatch").val();
                    }        
                });
        }
        $scope.ProductModel = { BillDetailId: 0, BillNo: 0, ProductId: 0,ProductName:"",Quantity:0,Price:0,Amount:0,ExpiryDate:"",TempId:0 };
        $("#txtExpiryDate").val("");
        $("#ddlProduct").val(0);
        $("#txtBatchNo").val("");
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

    var objdatehelper = new datehelper({ format: "dd/MM/yyyy", cdate: new Date() });
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
        //var dt=new Date($filter('mydate')());
        $("#txtTreatmentDate").val(objdatehelper.getFormatteddate($filter('mydate')(ProductTypeModel.TreatmentDate), "yyyy-MM-dd"));
        $scope.SelectedBillNo=ProductTypeModel.TreatId;

        $scope.TreatmentModel.TreatmentDetails=ProductTypeModel.TreatmentDetails;
        $scope.TreatmentModel.Procedures=ProductTypeModel.Procedures;
        $scope.Details = false;
        $scope.Add = false;
        $scope.Edit = true;

        $scope.GetProductList(ProductTypeModel.TreatId);
    }

    $scope.GetProductList=function(BillNo)
    {
        var url = GetVirtualDirectory() + '/Store/DoctorTreatment.aspx?RequestFor=GetBillDetails&BILLNo=' + BillNo;
        $http({
            method: 'GET',
            url: url,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            processData: false ,
        }).then(function (response) {
             var data=response.data;
             angular.forEach(data, function(value, key) {
                  var tempid=key + 1;
                  var model = { 
                                TempId:tempid,
                                BillDetailId: value.BillDetailId, 
                                BillNo: value.BillNo, 
                                ProductId: value.ProductId,
                                ProductName:value.ProductName,
                                Quantity:value.Quantity,
                                Price:value.Price,
                                Amount:value.Amount,
                                ExpiryDate: objdatehelper.getFormatteddate($filter('mydate')(value.ExpiryDate), "yyyy-MM-dd"),
                                BatchNo: value.BatchNo,
                                IsDelete:value.IsDelete, 
                              };
                $scope.AddedProductList.push(model);
             }); 
        },
        function (response) {
           
        });
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
        $scope.TreatmentModel.ProductList=$scope.AddedProductList;
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

             //

             $('#txtExpiryDate').datepicker({
                format: "yyyy-mm-dd"
            });
            $('#txtExpiryDate').datepicker()
             .on('changeDate', function (ev) {
                 $('#txtExpiryDate').val(ev.date);
                 $('#txtExpiryDate').datepicker("hide");
             });
        });
        GetPatientList();
    }

    $scope.init();

}]);