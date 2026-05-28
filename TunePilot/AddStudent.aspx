<%@ Page Title="Add Student" Language="C#" MasterPageFile="~/navbar.Master" AutoEventWireup="true" CodeBehind="AddStudent.aspx.cs" Inherits="TunePilot.AddStudent" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="admin-page-wrapper">
        <div class="admin-card">

            <div class="page-title"><asp:Label ID="pageTitlelbl" runat="server" Text="Add Student" Font-Bold="true" Font-Size="XX-Large" Font-Names="Plus Jakarta Sans"></asp:Label></div>

            <asp:Button ID="backbtn" runat="server" Text="Back to Dashboard" OnClick="backbtn_Click" CssClass="back-link" />

            <asp:Label ID="messagelbl" runat="server" Visible="false" ForeColor="Red" CssClass="message-label"></asp:Label>

            <div class="form-group">
                <label>First Name</label>
                <asp:TextBox ID="firstNametb" runat="server" CssClass="form-control" Width="100%"></asp:TextBox>
            </div>

            <div class="form-group">
                <label>Last Name</label>
                <asp:TextBox ID="lastNametb" runat="server" CssClass="form-control" Width="100%"></asp:TextBox>
            </div>

            <div class="form-group">
                <label>Username</label>
                <asp:TextBox ID="usernametb" runat="server" CssClass="form-control" Width="100%"></asp:TextBox>
            </div>

            <div class="form-group">
                <label>Email</label>
                <asp:TextBox ID="emailtb" runat="server" CssClass="form-control" Width="100%" TextMode="Email"></asp:TextBox>
            </div>

            <div class="form-group">
                <label>Password</label>
                <asp:TextBox ID="passwordtb" runat="server" CssClass="form-control" Width="100%" TextMode="Password"></asp:TextBox>
            </div>

            <div class="btn-row">
                <asp:Button ID="savebtn" runat="server" Text="Save Student" OnClick="savebtn_Click" CssClass="btn-primary" />
            </div>

        </div>
    </div>
</asp:Content>
