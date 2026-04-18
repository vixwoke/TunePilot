<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="editprofile.aspx.cs" Inherits="TunePilot.editprofile" MasterPageFile="~/navbar.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>TunePilot - Edit Profile</title>
    <link rel="stylesheet" href="css/editprofile.css" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="profile-wrapper">
        <div class="profile-box">
            <h2>Edit Profile</h2>
            <p class="profile-subtitle">Update your account information</p>

            <asp:Label ID="lblMessage" runat="server" CssClass="message-label" Visible="false"></asp:Label>

            <div class="form-row">
                <div class="form-group">
                    <label>First Name</label>
                    <asp:TextBox ID="txtFirstName" runat="server" CssClass="form-control" placeholder="First name" />
                    <asp:RequiredFieldValidator ID="rfvFirstName" runat="server"
                        ControlToValidate="txtFirstName"
                        ErrorMessage="Required."
                        CssClass="error-msg" Display="Dynamic" />
                </div>
                <div class="form-group">
                    <label>Last Name</label>
                    <asp:TextBox ID="txtLastName" runat="server" CssClass="form-control" placeholder="Last name" />
                    <asp:RequiredFieldValidator ID="rfvLastName" runat="server"
                        ControlToValidate="txtLastName"
                        ErrorMessage="Required."
                        CssClass="error-msg" Display="Dynamic" />
                </div>
            </div>

            <div class="form-group">
                <label>Username</label>
                <asp:TextBox ID="txtUsername" runat="server" CssClass="form-control" placeholder="Username" />
                <asp:RequiredFieldValidator ID="rfvUsername" runat="server"
                    ControlToValidate="txtUsername"
                    ErrorMessage="Username is required."
                    CssClass="error-msg" Display="Dynamic" />
            </div>

            <div class="form-group">
                <label>Email</label>
                <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" placeholder="Email" TextMode="Email" />
                <asp:RequiredFieldValidator ID="rfvEmail" runat="server"
                    ControlToValidate="txtEmail"
                    ErrorMessage="Email is required."
                    CssClass="error-msg" Display="Dynamic" />
            </div>

            <div class="profile-section-title">Change Password <span>(leave blank to keep current)</span></div>

            <div class="form-group">
                <label>New Password</label>
                <asp:TextBox ID="txtNewPassword" runat="server" CssClass="form-control" placeholder="New password" TextMode="Password" />
                <asp:RegularExpressionValidator ID="revNewPassword" runat="server"
                    ControlToValidate="txtNewPassword"
                    ValidationExpression="^$|.{6,}"
                    ErrorMessage="Password must be at least 6 characters."
                    CssClass="error-msg" Display="Dynamic" />
            </div>

            <div class="form-group">
                <label>Confirm New Password</label>
                <asp:TextBox ID="txtConfirmPassword" runat="server" CssClass="form-control" placeholder="Confirm new password" TextMode="Password" />
                <asp:CompareValidator ID="cvPassword" runat="server"
                    ControlToValidate="txtConfirmPassword"
                    ControlToCompare="txtNewPassword"
                    ErrorMessage="Passwords do not match."
                    CssClass="error-msg" Display="Dynamic" />
            </div>

            <div class="btn-row">
                <asp:Button ID="btnSave" runat="server" Text="Save Changes" CssClass="btn-primary" OnClick="btnSave_Click" />
                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn-secondary" OnClick="btnCancel_Click" CausesValidation="false" />
            </div>
        </div>
    </div>
</asp:Content>
