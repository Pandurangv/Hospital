<%@ Page Title="" Language="C#" MasterPageFile="~/Dashboard.Master" AutoEventWireup="true" CodeBehind="StoreProducts.aspx.cs" Inherits="Hospital.Store.StoreProducts" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row" data-ng-app="HospitalApp" data-ng-controller="ProductController">
    <h2>Product Master</h2>
    <div class="table-responsive" data-ng-show="Details==true">
        <div class="col-md-12">
            <div class="col-md-8 divsearch">
                <input type="text" placeholder="Search" class="form-control" data-ng-model="Prefix" data-ng-change="FilterList()" />
            </div>
            <div class="col-md-4">
                <button type="button" class="btn btn-primary" data-ng-click="FilterList()">Search</button>
                <button type="button" class="btn btn-primary" data-ng-click="AddNewUI()">New</button>
                <button type="button" class="btn btn-primary" data-ng-click="Reset()">Reset</button>
            </div>
            <hr />
        </div>

        <table class="table">
            <tr>
                <th>
                    Product Type
                </th>
                <th>
                    Description
                </th>
                <th>
                    Edit
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
                    <button type="button" class="btn btn-primary" data-ng-click="EditClick(accounttype)">Edit</button>
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
                        <%--<input type="text" placeholder="Product Type" data-ng-model="ProductModel.ProductTypeId" maxlength="50" minlength="2" 
                               class="form-control setMargin" />--%>
                        <select data-ng-options=""></select>
                    </div>
                </div>
                <br />
                <div class="col-md-12">
                    <div class="col-md-6">
                        <input type="text" placeholder="Product Name*" maxlength="50" minlength="2" data-ng-model="ProductModel.ProductName"
                               class="form-control setMargin" />
                    </div>
                    <div class="col-md-6" data-ng-show="ErrorModel.IsProductName==true">
                        <span>{{ErrorMessage}}</span>
                    </div>
                </div>
                <br />
                <div class="col-md-12">
                    <div class="col-md-6">
                        <input type="text" placeholder="Content" data-ng-model="ProductModel.Content" maxlength="50" minlength="2" 
                               class="form-control setMargin" />
                    </div>
                </div>
                <br />
                <div class="col-md-12">
                    <div class="col-md-6">
                        <input type="text" placeholder="UOM" data-ng-model="ProductModel.UOM" maxlength="50" minlength="2" 
                               class="form-control setMargin" />
                    </div>
                    <div class="col-md-6" data-ng-show="ErrorModel.IsUOM==true">
                        <span>{{ErrorMessage}}</span>
                    </div>
                </div>
                <br />
                <div class="col-md-12">
                    <div class="col-md-6">
                        <input type="text" placeholder="SUBUOM" data-ng-model="ProductModel.SubUOM" maxlength="50" minlength="2" 
                               class="form-control setMargin" />
                    </div>
                </div>
                <br />
                <div class="col-md-12">
                    <div class="col-md-6">
                        <input type="text" placeholder="SUBUOM" data-ng-model="ProductModel.Price" maxlength="50" minlength="2" 
                               class="form-control setMargin" />
                    </div>
                    <div class="col-md-6" data-ng-show="ErrorModel.IsPrice==true">
                        <span>{{ErrorMessage}}</span>
                    </div>
                </div>
                <br />
                
                
            </div>
            <hr />
            <br />
            <div class="col-md-3">
                <button type="button" class="btn btn-primary" data-ng-click="Save(true)" data-ng-show="Add==true">Save</button>
                <button type="button" class="btn btn-primary" data-ng-click="Save(false)" data-ng-show="Edit==true">Update</button>
                <button type="button" class="btn btn-primary" data-ng-click="CancelClick()">Cancel</button>
            </div>
    </div>
</div>
<script type="text/javascript" src="../Scripts/Bootstrap/ProductController.js"></script>
</asp:Content>
