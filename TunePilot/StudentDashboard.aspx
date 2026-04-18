<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StudentDashboard.aspx.cs" Inherits="TunePilot.StudentDashboard" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Student Dashboard</title>
</head>
<body>
    <form id="form1" runat="server">

        <!-- Hidden field -->
        <asp:HiddenField ID="HiddenInstrument" runat="server" />

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
        <asp:Label ID="RoleLabel" runat="server" Text=""></asp:Label>

        <!-- INSTRUMENT ICONS -->
        <div>
            <asp:Image ID="GuitarIcon" runat="server"
                ImageUrl="/resources/studentDashboard/guitar1.png"
                Width="80px"
                Style="border: 2px solid gray"
                onclick="selectInstrument(1)" />

            <asp:Image ID="DrumIcon" runat="server"
                ImageUrl="/resources/studentDashboard/drum.png"
                Width="80px"
                Style="border: 2px solid gray"
                onclick="selectInstrument(2)" />

            <asp:Image ID="TrumpetIcon" runat="server"
                ImageUrl="/resources/studentDashboard/trumpet.jpg"
                Width="80px"
                Style="border: 2px solid gray"
                onclick="selectInstrument(3)" />
        </div>

        <!-- Instrument Info -->
        <asp:Label ID="LabelInstrumentName" runat="server"></asp:Label>
        <asp:Label ID="LabelCategory" runat="server" ></asp:Label><br />
        <asp:Label ID="LabelDescription" runat="server"></asp:Label>

        <hr />

        <!-- MEDIA -->
        <h2>Media Tutorial</h2>

        <asp:LinkButton ID="MediaBtn1" runat="server" OnClick="SelectCourse" CommandArgument="1">
            <asp:Label ID="Media1" runat="server" Text="Beginner" /><br />
        </asp:LinkButton><asp:LinkButton ID="MediaBtn2" runat="server" OnClick="SelectCourse" CommandArgument="2">
            <asp:Label ID="Media2" runat="server" Text="Casual" /><br />
        </asp:LinkButton><asp:LinkButton ID="MediaBtn3" runat="server" OnClick="SelectCourse" CommandArgument="3">
            <asp:Label ID="Media3" runat="server" Text="Advance" />
        </asp:LinkButton><br />

        <!-- MEDIA PROGRESS -->
        <asp:Image ID="MediaProgress1" runat="server" ImageUrl="~/resources/studentDashboard/square.png" Height="148px" Width="157px" />
        <asp:Image ID="MediaProgress2" runat="server" ImageUrl="~/resources/studentDashboard/square.png" Height="148px" Width="157px" />
        <asp:Image ID="MediaProgress3" runat="server" ImageUrl="~/resources/studentDashboard/square.png" Height="148px" Width="157px" />
        <asp:Image ID="MediaProgress4" runat="server" ImageUrl="~/resources/studentDashboard/square.png" Height="148px" Width="157px" />
        <asp:Image ID="MediaProgress5" runat="server" ImageUrl="~/resources/studentDashboard/square.png" Height="148px" Width="157px" />
        <asp:Image ID="MediaProgress6" runat="server" ImageUrl="~/resources/studentDashboard/square.png" Height="148px" Width="157px" />
        <asp:Image ID="MediaProgress7" runat="server" ImageUrl="~/resources/studentDashboard/square.png" Height="148px" Width="157px" />
        <asp:Image ID="MediaProgress8" runat="server" ImageUrl="~/resources/studentDashboard/square.png" Height="148px" Width="157px" />
        <asp:Image ID="MediaProgress9" runat="server" ImageUrl="~/resources/studentDashboard/square.png" Height="148px" Width="157px" />

        <hr />

        <!-- QUIZ -->
        <h2>Quiz</h2><asp:LinkButton ID="QuizBtn1" runat="server" OnClick="SelectQuiz">
            <asp:Label ID="Quiz1" runat="server" Text="Quiz 1" /></asp:LinkButton><br />
        <asp:LinkButton ID="QuizBtn2" runat="server" OnClick="SelectQuiz">
            <asp:Label ID="Quiz2" runat="server" Text="Quiz 2" /></asp:LinkButton><br />
        <asp:LinkButton ID="QuizBtn3" runat="server" OnClick="SelectQuiz">
            <asp:Label ID="Quiz3" runat="server" Text="Quiz 3" /></asp:LinkButton><br />
        <asp:LinkButton ID="QuizBtn4" runat="server" OnClick="SelectQuiz">
            <asp:Label ID="Quiz4" runat="server" Text="Quiz 4" /></asp:LinkButton><br />
        <asp:LinkButton ID="QuizBtn5" runat="server" OnClick="SelectQuiz">
            <asp:Label ID="Quiz5" runat="server" Text="Quiz 5" /></asp:LinkButton><br />
        <asp:LinkButton ID="QuizBtn6" runat="server" OnClick="SelectQuiz">
            <asp:Label ID="Quiz6" runat="server" Text="Quiz 6" /></asp:LinkButton><br />
        <asp:LinkButton ID="QuizBtn7" runat="server" OnClick="SelectQuiz">
            <asp:Label ID="Quiz7" runat="server" Text="Quiz 7" /></asp:LinkButton><br />
        <asp:LinkButton ID="QuizBtn8" runat="server" OnClick="SelectQuiz">
            <asp:Label ID="Quiz8" runat="server" Text="Quiz 8" /></asp:LinkButton><br />
        <asp:LinkButton ID="QuizBtn9" runat="server" OnClick="SelectQuiz">
            <asp:Label ID="Quiz9" runat="server" Text="Quiz 9" /></asp:LinkButton><br />

        <!-- QUIZ PROGRESS -->
        <asp:Image ID="QuizProgress1" runat="server" ImageUrl="~/resources/studentDashboard/square.png" Height="148px" Width="157px" />
        <asp:Image ID="QuizProgress2" runat="server" ImageUrl="~/resources/studentDashboard/square.png" Height="148px" Width="157px" />
        <asp:Image ID="QuizProgress3" runat="server" ImageUrl="~/resources/studentDashboard/square.png" Height="148px" Width="157px" />
        <asp:Image ID="QuizProgress4" runat="server" ImageUrl="~/resources/studentDashboard/square.png" Height="148px" Width="157px" />
        <asp:Image ID="QuizProgress5" runat="server" ImageUrl="~/resources/studentDashboard/square.png" Height="148px" Width="157px" />
        <asp:Image ID="QuizProgress6" runat="server" ImageUrl="~/resources/studentDashboard/square.png" Height="148px" Width="157px" />
        <asp:Image ID="QuizProgress7" runat="server" ImageUrl="~/resources/studentDashboard/square.png" Height="148px" Width="157px" />
        <asp:Image ID="QuizProgress8" runat="server" ImageUrl="~/resources/studentDashboard/square.png" Height="148px" Width="157px" />
        <asp:Image ID="QuizProgress9" runat="server" ImageUrl="~/resources/studentDashboard/square.png" Height="148px" Width="157px" />

        <hr />

        <!-- EXAM -->
        <h2>Exam</h2><asp:LinkButton ID="ExamBtn1" runat="server" OnClick="SelectExam">
            <asp:Label ID="Exam1" runat="server" Text="Beginner" /></asp:LinkButton><br />
        <asp:LinkButton ID="ExamBtn2" runat="server" OnClick="SelectExam">
            <asp:Label ID="Exam2" runat="server" Text="Casual" /></asp:LinkButton><br />
        <asp:LinkButton ID="ExamBtn3" runat="server" OnClick="SelectExam">
            <asp:Label ID="Exam3" runat="server" Text="Advance" /></asp:LinkButton><br />

        <asp:Label ID="LabelExamAttempt" runat="server" Text="Exam Attempt: "></asp:Label><br />
        <asp:Label ID="LabelExamScore" runat="server" Text="%"></asp:Label><asp:Image ID="ExamProgress1" runat="server" ImageUrl="~/resources/studentDashboard/square.png" Height="148px" Width="157px" />
        <asp:Image ID="ExamProgress2" runat="server" ImageUrl="~/resources/studentDashboard/square.png" Height="148px" Width="157px" />
        <asp:Image ID="ExamProgress3" runat="server" ImageUrl="~/resources/studentDashboard/square.png" Height="148px" Width="157px" />

    </form>

    <script>
        function selectInstrument(id) {

            let sounds = ["guitarSound", "drumSound", "trumpetSound"];
            let audio = document.getElementById(sounds[id - 1]);

            if (audio) {
                audio.pause();
                audio.currentTime = 0;
                audio.play();
            }

            // Delay postback 2 seconds
            setTimeout(function () {
                __doPostBack("InstrumentSelect", id);
            }, 1300);
        }
    </script>

</body>
</html>
