<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Lesson.aspx.cs" Inherits="TunePilot.Lesson" MasterPageFile="~/navbar.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Lesson - TunePilot</title>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="lesson-page-wrapper">

        <!-- HEADER -->
        <div class="lesson-header-card">
            <h2>
                <asp:Label ID="LabelInstrument" runat="server" />
                <span class="level-badge">— <asp:Label ID="LabelLevel" runat="server" /></span>
            </h2>
        </div>

        <!-- TWO-COLUMN LAYOUT -->
        <div class="lesson-layout">

            <!-- SIDEBAR: Lesson List -->
            <div class="lesson-sidebar">
                <h3>Lessons</h3>
                <asp:PlaceHolder ID="LessonContainer" runat="server" />
            </div>

            <!-- MAIN: Lesson Content -->
            <div class="lesson-main">

                <!-- TITLE + VIDEO -->
                <div class="section-card">
                    <div class="lesson-title">
                        <asp:Label ID="LessonTitle" runat="server" />
                    </div>
                    <div class="video-wrapper">
                        <video id="VideoPlayer" runat="server" width="700" controls></video>
                    </div>
                </div>

                <!-- RESOURCES -->
                <div class="section-card lesson-resources">
                    <h4>Lesson Resources</h4>
                    <asp:PlaceHolder ID="ContentContainer" runat="server" />
                </div>

                <!-- SUMMARY -->
                <div class="section-card lesson-summary">
                    <h4>Summary</h4>
                    <p><asp:Label ID="LessonDesc" runat="server" /></p>
                    <span class="duration-badge"><asp:Label ID="LessonDuration" runat="server" /></span>
                </div>

                <!-- NAVIGATION -->
                <div class="lesson-nav">
                    <asp:Button ID="PrevBtn" runat="server" Text="Prev" OnClick="Prev_Click" CssClass="btn-primary" />
                    <asp:Button ID="CompleteBtn" runat="server" Text="Complete" OnClick="Complete_Click" CssClass="btn-complete" />
                    <asp:Button ID="NextBtn" runat="server" Text="Next" OnClick="Next_Click" CssClass="btn-primary" />
                </div>

                <div class="lesson-back">
                    <asp:Button ID="BackBtn" runat="server" Text="Back to Dashboard" OnClick="Back_Click" CssClass="btn-link" />
                </div>

            </div>
        </div>

    </div>

</asp:Content>
