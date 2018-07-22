<%@ Page Title="" Language="C#" MasterPageFile="~/Dashboard.Master" AutoEventWireup="true" CodeBehind="DailyCollection.aspx.cs" Inherits="Hospital.PathalogyReport.DailyCollection" %>
<asp:Content ID="Content" ContentPlaceHolderID="head" runat="server">
    <script>
        var doctorslist=<%=new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(GetDoctorsList()) %>
        var data=<%=new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(GetDailyCollection()) %>
    </script>
    <script type="text/javascript">
        function Print() {
            w = window.open(null, 'Print_Page', 'scrollbars=yes;');
            var myStyle = '<link rel="stylesheet" href="../css/bootstrap/bootstrap.min.css" />';
            if ($("#home").hasClass("active")) {
                w.document.write(myStyle + $('#home').html());
            }
            else {
                w.document.write(myStyle + $('#menu1').html());
            }
            w.document.close();
            setTimeout(function () {
                w.print();
            }, 500);
            return false;
        }
    </script>
</asp:Content>
<asp:Content ID="ContentPlaceHolder1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
  <div class="container-fluid">  
    <div class="row clearfix" data-ng-app="HospitalApp" data-ng-controller="DailyCollectionController">
     <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 card">
        <ul class="nav nav-tabs">
            <li class="active"><a data-toggle="tab" href="#home"><h4>OPD Collection</h4></a></li>
            <li><a data-toggle="tab" href="#menu1"><h4>IPD Collection</h4></a></li>
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
                    <button type="button" class="btn btn-primary" data-ng-click="FilterDate()">Search</button>
                    <button type="button" class="btn btn-primary" data-ng-click="Reset()">Reset</button>
                    <button type="button" class="btn btn-primary" onclick="Print()">Print</button>
                </div>
            </div>
            <div id="home" class="tab-pane fade in active">
                <hr />
                <div class="col-md-12">
                    <table class="table table-bordered">
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
                        <tbody>
                            <tr data-ng-repeat="opd in ListOPDCollection">
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
                <div class="col-md-12"><span class="pull-right" style="margin-right:100px;"><h3>Total : {{TotalAmount}}</h3></span></div>
            </div>
            <div id="menu1" class="tab-pane fade">
              <hr />
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
                            <tr data-ng-repeat="opd in ListIPDCollection">
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
                <div class="col-md-12"><span class="pull-right" style="margin-right:100px;"><h3>Total : {{TotalAmountIPD}}</h3></span></div>
            </div>
        </div>
    </div>
     </div>
      </div>
    <script type="text/javascript" src="../Scripts/Bootstrap/DailyCollection.js"></script>
</asp:Content>

