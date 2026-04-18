<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StudentDashboard.aspx.cs" Inherits="TunePilot.StudentDashboard" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <audio id="guitarSound" preload="auto">
            <source src="/resources/studentDashboard/guitarVoice.mp3" type="audio/mpeg">
        </audio>

        <audio id="drumSound" preload="auto">
            <source src="/resources/studentDashboard/drumsVoice.mp3" type="audio/mpeg">
        </audio>

        <audio id="trumpetSound" preload="auto">
            <source src="/resources/studentDashboard/trumpetVoice.mp3" type="audio/mpeg">
        </audio>
        <div>
            <!--on click session instrument = guitar/drum/trumpet
            if session = guitar GuitarIcon URL =guitar1-->
            <img src="/resources/studentDashboard/guitar1.png" alt="Guitar" onclick="guitarSelect()" id="GuitarIcon" style="width: 80px; border: solid 2px" />
            <img src="/resources/studentDashboard/drum.png" alt="Drum" onclick="drumSelect()" id="DrumIcon" style="width: 80px; border: solid 2px" />
            <img src="/resources/studentDashboard/trumpet.jpg" alt="Trumpet" onclick="trumpetSelect()" id="TrumpetIcon" style="width: 80px; border: solid 2px" />
        </div>

        <asp:Label ID="LabelInstrumentName" runat="server" Text="InstrumentName"></asp:Label>
        <br />
        <asp:Label ID="LabelDescription" runat="server" Text="InstrumentDescription"></asp:Label>
        <hr />

        <h2>Media Tutorial</h2>
        <!--on click session course = beginner, casual,advance and type = media-->
        <a href="#">
            <asp:Label ID="Media1" runat="server" Text="Beginner"></asp:Label></a>
        <br />
        <a href="#">
            <asp:Label ID="Media2" runat="server" Text="Casual"></asp:Label></a>
        <br />
        <a href="#">
            <asp:Label ID="Media3" runat="server" Text="Advance"></asp:Label></a>
        <br />
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
        <h2>Quiz</h2>
        <!--on click session course = beginner, casual,advance and type = media-->
        <a href="#">
            <asp:Label ID="Quiz1" runat="server" Text="Beginner"></asp:Label></a>
        <br />
        <a href="#">
            <asp:Label ID="Quiz2" runat="server" Text="Casual"></asp:Label></a>
        <br />
        <a href="#">
            <asp:Label ID="Quiz3" runat="server" Text="Advance"></asp:Label></a>
        <br />
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
        <h2>Exam</h2>
        <!--on click session course = beginner, casual,advance and type = exam-->
        <a href="#">
            <asp:Label ID="Exam1" runat="server" Text="Beginner"></asp:Label></a>
        <br />
        <a href="#">
            <asp:Label ID="Exam2" runat="server" Text="Casual"></asp:Label></a>
        <br />
        <a href="#">
            <asp:Label ID="Exam3" runat="server" Text="Advance"></asp:Label></a>
        <br />
        <asp:Label ID="LabelExamAttempt" runat="server" Text="Exam Attempt: "></asp:Label>
        <br />
        <asp:Label ID="LabelExamScore" runat="server" Text="%"></asp:Label>
        <br />
        <asp:Image ID="ExamProgress1" runat="server" ImageUrl="~/resources/studentDashboard/square.png" Height="148px" Width="157px" />
        <asp:Image ID="ExamProgress2" runat="server" ImageUrl="~/resources/studentDashboard/square.png" Height="148px" Width="157px" />
        <asp:Image ID="ExamProgress3" runat="server" ImageUrl="~/resources/studentDashboard/square.png" Height="148px" Width="157px" />

    </form>
</body>
<script>
    function guitarSelect() {
        //if (Session["instrument"] == null || Session["instrument"].ToString() != "Guitar") {
        //  Session["instrument"].ToString = "Guitar";
        //}
        let audio = document.getElementById("guitarSound");

        audio.pause();        // stop if already playing
        audio.currentTime = 0; // restart from beginning
        audio.play();         // play on click
        document.getElementById("GuitarIcon").src = "/resources/studentDashboard/guitar1.png";
        document.getElementById("DrumIcon").src = "/resources/studentDashboard/drum.png";
        document.getElementById("TrumpetIcon").src = "/resources/studentDashboard/trumpet.jpg";
    }
    function drumSelect() {
        // if (Session["instrument"] == null || Session["instrument"].ToString() != "Drums") {
        //   Session["instrument"].ToString = "Drums";
        //}
        let audio = document.getElementById("drumSound");

        audio.pause();        // stop if already playing
        audio.currentTime = 0; // restart from beginning
        audio.play();         // play on click
        document.getElementById("GuitarIcon").src = "/resources/studentDashboard/guitar.jpg";
        document.getElementById("DrumIcon").src = "/resources/studentDashboard/drum1.png";
        document.getElementById("TrumpetIcon").src = "/resources/studentDashboard/trumpet.jpg";
    }
    function trumpetSelect() {
        //if (Session["instrument"] == null || Session["instrument"].ToString() != "Trumpet") {
        //   Session["instrument"].ToString = "Trumpet";
        //}
        let audio = document.getElementById("trumpetSound");

        audio.pause();        // stop if already playing
        audio.currentTime = 0; // restart from beginning
        audio.play();         // play on click
        document.getElementById("GuitarIcon").src = "/resources/studentDashboard/guitar.jpg";
        document.getElementById("DrumIcon").src = "/resources/studentDashboard/drum.png";
        document.getElementById("TrumpetIcon").src = "/resources/studentDashboard/trumpet1.png";
    }
</script>
</html>
