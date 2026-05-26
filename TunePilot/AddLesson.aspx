<%@ Page Title="Add Lesson" Language="C#" MasterPageFile="~/navbar.Master" AutoEventWireup="true" CodeBehind="AddLesson.aspx.cs" Inherits="TunePilot.AddLesson" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:Label ID="pageTitlelbl" runat="server" Text="Add Lesson" Font-Bold="true" Font-Size="XX-Large" Font-Names="Montserrat"></asp:Label>

    <br />
    <br />

    <asp:Button ID="backbtn" runat="server" Text="← Back to Dashboard" OnClick="backbtn_Click" />

    <br />
    <br />

    <asp:Label ID="courseTitlelbl" runat="server" Font-Bold="true" Font-Size="Large" Font-Names="Montserrat"></asp:Label>

    <br />
    <br />

    <asp:Label ID="messagelbl" runat="server" Visible="false" ForeColor="Red"></asp:Label>

    <br />

    <asp:Label runat="server" Text="Lesson Details" Font-Bold="true" Font-Size="Large" Font-Names="Montserrat"></asp:Label>
    <br />
    <br />

    <asp:Label runat="server" Text="Title" Font-Names="Montserrat"></asp:Label>
    <br />
    <asp:TextBox ID="lessonTitletb" runat="server" Width="400px"></asp:TextBox>
    <br />
    <br />

    <asp:Label runat="server" Text="Summary" Font-Names="Montserrat"></asp:Label>
    <br />
    <asp:TextBox ID="summarytb" runat="server" TextMode="MultiLine" Width="400px" Rows="4"></asp:TextBox>
    <br />
    <br />

    <asp:Label runat="server" Text="Order" Font-Names="Montserrat"></asp:Label>
    <br />
    <asp:TextBox ID="ordertb" runat="server" Width="100px"></asp:TextBox>
    <br />
    <br />

    <asp:Label runat="server" Text="Duration (minutes)" Font-Names="Montserrat"></asp:Label>
    <br />
    <asp:TextBox ID="durationtb" runat="server" Width="100px"></asp:TextBox>
    <br />
    <br />

    <asp:Button ID="saveLessonbtn" runat="server" Text="Save Lesson" OnClick="saveLessonbtn_Click" />

    <br />
    <br />

    <asp:Panel ID="quizpnl" runat="server" Visible="false">

        <asp:Label runat="server" Text="Quiz Details" Font-Bold="true" Font-Size="Large" Font-Names="Montserrat"></asp:Label>
        <br />
        <br />

        <asp:Label runat="server" Text="Quiz Title" Font-Names="Montserrat"></asp:Label>
        <br />
        <asp:TextBox ID="quizTitletb" runat="server" Width="400px"></asp:TextBox>
        <br />
        <br />

        <asp:Label runat="server" Text="Description" Font-Names="Montserrat"></asp:Label>
        <br />
        <asp:TextBox ID="quizDesctb" runat="server" TextMode="MultiLine" Width="400px" Rows="3"></asp:TextBox>
        <br />
        <br />

        <asp:Label runat="server" Text="Passing Score (%)" Font-Names="Montserrat"></asp:Label>
        <br />
        <asp:TextBox ID="passingStoretb" runat="server" Width="100px"></asp:TextBox>
        <br />
        <br />

        <asp:Button ID="saveQuizbtn" runat="server" Text="Save Quiz" OnClick="saveQuizbtn_Click" />

        <br />
        <br />

        <asp:Panel ID="questionspnl" runat="server" Visible="false">

            <asp:Label runat="server" Text="Questions" Font-Bold="true" Font-Size="Large" Font-Names="Montserrat"></asp:Label>
            <br />
            <br />

            <asp:GridView ID="questionsgv" runat="server"
                AutoGenerateColumns="false"
                DataKeyNames="question_id"
                OnSelectedIndexChanged="questionsgv_SelectedIndexChanged">
                <Columns>
                    <asp:BoundField DataField="question_id" HeaderText="ID" />
                    <asp:BoundField DataField="question_text" HeaderText="Question" />
                    <asp:BoundField DataField="question_order" HeaderText="Order" />
                    <asp:CommandField ShowSelectButton="true" SelectText="Edit Options" />
                </Columns>
            </asp:GridView>

            <br />

            <asp:Label runat="server" Text="Add Question" Font-Bold="true" Font-Names="Montserrat"></asp:Label>
            <br />
            <br />

            <asp:Label runat="server" Text="Question Text" Font-Names="Montserrat"></asp:Label>
            <br />
            <asp:TextBox ID="questionTexttb" runat="server" TextMode="MultiLine" Width="400px" Rows="3"></asp:TextBox>
            <br />
            <br />

            <asp:Label runat="server" Text="Order" Font-Names="Montserrat"></asp:Label>
            <br />
            <asp:TextBox ID="questionOrdertb" runat="server" Width="100px"></asp:TextBox>
            <br />
            <br />

            <asp:Button ID="addQuestionbtn" runat="server" Text="+ Add Question" OnClick="addQuestionbtn_Click" />

            <br />
            <br />

            <asp:Panel ID="optionspnl" runat="server" Visible="false">

                <asp:Label runat="server" Text="Options" Font-Bold="true" Font-Names="Montserrat"></asp:Label>
                <br />
                <br />

                <asp:GridView ID="optionsgv" runat="server"
                    AutoGenerateColumns="false"
                    DataKeyNames="option_id"
                    OnRowEditing="optionsgv_RowEditing"
                    OnRowUpdating="optionsgv_RowUpdating"
                    OnRowCancelingEdit="optionsgv_RowCancelingEdit">
                    <Columns>
                        <asp:BoundField DataField="option_id" HeaderText="ID" ReadOnly="true" />
                        <asp:BoundField DataField="option_text" HeaderText="Option" />
                        <asp:CheckBoxField DataField="is_correct" HeaderText="Correct" />
                        <asp:CommandField ShowEditButton="true" />
                    </Columns>
                </asp:GridView>

                <br />

                <asp:Label runat="server" Text="Add Option" Font-Bold="true" Font-Names="Montserrat"></asp:Label>
                <br />
                <br />

                <asp:Label runat="server" Text="Option Text" Font-Names="Montserrat"></asp:Label>
                <br />
                <asp:TextBox ID="optionTexttb" runat="server" Width="400px"></asp:TextBox>
                <br />
                <br />

                <asp:Label runat="server" Text="Is Correct?" Font-Names="Montserrat"></asp:Label>
                <br />
                <asp:CheckBox ID="isCorrectcb" runat="server" />
                <br />
                <br />

                <asp:Button ID="addOptionbtn" runat="server" Text="+ Add Option" OnClick="addOptionbtn_Click" />

            </asp:Panel>

        </asp:Panel>

    </asp:Panel>

</asp:Content>
