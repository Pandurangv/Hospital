﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Dashboard.Master" AutoEventWireup="true" CodeBehind="ProductType.aspx.cs" Inherits="Hospital.Store.ProductType" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="container-fluid">
<div class="row clearfix" data-ng-app="HospitalApp" data-ng-controller="ProductTypeController">
 <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
 <div class="card">
    <div class="header">
    <h2>Product Type</h2>
    </div>
    <div class="body">
    <div class="table-responsive" data-ng-show="Details==true">
        <div class="col-md-12">
            <div class="col-md-8 divsearch">
                <input type="text" placeholder="Search" class="form-control" data-ng-model="Prefix" data-ng-change="FilterList()" />
            </div>
            <div class="col-md-4">
                <button type="button" class="btn btn-primary" data-ng-click="FilterList()" data-toggle="tooltip" title="Search">Search</button>
                <button type="button" class="btn btn-primary" data-ng-click="AddNewUI()" data-toggle="tooltip" title="New">New</button>
                <button type="button" class="btn btn-primary" data-ng-click="Reset()" data-toggle="tooltip" title="Reset">Reset</button>
            </div>
            <hr />
        </div>
       <table class="table table-bordered">
            <tr>
                <th>
                    Product Type
                </th>
                <th>
                    Description
                </th>
                <th>
                    Action
                </th>
            </tr>
            <tr data-ng-repeat="accounttype in SearchProductTypeList">
                <td>
                    {{accounttype.ProductType}}
                </td>
                <td>
                    {{accounttype.Description}}
                </td>
                <td>
                    <button type="button" class="btn btn-primary" data-ng-click="EditClick(accounttype)" data-toggle="tooltip" title="Edit">Edit</button>
                </td>
            </tr>
        </table>

        <ul class="pagination">
            <li data-ng-click="First()"><a href="#"><span class="glyphicon glyphicon-step-backward"></span></a></li>
            <li data-ng-click="Prev()"><a href="#"><span class="glyphicon glyphicon-backward"></span></a></li>
            <li data-ng-click="Next()"><a href="#"><span class="glyphicon glyphicon-forward"></span></a></li>
            <li data-ng-click="Last()"><a href="#"><span class="glyphicon glyphicon-step-forward"></span></a></li>
        </ul>
    </div>

    <div data-ng-show="Add==true || Edit==true">
            <div class="form-group">
                <input type="hidden" id="producttypeid" />
                   <div class="col-md-12">
                       <div class="col-md-6">
                        <div class="form-group">
                       <div class="form-line">
                        <input type="text" id="ProductType" placeholder="Product Type*" maxlength="50" minlength="2"
                               class="form-control setMargin" />
                    </div>
                    </div>
</div>
                    <div class="col-md-6" data-ng-show="ErrorModel.IsSelectProductType==true">
                        <span>{{ErrorMessage}}</span>
                    </div>
                </div>
                <br />
                <div class="col-md-12">
                    <div class="col-md-6">
                     <div class="form-group">
                       <div class="form-line">
                        <input type="text" id="Description" placeholder="Description" maxlength="50" minlength="2" 
                               class="form-control setMargin" />
                    </div>
                </div>
                 </div>
                </div>
            </div>
            <hr />
            <div class="col-md-3">
                <button type="button" class="btn btn-primary" data-ng-click="Save(true)" data-ng-show="Add==true">Save</button>
                <button type="button" class="btn btn-primary" data-ng-click="Save(false)" data-ng-show="Edit==true">Update</button>
                <button type="button" class="btn btn-primary" data-ng-click="CancelClick()">Cancel</button>
            </div>
    </div>
</div>
</div>
</div>
</div>
</div>
</div>
<script type="text/javascript" src="../Scripts/Bootstrap/ProductTypeController.js"></script>
</asp:Content>