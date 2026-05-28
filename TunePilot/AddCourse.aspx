<%@ Page Title="Add Course" Language="C#" MasterPageFile="~/navbar.Master" AutoEventWireup="true" CodeBehind="AddCourse.aspx.cs" Inherits="TunePilot.AddCourse" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="admin-page-wrapper">
        <div class="admin-card">

            <div class="page-title"><asp:Label ID="pageTitlelbl" runat="server" Text="Add Course" Font-Bold="true" Font-Size="XX-Large" Font-Names="Plus Jakarta Sans"></asp:Label></div>

            <asp:Label ID="messagelbl" runat="server" Visible="false" ForeColor="Red" CssClass="message-label"></asp:Label>

            <div class="form-group">
                <label>Instrument</label>
                <asp:DropDownList ID="instrumentddl" runat="server" CssClass="form-control"></asp:DropDownList>
            </div>

            <div class="form-group">
                <label>Title</label>
                <asp:TextBox ID="titletb" runat="server" CssClass="form-control" Width="100%"></asp:TextBox>
            </div>

            <div class="form-group">
                <label>Description</label>
                <asp:TextBox ID="descriptiontb" runat="server" TextMode="MultiLine" CssClass="form-control" Rows="4" Width="100%"></asp:TextBox>
            </div>

            <div class="form-group">
                <label>Difficulty Level</label>
                <asp:DropDownList ID="difficultyDdl" runat="server" CssClass="form-control">
                    <asp:ListItem Text="Select..." Value="" />
                    <asp:ListItem Text="Beginner" Value="beginner" />
                    <asp:ListItem Text="Intermediate" Value="intermediate" />
                    <asp:ListItem Text="Advanced" Value="advanced" />
                </asp:DropDownList>
            </div>

            <div class="btn-row">
                <asp:Button ID="savebtn" runat="server" Text="Save Course" OnClick="savebtn_Click" CssClass="btn-primary" />
                <asp:Button ID="cancelbtn" runat="server" Text="Cancel" OnClick="cancelbtn_Click" CssClass="btn-secondary" />
            </div>

        </div>
    </div>
</asp:Content>
