<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="TunePilot.login" MasterPageFile="~/navbar.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>TunePilot - Login</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="login-wrapper">
        <div class="login-box">
            <h2>Welcome Back</h2>
            <asp:Label ID="lblMessage" runat="server" Visible="false"></asp:Label>
            <div class="form-group">
                <label>Email / Username</label>
                <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" placeholder="Enter your email or username" />
                <asp:RequiredFieldValidator ID="rfvEmail" runat="server" ControlToValidate="txtEmail" ErrorMessage="Required." Display="Dynamic" />
            </div>
            <div class="form-group">
                <label>Password</label>
                <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" CssClass="form-control" placeholder="Enter your password" />
                <asp:RequiredFieldValidator ID="rfvPassword" runat="server" ControlToValidate="txtPassword" ErrorMessage="Required." Display="Dynamic" />
            </div>
            <asp:Button ID="btnLogin" runat="server" Text="Login" CssClass="btn-primary" OnClick="btnLogin_Click" />
            <div class="login-links">
                <a href="register.aspx">Don't have an account? Sign Up</a>
            </div>
        </div>
    </div>
</asp:Content>