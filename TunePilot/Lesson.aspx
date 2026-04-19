<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Lesson.aspx.cs" Inherits="TunePilot.Lesson" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Lesson</title>
</head>
<body>
<form id="form1" runat="server">

    <!-- COURSE INFO -->
    <asp:Label ID="LabelInstrument" runat="server" />
    <asp:Label ID="LabelInstrumentDifficult" runat="server" />

    <hr />

    <!-- LESSON BUTTONS -->
    <asp:Button ID="Tutorial1" runat="server" OnClick="SelectLesson" />
    <asp:Button ID="Tutorial2" runat="server" OnClick="SelectLesson" />
    <asp:Button ID="Tutorial3" runat="server" OnClick="SelectLesson" />

    <hr />

    <!-- LESSON INFO -->
    <h3><asp:Label ID="LabelLessonIndex" runat="server" /></h3>
    <asp:Label ID="LabelLessonDescription" runat="server" /><br />
    <asp:Label ID="LabelDuration" runat="server" />

    <hr />

    <!-- CONTENT LINKS -->
    <asp:HyperLink ID="Label1" runat="server" Target="_blank" /><br />
    <asp:HyperLink ID="Label2" runat="server" Target="_blank" /><br />
    <asp:HyperLink ID="Label3" runat="server" Target="_blank" /><br />
    <asp:HyperLink ID="Label4" runat="server" Target="_blank" /><br />
    <asp:HyperLink ID="Label5" runat="server" Target="_blank" /><br />

    <hr />

    <!-- VIDEO -->
    <video id="VideoPlayer" runat="server" controls></video>

    <hr />

    <!-- NAVIGATION -->
    <asp:ImageButton ID="left" runat="server" ImageUrl="~/resources/homepage/left.png" OnClick="PrevLesson" />
    <asp:Button ID="Complete" runat="server" Text="Complete" OnClick="ToggleComplete" />
    <asp:ImageButton ID="right" runat="server" ImageUrl="~/resources/homepage/right.png" OnClick="NextLesson" />

</form>
</body>
</html>