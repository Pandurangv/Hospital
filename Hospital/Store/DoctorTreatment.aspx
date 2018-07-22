<%@ Page Title="" Language="C#" MasterPageFile="~/Dashboard.Master" AutoEventWireup="true"
    CodeBehind="DoctorTreatment.aspx.cs" Inherits="Hospital.Store.DoctorTreatment" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<script type="text/javascript" src="../scripts/bootstrap/bootstrap.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid">
        <div class="row clearfix" data-ng-app="HospitalApp" data-ng-controller="DoctorTreatmentController">
            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                <div class="card">
                    <div class="header">
                        <h2>
                            Doctor Treatment</h2>
                    </div>
                    <div class="body">
                        <div class="table-responsive" data-ng-show="Details==true">
                            <ul class="nav nav-tabs">
                                <li class="active" id="lihome"><a data-toggle="tab" href="#home">
                                    <h4>
                                        Treatment Details</h4>
                                </a></li>
                                <li id="limenu"><a data-toggle="tab" href="#menu1" data-ng-show="DisplayProductList.length>0">
                                    <h4>
                                        {{PatientName}}</h4>
                                </a></li>
                            </ul>
                            <div class="tab-content">
                                <div id="home" class="tab-pane fade in active">
                                    <div class="col-md-12">
                                        <div class="col-md-8 divsearch">
                                            <input type="text" placeholder="Search" class="form-control" data-ng-model="Prefix"
                                                data-ng-change="FilterList()" />
                                        </div>
                                        <div class="col-md-4">
                                            <button type="button" class="btn btn-primary" data-ng-click="FilterList()" data-toggle="tooltip"
                                                title="Search">
                                                Search</button>
                                            <button type="button" class="btn btn-primary" data-ng-click="AddNewUI()" data-toggle="tooltip"
                                                title="New">
                                                New</button>
                                            <button type="button" class="btn btn-primary" data-ng-click="Reset()" data-toggle="tooltip"
                                                title="Reset">
                                                Reset</button>
                                        </div>
                                        <hr />
                                    </div>
                                    <table class="table table-bordered">
                                        <tr>
                                            <th>
                                                Patient Name
                                            </th>
                                            <th>
                                                Doctor Name
                                            </th>
                                            <th>
                                                Treatment Date
                                            </th>
                                            <th>
                                                Show Details
                                            </th>
                                        </tr>
                                        <tbody>
                                            <tr data-ng-repeat="accounttype in SearchTreatmentList">
                                                <td>
                                                    {{accounttype.PatientName}}
                                                </td>
                                                <td>
                                                    {{accounttype.EmployeeName}}
                                                </td>
                                                <td>
                                                    {{accounttype.TreatmentDate | mydate | date: 'yyyy-MM-dd'}}
                                                </td>
                                                <td>
                                                    <button type="button" class="btn btn-primary" data-ng-click="DisplayDetails(accounttype)">
                                                        Show</button>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                    <ul class="pagination">
                                        <li data-ng-click="First()"><a href="#"><span class="glyphicon glyphicon-step-backward">
                                        </span></a></li>
                                        <li data-ng-click="Prev()"><a href="#"><span class="glyphicon glyphicon-backward"></span>
                                        </a></li>
                                        <li data-ng-click="Next()"><a href="#"><span class="glyphicon glyphicon-forward"></span>
                                        </a></li>
                                        <li data-ng-click="Last()"><a href="#"><span class="glyphicon glyphicon-step-forward">
                                        </span></a></li>
                                    </ul>
                                </div>
                                <div id="menu1" class="tab-pane fade" data-ng-show="DisplayProductList.length>0">
                                    <div class="col-md-12">
                                        <div class="col-md-6">
                                            <div>
                                                <span class="pull-left patient_name"><strong>Patient Name </strong>: {{PatientName}}</span></div>
                                            <br />
                                        </div>
                                        <div class="col-md-6">
                                            <div class="col-md-6 pull-right">
                                                <div>
                                                    <span class="pull-left"><strong>MRN</strong> : {{PatientInfo.PatientCode}}</span></div>
                                                <br />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-12">
                                        <table class="table table-bordered">
                                            <thead>
                                                <tr>
                                                    <th>
                                                        Product Name
                                                    </th>
                                                    <th>
                                                        Batch No
                                                    </th>
                                                    <th>
                                                        Expiry Date
                                                    </th>
                                                    <th>
                                                        Morning Quantity
                                                    </th>
                                                    <th>
                                                        Afternoon Quantity
                                                    </th>
                                                    <th>
                                                        Evening Quantity
                                                    </th>
                                                    <th>
                                                        Night Quantity
                                                    </th>
                                                    <th>
                                                        Test Name
                                                    </th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <tr data-ng-repeat="product in DisplayProductList">
                                                    <td>
                                                        {{product.ProductName}}
                                                    </td>
                                                    <td>
                                                        {{product.BatchNo}}
                                                    </td>
                                                    <td>
                                                        {{product.ExpiryDate | mydate | date: 'yyyy-MM-dd'}}
                                                    </td>
                                                    <td>
                                                        {{product.MorningQty}}
                                                    </td>
                                                    <td>
                                                        {{product.AfterNoonQty}}
                                                    </td>
                                                    <td>
                                                        {{product.EveningQty }}
                                                    </td>
                                                    <td>
                                                        {{product.NightQty}}
                                                    </td>
                                                    <td>
                                                        {{product.TestName}}
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div data-ng-show="Add==true || Edit==true">
                            <input type="hidden" data-ng-model="TreatmentModel.TreatId" />
                            <div class="col-md-12">
                                <div class="col-md-6">
                                    <select class="form-control setMargin" id="ddlPType" data-ng-click="PatientChanged()">
                                    </select>
                                    <div class="col-md-12 validationError" data-ng-show="ErrorModel.IsPatientName==true">
                                        <span style="color: #d61a1a">{{ErrorMessage}}</span>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <select class="form-control setMargin" id="ddlDoctors">
                                    </select>
                                    <div class="col-md-12 validationError" data-ng-show="ErrorModel.IsDoctorName==true">
                                        <span style="color: #d61a1a">{{ErrorMessage}}</span>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-12">
                                <div class="col-md-6">
                                    <input type="text" placeholder="Treatment Date" id="txtTreatmentDate" maxlength="50"
                                        minlength="2" class="form-control setMargin" />
                                </div>
                                <div class="col-md-6">
                                    <input type="text" placeholder="Treatment Details && Instructions" data-ng-model="TreatmentModel.TreatmentDetails"
                                        maxlength="200" minlength="2" class="form-control setMargin" />
                                </div>
                            </div>
                            <div class="col-md-12">
                                <table class="table-table-bordered table-responsive">
                                    <thead>
                                        <tr>
                                            <th class="col-md-2">Product Name</th>
                                            <th class="col-md-1">Batch No</th>
                                            <th class="col-md-2">Expiry Date</th>
                                            <th class="col-md-1">Morning Quantity</th>
                                            <th class="col-md-2">Afternoon Quantity</th>
                                            <th class="col-md-1">Evening Quantity</th>
                                            <th class="col-md-1">Night Quantity</th>
                                            <th class="col-md-2">Pathology Tests</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr>
                                            <td>
                                                <select class="form-control setMargin" style="margin-top: 20px;" id="ddlProduct" data-ng-click="ProductChange()">
                                                </select>
                                                <br />
                                                    <span style="color: #d61a1a" data-ng-show="ErrorModel.IsProductName==true">{{ErrorMessage}}</span>
                                            </td>
                                            <td>
                                                <select class="form-control setMargin" id="ddlBatch" data-ng-show="CheckBatchNo==true">
                                                </select>
                                                <input type="text" placeholder="Batch No" id="txtBatchNo" data-ng-show="CheckBatchNo==false"
                                                    maxlength="200" minlength="2" class="form-control setMargin" />
                                            </td>
                                            <td>
                                                <select class="form-control setMargin" id="ddlExpiry" data-ng-show="CheckBatchNo==true">
                                                </select>
                                                <input type="text" placeholder="Expiry Date" id="txtExpiryDate" data-ng-show="CheckBatchNo==false"
                                                    maxlength="200" minlength="2" class="form-control setMargin" />
                                            </td>
                                            <td>
                                                <input type="text" placeholder="Morning Quantity" style="margin-top: 20px;" data-ng-model="ProductModel.MorningQty" 
                                                        class="form-control setMargin" />
                                                    <span style="color: #d61a1a"  data-ng-show="ErrorModel.IsQty==true">{{ErrorMessage}}</span>
                                            </td>
                                            <td>
                                                <input type="text" placeholder="Afternoon Quantity" data-ng-model="ProductModel.AfterNoonQty" 
                                                        class="form-control setMargin" />
                                                    <span style="color: #d61a1a" data-ng-show="ErrorModel.IsQty==true">{{ErrorMessage}}</span>
                                            </td>
                                            <td>
                                                <input type="text" placeholder="Evening Quantity" data-ng-model="ProductModel.EveningQty" 
                                                        class="form-control setMargin" />
                                                    <span style="color: #d61a1a"  data-ng-show="ErrorModel.IsQty==true">{{ErrorMessage}}</span>
                                            </td>
                                            <td>
                                                <input type="text" placeholder="Night Quantity" data-ng-model="ProductModel.NightQty" 
                                                        class="form-control setMargin" />
                                                    <span style="color: #d61a1a"  data-ng-show="ErrorModel.IsQty==true">{{ErrorMessage}}</span>
                                            </td>
                                            <td>
                                                <select class="form-control setMargin" id="ddlLabtest">
                                                </select>
                                            </td>
                                        </tr>
                                        <tr>
                                            
                                        </tr>
                                    </tbody>
                                </table>
                            </div>
                            <div class="col=md-12 center">
                                <button type="button" class="btn btn-primary" data-ng-click="SaveProduct(true)">
                                    Add</button>
                                
                            </div>
                            <div class="col-md-12">
                                <table class="table-table-bordered table-responsive">
                                    <thead>
                                        <tr>
                                            <th class="col-md-2">Product Name</th>
                                            <th class="col-md-1">Batch No</th>
                                            <th class="col-md-2">Expiry Date</th>
                                            <th class="col-md-1">Morning Quantity</th>
                                            <th class="col-md-1">Afternoon Quantity</th>
                                            <th class="col-md-1">Evening Quantity</th>
                                            <th class="col-md-1">Night Quantity</th>
                                            <th class="col-md-2">Pathology Tests</th>
                                            <th class="col-md-1">Delete</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr data-ng-repeat="product in AddedProductList">
                                            <td class="col-md-2">
                                                {{product.ProductName}}
                                            </td>
                                            <td class="col-md-1">
                                                {{product.BatchNo}}
                                            </td>
                                            <td class="col-md-2">
                                                {{product.ExpiryDate | mydate | date: 'yyyy-MM-dd'}}
                                            </td>
                                            <td class="col-md-1">
                                                {{product.MorningQty}}
                                            </td>
                                            <td class="col-md-1">
                                                {{product.AfterNoonQty}}
                                            </td>
                                            <td class="col-md-1">
                                                {{product.EveningQty }}
                                            </td>
                                            <td class="col-md-1">
                                                {{product.NightQty}}
                                            </td>
                                            <td class="col-md-2">
                                                {{product.TestName}}
                                            </td>
                                            <td class="col-md-1">
                                                <span class="glyphicon glyphicon-pencil" data-ng-click="EditProductDetails(product)"></span>
                                                <span class="glyphicon glyphicon-trash" data-ng-show="product.BillDetailId==0" data-ng-click="RemoveProduct(product)"></span>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </div>
                            <div class="col-md-12">
                                <button type="button" class="btn btn-primary" data-ng-click="Save(true)" data-ng-show="Add==true"
                                    data-ng-disabled="DisableButton">
                                    Save</button>
                                <button type="button" class="btn btn-primary" data-ng-click="Save(false)" data-ng-show="Edit==true"
                                    data-ng-disabled="DisableButton">
                                    Update</button>
                                <button type="button" class="btn btn-primary" data-ng-click="CancelClick()">
                                    Cancel</button>
                            </div>
                        </div>
                    </div>
                </div>
                
            </div>
        </div>
    </div>
    <script type="text/javascript" src="../Scripts/Bootstrap/angucomplete-alt.js"></script>
    <script type="text/javascript" src="../Scripts/Bootstrap/DoctorTreatmentController.js"></script>
</asp:Content>
