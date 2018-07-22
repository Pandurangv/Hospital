<%@ Page Title="" Language="C#" MasterPageFile="~/Dashboard.Master" AutoEventWireup="true" CodeBehind="IPDMedicineBill.aspx.cs" Inherits="Hospital.PathalogyReport.IPDMedicineBill" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid">
        <div class="row clearfix" data-ng-app="HospitalApp" data-ng-controller="IPDMedicineBillController">
            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                <div class="card">
                    <div class="header">
                        <h2>
                            IPD Medicine Bills</h2>
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
                                        Admit Date
                                    </th>
                                    <th>
                                        Doctor Name
                                    </th>
                                    <th>
                                        Bill Amount
                                    </th>
                                    <th>
                                        Add Bill
                                    </th>
                                    <th>
                                        Print
                                    </th>
                                </tr>
                                <tbody>
                                    <tr data-ng-repeat="bill in SearchBillList">
                                        <td>
                                            {{bill.PatientName}}
                                        </td>
                                        <td>
                                            {{bill.AdmitDate | mydate | date: 'yyyy-MM-dd'}}
                                        </td>
                                        <td>
                                            {{bill.EmployeeName}}
                                        </td>
                                        <td>
                                            {{bill.NetAmount}}
                                        </td>
                                        <td>
                                            <span class="glyphicon glyphicon-floppy-disk" data-ng-show="bill.NetAmount==0" data-ng-click="AddNewUI(bill)"></span>
                                        </td>
                                        <td>
                                            <span class="glyphicon glyphicon-print" data-ng-show="bill.NetAmount>0" data-ng-click="PrintBill(bill)"></span>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                            <div class="col-md-6">
                               <div class="col-md-3">
                                <ul class="pagination" style="margin:0px;">
                                    <li data-ng-click="First()"><a href="#"><span class="glyphicon glyphicon-step-backward">
                                    </span></a></li>
                                    <li data-ng-click="Prev()"><a href="#"><span class="glyphicon glyphicon-backward"></span>
                                    </a></li>
                                </ul></div>
                                <div class="col-md-4">
                                    <select class="form-control" id="ddlPagination"></select>
                                </div>
                                <div class="col-md-3">
                                <ul class="pagination" style="margin:0px;">
                                    <li data-ng-click="Next()"><a href="#"><span class="glyphicon glyphicon-forward"></span>
                                    </a></li>
                                    <li data-ng-click="Last()"><a href="#"><span class="glyphicon glyphicon-step-forward">
                                    </span></a></li>
                                </ul>
                                </div>
                            </div>
                        </div>
                        <div data-ng-show="Add==true || Edit==true">
                            <input type="hidden" data-ng-model="BillModel.TreatId" />
                            <div class="col-md-12">
                                <div class="col-md-6">
                                    <select class="form-control setMargin" id="ddlPType">
                                    </select>
                                    <div class="col-md-12 validationError" data-ng-show="ErrorModel.IsPatientName==true">
                                        <span style="color: #d61a1a">{{ErrorMessage}}</span>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-12">
                                <div class="col-md-6">
                                    <div class="demo-radio-button">
                                        <input name="group1" type="radio" id="IsCash" checked="checked"/>
                                        <label for="IsCash">
                                            Cash</label>
                                        <input name="group1" type="radio" id="IsCredit" />
                                        <label for="IsCredit">
                                            Credit</label>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-12">
                                <table class="table table-bordered">
                                    <thead>
                                        <tr>
                                            <th>Product Name</th>
                                            <th>Quantity</th>
                                            <th>Price</th>
                                            <th>Amount</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr data-ng-repeat="product in ProductList">
                                            <td>
                                                {{product.ProductName}}
                                            </td>
                                            <td>
                                                <input type="text" class="form-control" data-ng-model="product.Quantity" />
                                            </td>
                                            <td>
                                                <input type="text" class="form-control" data-ng-model="product.Price" data-ng-blur="AmountCal()" />
                                            </td>
                                            <td>
                                                <input type="text" class="form-control" data-ng-model="product.Amount" />
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>

                                <table class="table table-bordered">
                                    <thead>
                                        <tr>
                                            <th>Test Name</th>
                                            <th>Charges</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr data-ng-repeat="test in LabTestList">
                                            <td>
                                                {{test.TestName}}
                                            </td>
                                            <td>
                                                <input type="text" class="form-control text-right" data-ng-model="test.TestCharge" />
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </div>
                            <div class="col-md-12 center">
                                <div class="col-md-4 pull-left">
                                    <span>Discount</span>
                                    <input type="text" class="form-control" data-ng-blur="AmountCal()" data-ng-model="BillModel.Discount" />
                                </div>
                                <div class="col-md-4">
                                </div>
                                <div class="col-md-4 pull-right">
                                    <span>Net Amount</span>
                                    <input type="text" class="form-control" data-ng-model="BillModel.NetAmount"/>
                                    <div class="col-md-12 validationError" data-ng-show="ErrorModel.IsMedicineQty==true">
                                        <span style="color: #d61a1a">{{ErrorMessage}}</span>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-12">
                                <div class="col-md-4 pull-left">
                                    
                                </div>
                                <div class="col-md-4">
                                </div>
                                <div class="col-md-4 pull-right">
                                    <span>Test Invoice Amount</span>
                                    <input type="text" class="form-control" data-ng-model="TestInvoice.Amount"/>
                                    <div class="col-md-12 validationError" data-ng-show="ErrorModel.IsTestCharges==true">
                                        <span style="color: #d61a1a">{{ErrorMessage}}</span>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-12">
                                <button type="button" class="btn btn-primary"  data-ng-click="Save()">
                                    Save</button>
                                <button type="button" class="btn btn-primary">
                                    Cancel</button>
                            </div>
                        </div>
                    </div>
                </div>
                
            </div>
        </div>
    </div>
    <script type="text/javascript" src="../Scripts/Bootstrap/IPDMedicineBillController.js"></script>
</asp:Content>
