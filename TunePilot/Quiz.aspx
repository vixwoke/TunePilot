<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Quiz.aspx.cs" Inherits="TunePilot.Quiz" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            Quiz<br />
            <asp:PlaceHolder
                ID="QuestionContainer"
                runat="server"></asp:PlaceHolder>
            <hr />
            <asp:Label ID="QuizTitle" runat="server" Text="Label"></asp:Label>
        </div>
        <asp:Label ID="QuizDecription" runat="server" Text="Label"></asp:Label>
        <asp:Label ID="PassingScore" runat="server" Text="Label"></asp:Label>
        <hr />
        <asp:Label ID="QuizQuestion" runat="server" Text="Label"></asp:Label>
        <br />
        <asp:Button ID="QuizOption1" runat="server" OnClick="Option_Click" />
        <asp:Button ID="QuizOption2" runat="server" OnClick="Option_Click" />
        <asp:Button ID="QuizOption3" runat="server" OnClick="Option_Click" />
        <asp:Button ID="QuizOption4" runat="server" OnClick="Option_Click" />
        <hr />
        <asp:Button ID="PrevBtn" runat="server" OnClick="Prev_Click" Text="Prev" />
        <asp:Button ID="CompleteBtn" runat="server" OnClick="Complete_Click" Text="Complete" />
        <asp:Button ID="NextBtn" runat="server" OnClick="Next_Click" Text="Next" />
        <br />
        <asp:Button ID="BackBtn" runat="server" Text="Back" OnClick="Back_Click" />
        <hr />
        Previos Attempt
        <br />
                    <asp:PlaceHolder
                ID="Attempt"
                runat="server"></asp:PlaceHolder>

    </form>
</body>
</html>
