<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StudentDashboard.aspx.cs" Inherits="TunePilot.StudentDashboard" MasterPageFile="~/navbar.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Student Dashboard - TunePilot</title>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="dashboard-header">
        <h1><asp:Label ID="lblGreeting" runat="server" /></h1>
        <asp:Label ID="RoleLabel" runat="server" CssClass="role-badge" />
    </div>

    <div class="dashboard-body">
        <div class="db-sidebar">
            <div class="instrument-selector">
            <button type="submit" id="CardGuitar" runat="server" class="instrument-card">
                <svg class="instrument-svg" viewBox="0 0 64 64" fill="none" xmlns="http://www.w3.org/2000/svg">
                    <path d="M32 52 C24 52 18 46 18 38 C18 30 22 26 24 24 L24 18 L26 18 L26 24 C26 22 28 18 32 18 C36 18 38 22 38 24 L38 18 L40 18 L40 24 C42 26 46 30 46 38 C46 46 40 52 32 52Z" stroke="currentColor" stroke-width="2" stroke-linejoin="round"/>
                    <rect x="29" y="4" width="6" height="16" rx="2" stroke="currentColor" stroke-width="2"/>
                    <rect x="28" y="4" width="8" height="4" rx="1.5" stroke="currentColor" stroke-width="1.5"/>
                    <circle cx="32" cy="34" r="4" stroke="currentColor" stroke-width="1.5"/>
                    <line x1="28" y1="30" x2="36" y2="30" stroke="currentColor" stroke-width="1.2"/>
                    <line x1="28" y1="38" x2="36" y2="38" stroke="currentColor" stroke-width="1.2"/>
                </svg>
                <span>Guitar</span>
            </button>
            <button type="submit" id="CardDrum" runat="server" class="instrument-card">
                <svg class="instrument-svg" viewBox="0 0 64 64" fill="none" xmlns="http://www.w3.org/2000/svg">
                    <ellipse cx="32" cy="26" rx="18" ry="8" stroke="currentColor" stroke-width="2.5"/>
                    <path d="M14 26 L14 42 C14 46 18 48 32 48 C46 48 50 46 50 42 L50 26" stroke="currentColor" stroke-width="2.5" stroke-linejoin="round"/>
                    <ellipse cx="32" cy="26" rx="12" ry="5" stroke="currentColor" stroke-width="1.2" stroke-dasharray="2 3"/>
                    <line x1="22" y1="48" x2="20" y2="56" stroke="currentColor" stroke-width="2" stroke-linecap="round"/>
                    <line x1="42" y1="48" x2="44" y2="56" stroke="currentColor" stroke-width="2" stroke-linecap="round"/>
                    <line x1="18" y1="30" x2="12" y2="36" stroke="currentColor" stroke-width="2" stroke-linecap="round"/>
                    <line x1="46" y1="30" x2="52" y2="36" stroke="currentColor" stroke-width="2" stroke-linecap="round"/>
                </svg>
                <span>Drums</span>
            </button>
            <button type="submit" id="CardTrumpet" runat="server" class="instrument-card">
                <svg class="instrument-svg" viewBox="0 0 64 64" fill="none" xmlns="http://www.w3.org/2000/svg">
                    <rect x="13" y="30" width="4" height="8" rx="1" stroke="currentColor" stroke-width="1.5"/>
                    <path d="M17 32 L38 22 C42 20 48 20 52 24" stroke="currentColor" stroke-width="2.5" stroke-linecap="round"/>
                    <path d="M38 22 L38 16" stroke="currentColor" stroke-width="2" stroke-linecap="round"/>
                    <path d="M42 24 L42 18" stroke="currentColor" stroke-width="2" stroke-linecap="round"/>
                    <path d="M46 24 L46 20" stroke="currentColor" stroke-width="2" stroke-linecap="round"/>
                    <ellipse cx="52" cy="24" rx="6" ry="4" stroke="currentColor" stroke-width="2"/>
                    <line x1="38" y1="22" x2="38" y2="28" stroke="currentColor" stroke-width="1.5"/>
                    <line x1="42" y1="24" x2="42" y2="30" stroke="currentColor" stroke-width="1.5"/>
                    <line x1="46" y1="24" x2="46" y2="30" stroke="currentColor" stroke-width="1.5"/>
                </svg>
                <span>Trumpet</span>
            </button>
            </div>

            <div class="instrument-info">
                <div class="instrument-info-header">
                    <h2><asp:Label ID="LabelInstrumentName" runat="server" /></h2>
                    <span class="category"><asp:Label ID="LabelCategory" runat="server" /></span>
                </div>
                <p><asp:Label ID="LabelDescription" runat="server" /></p>
            </div>
        </div>

        <div class="db-main">
            <div class="section-card">
                <div class="section-header">
                    <svg class="section-icon" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round">
                        <path d="M2 3h6a4 4 0 0 1 4 4v14a3 3 0 0 0-3-3H2z"/>
                        <path d="M22 3h-6a4 4 0 0 0-4 4v14a3 3 0 0 1 3-3h7z"/>
                    </svg>
                    <h2 id="lblLessons">Lessons</h2>
                </div>
                <asp:PlaceHolder ID="LessonContainer" runat="server" />
                <div class="progress-grid">
                    <asp:PlaceHolder ID="LessonProgressContainer" runat="server" />
                </div>
            </div>

            <div class="section-card">
                <div class="section-header">
                    <svg class="section-icon" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round">
                        <circle cx="12" cy="12" r="10"/>
                        <path d="M12 6v6l4 2"/>
                    </svg>
                    <h2 id="lblQuizzes" runat="server">Quizzes</h2>
                </div>
                <asp:PlaceHolder ID="QuizContainer" runat="server" />
                <div class="progress-grid">
                    <asp:PlaceHolder ID="QuizProgressContainer" runat="server" />
                </div>
            </div>

            <div class="section-card">
                <div class="section-header">
                    <svg class="section-icon" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round">
                        <path d="M9 5H7a2 2 0 0 0-2 2v12a2 2 0 0 0 2 2h10a2 2 0 0 0 2-2V7a2 2 0 0 0-2-2h-2"/>
                        <rect x="9" y="3" width="6" height="4" rx="1"/>
                        <path d="M9 12l2 2 4-4"/>
                    </svg>
                    <h2 ID="lblExams" runat="server">Exams</h2>
                </div>
                <asp:PlaceHolder ID="ExamContainer" runat="server" />
                <div class="progress-grid">
                    <asp:PlaceHolder ID="ExamProgressContainer" runat="server" />
                </div>
            </div>
        </div>
    </div>


</asp:Content>
