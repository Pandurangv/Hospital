<%@ Page Title="" Language="C#" MasterPageFile="~/Dashboard.Master" AutoEventWireup="true" CodeBehind="DoctorWiseCollection.aspx.cs" Inherits="Hospital.PathalogyReport.DoctorWiseCollection" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
@media print {
.col-sm-1, .col-sm-2, .col-sm-3, .col-sm-4, .col-sm-5, .col-sm-6, .col-sm-7, .col-sm-8, .col-sm-9, .col-sm-10, .col-sm-11, .col-sm-12 {
  float: left;
}
.col-sm-12 {
  width: 100%;
}
.col-sm-11 {
  width: 91.66666666666666%;
}
.col-sm-10 {
  width: 83.33333333333334%;
}
.col-sm-9 {
  width: 75%;
}
.col-sm-8 {
  width: 66.66666666666666%;
}
.col-sm-7 {
  width: 58.333333333333336%;
}
.col-sm-6 {
  width: 50%;
}
.col-sm-5 {
  width: 41.66666666666667%;
}
.col-sm-4 {
  width: 33.33333333333333%;
 }
 .col-sm-3 {
   width: 25%;
 }
 .col-sm-2 {
   width: 16.666666666666664%;
 }
 .col-sm-1 {
  width: 8.333333333333332%;
 }
 .col-md-12.marign_bottom_50 {
    margin-bottom: 35px !important;
    margin-top: 35px !important;
}
  }
        .right_side
        {
            text-align: right;
            margin-top: 15px;
        }
        .left_side
        {
            margin-top: 15px;
        }
        .title
        {
            color: #cc0000;
            text-align: left;
        }
       .dr_class > p, .left_side > h3
       {
              text-align: left;
        }
    .close_class 
    {
      text-align: left;
     }
.reg > h1 {
    padding-left: 100px;
    text-align: left;
   font-size: 24px;
}
.center_tag_line {
    font-size: 16px;
    font-weight: bold;
}
.date > strong {
    font-size: 16px;
    font-weight: bold;
    padding-right: 150px;
}
.col-md-12.marign_bottom_50 {
    margin-bottom: 35px!important;
    margin-top: 35px!important;
}
.col-md-12.marign_bottom_35 {
    margin-bottom: 35px;
}
.midicine_name {
    background-color: #b9b9b9;
    text-align: center;
}
.table > thead > tr > th {
    border-bottom: 2px solid #ddd;
    vertical-align: middle;
}
.spanslogan {
  color: #222;
    font-size: 18px;
    font-weight: bold;
    margin: 0;
}
.dr_class > p,.right_side > p
{
     margin: 0 0 5px;
}
.tagline {
    -moz-border-bottom-colors: none;
    -moz-border-left-colors: none;
    -moz-border-right-colors: none;
    -moz-border-top-colors: none;
    border-color: #333 currentcolor;
    border-image: none;
    border-style: solid none;
    border-width: 1px medium;
    padding-bottom: 5px;
    padding-top: 5px;
}
.midicine_name {
    background-color: #1F91F3;
}
.reg > p {
    font-size: 18px;
    padding-top: 10px;
}
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
        <div class="col-md-12">
            <button type="button" class="btn btn-Default pull-left" onclick="PrintPanel()">
                Print</button>
        </div>
    </div>
    <div id="panel" class="row card" data-ng-app="HospitalApp" data-ng-controller="DoctorWiseCollectionController">
        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
            <div class="col-md-6  col-sm-6  col-xs-12">
                <div class="left_side">
                    <h4 class="title">
                        <strong>GANGA NURSING HOME</strong>
                    </h4>
                    <p>
                        <strong>DR.RAJESH TAPADIA</strong></p>
                    <div class="dr_class">
                        <p>
                            M.B.B.S M.D (MEDICINE)</p>
                        <p>
                            CONSULING PHYSICIAN</p>
                        <p>
                            <strong>Red.No. 46039</strong></p>
                        <p>
                            <strong>Time</strong>: 8.30 AM TO 12.30 PM &<br />
                            6.00 PM TO 9.00 PM
                        </p>
                    </div>
                </div>
            </div>
            <div class="col-md-6 col-sm-6  col-xs-12">
                <div class="right_side">
                    <p class="par_title">
                        NEXT TO PASSPORT OFFICE,MUNDHWA ROAD,PUNE - 411036
                        <br />
                        Tel.:26 88 19 90 /26 88 09 90
                    </p>
                    <p>
                        <strong>DR. GAYATRI R. TAPADIA</strong></p>
                    <p>
                        M.D (OBST. & GYNAEC)</p>
                    <p>
                        CONSULING OBSTETRICIAN & GYNACOLOGIST</p>
                    <p>
                        <strong>Red.No. 50505</strong></p>
                    <p>
                        <strong>Time</strong>: 7.00 PM TO 9.00 PM</p>
                </div>
            </div>
        </div>
        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 ">
            <center>
                <strong>(Sunday Closed)</strong></p></center>
        </div>
        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 tagline">
            <center>
                <strong>Panel Consultant : Columbia Asia, jahangir,inlaks,Ruby Hall</strong></center>
        </div>
        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
            <div class="col-md-6 col-sm-6 col-xs-12">
                <div class="reg">
                    <p>
                        Rx</p>
                </div>
            </div>
            <div class="col-md-6 col-sm-6 col-xs-12">
                <div class="right_side pull-right">
                    <p class="date">
                        <strong>Date:
                            <%=string.Format("{0:dd-MMM-yyyy}",DateTime.Now.Date) %>
                        </strong>
                    </p>
                </div>
            </div>
        </div>
        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 card">
            <ul class="nav nav-tabs">
                <li class="active" id="lidoctors"><a data-toggle="tab" href="#home">
                    <h4>
                        Doctor Names</h4>
                </a></li>
                <li id="liBills"><a data-toggle="tab" href="#menu1">
                    <h4>
                        {{SelectedDoctor.FullName}}</h4>
                </a></li>
            </ul>
            <div class="tab-content">
                <div id="home" class="tab-pane fade in active">
                <hr />
                    <table class="table table-bordered">
                        <thead>
                            <tr>
                                <th>
                                    Doctor Name
                                </th>
                                <th>
                                    Show Collection
                                </th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr data-ng-repeat="doctor in doctorlist">
                                <td>
                                    {{doctor.FullName}}
                                </td>
                                <td>
                                    <button type="button" class="btn btn-Primary" data-ng-click="ShowDoctorCollection(doctor)">
                                        Show</button>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </div>

                <div id="menu1" class="tab-pane fade">
                    <table class="table table-bordered">
                        <thead>
                            <tr>
                                <th>
                                    Patient Name
                                </th>
                                <th>
                                    Admit Date
                                </th>
                                <th>
                                    Amount
                                </th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr data-ng-repeat="invoice in PatientInvoiceList">
                                <td>
                                    {{invoice.Name}}
                                </td>
                                <td>
                                    {{invoice.AdmitDate | mydate | date : 'yyyy-MM-dd'}}
                                </td>
                                <td>
                                    {{invoice.NetAmount}}
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </div>
            </div>
        </div>
    </div>
    <script type="text/javascript" src="../Scripts/Bootstrap/DoctorWiseCollectionController.js"></script>
</asp:Content>
