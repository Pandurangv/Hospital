<%@ Page Title="" Language="C#" MasterPageFile="~/Dashboard.Master" AutoEventWireup="true" CodeBehind="Nursetreatment.aspx.cs" Inherits="Hospital.Treatment.Nursetreatment" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="container-fluid">
        <div class="row clearfix" data-ng-app="HospitalApp" data-ng-controller="NurseTreatmentController">
            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                <div class="card">
                    <div class="header">
                        <h2>
                            Nurse Treatment</h2>
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
                                        Admit Date
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
                                            {{accounttype.FollowUpDate | mydate | date: 'yyyy-MM-dd'}}
                                        </td>
                                        <td>
                                            <button type="button" class="btn btn-primary" data-ng-click="EditClick(accounttype)">
                                                Edit</button>
                                        </td>
                                        <td>
                                            <button type="button" class="btn btn-primary" data-ng-click="Print(accounttype)">
                                                Print</button>
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
                                        <div class="form-group">
                                            <div class="form-line">
                                                <select class="form-control setMargin" id="ddlPType">
                                                </select>
                                            </div>
                                        </div>
                                        <div class="col-md-6" data-ng-show="ErrorModel.IsPatientName==true">
                                            <span>{{ErrorMessage}}</span>
                                        </div>
                                    </div>
                                    <br />
                                    <div class="col-md-12">
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <div class="form-line">
                                                    <select class="form-control setMargin" id="ddlNurse">
                                                    </select>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-6" data-ng-show="ErrorModel.IsNurseName==true">
                                            <span>{{ErrorMessage}}</span>
                                        </div>
                                    </div>
                                    <br />
                                    <div class="col-md-12">
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <div class="form-line">
                                                    <input type="text" placeholder="Treatment Date" id="txtTreatmentDate" maxlength="50"
                                                        minlength="2" class="form-control setMargin" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <br />
                                    <div class="col-md-12">
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <div class="form-line">
                                                    <input type="text" placeholder="Followup Date" id="txtFollowUpDate" maxlength="50"
                                                        minlength="2" class="form-control setMargin" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <br />
                                    <div class="col-md-12">
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <div class="form-line">
                                                    <input type="text" placeholder="Treatment Details" data-ng-model="TreatmentModel.TreatmentDetails"
                                                        maxlength="200" minlength="2" class="form-control setMargin" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-6" data-ng-show="ErrorModel.IsTreatment==true">
                                            <span>{{ErrorMessage}}</span>
                                        </div>
                                        <br />
                                        <div class="col-md-12">
                                            <div class="col-md-6">
                                                <div class="form-group">
                                                    <div class="form-line">
                                                        <input type="text" placeholder="Procedures" data-ng-model="TreatmentModel.Procedures"
                                                            maxlength="200" minlength="2" class="form-control setMargin" />
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <br />
                                    </div>
                                    <hr />
                                    <div class="col-md-3">
                                        <button type="button" class="btn btn-primary" data-ng-click="Save(true)" data-ng-show="Add==true">
                                            Save</button>
                                        <button type="button" class="btn btn-primary" data-ng-click="Save(false)" data-ng-show="Edit==true">
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
