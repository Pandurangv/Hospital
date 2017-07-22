<%@ Page Title="" Language="C#" MasterPageFile="~/Dashboard.Master" AutoEventWireup="true" CodeBehind="DailyCollection.aspx.cs" Inherits="Hospital.PathalogyReport.DailyCollection" %>
<asp:Content ID="Content" ContentPlaceHolderID="head" runat="server">
    <script>
        var doctorslist=<%=new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(GetDoctorsList()) %>
        var data=<%=new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(GetDailyCollection()) %>
    </script>
    
</asp:Content>
<asp:Content ID="ContentPlaceHolder1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    <div data-ng-app="HospitalApp" data-ng-controller="DailyCollectionController" class="row">
        <ul class="nav nav-tabs">
            <li class="active"><a data-toggle="tab" href="#home">OPD Collection</a></li>
            <li><a data-toggle="tab" href="#menu1">IPD Collection</a></li>
        </ul>
        <div class="tab-content">
            <div class="col-md-12">
                <div class="col-md-4">
                    <select id="ddldoctor" class="form-control" data-ng-click="SelectDoctor()">
                    </select>
                </div>
                <div class="col-md-3">
                    <input type="text" id="txtFromdate" class="form-control" placeholder="From date"/>
                </div>
                 <div class="col-md-3">
                    <input type="text" id="txtTODate" class="form-control" placeholder="To date"/>
                </div>
                <div class="col-md-2">
                    <button type="button" class="btn btn-primary" data-ng-click="Reset()">Reset</button>
                </div>
            </div>
            <div id="home" class="tab-pane fade in active">
                <h3>OPD Collection</h3>
                <div class="col-md-12">
                    <table class="table table-bordered">
                        <thead>
                            <tr>
                                <th>
                                    Patient Id
                                </th>
                                <th>
                                    Full Name
                                </th>
                                <th>
                                    Admit Date
                                </th>
                                <th>
                                    Bill No
                                </th>
                                <th>
                                    Bill Date
                                </th>
                                <th>
                                    Net Amount
                                </th>
                                <th>
                                    Amount
                                </th>
                                <th>
                                    Discount
                                </th>
                                <th>
                                    Doctor Name
                                </th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr data-ng-repeat="opd in SearchOPDCollectionList">
                                <td>
                                    {{opd.PatientId}}
                                </td>
                                <td>
                                    {{opd.FullName}}
                                </td>
                                <td>
                                    {{opd.AdmitDate | mydate | date: 'yyyy-MM-dd'}}
                                </td>
                                <td>
                                    {{opd.BillNo}}
                                </td>
                                <td>
                                    {{opd.BillDate | mydate | date: 'yyyy-MM-dd'}}
                                </td>
                                <td>
                                    {{opd.NetAmount}}
                                </td>
                                <td>
                                    {{opd.Amount}}
                                </td>
                                <td>
                                    {{opd.Discount}}
                                </td>
                                <td>
                                    {{opd.DoctorName}}
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </div>
                <div class="pull-left" style="margin-left:10px">
                    <ul class="pagination">
                        <li data-ng-click="First()"><a href="#"><span><</span></a></li>
                        <li data-ng-click="Prev()"><a href="#"><span><<</span></a></li>
                        <li data-ng-click="Next()"><a href="#"><span>>></span></a></li>
                        <li data-ng-click="Last()"><a href="#"><span>></span></a></li>
                    </ul>
                </div>
                
            </div>
            <div id="menu1" class="tab-pane fade">
                <h3>IPD Collection</h3>
                <div class="col-md-12">
                    <table class="table table-bordered">
                        <thead>
                            <tr>
                                <th>
                                    Patient Id
                                </th>
                                <th>
                                    Full Name
                                </th>
                                <th>
                                    Admit Date
                                </th>
                                <th>
                                    Bill No
                                </th>
                                <th>
                                    Bill Date
                                </th>
                                <th>
                                    Net Amount
                                </th>
                                <th>
                                    Amount
                                </th>
                                <th>
                                    Discount
                                </th>
                                <th>
                                    Doctor Name
                                </th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr data-ng-repeat="opd in SearchIPDCollectionList">
                                <td>
                                    {{opd.PatientId}}
                                </td>
                                <td>
                                    {{opd.FullName}}
                                </td>
                                <td>
                                    {{opd.AdmitDate | mydate | date: 'yyyy-MM-dd'}}
                                </td>
                                <td>
                                    {{opd.BillNo}}
                                </td>
                                <td>
                                    {{opd.BillDate | mydate | date: 'yyyy-MM-dd'}}
                                </td>
                                <td>
                                    {{opd.NetAmount}}
                                </td>
                                <td>
                                    {{opd.Amount}}
                                </td>
                                <td>
                                    {{opd.Discount}}
                                </td>
                                <td>
                                    {{opd.DoctorName}}
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </div>
                <div class="pull-left" style="margin-left:10px">
                    <ul class="pagination">
                        <li data-ng-click="FirstIPD()"><a href="#"><span><</span></a></li>
                        <li data-ng-click="PrevIPD()"><a href="#"><span><<</span></a></li>
                        <li data-ng-click="NextIPD()"><a href="#"><span>>></span></a></li>
                        <li data-ng-click="LastIPD()"><a href="#"><span>></span></a></li>
                    </ul>
                </div>
            </div>
        </div>
    </div>
    <script type="text/javascript" src="../Scripts/Bootstrap/DailyCollection.js"></script>
</asp:Content>
