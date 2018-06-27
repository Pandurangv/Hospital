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
                                        Action
                                    </th>
                                    <th>
                                        Print
                                    </th>
                                    <th>
                                        Print Bill
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
                                            <button type="button" class="btn btn-primary" data-ng-click="EditClick(accounttype)">
                                                Edit</button>
                                        </td>
                                        <td>
                                            <button type="button" class="btn btn-primary" data-ng-click="Print(accounttype)">
                                                Print</button>
                                        </td>
                                        <td>
                                            <button type="button" class="btn btn-primary" data-ng-click="PrintBill(accounttype)">
                                                Print Bill</button>
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
                        <div data-ng-show="Add==true || Edit==true">
                            <div class="form-group">
                                <input type="hidden" data-ng-model="TreatmentModel.TreatId" />
                                <div class="col-md-12">
                                    <div class="col-md-6">
                                            <div class="form-group margin-bottom_25">
                                                <div class="form-line">
                                                    <select class="form-control setMargin" id="ddlPType">
                                                    </select>
                                                </div>
                                            </div>
                                    </div>
                                    <div class="col-md-6">
                                            <div class="form-group margin-bottom_25">
                                                <div class="form-line">
                                                    <select class="form-control setMargin" id="ddlDoctors">
                                                    </select>
                                                </div>
                                            </div>
                                    </div>
                                    <div class="col-md-12">
                                        <div class="col-md-6" data-ng-show="ErrorModel.IsPatientName==true">
                                            <span>{{ErrorMessage}}</span>
                                        </div>
                                        <div class="col-md-6" data-ng-show="ErrorModel.IsDoctorName==true">
                                            <span>{{ErrorMessage}}</span>
                                        </div>
                                    </div>
                                    <!--Second Level-->
                                    <div class="col-md-6">
                                            <div class="form-group margin-bottom_25">
                                                <div class="form-line">
                                                    <input type="text" placeholder="Treatment Date" id="txtTreatmentDate" maxlength="50"
                                                        minlength="2" class="form-control setMargin" />
                                                </div>
                                            </div>
                                    </div>
                                    <div class="col-md-6">
                                            <div class="form-group margin-bottom_25">
                                                <div class="form-line">
                                                    <input type="text" placeholder="Treatment Details" data-ng-model="TreatmentModel.TreatmentDetails"
                                                        maxlength="200" minlength="2" class="form-control setMargin" />
                                                </div>
                                            </div>
                                    </div>
                                    <div class="col-md-6">
                                    </div>
                                    <div class="col-md-6" data-ng-show="ErrorModel.IsTreatment==true">
                                        <span>{{ErrorMessage}}</span>
                                    </div>
                                   

                                     <!--Third Level-->
                                    <div class="col-md-12">
                                        <div class="col-md-6">
                                            <div class="form-group margin-bottom_25">
                                                <div class="form-line">
                                                    <input type="text" placeholder="Procedures" data-ng-model="TreatmentModel.Procedures"
                                                        maxlength="200" minlength="2" class="form-control setMargin" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-6">
                                            <div class="form-group margin-bottom_25">
                                                <div class="form-line">
                                                    <input type="text" placeholder="Treatment Time" value="<%=string.Format("{0:hh:mm tt}",DateTime.Now) %>"
                                                        id="TreatmentTime" maxlength="200" minlength="2" class="form-control setMargin" />
                                                    </div> 
                                                </div> 
                                            </div> 
                                       </div>
                                       <!--End Third Level-->

                                     <div class="col-md-12">
                                        <div class="col-md-6">
                                            <div class="form-group margin-bottom_25">
                                                <div class="form-line">
                                                    <select class="form-control setMargin" id="ddlProduct" data-ng-click="ProductChange()">
                                                    </select>
                                                </div>
                                            </div>
                                    </div>
                                    <div class="col-md-6">
                                            <div class="form-group margin-bottom_25">
                                                <div class="form-line">
                                                    <input type="text" placeholder="Quantity" id="qty" maxlength="50" 
                                                        minlength="2" class="form-control setMargin" />
                                                </div>
                                            </div>
                                    </div>
                                    <div class="col-md-12">
                                        <div class="col-md-6" data-ng-show="ErrorModel.IsProductName==true">
                                            <span>{{ErrorMessage}}</span>
                                        </div>
                                        <div class="col-md-6" data-ng-show="ErrorModel.IsProductQuantity==true">
                                            <span>{{ErrorMessage}}</span>
                                        </div>
                                    </div>
                                     </div>

                                     <div class="col-md-12">
                                        <div class="col-md-6">
                                            <div class="form-group margin-bottom_25">
                                                <div class="form-line">
                                                    <select class="form-control setMargin" id="ddlBatch" data-ng-show="CheckBatchNo==true">
                                                    </select>
                                                    <input type="text" placeholder="Batch No" id="txtBatchNo" data-ng-show="CheckBatchNo==false"
                                                        maxlength="200" minlength="2" class="form-control setMargin" />
                                                </div>
                                            </div>
                                    </div>
                                    <div class="col-md-6">
                                            <div class="form-group margin-bottom_25">
                                                <div class="form-line">
                                                    <select class="form-control setMargin" id="ddlExpiry" data-ng-show="CheckBatchNo==true">
                                                    </select>
                                                    <input type="text" placeholder="Expiry Date" id="txtExpiryDate" data-ng-show="CheckBatchNo==false"
                                                        maxlength="200" minlength="2" class="form-control setMargin" />
                                                </div>
                                            </div>
                                    </div>
                                    <div class="col-md-12">
                                        <div class="col-md-6" data-ng-show="ErrorModel.IsProductBatch==true">
                                            <span>{{ErrorMessage}}</span>
                                        </div>
                                        <div class="col-md-6" data-ng-show="ErrorModel.IsProductExpiry==true">
                                            <span>{{ErrorMessage}}</span>
                                        </div>
                                    </div>
                                     </div>

                                     <div class="col-md-12">
                                        <div class="col-md-6">
                                            <div class="form-group margin-bottom_25">
                                                <div class="form-line">
                                                    <input type="text" placeholder="Price" data-ng-model="ProductModel.Price" data-ng-change="AmountChanged()" maxlength="50"
                                                        minlength="2" class="form-control setMargin" />
                                                </div>
                                            </div>
                                    </div>
                                    <div class="col-md-6">
                                            <div class="form-group margin-bottom_25">
                                                <div class="form-line">
                                                    <input type="text" placeholder="Amount" id="amt"  maxlength="50"
                                                        minlength="2" class="form-control setMargin" />
                                                </div>
                                            </div>
                                    </div>
                                    </div>
                                    <div class="col-md-12">
                                        <div class="col-md-6" data-ng-show="ErrorModel.IsProductPrice==true">
                                            <span>{{ErrorMessage}}</span>
                                        </div>
                                    </div>
                                    <div class="col-md-12" data-ng-show="ProductError==true">
                                        <span>{{ErrorMessage}}</span>
                                    </div>

                                    <div class="col-md-12" style="display:none">
                                        <div class="col-md-6">
                                            <div class="form-group margin-bottom_25">
                                                <div class="form-line">
                                                    <input type="text" placeholder="Tax Percent" id="taxpercent"  maxlength="50"
                                                        minlength="2" class="form-control setMargin" />
                                                </div>
                                            </div>
                                    </div>
                                    <div class="col-md-6">
                                            <div class="form-group margin-bottom_25">
                                                <div class="form-line">
                                                    <input type="text" placeholder="Tax Amount" id="txamt" maxlength="50"
                                                        minlength="2" class="form-control setMargin" />
                                                </div>
                                            </div>
                                    </div>
                                    </div>
                                    <div class="col-md-12" style="display:none">
                                        <div class="col-md-6" data-ng-show="ErrorModel.IsProductTax==true">
                                            <span>{{ErrorMessage}}</span>
                                        </div>
                                    </div>
                                    <div class="col-md-12" data-ng-show="ErrorModel.IsTaxAmount==true" style="display:none">
                                        <span>{{ErrorMessage}}</span>
                                    </div>

                                    <div class="col-md-12">
                                        <button class="btn btn-Default" data-ng-show="AddProduct==true" data-ng-click="SaveProduct(true)" data-ng-disabled="DisableButton">Add</button>
                                        <button class="btn btn-Default" data-ng-show="EditProduct==true" data-ng-click="SaveProduct(false)" data-ng-disabled="DisableButton">Update</button>
                                        <button class="btn btn-Default">Cancel</button>
                                    </div>
                                        <div class="col-md-12" data-ng-show="AddedProductList.length>0">
                                             <table class="table table-bordered">
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
                                                         Price
                                                     </th>
                                                     <th>
                                                         Quantity
                                                     </th>
                                                     <th>
                                                         Amount
                                                     </th>
                                                     <th>
                                                         Edit
                                                     </th>
                                                 </tr>
                                                 <tbody>
                                                     <tr data-ng-repeat="product in AddedProductList">
                                                         <td>
                                                             {{product.ProductName}}
                                                         </td>
                                                         <td>
                                                             {{product.BatchNo}}
                                                         </td>
                                                         <td>
                                                             {{product.ExpiryDate}}
                                                         </td>
                                                         <td>
                                                            {{product.Price}}
                                                         </td>
                                                         <td>
                                                            {{product.Quantity}}
                                                         </td>
                                                         <td>
                                                            {{product.Amount}}
                                                         </td>
                                                         <td>
                                                             <button type="button" class="btn btn-primary" data-ng-click="EditProduct(product)">
                                                                 Edit</button>
                                                         </td>
                                                     </tr>
                                                 </tbody>
                                             </table>
                                         </div>
                                     


                                    <div class="col-md-12">
                                        <button type="button" class="btn btn-primary" data-ng-click="Save(true)" data-ng-show="Add==true" data-ng-disabled="DisableButton">
                                            Save</button>
                                        <button type="button" class="btn btn-primary" data-ng-click="Save(false)" data-ng-show="Edit==true" data-ng-disabled="DisableButton">
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
        </div>
    </div>
    <script type="text/javascript" src="../Scripts/Bootstrap/DoctorTreatmentController.js"></script>
</asp:Content>
