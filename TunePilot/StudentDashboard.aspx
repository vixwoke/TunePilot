<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StudentDashboard.aspx.cs" Inherits="TunePilot.StudentDashboard" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Student Dashboard</title>

    <style>

        .instrument-icon {
            width: 80px;
            border: 2px solid gray;
            margin-right: 10px;
            cursor: pointer;
        }

        .course-title {
            margin-top: 20px;
            font-size: 22px;
            font-weight: bold;
        }

        .progress-img {
            width: 40px;
            height: 40px;
            margin-right: 5px;
        }

        .locked-text {
            color: red;
            font-weight: bold;
        }

    </style>

</head>
<body>

<form id="form1" runat="server">

    <asp:Label ID="RoleLabel" runat="server"></asp:Label>

    <hr />

    <!-- AUDIO -->
    <audio id="guitarSound" preload="auto">
        <source src="/resources/studentDashboard/guitarVoice.mp3" type="audio/mpeg" />
    </audio>

    <audio id="drumSound" preload="auto">
        <source src="/resources/studentDashboard/drumsVoice.mp3" type="audio/mpeg" />
    </audio>

    <audio id="trumpetSound" preload="auto">
        <source src="/resources/studentDashboard/trumpetVoice.mp3" type="audio/mpeg" />
    </audio>

    <!-- INSTRUMENT -->
    <div>

        <asp:Image ID="GuitarIcon"
            runat="server"
            CssClass="instrument-icon"
            ImageUrl="/resources/studentDashboard/guitar1.png"
            onclick="selectInstrument(1)" />

        <asp:Image ID="DrumIcon"
            runat="server"
            CssClass="instrument-icon"
            ImageUrl="/resources/studentDashboard/drum.png"
            onclick="selectInstrument(2)" />

        <asp:Image ID="TrumpetIcon"
            runat="server"
            CssClass="instrument-icon"
            ImageUrl="/resources/studentDashboard/trumpet.jpg"
            onclick="selectInstrument(3)" />

    </div>

    <br />

    <!-- INSTRUMENT INFO -->
    <asp:Label ID="LabelInstrumentName" runat="server"></asp:Label>

    <asp:Label ID="LabelCategory" runat="server"></asp:Label>

    <br /><br />

    <asp:Label ID="LabelDescription" runat="server"></asp:Label>

    <hr />

    <!-- LESSON -->
    <h2>Lessons</h2>

    <asp:PlaceHolder ID="LessonContainer" runat="server"></asp:PlaceHolder>

    <br />

    <asp:PlaceHolder ID="LessonProgressContainer" runat="server"></asp:PlaceHolder>

    <hr />

    <!-- QUIZ -->
    <h2>Quizzes</h2>

    <asp:PlaceHolder ID="QuizContainer" runat="server"></asp:PlaceHolder>

    <br />

    <asp:PlaceHolder ID="QuizProgressContainer" runat="server"></asp:PlaceHolder>

    <hr />

    <!-- EXAM -->
    <h2>Exams</h2>

    <asp:PlaceHolder ID="ExamContainer" runat="server"></asp:PlaceHolder>

    <br />

    <asp:PlaceHolder ID="ExamProgressContainer" runat="server"></asp:PlaceHolder>

    <br /><br />

    <!-- LOGIN -->
    <asp:HyperLink
        ID="LoginUnlock"
        runat="server"
        NavigateUrl="~/Login.aspx"
        CssClass="locked-text">
    </asp:HyperLink>

</form>

<script>

    function selectInstrument(id) {
        let sounds =
            [
                "guitarSound",
                "drumSound",
                "trumpetSound"
            ];

        let audio =
            document.getElementById(sounds[id - 1]);

        if (audio) {
            audio.pause();
            audio.currentTime = 0;
            audio.play();
        }

        setTimeout(function () {
            __doPostBack("InstrumentSelect", id);
        }, 1300);
    }

</script>

</body>
</html>