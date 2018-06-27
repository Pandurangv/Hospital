<%@ Page Title="" Language="C#" MasterPageFile="~/mstAdmin.Master" AutoEventWireup="true" CodeBehind="frmLogout.aspx.cs" Inherits="Hospital.frmLogout" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript" language="javascript">
        window.history.forward(1);
        document.attachEvent("onkeydown", my_onkeydown_handler);
        function my_onkeydown_handler() {
            switch (event.keyCode) {
                case 116: // F5;
                    event.returnValue = false;
                    event.keyCode = 0;
                    window.status = "We have disabled F5";
                    break;
            }
        }

        function ClearHistory() {
            var backlen = history.length;
            history.go(-backlen);
            window.location.href = loggedOutPageUrl
        }

        function preventBack() { window.history.forward(); }
        setTimeout("preventBack()", 0);
        window.onunload = function () { null };

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div>
        <table width="100%">
            <tr>
                <td>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>

