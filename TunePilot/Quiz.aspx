<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Quiz.aspx.cs" Inherits="TunePilot.Quiz" MasterPageFile="~/navbar.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Quiz - TunePilot</title>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="quiz-page-wrapper">

        <div class="quiz-card">
            <h2><asp:Label ID="QuizTitle" runat="server" /></h2>
            <div class="quiz-meta">
                <asp:Label ID="QuizDecription" runat="server" /> —
                <asp:Label ID="PassingScore" runat="server" />
            </div>

            <div class="quiz-question-text">
                <asp:Label ID="QuizQuestion" runat="server" />
            </div>

            <div class="quiz-options">
                <asp:Button ID="QuizOption1" runat="server" OnClick="Option_Click" CssClass="quiz-option-btn" />
                <asp:Button ID="QuizOption2" runat="server" OnClick="Option_Click" CssClass="quiz-option-btn" />
                <asp:Button ID="QuizOption3" runat="server" OnClick="Option_Click" CssClass="quiz-option-btn" />
                <asp:Button ID="QuizOption4" runat="server" OnClick="Option_Click" CssClass="quiz-option-btn" />
            </div>

            <div class="quiz-nav">
                <asp:Button ID="PrevBtn" runat="server" OnClick="Prev_Click" Text="Prev" CssClass="btn-primary" />
                <asp:Button ID="CompleteBtn" runat="server" OnClick="Complete_Click" Text="Complete" CssClass="btn-primary" />
                <asp:Button ID="NextBtn" runat="server" OnClick="Next_Click" Text="Next" CssClass="btn-primary" />
            </div>
        </div>

        <div class="quiz-card">
            <div class="quiz-status-header">
                <span>Question Status</span>
                <asp:PlaceHolder ID="QuestionContainer" runat="server" />
            </div>
        </div>

        <div class="quiz-card">
            <h3 class="quiz-subheading">Previous Attempts</h3>
            <asp:PlaceHolder ID="Attempt" runat="server" />
        </div>

        <asp:Button ID="BackBtn" runat="server" Text="Back to Dashboard" OnClick="Back_Click" CssClass="back-link" />

    </div>
</asp:Content>
