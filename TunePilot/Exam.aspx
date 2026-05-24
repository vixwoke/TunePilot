<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Exam.aspx.cs" Inherits="TunePilot.Exam" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Exam</title>
    <link rel="stylesheet" href="css/style.css" />
</head>
<body>
<form id="form1" runat="server">

    <%-- Exam heading --%>
    <h2><asp:Label ID="ExamTitle" runat="server" /></h2>
    <p><asp:Label ID="ExamDescription" runat="server" /></p>
    <p><asp:Label ID="PassingScore" runat="server" /></p>

    <hr />

    <%-- Question dots --%>
    <asp:PlaceHolder ID="QuestionStatusContainer" runat="server" />

    <%-- Question number --%>
    <asp:Label ID="QuestionCounter" runat="server" />

    <hr />

    <%-- Question Instructions --%>
    <h3>Instruction</h3>
    <asp:Label ID="QuestionInstruction" runat="server" />

    <br /><br />

    <%-- Expected Notes --%>
    <asp:HiddenField ID="ExpectedNotesField" runat="server" />

    <%-- Recording Controls --%>
    <button type="button" id="StartRecordingBtn" onclick="startRecording()">Start Recording</button>
    <button type="button" id="StopRecordingBtn" onclick="stopRecording()" disabled>Stop Recording</button>

    <br /><br />
    <asp:Label ID="RecordingStatus" runat="server" Text="Press Start Recording when ready." />

    <br /><br />

    <%-- Detected Notes --%>
    <asp:HiddenField ID="DetectedNotesField" runat="server" />
    <asp:HiddenField ID="AccuracyField" runat="server" />

    <%-- Display --%>
    <p>Detected notes: <asp:Label ID="DetectedNotesDisplay" runat="server" Text="—" /></p>
    <p>Expected notes: <asp:Label ID="ExpectedNotesDisplay" runat="server" Text="—" /></p>
    <p>Accuracy: <asp:Label ID="AccuracyDisplay" runat="server" Text="—" /></p>

    <hr />

    <%-- Navigation --%>
    <asp:Button ID="PrevBtn" runat="server" Text="Prev" OnClick="Prev_Click" />
    <asp:Button ID="NextBtn" runat="server" Text="Next" OnClick="Next_Click" />
    <asp:Button ID="SubmitBtn" runat="server" Text="Submit Exam" OnClick="Submit_Click" Visible="false" />

    <br /><br />
    <asp:Button ID="BackBtn" runat="server" Text="Back to Dashboard" OnClick="Back_Click" />

    <hr />

    <%-- Previous attempts --%>
    <h3>Previous Attempts</h3>
    <asp:PlaceHolder ID="AttemptsContainer" runat="server" />

</form>

<script></script>

</body>
</html>