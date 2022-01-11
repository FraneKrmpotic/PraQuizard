<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PrijavaPostoječegKorisnika.aspx.cs" UnobtrusiveValidationMode="None" Inherits="PRA.WEBFORME.PrijavaPostoječegKorisnika" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
    <link href="../../Content/bootstrap.css" rel="stylesheet" />
<head runat="server">
    <title></title>
</head>
<body>
    <h1 style="text-align:center;">Prijavi se</h1>
    <form id="form1" runat="server" >
    <div class="container"  style="width:50%; align-items:center">
        <div class="form-group">
            <asp:Label id="errorSpan" runat="server"></asp:Label>
            <br />
            <asp:Label ID="lbEmail" runat="server" Text="Email"></asp:Label>
            <asp:TextBox ID="txtEmail" CssClass="form-control" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="Val3" ControlToValidate="txtEmail" runat="server" ErrorMessage="Email je obvezan"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="ValEmail" runat="server" ErrorMessage="Unesite ispravnu e-mail adresu!" ControlToValidate="txtEmail" SetFocusOnError="True" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ></asp:RegularExpressionValidator>
        </div> 
        <div class="form-group">
            <asp:Label ID="lbLozinka" runat="server" Text="Lozinka"></asp:Label>
            <asp:TextBox ID="txtLozinka" TextMode="Password" CssClass="form-control" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="Val4" ControlToValidate="txtLozinka" runat="server" ErrorMessage="Lozinka je obvezna"></asp:RequiredFieldValidator>
        </div>
        <div class="form-group">
            <asp:Button ID="btnOdustani" CssClass="btn btn-danger" runat="server" text="Odustani" OnClientClick="JavaScript:window.history.back(1);return false;" />
            <asp:Button ID="btnPrijavi" style="margin-left:668px;" CssClass="btn btn-primary" runat="server" Text="Prijavi se" OnClick="btnPrijavi_Click" />
        </div>
    </div>
    </form>
</body>
</html>
