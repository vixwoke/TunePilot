<%@ Page Title="Add Student" Language="C#" MasterPageFile="~/navbar.Master" AutoEventWireup="true" CodeBehind="AddStudent.aspx.cs" Inherits="TunePilot.AddStudent" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:Label ID="pageTitlelbl" runat="server" Text="Add Student" Font-Bold="true" Font-Size="XX-Large" Font-Names="Montserrat"></asp:Label>

    <br /><br />

    <asp:Button ID="backbtn" runat="server" Text="← Back to Dashboard" OnClick="backbtn_Click" />

    <br /><br />

    <asp:Label ID="messagelbl" runat="server" Visible="false" ForeColor="Red" Font-Names="Montserrat"></asp:Label>

    <br />

    <asp:Label runat="server" Text="First Name" Font-Names="Montserrat"></asp:Label>
    <br />
    <asp:TextBox ID="firstNametb" runat="server" Width="400px"></asp:TextBox>
    <br /><br />

    <asp:Label runat="server" Text="Last Name" Font-Names="Montserrat"></asp:Label>
    <br />
    <asp:TextBox ID="lastNametb" runat="server" Width="400px"></asp:TextBox>
    <br /><br />

    <asp:Label runat="server" Text="Username" Font-Names="Montserrat"></asp:Label>
    <br />
    <asp:TextBox ID="usernametb" runat="server" Width="400px"></asp:TextBox>
    <br /><br />

    <asp:Label runat="server" Text="Email" Font-Names="Montserrat"></asp:Label>
    <br />
    <asp:TextBox ID="emailtb" runat="server" Width="400px"></asp:TextBox>
    <br /><br />

    <asp:Label runat="server" Text="Password" Font-Names="Montserrat"></asp:Label>
    <br />
    <asp:TextBox ID="passwordtb" runat="server" Width="400px" TextMode="Password"></asp:TextBox>
    <br /><br />

    <asp:Button ID="savebtn" runat="server" Text="Save Student" OnClick="savebtn_Click" />

</asp:Content>