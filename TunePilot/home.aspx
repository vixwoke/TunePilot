<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="home.aspx.cs" Inherits="TunePilot.home" MasterPageFile="~/navbar.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- HERO SECTION -->
    <div class="hero-section">
        <video class="hero-bg-video" autoplay muted loop playsinline>
            <source src="\resources\homepage\Layla.mp4" type="video/mp4">
            Your browser does not support the video tag.
        </video>
        <div class="hero-overlay"></div>
        <div class="hero-content">
            <h1>Master Any Instrument, At Your Own Pace</h1>
            <p>
                Free, flexible music education for everyone. Learn guitar, drums, trumpet and more
                through structured lessons, quizzes, and exams — anytime, anytime.
            </p>
            <div class="hero-buttons">
                <asp:Button ID="buttonGetStart" runat="server" Text="Get Started" CssClass="btn-primary" OnClick="buttonGetStart_Click" />
                <asp:Button ID="buttonAboutUs" runat="server" Text="Learn More" CssClass="btn-secondary" OnClick="buttonAboutUs_Click" />
            </div>
        </div>
    </div>

    <!-- CONTENT SECTION -->
    <div class="container">
        <div class="home-content-layout">
            <div class="home-gallery">
                <div class="gallery-controls">
                    <img src="resources/homepage/left.png" onclick="prevImage()" class="gallery-arrow" />
                    <img id="imgGallery" src="resources/homepage/acoustic.jpg" class="gallery-image" />
                    <img src="resources/homepage/right.png" onclick="nextImage()" class="gallery-arrow" />
                </div>
            </div>
            <div class="home-description">
                <h3>Start Your Musical Journey</h3>
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