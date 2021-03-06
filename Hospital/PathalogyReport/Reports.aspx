﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Dashboard.Master" AutoEventWireup="true"
    CodeBehind="Reports.aspx.cs" Inherits="Hospital.PathalogyReport.Reports" %>

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
 .ng-hide {
    display: none !important;
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
.hrMargin
{
    margin:0 !important;
    }
    </style>
    <script>
        var reportType="<%=GetReportType() %>"
        var data=<%=GetReportData() %>
    </script>
    <script type="text/javascript">
        function PrintPanel() {
            w = window.open(null, 'Print_Page', 'scrollbars=yes;');
            var myStyle = '<link rel="stylesheet" href="../css/bootstrap/bootstrap.min.css" />';
            if (reportType == "Prescription") {
                w.document.write(myStyle + $('#panel').html());
            }
            else if (reportType == "DoctorTreatmentChart") {
                w.document.write(myStyle + $('#divDoctorTreatmentChart').html());
            }
            else if (reportType == "OTMedicinBill" || reportType == "PatientInvoice") {
                w.document.write(myStyle + $('#OTMedicinBill').html());
            }
            w.document.close();
            setTimeout(function () {
                w.print();
            }, 500);
            return false;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<div class="container-fluid" data-ng-app="HospitalApp" data-ng-controller="ReportsController"> 
    <div class="row">
        <div class="col-md-12">
            <button type="button" class="btn btn-Default pull-left" onclick="PrintPanel()">
                Print</button>
        </div>
    </div>
    <div id="panel" class="row card" data-ng-show="ReportType=='Prescription'">
        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
            <div class="col-md-6  col-sm-6  col-xs-12">
                <div class="left_side">
                    <h4 class="title">
                        <strong><%=GetGlobalResourceObject("BrandDetails", "HospitalName")%></strong>
                    </h4>
                    <p>
                        <strong><%=GetGlobalResourceObject("BrandDetails", "DR1")%></strong>
                        </p>
                    <div class="dr_class">
                        <p>
                            <%=GetGlobalResourceObject("BrandDetails", "DR1Q")%>
                            </p>
                        <p>
                            <%=GetGlobalResourceObject("BrandDetails", "DR1Q1")%>
                            </p>
                        <p>
                            <strong><%=GetGlobalResourceObject("BrandDetails", "DR1REGNO")%></strong></p>
                        <p>
                            <%=GetGlobalResourceObject("BrandDetails", "DR1Time")%>
                        </p>
                    </div>
                </div>
            </div>
            <div class="col-md-6 col-sm-6  col-xs-12">
                <div class="right_side">
                    <p class="par_title">
                        <%=GetGlobalResourceObject("BrandDetails", "Address")%>
                        <br />
                        <%=GetGlobalResourceObject("BrandDetails", "Address1")%>
                    </p>
                    <p>
                        <strong><%=GetGlobalResourceObject("BrandDetails", "DR2")%></strong></p>
                    <p>
                        <%=GetGlobalResourceObject("BrandDetails", "DR2Q")%></p>
                    <p>
                        <%=GetGlobalResourceObject("BrandDetails", "DR2Q2")%></p>
                    <p>
                        <strong><%=GetGlobalResourceObject("BrandDetails", "DR2REGNO")%></strong></p>
                    <p>
                        <%=GetGlobalResourceObject("BrandDetails", "DR2Time")%>
                        </p>
                </div>
            </div>
        </div>
        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 ">
            <center>
                <strong><%=GetGlobalResourceObject("BrandDetails", "Instruction")%></strong></p></center>
        </div>
        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 tagline">
            <center>
                <strong><%=GetGlobalResourceObject("BrandDetails", "Information")%></strong></center>
        </div>
        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
            <div class="col-md-6 col-sm-6 col-xs-12">
                <div class="reg">
                    <p>
                        Rx</p>
                </div>
            </div>
            <div class="col-md-6 col-sm-6 col-xs-12">
                <div class="right_side">
                    <p class="date">
                        <strong>Date:
                            <%=string.Format("{0:dd-MMM-yyyy}",DateTime.Now.Date) %>
                        </strong>
                    </p>
                </div>
            </div>
        </div>
        <div id="content">
            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 tagline">
                <center>
                    <strong>Prescription Report</strong></center>
            </div>
            <div class="row marign_bottom_50">
            <div class="col-md-12">
                <div class="col-md-6">
                    <div>
                        <span class="pull-left patient_name"><strong>Patient Name </strong>: {{PatientInfo.FullName}}</span></div>
                    <br />
                </div>
                <div class="col-md-6 pull-right">
                        <div>
                            <span class="pull-right"><strong>MRN</strong> : {{PatientInfo.PatientCode}}</span></div>
                        <br />
                    </div>
            </div>
            </div>
            <div class="row">
                <div class="col-md-12">
                <div class="col-md-6">
                    
                </div>
                <div class="col-md-6 pull-right">
                        <div>
                            <span class="pull-right"><strong>Doctor</strong> : {{PatientInfo.EmpName}}</span></div>
                        <br />
                    </div>
           </div>
            </div>
            
           <div class="row">
            <div class="col-md-12">
                <div class="col-md-6">
                    <div id="dressingCheck">
                        <span class="pull-left patient_name"><strong>Dressing</strong>: {{PatientInfo.IsDressing==true?'Done':'Not
                            Done'}}</span></div>
                    <br />
                </div>
                <div class="col-md-6 pull-right" id="injectionCheck">
                        <div>
                            <span class="pull-right"><strong>Injection</strong> : {{PatientInfo.IsInjection==true?'Done':'Not
                                Done'}}</span></div>
                        <br />
                    </div>
            </div>
           </div>
           
            <div class="col-md-12">
                <table class="table table-bordered">
                    <thead>
                        <tr>
                            <th class="midicine_name" rowspan="2">
                                Medicine Name
                            </th>
                            <th class="midicine_name" colspan="4">
                                <span>Medicine Timing</span>
                            </th>
                            <th class="midicine_name" rowspan="2">
                                No Of Days
                            </th>
                            <th class="midicine_name" rowspan="2">
                                Quantity
                            </th>
                        </tr>
                        <tr>
                            <th class="midicine_name" style="width: 15%">
                            </th>
                            <th class="midicine_name" style="width: 15%">
                                {{LanTypeValues.Morning}}
                            </th>
                            <th class="midicine_name" style="width: 15%">
                                {{LanTypeValues.Afternoon}}
                            </th>
                            <th class="midicine_name" style="width: 15%">
                                {{LanTypeValues.Evening}}
                            </th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr data-ng-repeat="tabs in data">
                            <td>
                                {{tabs.ProductName}}
                                <br />
                                {{tabs.ProductContent}}
                            </td>
                            <td>
                                {{tabs.IsbeforeLunch==true?LanTypeValues.BeforeLunch:LanTypeValues.AfterLunch}}
                            </td>
                            <td>
                                {{tabs.Morning}}
                            </td>
                            <td>
                                {{tabs.Afternoon}}
                            </td>
                            <td>
                                {{tabs.Night}}
                            </td>
                            <td>
                                {{tabs.NoOfDays}}
                            </td>
                            <td>
                                {{tabs.Quantity}}
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
            <div class="col-md-12 marign_bottom_35">
                <div class="col-md-6">
                    <div>
                        <span class="pull-left"><strong>Instructions </strong>: {{PatientInfo.Remarks}}</span></div>
                    <br />
                </div>
            </div>
        </div>
    </div>
    <div id="divDoctorTreatmentChart" class="row card" data-ng-show="ReportType=='DoctorTreatmentChart'">
        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
            <div class="col-md-6  col-sm-6  col-xs-12">
                <div class="left_side">
                    <h4 class="title">
                        <strong><%=GetGlobalResourceObject("BrandDetails", "HospitalName")%></strong>
                    </h4>
                    <p>
                        <strong><%=GetGlobalResourceObject("BrandDetails", "DR1")%></strong>
                        </p>
                    <div class="dr_class">
                        <p>
                            <%=GetGlobalResourceObject("BrandDetails", "DR1Q")%>
                            </p>
                        <p>
                            <%=GetGlobalResourceObject("BrandDetails", "DR1Q1")%>
                            </p>
                        <p>
                            <strong><%=GetGlobalResourceObject("BrandDetails", "DR1REGNO")%></strong></p>
                        <p>
                            <%=GetGlobalResourceObject("BrandDetails", "DR1Time")%>
                        </p>
                    </div>
                </div>
            </div>
            <div class="col-md-6 col-sm-6  col-xs-12">
                <div class="right_side">
                    <p class="par_title">
                        <%=GetGlobalResourceObject("BrandDetails", "Address")%>
                        <br />
                        <%=GetGlobalResourceObject("BrandDetails", "Address1")%>
                    </p>
                    <p>
                        <strong><%=GetGlobalResourceObject("BrandDetails", "DR2")%></strong></p>
                    <p>
                        <%=GetGlobalResourceObject("BrandDetails", "DR2Q")%></p>
                    <p>
                        <%=GetGlobalResourceObject("BrandDetails", "DR2Q2")%></p>
                    <p>
                        <strong><%=GetGlobalResourceObject("BrandDetails", "DR2REGNO")%></strong></p>
                    <p>
                        <%=GetGlobalResourceObject("BrandDetails", "DR2Time")%>
                        </p>
                </div>
            </div>
        </div>
        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 ">
            <center>
                <strong><%=GetGlobalResourceObject("BrandDetails", "Instruction")%></strong></p></center>
        </div>
        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 tagline">
            <center>
                <strong><%=GetGlobalResourceObject("BrandDetails", "Information")%></strong></center>
        </div>
        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
            <div class="col-md-6 col-sm-6 col-xs-12">
                <div class="reg">
                    <p>
                        Rx</p>
                </div>
            </div>
            <div class="col-md-6 col-sm-6 col-xs-12">
                <div class="right_side">
                    <p class="date">
                        <strong>Date:
                            <%=string.Format("{0:dd-MMM-yyyy}",DateTime.Now.Date) %>
                        </strong>
                    </p>
                </div>
            </div>
        </div>
        <div id="Div2">
            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 tagline">
                <center>
                    <strong>Doctor Treatment Report</strong></center>
            </div>
            <div class="col-md-12 marign_bottom_50">
                <div class="col-md-6">
                    <div>
                        <span class="pull-left patient_name"><strong>Patient Name </strong>: {{PatientInfo.PatientName}}</span></div>
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
                                Doctor
                            </th>
                            <th>
                                Treatment Date
                            </th>
                            <th>
                                Treatment Time
                            </th>
                            <th>
                                Treatment Details
                            </th>
                            <th>
                                Procedures
                            </th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr data-ng-repeat="treat in data.TreatmentList">
                            <td>
                                {{treat.EmployeeName}}
                            </td>
                            <td>
                                {{treat.Bill_Date | mydate | date : 'yyyy-MM-dd'}}
                            </td>
                            <td>
                                {{treat.TreatmentTime}}
                            </td>
                            <td>
                                {{treat.TreatmentDetails}}
                                <table>
                                    <tr data-ng-repeat="product in treat.ProductList">
                                        <td>
                                            {{product.ProductName}}
                                        </td>
                                        <td>
                                            {{product.MorningQty}}-{{product.AfterNoonQty}}-{{product.EveningQty}}-{{product.NightQty}}
                                        </td>
                                        <td>
                                            {{product.Quantity}}
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td>
                                {{treat.TreatmentPro}}
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
        </div>
    </div>
    <div id="OTMedicinBill" class="row card" data-ng-show="ReportType=='OTMedicinBill' || ReportType=='PatientInvoice'">
        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
            <div class="col-md-6  col-sm-6  col-xs-12">
                <div class="left_side">
                    <h4 class="title">
                        <strong><%=GetGlobalResourceObject("BrandDetails", "HospitalName")%></strong>
                    </h4>
                    <p>
                        <strong><%=GetGlobalResourceObject("BrandDetails", "DR1")%></strong>
                        </p>
                    <div class="dr_class">
                        <p>
                            <%=GetGlobalResourceObject("BrandDetails", "DR1Q")%>
                            </p>
                        <p>
                            <%=GetGlobalResourceObject("BrandDetails", "DR1Q1")%>
                            </p>
                        <p>
                            <strong><%=GetGlobalResourceObject("BrandDetails", "DR1REGNO")%></strong></p>
                        <p>
                            <%=GetGlobalResourceObject("BrandDetails", "DR1Time")%>
                        </p>
                    </div>
                </div>
            </div>
            <div class="col-md-6 col-sm-6  col-xs-12">
                <div class="right_side">
                    <p class="par_title">
                        <%=GetGlobalResourceObject("BrandDetails", "Address")%>
                        <br />
                        <%=GetGlobalResourceObject("BrandDetails", "Address1")%>
                    </p>
                    <p>
                        <strong><%=GetGlobalResourceObject("BrandDetails", "DR2")%></strong></p>
                    <p>
                        <%=GetGlobalResourceObject("BrandDetails", "DR2Q")%></p>
                    <p>
                        <%=GetGlobalResourceObject("BrandDetails", "DR2Q2")%></p>
                    <p>
                        <strong><%=GetGlobalResourceObject("BrandDetails", "DR2REGNO")%></strong></p>
                    <p>
                        <%=GetGlobalResourceObject("BrandDetails", "DR2Time")%>
                        </p>
                </div>
            </div>
        </div>
        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 ">
            <center>
                <strong><%=GetGlobalResourceObject("BrandDetails", "Instruction")%></strong></p></center>
        </div>
        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 tagline">
            <center>
                <strong><%=GetGlobalResourceObject("BrandDetails", "Information")%></strong></center>
        </div>
        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
            <div class="col-md-6 col-sm-6 col-xs-12">
                <div class="reg">
                    <p>
                        Rx</p>
                </div>
            </div>
            <div class="col-md-6 col-sm-6 col-xs-12">
                <div class="right_side">
                    <p class="date">
                        <strong>Date:
                            <%=string.Format("{0:dd-MMM-yyyy}",DateTime.Now.Date) %>
                        </strong>
                    </p>
                </div>
            </div>
        </div>
        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 tagline" id="divIPDBill">
                <hr class="hrMargin"/>
                <center>
                    <strong>IPD Medicine Bill</strong></center>
                    <hr class="hrMargin"/>
            </div>
            <div class="col-md-12 marign_bottom_50" id="divIPDBillDetail">
                <div class="row">
                    <div class="col-md-6">
                        <div>
                            <span class="pull-left patient_name"><strong>Bill No </strong>: {{PatientInfo.BillNo}}</span></div>
                    </div>
                    <div class="col-md-6">
                        <div class="col-md-6 pull-right">
                            <div>
                                <span class="pull-left"><strong>Bill Date</strong> : {{PatientInfo.Bill_Date | mydate
                                    | date : 'yyyy-MM-dd'}}</span></div>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-6">
                        <div>
                            <span class="pull-left patient_name"><strong>Patient Name </strong>: {{PatientInfo.PatientName}}</span></div>
                    </div>
                    <div class="col-md-6">
                        <div class="col-md-6 pull-right">
                            <div>
                                <span class="pull-left"><strong>MRN</strong> : {{PatientInfo.PatientCode}}</span></div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-md-12" id="divIPDBillDetail1">
                <table class="table table-bordered">
                    <thead>
                        <tr>
                            <th>
                                Poduct Name
                            </th>
                            <th>
                                Batch No
                            </th>
                            <th>
                                Expiry Date
                            </th>
                            <th>
                                Quantity
                            </th>
                            <th>
                                Price
                            </th>
                            <th>
                                Amount
                            </th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr data-ng-repeat="product in PatientInfo.ProductList">
                            <td>
                                {{product.ProductName}}
                            </td>
                            <td>
                                {{product.BatchNo}}
                            </td>
                            <td>
                                {{product.ExpiryDate | mydate | date : 'yyyy-MM-dd'}}
                            </td>
                            <td>
                                {{product.Quantity}}
                            </td>
                            <td>
                                {{product.Price}}
                            </td>
                            <td>
                                {{product.Amount}}
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
            
            <div class="col-md-12" id="divIPDBillDetail2">
                <div class="row">
                    <div class="col-md-6">
                        
                    </div>
                    <div class="col-md-6">
                        <div class="col-md-6 pull-right">
                            <div>
                                <span class="pull-left"><strong>Net Amount</strong> : {{PatientInfo.NetAmount}}</span></div>
                        </div>
                    </div>
                </div>
            </div>
            
            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 tagline" id="divInvoice">
                <hr class="hrMargin"/>
                <center>
                    <strong>Patient Invoice</strong></center>
                    <hr class="hrMargin"/>
            </div>

            <div class="col-md-12 marign_bottom_50" id="divInvoiceDtl">
                <div class="row">
                    <div class="col-md-12">
                        <div class="col-md-6">
                            <div>
                                <span class="pull-left patient_name"><strong>Invoice No </strong>: {{InvoiceDetails.BillNo}}</span></div>
                        </div>
                        <div class="col-md-6">
                            <div class="col-md-6 pull-right">
                                <div>
                                    <span class="pull-left"><strong>Invoice Date</strong> : {{InvoiceDetails.BillDate |
                                        mydate | date : 'yyyy-MM-dd'}}</span></div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12">
                        <div class="col-md-6">
                            <div>
                                <span class="pull-left patient_name"><strong>Patient Name </strong>: {{InvoiceDetails.FullName}}</span></div>
                        </div>
                        <div class="col-md-6">
                            <div class="col-md-6 pull-right">
                                <div>
                                    <span class="pull-left"><strong>MRN</strong> : {{InvoiceDetails.PatientCode}}</span></div>
                            </div>
                        </div>
                    </div>
                </div>
                
                <div class="row">
                    <div class="col-md-12">
                        <div class="col-md-6">
                            <div>
                                <span class="pull-left patient_name"><strong>Address </strong>: {{InvoiceDetails.Address}}</span></div>
                        </div>
                        <div class="col-md-6">
                            <div class="col-md-6 pull-right">
                                <div>
                                    <span class="pull-left"><strong>Room Type</strong> : {{InvoiceDetails.CategoryDesc}}</span></div>
                            </div>
                        </div>
                    </div>
                </div>
                
                <div class="col-md-12">
                    <table class="table table-bordered">
                        <thead>
                            <tr>
                                <th>
                                    Particulars
                                </th>
                                <th>
                                    Quantity
                                </th>
                                <th>
                                    No Of Days
                                </th>
                                <th>
                                    Charge
                                </th>
                                <th>
                                    Amount
                                </th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr data-ng-repeat="invoice in data.PatientInvoice">
                                <td>
                                    {{invoice.ChargesName}}
                                </td>
                                <td>
                                    {{invoice.Quantity}}
                                </td>
                                <td>
                                    {{invoice.NoOfDays}}
                                </td>
                                <td>
                                    {{invoice.ChargePerDay}}
                                </td>
                                <td>
                                    {{invoice.Amount}}
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </div>
                <div class="col-md-12">
                    <div class="row">
                        <div class="col-md-6">
                        </div>
                        <div class="col-md-6">
                            <div class="col-md-6 pull-right">
                                <div>
                                    <span class="pull-left"><strong>Total Amount</strong> : {{InvoiceDetails.Total}}</span></div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-md-12">
                    <div class="row">
                        <div class="col-md-6">
                        </div>
                        <div class="col-md-6">
                            <div class="col-md-6 pull-right">
                                <div>
                                    <span class="pull-left"><strong>Discount</strong> : {{InvoiceDetails.FixedDiscount}}</span></div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-md-12">
                    <div class="row">
                        <div class="col-md-6">
                        </div>
                        <div class="col-md-6">
                            <div class="col-md-6 pull-right">
                                <div>
                                    <span class="pull-left"><strong>Net Amount</strong> : {{InvoiceDetails.NetAmount}}</span></div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 tagline" id="divTestInoice1">
                <hr class="hrMargin" />
                <center>
                    <strong>Lab Test Invoice</strong></center>
                    <hr class="hrMargin"/>
            </div>

            <div class="col-md-12 marign_bottom_50" id="divTestInoice">
                <div class="row">
                    <div class="col-md-12">
                        <div class="row">
                            <div class="col-md-6">
                                <div>
                                    <span class="pull-left patient_name"><strong>Invoice No </strong>: {{LabTestInvoice.TestInvoiceNo}}</span></div>
                            </div>
                            <div class="col-md-6">
                                <div class="col-md-6 pull-right">
                                    <div>
                                        <span class="pull-left"><strong>Invoice Date</strong> : {{LabTestInvoice.TestInvoiceDate
                                            | mydate | date : 'yyyy-MM-dd'}}</span></div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12">
                        <div class="row">
                            <div class="col-md-6">
                                <div>
                                    <span class="pull-left patient_name"><strong>Patient Name </strong>: {{LabTestInvoice.PatientName}}</span></div>
                            </div>
                            <div class="col-md-6">
                                <div class="col-md-6 pull-right">
                                    <div>
                                        <span class="pull-left"><strong>MRN</strong> : {{LabTestInvoice.PatientCode}}</span></div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12">
                        <table class="table table-bordered">
                            <thead>
                                <tr>
                                    <th>
                                        Test Name
                                    </th>
                                    <th>
                                        Charges
                                    </th>
                                </tr>
                            </thead>
                            <tbody>
                                <tr data-ng-repeat="test in data.LabTestList">
                                    <td>
                                        {{test.TestName}}
                                    </td>
                                    <td>
                                        {{test.Charges}}
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                </div>
                
                <div class="col-md-12">
                    <div class="col-md-6">
                        <div>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="col-md-6 pull-right">
                            <div class="col-md-8 pull-left">
                                <span class="pull-left"><strong>Total Amount</strong> :</span>
                            </div>
                            <div class="col-md-4 pull-left">
                                <span class="pull-right"><strong>{{LabTestInvoice.Amount}}</strong></span>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
     </div>
    <script type="text/javascript" src="../Scripts/Bootstrap/ReportsController.js"></script>
</asp:Content>
