<%@ Page Title="Add Exam" Language="C#" MasterPageFile="~/navbar.Master" AutoEventWireup="true" CodeBehind="AddExam.aspx.cs" Inherits="TunePilot.AddExam" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:Label ID="pageTitlelbl" runat="server" Text="Add Exam" Font-Bold="true" Font-Size="XX-Large" Font-Names="Montserrat"></asp:Label>

    <br /><br />

    <asp:Button ID="backbtn" runat="server" Text="← Back to Dashboard" OnClick="backbtn_Click" />

    <br /><br />

    <asp:Label ID="courseTitlelbl" runat="server" Font-Bold="true" Font-Size="Large" Font-Names="Montserrat"></asp:Label>

    <br /><br />

    <asp:Label ID="messagelbl" runat="server" Visible="false" ForeColor="Red" Font-Names="Montserrat"></asp:Label>

    <br />

    <asp:Label runat="server" Text="Exam Details" Font-Bold="true" Font-Size="Large" Font-Names="Montserrat"></asp:Label>
    <br /><br />

    <asp:Label runat="server" Text="Title" Font-Names="Montserrat"></asp:Label>
    <br />
    <asp:TextBox ID="examTitletb" runat="server" Width="400px"></asp:TextBox>
    <br /><br />

    <asp:Label runat="server" Text="Description" Font-Names="Montserrat"></asp:Label>
    <br />
    <asp:TextBox ID="examDesctb" runat="server" TextMode="MultiLine" Width="400px" Rows="4"></asp:TextBox>
    <br /><br />

    <asp:Label runat="server" Text="Passing Score (%)" Font-Names="Montserrat"></asp:Label>
    <br />
    <asp:TextBox ID="passingStoretb" runat="server" Width="100px"></asp:TextBox>
    <br /><br />

    <asp:Button ID="saveExambtn" runat="server" Text="Save Exam" OnClick="saveExambtn_Click" />

    <br /><br />

    <asp:Panel ID="questionspnl" runat="server" Visible="false">

        <asp:Label runat="server" Text="Exam Questions" Font-Bold="true" Font-Size="Large" Font-Names="Montserrat"></asp:Label>
        <br /><br />

        <asp:GridView ID="questionsgv" runat="server"
            AutoGenerateColumns="false"
            DataKeyNames="question_id">
            <Columns>
                <asp:BoundField DataField="question_id"    HeaderText="ID" />
                <asp:BoundField DataField="instruction"    HeaderText="Instruction" />
                <asp:BoundField DataField="expected_notes" HeaderText="Expected Notes" />
                <asp:BoundField DataField="question_order" HeaderText="Order" />
            </Columns>
        </asp:GridView>

        <br />

        <asp:Label runat="server" Text="Add Question" Font-Bold="true" Font-Names="Montserrat"></asp:Label>
        <br /><br />

        <asp:Label runat="server" Text="Instruction" Font-Names="Montserrat"></asp:Label>
        <br />
        <asp:TextBox ID="instructiontb" runat="server" TextMode="MultiLine" Width="400px" Rows="3"></asp:TextBox>
        <br /><br />

        <asp:Label runat="server" Text="Expected Notes (space separated e.g. C4 D4 E4)" Font-Names="Montserrat"></asp:Label>
        <br />
        <asp:TextBox ID="expectedNotestb" runat="server" Width="400px"></asp:TextBox>
        <br /><br />

        <asp:Label runat="server" Text="Order" Font-Names="Montserrat"></asp:Label>
        <br />
        <asp:TextBox ID="questionOrdertb" runat="server" Width="100px"></asp:TextBox>
        <br /><br />

        <asp:Button ID="addQuestionbtn" runat="server" Text="+ Add Question" OnClick="addQuestionbtn_Click" />

    </asp:Panel>

</asp:Content>