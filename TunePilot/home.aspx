<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="home.aspx.cs" Inherits="TunePilot.home" MasterPageFile="~/navbar.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>TunePilot - Home</title>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <!-- ===== HERO ===== -->
    <section class="home-hero">
        <video class="home-hero-video" autoplay muted loop playsinline>
            <source src="\resources\homepage\Layla.mp4" type="video/mp4">
        </video>
        <div class="home-hero-overlay"></div>

        <div class="home-hero-decor">
            <div class="decor-ring" style="width:480px;height:480px;top:-120px;right:-80px;border-color:rgba(255,255,255,0.05);"></div>
            <div class="decor-ring" style="width:240px;height:240px;bottom:10%;left:-60px;border-color:rgba(255,255,255,0.04);"></div>
            <div class="decor-ring" style="width:120px;height:120px;top:25%;left:55%;border-color:rgba(255,255,255,0.03);"></div>
            <div class="decor-dot" style="width:10px;height:10px;top:30%;right:22%;background:var(--tp-color-strong);opacity:0.25;"></div>
            <div class="decor-dot" style="width:6px;height:6px;bottom:35%;left:35%;background:var(--tp-color-strong);opacity:0.2;"></div>
            <div class="decor-arc" style="bottom:18%;right:25%;transform:rotate(35deg);border-color:rgba(255,255,255,0.04);"></div>
            <div class="decor-line" style="width:120px;top:40%;left:18%;background:linear-gradient(90deg,transparent,rgba(255,255,255,0.06),transparent);"></div>
            <div class="decor-dots-grid" style="top:15%;right:12%;opacity:0.4;"></div>
            <div class="decor-glow" style="width:400px;height:400px;bottom:-80px;left:20%;background:radial-gradient(circle,rgba(165,157,132,0.08) 0%,transparent 70%);"></div>
        </div>

        <div class="home-hero-content">
            <h1 class="home-hero-heading">
                <span class="hh-light">Master Any</span>
                <span class="hh-bold">Instrument.</span>
            </h1>
            <p class="home-hero-text">
                Free, flexible music education for everyone. Learn guitar, drums, trumpet and more
                through structured lessons, quizzes, and exams — anytime, anywhere.
            </p>
            <div class="home-hero-buttons">
                <asp:Button ID="buttonGetStart" runat="server" Text="Get Started" CssClass="hh-btn-primary" OnClick="buttonGetStart_Click" />
                <asp:Button ID="buttonTryCourse" runat="server" Text="Try Course" CssClass="hh-btn-secondary" OnClick="buttonTryCourse_Click" />
                <asp:Button ID="buttonAboutUs" runat="server" Text="Learn More" CssClass="hh-btn-secondary" OnClick="buttonAboutUs_Click" />
            </div>
        </div>
    </section>

    <!-- ===== FEATURES ===== -->
    <section class="home-section">
        <div class="home-section-inner">
            <div class="home-section-header">
                <span class="hp-badge">What We Offer</span>
                <h2 class="hp-title">Start Your <span class="hp-gradient-text">Musical Journey</span></h2>
                <p class="hp-desc">Everything you need to learn music, completely free.</p>
            </div>

            <div class="feature-grid">
                <div class="feature-card">
                    <div class="feature-card-icon">&#9835;</div>
                    <h3>Structured Lessons</h3>
                    <p>Step-by-step courses with video and written materials for every skill level.</p>
                </div>
                <div class="feature-card">
                    <div class="feature-card-icon">&#9836;</div>
                    <h3>Interactive Quizzes</h3>
                    <p>Test your knowledge after each lesson to reinforce what you've learned.</p>
                </div>
                <div class="feature-card">
                    <div class="feature-card-icon">&#9834;</div>
                    <h3>Progress Tracking</h3>
                    <p>See exactly which lessons you've completed and how far you've come.</p>
                </div>
                <div class="feature-card">
                    <div class="feature-card-icon">&#9839;</div>
                    <h3>Certified Exams</h3>
                    <p>Earn certificates to showcase your skills for each instrument you master.</p>
                </div>
            </div>
        </div>
    </section>

    <!-- ===== GALLERY + ABOUT ===== -->
    <section class="home-section home-section-alt">
        <div class="gallery-layout">
            <div class="gallery-text">
                <h2 class="hp-title">Learn From <span class="hp-gradient-text">Anywhere</span></h2>
                <p>
                    TunePilot is a user-friendly online learning platform that provides structured music lessons
                    for instruments such as guitar, drums, and trumpet, catering to beginners and hobby learners.
                </p>
                <p>
                    Designed for students, working adults, and casual learners — you can learn at your own pace
                    without the constraints of traditional classes.
                </p>
            </div>
            <div class="gallery-visual">
                <div class="gallery-frame">
                    <div class="gallery-frame-border"></div>
                    <img id="imgGallery" src="resources/homepage/acoustic.jpg" class="gallery-img" />
                    <div class="gallery-shine"></div>
                </div>
                <div class="gallery-controls">
                    <button type="button" onclick="prevImage()" class="gallery-btn" aria-label="Previous image">
                        <svg width="20" height="20" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round">
                            <polyline points="15 18 9 12 15 6" />
                        </svg>
                    </button>
                    <span class="gallery-counter"><span id="galleryCurrent">1</span> / <span id="galleryTotal">4</span></span>
                    <button type="button" onclick="nextImage()" class="gallery-btn" aria-label="Next image">
                        <svg width="20" height="20" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round">
                            <polyline points="9 18 15 12 9 6" />
                        </svg>
                    </button>
                </div>
            </div>
        </div>
    </section>

    <script>
        var images = [
            "resources/homepage/acoustic.jpg",
            "resources/homepage/drum.jpg",
            "resources/homepage/drum2.jpeg",
            "resources/homepage/trumpet.jpg"
        ];
        var index = 0;

        function nextImage() {
            index = (index + 1) % images.length;
            document.getElementById("imgGallery").src = images[index];
            document.getElementById("galleryCurrent").textContent = index + 1;
        }

        function prevImage() {
            index = (index - 1 + images.length) % images.length;
            document.getElementById("imgGallery").src = images[index];
            document.getElementById("galleryCurrent").textContent = index + 1;
        }
    </script>

</asp:Content>
