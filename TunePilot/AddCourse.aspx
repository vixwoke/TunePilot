<%@ Page Title="Add Course" Language="C#" MasterPageFile="~/navbar.Master" AutoEventWireup="true" CodeBehind="AddCourse.aspx.cs" Inherits="TunePilot.AddCourse" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:Label ID="pageTitlelbl" runat="server" Text="Add Course" Font-Bold="true" Font-Size="XX-Large" Font-Names="Montserrat"></asp:Label>

    <br /><br />

    <asp:Label runat="server" Text="Instrument" Font-Names="Montserrat"></asp:Label>
    <br />
    <asp:DropDownList ID="instrumentddl" runat="server"></asp:DropDownList>

    <br /><br />

    <asp:Label runat="server" Text="Title" Font-Names="Montserrat"></asp:Label>
    <br />
    <asp:TextBox ID="titletb" runat="server" Width="400px"></asp:TextBox>

    <br /><br />

    <asp:Label runat="server" Text="Description" Font-Names="Montserrat"></asp:Label>
    <br />
    <asp:TextBox ID="descriptiontb" runat="server" TextMode="MultiLine" Width="400px" Rows="4"></asp:TextBox>

    <br /><br />

    <asp:Label runat="server" Text="Difficulty Level" Font-Names="Montserrat"></asp:Label>
    <br />
    <asp:DropDownList ID="difficultyDdl" runat="server">
        <asp:ListItem Text="Select..." Value="" />
        <asp:ListItem Text="Beginner" Value="beginner" />
        <asp:ListItem Text="Intermediate" Value="intermediate" />
        <asp:ListItem Text="Advanced" Value="advanced" />
    </asp:DropDownList>

    <br /><br />

    <asp:Label ID="messagelbl" runat="server" Visible="false" ForeColor="Red"></asp:Label>

    <br />

    <asp:Button ID="savebtn" runat="server" Text="Save Course" OnClick="savebtn_Click" />
    <asp:Button ID="cancelbtn" runat="server" Text="Cancel" OnClick="cancelbtn_Click" />

</asp:Content>