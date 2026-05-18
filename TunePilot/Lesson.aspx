<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Lesson.aspx.cs" Inherits="TunePilot.Lesson" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title>Lesson</title>

    <style>
        .lesson-btn {
            display: block;
            margin: 6px 0;
            background: none;
            border: none;
            cursor: pointer;
            font-size: 16px;
        }

        .current {
            color: green;
            font-weight: bold;
        }

        .completed {
            color: green;
        }

        .normal {
            color: black;
        }
    </style>
</head>

<body>
    <form runat="server">

        <h2>
            <asp:Label ID="LabelInstrument" runat="server" />
            -
        <asp:Label ID="LabelLevel" runat="server" />
        </h2>

        <hr />
        <!-- LESSON LIST -->
        <asp:PlaceHolder
            ID="LessonContainer"
            runat="server"></asp:PlaceHolder>

        <hr />

        <!-- CURRENT LESSON TITLE -->
        <h2>
            <asp:Label
                ID="LessonTitle"
                runat="server" />
        </h2>

        <!-- LESSON CONTENT FILES -->
        <h3>Lesson Resources</h3>

        <asp:PlaceHolder
            ID="ContentContainer"
            runat="server"></asp:PlaceHolder>

        <hr />

        <!-- VIDEO -->
        <video
            id="VideoPlayer"
            runat="server"
            width="700"
            controls>
        </video>

        <hr />

        <!-- SUMMARY -->
        <h3>Summary</h3>

        <asp:Label
            ID="LessonDesc"
            runat="server" />

        <br />
        <br />

        <asp:Label
            ID="LessonDuration"
            runat="server" />

        <hr />

        <!-- NAV -->
        <asp:Button ID="PrevBtn" runat="server" Text="Prev" OnClick="Prev_Click" />
        <asp:Button ID="CompleteBtn" runat="server" Text="Complete" OnClick="Complete_Click" />
        <asp:Button ID="NextBtn" runat="server" Text="Next" OnClick="Next_Click" />

        <br />
        <br />

        <asp:Button ID="BackBtn" runat="server" Text="Back" OnClick="Back_Click" />

    </form>
</body>
</html>
