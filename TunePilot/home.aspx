<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="home.aspx.cs" Inherits="TunePilot.home" MasterPageFile="~/navbar.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div runat="server" class="container">
         <h1 style="text-align: center">Welcome to TunePilot</h1>

        <br />
        <br />
        <h3 style="text-align: center">Use TunePilot .to learn musical instruments anytime and anywhere.</h3>

        <video style="width: 70%; display: block; margin: 20px auto;" autoplay muted controls>
            <source src="\resources\homepage\Layla.mp4" type="video/mp4">
            <source src="\resources\homepage\Layla.mp4" type="video/ogg">
            Your browser does not support the video tag.
        </video>
        <br />

        <div style="display: flex; align-items: flex-start;">

            <!-- LEFT SIDE (image + arrows) -->
            <div >
                 <div style="display: flex; align-items: center; margin-left: 40px;">

                <img src="resources/homepage/left.png"
                    onclick="prevImage()"
                    style="width: 50px; height: 50px; position: relative; top: -10px; cursor: pointer;" />

                <img id="imgGallery"
                    src="resources/homepage/acoustic.jpg"
                    style="width: 450px; height: 250px; margin: 20px;" />

                <img src="resources/homepage/right.png"
                    onclick="nextImage()"
                    style="width: 50px; height: 50px; position: relative; top: -10px; cursor: pointer;" />

            </div>
                <div style="display: flex;justify-content: center;align-items: center;">
                    <asp:Button ID="buttonGetStart" runat="server" Text="Get Start" style="margin:10px;width:100px" OnClick="buttonGetStart_Click"/>
                    <asp:Button ID="buttonAboutUs" runat="server" Text="About Us" style="margin:10px;width:100px" OnClick="buttonAboutUs_Click"/>
                </div>
            </div>
           


            <!-- RIGHT SIDE (text) -->
            <div style="margin: 30px; max-width: 500px;">
                <p>
                    TunePilot is a user-friendly online learning platform that provides structured music lessons for instruments such as guitar,
                    drums, and trumpet, catering to beginners and hobby learners. The platform integrates multimedia content, 
                    quizzes, and progress tracking features to create an engaging and flexible learning experience. Designed for students, working adults, 
                    and casual learners, TunePilot allows users to learn at their own pace without the constraints of traditional classes. With its accessible design and free content,
                    it encourages users to explore music education conveniently while building foundational skills and confidence.

                </p>

            </div>

        </div>

    </div>
     <script>
         var images = [
             "resources/homepage/acoustic.jpg",
             "resources/homepage/drum.jpg",
             "resources/homepage/drum2.jpeg",
             "resources/homepage/trumpet.jpg"
         ];

         var index = 0;

         function nextImage() {
             index++;
             if (index >= images.length) index = 0;
             document.getElementById("imgGallery").src = images[index];
         }

         function prevImage() {
             index--;
             if (index < 0) index = images.length - 1;
             document.getElementById("imgGallery").src = images[index];
         }
     </script>
</asp:Content>