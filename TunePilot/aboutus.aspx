<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="aboutus.aspx.cs" Inherits="TunePilot.aboutus" MasterPageFile="~/navbar.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>TunePilot - About Us</title>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <!-- Banner -->
    <div class="about-banner">
        <div class="about-banner-overlay"></div>
        <div class="about-banner-text">
            <h1>About TunePilot</h1>
            <p>Free, flexible music education for everyone — anytime, anywhere.</p>
        </div>
    </div>

    <!-- About Us -->
    <div class="about-section">
        <h2>Who we are</h2>
        <p>
            TunePilot is an easy to access, online platform that makes music education easy to acquire
            and engaging, so that users of all ages, regardless of skill level and financial ability can learn
            and master instruments.
        </p>
        <p>
            Low prices. No fixed class schedules. Just open the browser and learn
            at your own pace, whether you have five minutes or five hours.
        </p>
    </div>

    <hr class="section-divider" />

    <!-- What we offer -->
    <div class="about-section">
        <h2>What we offer</h2>
        <div class="offer-grid">
            <div class="offer-card">
                <h3>Structured lessons</h3>
                <p>Step-by-step courses from beginner to advanced, with video and written materials.</p>
            </div>
            <div class="offer-card">
                <h3>Quizzes</h3>
                <p>Test your knowledge after each lesson to reinforce what you've learned.</p>
            </div>
            <div class="offer-card">
                <h3>Progress tracking</h3>
                <p>See exactly which lessons you've completed and how far you've come.</p>
            </div>
            <div class="offer-card">
                <h3>Exams</h3>
                <p>Sit a final exam to certify your skill level for each instrument.</p>
            </div>
        </div>
    </div>

    <hr class="section-divider" />

    <!-- Reviews carousel -->
    <div class="reviews-section">
        <h2>What our students say</h2>
        <div class="reviews-track-wrapper">
            <div class="reviews-track">

                <%-- First set --%>
                <div class="review-card">
                    <div class="review-stars">★★★★★</div>
                    <p>"I picked up basic guitar chords in two weeks. The lessons are clear and the videos really help."</p>
                    <div class="review-author">Ahmad R.</div>
                </div>
                <div class="review-card">
                    <div class="review-stars">★★★★★</div>
                    <p>"Finally a platform that lets me learn drums without paying for a physical class. Love it."</p>
                    <div class="review-author">Priya S.</div>
                </div>
                <div class="review-card">
                    <div class="review-stars">★★★★☆</div>
                    <p>"The quizzes after each lesson are a great touch. Helps me actually remember what I studied."</p>
                    <div class="review-author">Daniel K.</div>
                </div>
                <div class="review-card">
                    <div class="review-stars">★★★★★</div>
                    <p>"I study during lunch breaks. The short lessons fit perfectly into a busy schedule."</p>
                    <div class="review-author">Nurul H.</div>
                </div>
                <div class="review-card">
                    <div class="review-stars">★★★★★</div>
                    <p>"Progress tracking kept me motivated. Seeing those completed lessons add up feels great."</p>
                    <div class="review-author">Wei Liang C.</div>
                </div>
                <div class="review-card">
                    <div class="review-stars">★★★★☆</div>
                    <p>"Trumpet beginner here — the course structure makes it so easy to know what to do next."</p>
                    <div class="review-author">Fatimah Z.</div>
                </div>

                <%-- Duplicated set for seamless loop --%>
                <div class="review-card">
                    <div class="review-stars">★★★★★</div>
                    <p>"I picked up basic guitar chords in two weeks. The lessons are clear and the videos really help."</p>
                    <div class="review-author">Ahmad R.</div>
                </div>
                <div class="review-card">
                    <div class="review-stars">★★★★★</div>
                    <p>"Finally a platform that lets me learn drums without paying for a physical class. Love it."</p>
                    <div class="review-author">Priya S.</div>
                </div>
                <div class="review-card">
                    <div class="review-stars">★★★★☆</div>
                    <p>"The quizzes after each lesson are a great touch. Helps me actually remember what I studied."</p>
                    <div class="review-author">Daniel K.</div>
                </div>
                <div class="review-card">
                    <div class="review-stars">★★★★★</div>
                    <p>"I study during lunch breaks. The short lessons fit perfectly into a busy schedule."</p>
                    <div class="review-author">Nurul H.</div>
                </div>
                <div class="review-card">
                    <div class="review-stars">★★★★★</div>
                    <p>"Progress tracking kept me motivated. Seeing those completed lessons add up feels great."</p>
                    <div class="review-author">Wei Liang C.</div>
                </div>
                <div class="review-card">
                    <div class="review-stars">★★★★☆</div>
                    <p>"Trumpet beginner here — the course structure makes it so easy to know what to do next."</p>
                    <div class="review-author">Fatimah Z.</div>
                </div>

            </div>
        </div>
    </div>

    <hr class="section-divider" />

    <!-- The team -->
    <div class="about-section">
        <h2>The team</h2>

        <div class="team-member">
            <img src="/resources/aboutus/member1.jpg" alt="Daniel" />
            <div class="team-member-info">
                <h3>Daniel Christopher Widodo</h3>
                <div class="role">[Insert Role]</div>
                <p>[Insert Description]</p>
            </div>
        </div>

        <div class="team-member reverse">
            <img src="/resources/aboutus/member2.jpg" alt="Ng Jeechian" />
            <div class="team-member-info">
                <h3>Ng Jeechian</h3>
                <div class="role">[Insert Role]</div>
                <p>[Insert Description]</p>
            </div>
        </div>

        <div class="team-member">
            <img src="/resources/aboutus/member3.jpg" alt="Darren William" />
            <div class="team-member-info">
                <h3>Darren William</h3>
                <div class="role">[Insert Role]</div>
                <p>[Insert Description]</p>
            </div>
        </div>

        <div class="team-member reverse">
            <img src="/resources/aboutus/member4.png" alt="Gilang Suherlambang" />
            <div class="team-member-info">
                <h3>Gilang Suherlambang</h3>
                <div class="role">Website UI/UX</div>
                <p>Taking responsibility for the website design, including UI elements, layout structure, and styling with CSS to create a responsive and user-friendly experience. Developed and customized visual components, maintaining clean and modern design standards.</p>
                <a class="portfolio-link" href="https://vixwoke.com" target="_blank">
                    <span>View portfolio</span>
                    <strong>vixwoke.com</strong>
                </a>
            </div>
        </div>

        <div class="team-member">
            <img src="/resources/aboutus/member5.jpg" alt="Yoosuf Haami" />
            <div class="team-member-info">
                <h3>Yoosuf Haami</h3>
                <div class="role">[Insert Role]</div>
                <p>[Insert Description]</p>
            </div>
        </div>

    </div>

    <hr class="section-divider" />

    <!-- Contact Us -->
    <div class="contact-section">
        <h2>Contact us</h2>
        <p>Have a question, suggestion, or just want to say hello? We'd love to hear from you.</p>

        <div class="contact-layout">

            <div class="contact-details">
                <h3>Get in touch</h3>

                <div class="contact-detail-item">
                    <span class="detail-label">Email</span>
                    <span class="detail-value">hello@tunepilot.com</span>
                    <span class="detail-sub">We reply within 1–2 business days</span>
                </div>

                <div class="contact-detail-item">
                    <span class="detail-label">Phone</span>
                    <span class="detail-value">+60 12-345 6789</span>
                    <span class="detail-sub">Available during working hours</span>
                </div>

                <div class="contact-detail-item">
                    <span class="detail-label">Working hours</span>
                    <span class="detail-value">Monday – Friday, 9am – 6pm</span>
                    <span class="detail-sub">Malaysia Time (MYT, UTC+8)</span>
                </div>

                <div class="contact-detail-item">
                    <span class="detail-label">Support</span>
                    <span class="detail-value">support@tunepilot.com</span>
                    <span class="detail-sub">For technical issues and account help</span>
                </div>
            </div>

            <div class="contact-form-side">
                <h3>Send a message</h3>
                <div class="contact-form">
                    <input type="text" placeholder="Your name" />
                    <input type="email" placeholder="Your email" />
                    <textarea placeholder="Your message"></textarea>
                    <button type="button">Send message</button>
                </div>
            </div>

        </div>
    </div>

</asp:Content>
