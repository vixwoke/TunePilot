<%@ Page Title="Edit Lesson" Language="C#" MasterPageFile="~/navbar.Master" AutoEventWireup="true" CodeBehind="EditLesson.aspx.cs" Inherits="TunePilot.EditLesson" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="admin-page-wrapper">
        <div class="admin-card">

            <div class="page-title"><asp:Label ID="pageTitlelbl" runat="server" Text="Edit Lesson" Font-Bold="true" Font-Size="XX-Large" Font-Names="Plus Jakarta Sans"></asp:Label></div>

            <asp:Button ID="backbtn" runat="server" Text="Back to Dashboard" OnClick="backbtn_Click" CssClass="back-link" />

            <asp:Label ID="lessonTitlelbl" runat="server" Text="Lesson Details" Font-Bold="true" Font-Size="Large" Font-Names="Plus Jakarta Sans" CssClass="section-title" style="display: block;"></asp:Label>

            <asp:DetailsView ID="lessondv" runat="server"
                AutoGenerateRows="false"
                AutoGenerateEditButton="true"
                DataKeyNames="lesson_id"
                OnModeChanging="lessondv_ModeChanging"
                OnItemUpdating="lessondv_ItemUpdating"
                CssClass="admin-table"
                style="width: 100%; border-collapse: collapse; margin-bottom: 24px;">
                <Fields>
                    <asp:BoundField DataField="lesson_id" HeaderText="ID" ReadOnly="true" />
                    <asp:BoundField DataField="title" HeaderText="Title" />
                    <asp:BoundField DataField="summary" HeaderText="Summary" />
                    <asp:BoundField DataField="lesson_order" HeaderText="Order" />
                    <asp:BoundField DataField="duration_minutes" HeaderText="Duration (min)" />
                </Fields>
            </asp:DetailsView>

            <asp:Label ID="contentsTitlelbl" runat="server" Text="Lesson Contents" Font-Bold="true" Font-Size="Large" Font-Names="Plus Jakarta Sans" CssClass="section-title" style="display: block;"></asp:Label>

            <asp:GridView ID="lessonContentsgv" runat="server"
                AutoGenerateColumns="false"
                DataKeyNames="content_id"
                OnRowEditing="lessonContentsgv_RowEditing"
                OnRowUpdating="lessonContentsgv_RowUpdating"
                OnRowCancelingEdit="lessonContentsgv_RowCancelingEdit"
                style="width: 100%; border-collapse: collapse; margin-bottom: 24px;">
                <Columns>
                    <asp:BoundField DataField="content_id" HeaderText="ID" ReadOnly="true" />
                    <asp:BoundField DataField="content_type" HeaderText="Type" ReadOnly="true" />
                    <asp:BoundField DataField="title" HeaderText="Title" />
                    <asp:BoundField DataField="body" HeaderText="Body" />
                    <asp:BoundField DataField="media_url" HeaderText="URL" />
                    <asp:BoundField DataField="content_order" HeaderText="Order" />
                    <asp:CommandField ShowEditButton="true" />
                </Columns>
            </asp:GridView>

            <asp:Label ID="quizTitlelbl" runat="server" Text="Quiz" Font-Bold="true" Font-Size="Large" Font-Names="Plus Jakarta Sans" CssClass="section-title" style="display: block;"></asp:Label>

            <asp:DetailsView ID="quizdv" runat="server"
                AutoGenerateRows="false"
                AutoGenerateEditButton="true"
                DataKeyNames="quiz_id"
                OnModeChanging="quizdv_ModeChanging"
                OnItemUpdating="quizdv_ItemUpdating"
                style="width: 100%; border-collapse: collapse; margin-bottom: 24px;">
                <Fields>
                    <asp:BoundField DataField="quiz_id" HeaderText="ID" ReadOnly="true" />
                    <asp:BoundField DataField="title" HeaderText="Title" />
                    <asp:BoundField DataField="description" HeaderText="Description" />
                    <asp:BoundField DataField="passing_score" HeaderText="Passing Score" />
                </Fields>
            </asp:DetailsView>

            <asp:Label ID="questionsTitlelbl" runat="server" Text="Questions" Font-Bold="true" Font-Size="Medium" Font-Names="Plus Jakarta Sans" CssClass="section-title" style="display: block;"></asp:Label>

            <asp:GridView ID="questionsgv" runat="server"
                AutoGenerateColumns="false"
                DataKeyNames="question_id"
                OnRowEditing="questionsgv_RowEditing"
                OnRowUpdating="questionsgv_RowUpdating"
                OnRowCancelingEdit="questionsgv_RowCancelingEdit"
                OnSelectedIndexChanged="questionsgv_SelectedIndexChanged"
                style="width: 100%; border-collapse: collapse; margin-bottom: 24px;">
                <Columns>
                    <asp:BoundField DataField="question_id" HeaderText="ID" ReadOnly="true" />
                    <asp:BoundField DataField="question_text" HeaderText="Question" />
                    <asp:BoundField DataField="question_order" HeaderText="Order" />
                    <asp:CommandField ShowEditButton="true" />
                    <asp:CommandField ShowSelectButton="true" SelectText="Edit Options" />
                </Columns>
            </asp:GridView>

            <asp:Panel ID="optionspnl" runat="server" Visible="false">

                <asp:Label ID="optionsTitlelbl" runat="server" Text="Options" Font-Bold="true" Font-Size="Medium" Font-Names="Plus Jakarta Sans" CssClass="section-title" style="display: block;"></asp:Label>

                <asp:GridView ID="optionsgv" runat="server"
                    AutoGenerateColumns="false"
                    DataKeyNames="option_id"
                    OnRowEditing="optionsgv_RowEditing"
                    OnRowUpdating="optionsgv_RowUpdating"
                    OnRowCancelingEdit="optionsgv_RowCancelingEdit"
                    style="width: 100%; border-collapse: collapse; margin-bottom: 16px;">
                    <Columns>
                        <asp:BoundField DataField="option_id" HeaderText="ID" ReadOnly="true" />
                        <asp:BoundField DataField="option_text" HeaderText="Option" />
                        <asp:CheckBoxField DataField="is_correct" HeaderText="Correct" />
                        <asp:CommandField ShowEditButton="true" />
                    </Columns>
                </asp:GridView>

            </asp:Panel>

        </div>
    </div>
</asp:Content>
