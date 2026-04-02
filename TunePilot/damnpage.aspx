<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="damnpage.aspx.cs" Inherits="TunePilot.damnpage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="~/css/style.css" rel="stylesheet" runat="server" />
</head>
<body>
    <form id="form1" runat="server">
        <div style="display:flex; justify-content:center; align-items:center; height:100vh;" id="divFirst">
            <p style="padding:15px 30px; font-size:18px;">very fun →</p>
            <asp:Button 
                Text="Start fun"
                runat="server" 
                style="padding:15px 30px; font-size:18px;" ID="btnStart" OnClientClick="hideStartFun(); return false;" />
            <p style="padding:15px 30px; font-size:18px;">← very fun</p>
        </div>
        <div id="divJumpscare" style="display:none">
            <asp:HyperLink 
                NavigateUrl="home.aspx" 
                CssClass="btn btn-primary" 
                style="height:20px;"
                runat="server">
                Press this. I'll bring you back to the homepage
            </asp:HyperLink>
            <br />
            <img alt="shafou" style="width:100vw;height:90vh" longdesc="a shafou. why would I explain what is shafou." src="resources/img/shafou.jpg" />
            <audio id="scary">
                <source src="resources/sfx/scarymazegamesound.mp3" type="audio/mpeg"/>
            </audio>
        </div>
        <script>
            function hideJumpscare() {
                document.getElementById("divJumpscare").style.display = "none";
            }
            function hideStartFun() {
                document.getElementById("divFirst").style.display = "none";
                document.getElementById("divJumpscare").style.display = "block";
                var audio = document.getElementById("scary");
                audio.play();
                audio.addEventListener("ended", function () {
                    audio.currentTime = 0;
                    audio.play();
                });
            }
        </script>
    </form>

</body>
</html>


