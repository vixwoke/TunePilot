<%@ Page Title="Add Exam" Language="C#" MasterPageFile="~/navbar.Master" AutoEventWireup="true" CodeBehind="AddExam.aspx.cs" Inherits="TunePilot.AddExam" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="admin-page-wrapper">
        <div class="admin-card">

            <div class="page-title"><asp:Label ID="pageTitlelbl" runat="server" Text="Add Exam" Font-Bold="true" Font-Size="XX-Large" Font-Names="Plus Jakarta Sans"></asp:Label></div>

            <asp:Button ID="backbtn" runat="server" Text="Back to Dashboard" OnClick="backbtn_Click" CssClass="back-link" />

            <asp:Label ID="courseTitlelbl" runat="server" Font-Bold="true" Font-Size="Large" Font-Names="Plus Jakarta Sans" CssClass="section-title" style="display: block;"></asp:Label>

            <asp:Label ID="messagelbl" runat="server" Visible="false" ForeColor="Red" CssClass="message-label"></asp:Label>

            <div class="section-title">Exam Details</div>

            <div class="form-group">
                <label>Title</label>
                <asp:TextBox ID="examTitletb" runat="server" CssClass="form-control" Width="100%"></asp:TextBox>
            </div>

            <div class="form-group">
                <label>Description</label>
                <asp:TextBox ID="examDesctb" runat="server" TextMode="MultiLine" CssClass="form-control" Rows="4" Width="100%"></asp:TextBox>
            </div>

            <div class="form-group">
                <label>Passing Score (%)</label>
                <asp:TextBox ID="passingStoretb" runat="server" CssClass="form-control" Width="120px"></asp:TextBox>
            </div>

            <div class="btn-row">
                <asp:Button ID="saveExambtn" runat="server" Text="Save Exam" OnClick="saveExambtn_Click" CssClass="btn-primary" />
            </div>

            <asp:Panel ID="questionspnl" runat="server" Visible="false">

                <div class="section-title">Exam Questions</div>

                <asp:GridView ID="questionsgv" runat="server"
                    AutoGenerateColumns="false"
                    DataKeyNames="question_id"
                    CssClass="admin-table">
                    <Columns>
                        <asp:BoundField DataField="question_id" HeaderText="ID" />
                        <asp:BoundField DataField="instruction" HeaderText="Instruction" />
                        <asp:BoundField DataField="expected_notes" HeaderText="Expected Notes" />
                        <asp:BoundField DataField="question_order" HeaderText="Order" />
                    </Columns>
                </asp:GridView>

                <div class="section-title" style="font-size: 0.95rem;">Add Question</div>

                <div class="form-group">
                    <label>Instruction</label>
                    <asp:TextBox ID="instructiontb" runat="server" TextMode="MultiLine" CssClass="form-control" Rows="3" Width="100%"></asp:TextBox>
                </div>

                <div class="form-group">
                    <label>Expected Notes (space separated e.g. C4 D4 E4)</label>
                    <asp:TextBox ID="expectedNotestb" runat="server" CssClass="form-control" Width="100%"></asp:TextBox>
                </div>

                <div class="form-group">
                    <label>Order</label>
                    <asp:TextBox ID="questionOrdertb" runat="server" CssClass="form-control" Width="100px"></asp:TextBox>
                </div>

                <div class="btn-row">
                    <asp:Button ID="addQuestionbtn" runat="server" Text="+ Add Question" OnClick="addQuestionbtn_Click" CssClass="btn-primary" />
                </div>

            </asp:Panel>

        </div>
    </div>
</asp:Content>
