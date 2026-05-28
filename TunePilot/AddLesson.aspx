<%@ Page Title="Add Lesson" Language="C#" MasterPageFile="~/navbar.Master" AutoEventWireup="true" CodeBehind="AddLesson.aspx.cs" Inherits="TunePilot.AddLesson" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="admin-page-wrapper">
        <div class="admin-card">

            <div class="page-title"><asp:Label ID="pageTitlelbl" runat="server" Text="Add Lesson" Font-Bold="true" Font-Size="XX-Large" Font-Names="Plus Jakarta Sans"></asp:Label></div>

            <asp:Button ID="backbtn" runat="server" Text="Back to Dashboard" OnClick="backbtn_Click" CssClass="back-link" />

            <asp:Label ID="courseTitlelbl" runat="server" Font-Bold="true" Font-Size="Large" Font-Names="Plus Jakarta Sans" CssClass="section-title" style="display: block;"></asp:Label>

            <asp:Label ID="messagelbl" runat="server" Visible="false" ForeColor="Red" CssClass="message-label"></asp:Label>

            <div class="section-title">Lesson Details</div>

            <div class="form-group">
                <label>Title</label>
                <asp:TextBox ID="lessonTitletb" runat="server" CssClass="form-control" Width="100%"></asp:TextBox>
            </div>

            <div class="form-group">
                <label>Summary</label>
                <asp:TextBox ID="summarytb" runat="server" TextMode="MultiLine" CssClass="form-control" Rows="4" Width="100%"></asp:TextBox>
            </div>

            <div class="form-group">
                <label>Order</label>
                <asp:TextBox ID="ordertb" runat="server" CssClass="form-control" Width="100px"></asp:TextBox>
            </div>

            <div class="form-group">
                <label>Duration (minutes)</label>
                <asp:TextBox ID="durationtb" runat="server" CssClass="form-control" Width="120px"></asp:TextBox>
            </div>

            <div class="btn-row">
                <asp:Button ID="saveLessonbtn" runat="server" Text="Save Lesson" OnClick="saveLessonbtn_Click" CssClass="btn-primary" />
            </div>

            <asp:Panel ID="quizpnl" runat="server" Visible="false">

                <div class="section-title">Quiz Details</div>

                <div class="form-group">
                    <label>Quiz Title</label>
                    <asp:TextBox ID="quizTitletb" runat="server" CssClass="form-control" Width="100%"></asp:TextBox>
                </div>

                <div class="form-group">
                    <label>Description</label>
                    <asp:TextBox ID="quizDesctb" runat="server" TextMode="MultiLine" CssClass="form-control" Rows="3" Width="100%"></asp:TextBox>
                </div>

                <div class="form-group">
                    <label>Passing Score (%)</label>
                    <asp:TextBox ID="passingStoretb" runat="server" CssClass="form-control" Width="120px"></asp:TextBox>
                </div>

                <div class="btn-row">
                    <asp:Button ID="saveQuizbtn" runat="server" Text="Save Quiz" OnClick="saveQuizbtn_Click" CssClass="btn-primary" />
                </div>

                <asp:Panel ID="questionspnl" runat="server" Visible="false">

                    <div class="section-title">Questions</div>

                    <asp:GridView ID="questionsgv" runat="server"
                        AutoGenerateColumns="false"
                        DataKeyNames="question_id"
                        OnSelectedIndexChanged="questionsgv_SelectedIndexChanged"
                        CssClass="admin-table">
                        <Columns>
                            <asp:BoundField DataField="question_id" HeaderText="ID" />
                            <asp:BoundField DataField="question_text" HeaderText="Question" />
                            <asp:BoundField DataField="question_order" HeaderText="Order" />
                            <asp:CommandField ShowSelectButton="true" SelectText="Edit Options" />
                        </Columns>
                    </asp:GridView>

                    <div class="section-title" style="font-size: 0.95rem;">Add Question</div>

                    <div class="form-group">
                        <label>Question Text</label>
                        <asp:TextBox ID="questionTexttb" runat="server" TextMode="MultiLine" CssClass="form-control" Rows="3" Width="100%"></asp:TextBox>
                    </div>

                    <div class="form-group">
                        <label>Order</label>
                        <asp:TextBox ID="questionOrdertb" runat="server" CssClass="form-control" Width="100px"></asp:TextBox>
                    </div>

                    <div class="btn-row">
                        <asp:Button ID="addQuestionbtn" runat="server" Text="+ Add Question" OnClick="addQuestionbtn_Click" CssClass="btn-primary" />
                    </div>

                    <asp:Panel ID="optionspnl" runat="server" Visible="false">

                        <div class="section-title">Options</div>

                        <asp:GridView ID="optionsgv" runat="server"
                            AutoGenerateColumns="false"
                            DataKeyNames="option_id"
                            OnRowEditing="optionsgv_RowEditing"
                            OnRowUpdating="optionsgv_RowUpdating"
                            OnRowCancelingEdit="optionsgv_RowCancelingEdit"
                            CssClass="admin-table">
                            <Columns>
                                <asp:BoundField DataField="option_id" HeaderText="ID" ReadOnly="true" />
                                <asp:BoundField DataField="option_text" HeaderText="Option" />
                                <asp:CheckBoxField DataField="is_correct" HeaderText="Correct" />
                                <asp:CommandField ShowEditButton="true" />
                            </Columns>
                        </asp:GridView>

                        <div class="section-title" style="font-size: 0.95rem;">Add Option</div>

                        <div class="form-group">
                            <label>Option Text</label>
                            <asp:TextBox ID="optionTexttb" runat="server" CssClass="form-control" Width="100%"></asp:TextBox>
                        </div>

                        <div class="form-group">
                            <label>
                                <asp:CheckBox ID="isCorrectcb" runat="server" />
                                Is Correct?
                            </label>
                        </div>

                        <div class="btn-row">
                            <asp:Button ID="addOptionbtn" runat="server" Text="+ Add Option" OnClick="addOptionbtn_Click" CssClass="btn-primary" />
                        </div>

                    </asp:Panel>

                </asp:Panel>

            </asp:Panel>

        </div>
    </div>
</asp:Content>
