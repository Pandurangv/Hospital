﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Dashboard.Master" AutoEventWireup="true" CodeBehind="PresProducts.aspx.cs" Inherits="Hospital.Store.PresProducts" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="container-fluid">
    <div class="row clearfix" data-ng-app="HospitalApp" data-ng-controller="ProductController">
    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
     <div class="card">
    <div class="header">
      <h2>Product Master</h2>
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
                    Product Name
                </th>
                <th>
                    Product Type
                </th>
                <th>
                    UOM
                </th>
                <th>
                    Price
                </th>
                <th>
                    Content
                </th>
                <th>
                    Action
                </th>
            </tr>
            <tbody>
            <tr data-ng-repeat="accounttype in SearchProductList">
                    <td>
                    {{accounttype.ProductName}}
                </td>
                <td>
                    {{accounttype.ProductTyepe}}
                </td>
                <td>
                    {{accounttype.UOM}}
                </td>
                <td>
                    {{accounttype.Price}}
                </td>
                <td>
                    {{accounttype.ProductContent}}
                </td>
                <td>
                    <button type="button" class="btn btn-primary" data-ng-click="EditClick(accounttype)">Edit</button>
                </td>
            </tr>
            </tbody>
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
                <input type="hidden" data-ng-model="ProductModel.ProductId" />
                <div class="col-md-12">
                    <div class="col-md-6">
                    <div class="form-group">
                       <div class="form-line">
                        <select  class="form-control setMargin" id="ddlPType">
                        </select>
                         <%--<select class="form-control setMargin" name="repeatSelect" id="repeatSelect" ng-model="SelectedProductType">
                            <option ng-repeat="ptype in ProductTypeList" value="{{ptype.ProductTypeId}}">{{ptype.ProductType}}</option>
                        </select>--%>
                    </div>
                    </div>
                    <div class="col-md-6" data-ng-show="ErrorModel.IsProductType==true">
                        <span>{{ErrorMessage}}</span>
                    </div>
                </div>
                <br />
                <div class="col-md-12">
                    <div class="col-md-6">
                     <div class="form-group">
                       <div class="form-line">
                        <input type="text" placeholder="Product Name*" maxlength="50" minlength="2" data-ng-model="ProductModel.ProductName"
                               class="form-control setMargin" />
                    </div>
                    </div>
                    </div>
                    <div class="col-md-6" data-ng-show="ErrorModel.IsProductName==true">
                        <span>{{ErrorMessage}}</span>
                    </div>
                </div>
                <br />
                <div class="col-md-12">
                    <div class="col-md-6">
                     <div class="form-group">
                       <div class="form-line">
                        <input type="text" placeholder="Content" data-ng-model="ProductModel.Content" maxlength="50" minlength="2" 
                               class="form-control setMargin" />
                    </div>
                     </div>
                    </div>
                </div>
                <br />
                <div class="col-md-12">
                    <div class="col-md-6">
                    <div class="form-group">
                       <div class="form-line">
                        <input type="text" placeholder="UOM" data-ng-model="ProductModel.UOM" maxlength="50" minlength="2" 
                               class="form-control setMargin" />
                    </div>
                     </div>
                    </div>
                    <div class="col-md-6" data-ng-show="ErrorModel.IsUOM==true">
                        <span>{{ErrorMessage}}</span>
                    </div>
                </div>
                <br />
                <div class="col-md-12">
                    <div class="col-md-6">
                     <div class="form-group">
                       <div class="form-line">
                        <input type="text" placeholder="SUBUOM" data-ng-model="ProductModel.SubUOM" maxlength="50" minlength="2" 
                               class="form-control setMargin" />
                    </div>
                </div>
                 </div>
                <br />
                <div class="col-md-12">
                    <div class="col-md-6">
                     <div class="form-group">
                       <div class="form-line">
                        <input type="text" placeholder="Price" data-ng-model="ProductModel.Price" maxlength="50" minlength="2" 
                               class="form-control setMargin" />
                    </div>
                     </div>
                </div>
                    <div class="col-md-6" data-ng-show="ErrorModel.IsPrice==true">
                        <span>{{ErrorMessage}}</span>
                    </div>
                </div>
                <br />
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
<script type="text/javascript" src="../Scripts/Bootstrap/ProductController.js"></script>
</asp:Content>
