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
                <svg class="instrument-svg" viewBox="0 0 256 256" fill="currentColor" xmlns="http://www.w3.org/2000/svg">
                    <path d="M249.66,46.34l-40-40a8,8,0,0,0-11.32,11.32L200.69,20,140.52,80.16C117.73,68.3,92.21,69.29,76.75,84.74a42.27,42.27,0,0,0-9.39,14.37A8.24,8.24,0,0,1,59.81,104c-14.59.49-27.26,5.72-36.65,15.11C11.08,131.22,6,148.6,8.74,168.07,11.4,186.7,21.07,205.15,36,220s33.34,24.56,52,27.22A71.13,71.13,0,0,0,98.1,248c15.32,0,28.83-5.23,38.76-15.16,9.39-9.39,14.62-22.06,15.11-36.65a8.24,8.24,0,0,1,4.92-7.55,42.22,42.22,0,0,0,14.37-9.39c15.45-15.46,16.44-41,4.58-63.77L236,55.31l2.34,2.35a8,8,0,0,0,11.32-11.32Zm-156,159.31a8,8,0,0,1-11.31,0l-32-32a8,8,0,0,1,11.32-11.31l32,32A8,8,0,0,1,93.66,205.65Zm42.14-45.86a28,28,0,1,1,0-39.59A28,28,0,0,1,135.8,159.79Zm31.06-58a86.94,86.94,0,0,0-6-6.68,85.23,85.23,0,0,0-6.69-6L176,67.31,188.69,80ZM200,68.68,187.32,56,212,31.31,224.69,44Z"/>
                </svg>
                <span>Guitar</span>
            </button>
            <button type="submit" id="CardDrum" runat="server" class="instrument-card">
                <svg class="instrument-svg" viewBox="0 0 32 32" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" xmlns="http://www.w3.org/2000/svg">
                    <circle cx="16" cy="20" r="8"/>
                    <circle cx="18" cy="22" r="2"/>
                    <line x1="9" y1="29" x2="11" y2="27"/>
                    <line x1="23" y1="29" x2="21" y2="27"/>
                    <ellipse cx="8.5" cy="10.4" rx="4.5" ry="1.6"/>
                    <path d="M13,10.4V5.6C13,4.7,11,4,8.5,4S4,4.7,4,5.6v4.9"/>
                    <ellipse cx="24.5" cy="10.4" rx="4.5" ry="1.6"/>
                    <path d="M29,10.4V5.6C29,4.7,27,4,24.5,4S20,4.7,20,5.6v4.9"/>
                </svg>
                <span>Drums</span>
            </button>
            <button type="submit" id="CardTrumpet" runat="server" class="instrument-card">
                <svg class="instrument-svg" viewBox="0 0 512 512" fill="currentColor" xmlns="http://www.w3.org/2000/svg">
                    <path d="M393.846,52.513v45.949c0,54.292-44.17,98.462-98.462,98.462h-19.692v-45.949h-39.385v45.949h-39.385v-45.949h-39.385 v45.949h-39.385v-45.949H78.769v45.949H0v118.154h69.028c-2.186,6.164-3.387,12.79-3.387,19.692 c0,32.575,26.502,59.077,59.077,59.077h105.026c32.575,0,59.077-26.502,59.077-59.077c0-6.903-1.201-13.529-3.387-19.692h9.951 c54.292,0,98.462,44.17,98.462,98.462v45.949H512V52.513H393.846z M229.744,354.462H124.718c-10.858,0-19.692-8.834-19.692-19.692 s8.834-19.692,19.692-19.692h105.026c10.858,0,19.692,8.834,19.692,19.692S240.602,354.462,229.744,354.462z M472.615,420.103 h-39.385v-6.564c0-76.008-61.838-137.846-137.846-137.846h-256v-39.385h256c76.008,0,137.846-61.838,137.846-137.846v-6.564 h39.385V420.103z"/>
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
