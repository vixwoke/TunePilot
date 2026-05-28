<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="TunePilot.login" %>
<!DOCTYPE html>
<html lang="en">
<head runat="server">
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>TunePilot - Login</title>
    <link rel="preconnect" href="https://fonts.googleapis.com">
    <link rel="preconnect" href="https://fonts.gstatic.com" crossorigin>
    <link href="https://fonts.googleapis.com/css2?family=Figtree:wght@300;400;500;600;700;800;900&family=Plus+Jakarta+Sans:wght@200;300;400;500;600;700;800&display=swap" rel="stylesheet">
    <link rel="stylesheet" type="text/css" href="css/style.css" />
</head>
<body>
    <form id="form1" runat="server">

        <div class="split-layout">

            <!-- LEFT: Branding Panel (60%) -->
            <div class="split-left">
                <div class="split-left-decor">
                    <div class="decor-ring decor-ring-1"></div>
                    <div class="decor-ring decor-ring-2"></div>
                    <div class="decor-ring decor-ring-3"></div>
                    <div class="decor-dot decor-dot-1"></div>
                    <div class="decor-dot decor-dot-2"></div>
                    <div class="decor-arc decor-arc-1"></div>
                    <div class="decor-line decor-line-1"></div>
                    <div class="decor-line decor-line-2"></div>
                    <div class="decor-dots-grid"></div>
                    <div class="decor-glow decor-glow-1"></div>
                </div>

                <div class="split-left-content">
                    <div class="brand-logo" style="--nav_tp_full_color: rgba(255,255,255,0.9);">
                        <svg xmlns="http://www.w3.org/2000/svg" viewBox="52.5 404.53 884.33 181.85" class="login-logo-svg">
                            <defs>
                                <radialGradient id="lg-radial" cx="219.84" cy="566.82" r="14.56" fx="219.84" fy="566.82" gradientTransform="rotate(135 472.173 1265.015)scale(-1.23 2.77)" gradientUnits="userSpaceOnUse">
                                    <stop offset="0" stop-color="#fff"/><stop offset="1" stop-color="#fff" stop-opacity="0"/>
                                </radialGradient>
                                <radialGradient xlink:href="#lg-radial" id="lg-radial-2" cx="-558.98" cy="1251.73" r="14.56" fx="-558.98" fy="1251.73" gradientTransform="rotate(135 -797.861 -1211.297)scale(1.23 -2.77)"/>
                                <radialGradient id="lg-radial-3" cx="-139.82" cy="2074.48" r="10.83" fx="-139.82" fy="2074.48" gradientTransform="matrix(0 2.78 .82 0 -957.78 882.54)" gradientUnits="userSpaceOnUse">
                                    <stop offset="0" stop-color="#6c6d6d"/><stop offset="1" stop-color="#3f3f3f"/>
                                </radialGradient>
                                <radialGradient id="lg-radial-4" cx="-143.78" cy="1858.51" r="8.45" fx="-143.78" fy="1858.51" gradientTransform="matrix(0 2.87 1 0 -1107.16 907.15)" gradientUnits="userSpaceOnUse">
                                    <stop offset="0" stop-color="#f7f8f8"/><stop offset="1" stop-color="#e1e2e1"/>
                                </radialGradient>
                                <style>.lg-cls-10{fill:var(--nav_tp_full_color)}.lg-cls-3{fill:#fff;isolation:isolate;opacity:.74}.lg-cls-7{fill-rule:evenodd;fill:#010101}</style>
                            </defs>
                            <path d="M196.68 472.18h-22.69l-12.86 72.83h-33.24l12.86-72.83h-23.12l5.06-28.32h78.9zM198.42 515.39c0-3.9.43-8.38 1.3-13.15l10.26-58.38h32.66l-10.4 58.67c-.58 3.32-1.01 6.21-1.01 8.53 0 5.35 2.02 7.8 7.51 7.8 7.66 0 10.4-5.2 12.43-16.33l10.4-58.67h32.51l-10.4 59.1c-5.78 32.8-22.25 44.07-48.41 44.07-22.4 0-36.85-8.38-36.85-31.65ZM335.69 516.11c-2.02-3.61-4.33-7.66-6.79-11.56h-.43c-.72 5.06-1.59 10.12-2.46 15.17l-4.48 25.29h-32.08l17.92-101.15h22.4l16.91 29.77c1.73 3.03 3.9 6.65 5.92 9.97h.43c.58-5.64 1.45-11.42 2.46-17.05l4.05-22.69h32.08L373.7 545.01h-22.11zM432.8 470.88l-1.88 10.4h29.77l-4.48 25.29h-29.77l-2.02 11.27h33.96l-4.77 27.17h-66.47l17.77-101.15h65.46l-4.77 27.02zM584.39 471.17c0 29.04-19.8 46.96-51.73 46.96h-2.31l-4.77 26.88h-33.24l17.77-101.15h36.12c26.16 0 38.15 8.67 38.15 27.31Zm-33.67 6.94c0-4.91-2.75-7.08-9.1-7.08h-3.03l-3.61 20.95h2.6c8.53 0 13.15-4.91 13.15-13.87ZM599.13 443.86h33.24L614.6 545.01h-33.24zM645.8 443.86h32.66L665.6 516.4h32.95l-5.06 28.61h-65.46zM799.84 481.29c0 35.54-19.65 65.75-56.79 65.75-24.42 0-39.73-15.32-39.73-39.6 0-35.54 19.65-65.6 56.79-65.6 24.42 0 39.73 15.17 39.73 39.45M885.25 472.18h-22.69l-12.86 72.83h-33.24l12.86-72.83H806.2l5.06-28.32h78.9z" class="lg-cls-10"/>
                            <path fill="none" stroke="var(--nav_tp_full_color)" stroke-miterlimit="10" stroke-width="15" d="M873.02 578.88H89.28c-19.43 0-33.47-20.24-28.14-40.57l27.03-103.17c3.58-13.68 15.07-23.11 28.14-23.11h783.74c19.43 0 33.47 20.24 28.14 40.57l-27.03 103.17c-3.58 13.68-15.07 23.11-28.14 23.11Z"/>
                            <g id="disc">
                                <path fill="none" fill-rule="evenodd" stroke="#fff" stroke-miterlimit="10" stroke-width="2" d="M751.35 464.06c-16.58 0-30.03 13.46-30.03 30.03 0 16.58 13.46 30.03 30.03 30.03s30.03-13.46 30.03-30.03-13.46-30.03-30.03-30.03Zm0 29.19c.47 0 .84.38.84.84s-.38.84-.84.84-.84-.38-.84-.84.38-.84.84-.84Z"/>
                                <path d="M751.35 464.99c-16.07 0-29.11 13.04-29.11 29.11s13.04 29.11 29.11 29.11 29.11-13.04 29.11-29.11-13.04-29.11-29.11-29.11m0 28.29c.45 0 .82.37.82.82s-.37.82-.82.82-.82-.37-.82-.82.37-.82.82-.82" class="lg-cls-7"/>
                                <path fill="#333" fill-rule="evenodd" d="M751.35 483.5c-5.85 0-10.6 4.75-10.6 10.6s4.75 10.6 10.6 10.6 10.6-4.75 10.6-10.6-4.75-10.6-10.6-10.6m0 9.9c.39 0 .7.31.7.7s-.31.7-.7.7-.7-.31-.7-.7.31-.7.7-.7"/>
                                <path d="M751.35 464.99c-16.07 0-29.11 13.04-29.11 29.11s13.04 29.11 29.11 29.11 29.11-13.04 29.11-29.11-13.04-29.11-29.11-29.11m0 .56c15.76 0 28.55 12.79 28.55 28.55s-12.79 28.55-28.55 28.55-28.55-12.79-28.55-28.55 12.79-28.55 28.55-28.55" class="lg-cls-7"/>
                                <path fill="url(#lg-radial)" fill-rule="evenodd" d="M766.07 494.1c0 8.13-6.59 14.73-14.72 14.73v13.82c15.76 0 28.55-12.79 28.55-28.55z"/>
                                <path fill="url(#lg-radial-2)" fill-rule="evenodd" d="M736.63 494.1c0-8.13 6.59-14.73 14.72-14.73v-13.82c-15.76 0-28.55 12.79-28.55 28.55z"/>
                                <path fill="url(#lg-radial-3)" fill-rule="evenodd" d="M759.01 486.44c-4.23-4.23-11.09-4.23-15.32 0s-4.23 11.09 0 15.32 11.09 4.23 15.32 0 4.23-11.09 0-15.32m-.34.33c4.04 4.04 4.04 10.6 0 14.65-4.04 4.04-10.6 4.04-14.65 0-4.04-4.04-4.04-10.6 0-14.65 4.04-4.04 10.6-4.04 14.65 0"/>
                                <path fill="url(#lg-radial-4)" fill-rule="evenodd" d="M757.32 488.12a8.45 8.45 0 0 0-11.95 0 8.45 8.45 0 0 0 0 11.95 8.45 8.45 0 0 0 11.95 0 8.45 8.45 0 0 0 0-11.95m-5.4 5.4c.32.32.32.84 0 1.15s-.84.32-1.15 0-.32-.84 0-1.15c.32-.32.84-.32 1.15 0"/>
                                <path d="M758.26 487.18a9.66 9.66 0 0 0-3.82-2.36l-.2.59a9.05 9.05 0 0 1 3.58 2.21 9.13 9.13 0 0 1 2.45 4.44l.6-.14a9.77 9.77 0 0 0-2.62-4.75Z" class="lg-cls-3"/>
                                <path d="m742.32 492.56-.61-.11c-.51 3.02.4 6.23 2.73 8.55a9.77 9.77 0 0 0 8.53 2.73l-.1-.61c-2.82.47-5.82-.39-7.99-2.56a9.13 9.13 0 0 1-2.55-8.01Z" class="lg-cls-3"/>
                            </g>
                        </svg>
                    </div>
                    <h1 class="brand-heading">
                        <span class="brand-heading-light">Master</span>
                        <span class="brand-heading-bold">Music.</span>
                    </h1>
                    <p class="brand-subtitle">
                        Free, flexible music education for everyone.
                        Learn guitar, drums, trumpet, and more through
                        structured lessons at your own pace.
                    </p>
                </div>

                <div class="split-left-footer">
                    &copy; 2026 TunePilot. All rights reserved.
                </div>
            </div>

            <!-- RIGHT: Login Panel (40%) -->
            <div class="split-right">
                <div class="login-card">
                    <div class="login-card-logo" style="--nav_tp_full_color: var(--tp-color-strong);">
                        <svg xmlns="http://www.w3.org/2000/svg" viewBox="52.5 404.53 884.33 181.85" class="login-logo-svg-card">
                            <defs>
                                <radialGradient id="c-radial" cx="219.84" cy="566.82" r="14.56" fx="219.84" fy="566.82" gradientTransform="rotate(135 472.173 1265.015)scale(-1.23 2.77)" gradientUnits="userSpaceOnUse">
                                    <stop offset="0" stop-color="#fff"/><stop offset="1" stop-color="#fff" stop-opacity="0"/>
                                </radialGradient>
                                <radialGradient xlink:href="#c-radial" id="c-radial-2" cx="-558.98" cy="1251.73" r="14.56" fx="-558.98" fy="1251.73" gradientTransform="rotate(135 -797.861 -1211.297)scale(1.23 -2.77)"/>
                                <radialGradient id="c-radial-3" cx="-139.82" cy="2074.48" r="10.83" fx="-139.82" fy="2074.48" gradientTransform="matrix(0 2.78 .82 0 -957.78 882.54)" gradientUnits="userSpaceOnUse">
                                    <stop offset="0" stop-color="#6c6d6d"/><stop offset="1" stop-color="#3f3f3f"/>
                                </radialGradient>
                                <radialGradient id="c-radial-4" cx="-143.78" cy="1858.51" r="8.45" fx="-143.78" fy="1858.51" gradientTransform="matrix(0 2.87 1 0 -1107.16 907.15)" gradientUnits="userSpaceOnUse">
                                    <stop offset="0" stop-color="#f7f8f8"/><stop offset="1" stop-color="#e1e2e1"/>
                                </radialGradient>
                                <style>.c-cls-10{fill:var(--nav_tp_full_color)}.c-cls-3{fill:#fff;isolation:isolate;opacity:.74}.c-cls-7{fill-rule:evenodd;fill:#010101}</style>
                            </defs>
                            <path d="M196.68 472.18h-22.69l-12.86 72.83h-33.24l12.86-72.83h-23.12l5.06-28.32h78.9zM198.42 515.39c0-3.9.43-8.38 1.3-13.15l10.26-58.38h32.66l-10.4 58.67c-.58 3.32-1.01 6.21-1.01 8.53 0 5.35 2.02 7.8 7.51 7.8 7.66 0 10.4-5.2 12.43-16.33l10.4-58.67h32.51l-10.4 59.1c-5.78 32.8-22.25 44.07-48.41 44.07-22.4 0-36.85-8.38-36.85-31.65ZM335.69 516.11c-2.02-3.61-4.33-7.66-6.79-11.56h-.43c-.72 5.06-1.59 10.12-2.46 15.17l-4.48 25.29h-32.08l17.92-101.15h22.4l16.91 29.77c1.73 3.03 3.9 6.65 5.92 9.97h.43c.58-5.64 1.45-11.42 2.46-17.05l4.05-22.69h32.08L373.7 545.01h-22.11zM432.8 470.88l-1.88 10.4h29.77l-4.48 25.29h-29.77l-2.02 11.27h33.96l-4.77 27.17h-66.47l17.77-101.15h65.46l-4.77 27.02zM584.39 471.17c0 29.04-19.8 46.96-51.73 46.96h-2.31l-4.77 26.88h-33.24l17.77-101.15h36.12c26.16 0 38.15 8.67 38.15 27.31Zm-33.67 6.94c0-4.91-2.75-7.08-9.1-7.08h-3.03l-3.61 20.95h2.6c8.53 0 13.15-4.91 13.15-13.87ZM599.13 443.86h33.24L614.6 545.01h-33.24zM645.8 443.86h32.66L665.6 516.4h32.95l-5.06 28.61h-65.46zM799.84 481.29c0 35.54-19.65 65.75-56.79 65.75-24.42 0-39.73-15.32-39.73-39.6 0-35.54 19.65-65.6 56.79-65.6 24.42 0 39.73 15.17 39.73 39.45M885.25 472.18h-22.69l-12.86 72.83h-33.24l12.86-72.83H806.2l5.06-28.32h78.9z" class="c-cls-10"/>
                            <path fill="none" stroke="var(--nav_tp_full_color)" stroke-miterlimit="10" stroke-width="15" d="M873.02 578.88H89.28c-19.43 0-33.47-20.24-28.14-40.57l27.03-103.17c3.58-13.68 15.07-23.11 28.14-23.11h783.74c19.43 0 33.47 20.24 28.14 40.57l-27.03 103.17c-3.58 13.68-15.07 23.11-28.14 23.11Z"/>
                            <g id="disc">
                                <path fill="none" fill-rule="evenodd" stroke="#fff" stroke-miterlimit="10" stroke-width="2" d="M751.35 464.06c-16.58 0-30.03 13.46-30.03 30.03 0 16.58 13.46 30.03 30.03 30.03s30.03-13.46 30.03-30.03-13.46-30.03-30.03-30.03Zm0 29.19c.47 0 .84.38.84.84s-.38.84-.84.84-.84-.38-.84-.84.38-.84.84-.84Z"/>
                                <path d="M751.35 464.99c-16.07 0-29.11 13.04-29.11 29.11s13.04 29.11 29.11 29.11 29.11-13.04 29.11-29.11-13.04-29.11-29.11-29.11m0 28.29c.45 0 .82.37.82.82s-.37.82-.82.82-.82-.37-.82-.82.37-.82.82-.82" class="c-cls-7"/>
                                <path fill="#333" fill-rule="evenodd" d="M751.35 483.5c-5.85 0-10.6 4.75-10.6 10.6s4.75 10.6 10.6 10.6 10.6-4.75 10.6-10.6-4.75-10.6-10.6-10.6m0 9.9c.39 0 .7.31.7.7s-.31.7-.7.7-.7-.31-.7-.7.31-.7.7-.7"/>
                                <path d="M751.35 464.99c-16.07 0-29.11 13.04-29.11 29.11s13.04 29.11 29.11 29.11 29.11-13.04 29.11-29.11-13.04-29.11-29.11-29.11m0 .56c15.76 0 28.55 12.79 28.55 28.55s-12.79 28.55-28.55 28.55-28.55-12.79-28.55-28.55 12.79-28.55 28.55-28.55" class="c-cls-7"/>
                                <path fill="url(#c-radial)" fill-rule="evenodd" d="M766.07 494.1c0 8.13-6.59 14.73-14.72 14.73v13.82c15.76 0 28.55-12.79 28.55-28.55z"/>
                                <path fill="url(#c-radial-2)" fill-rule="evenodd" d="M736.63 494.1c0-8.13 6.59-14.73 14.72-14.73v-13.82c-15.76 0-28.55 12.79-28.55 28.55z"/>
                                <path fill="url(#c-radial-3)" fill-rule="evenodd" d="M759.01 486.44c-4.23-4.23-11.09-4.23-15.32 0s-4.23 11.09 0 15.32 11.09 4.23 15.32 0 4.23-11.09 0-15.32m-.34.33c4.04 4.04 4.04 10.6 0 14.65-4.04 4.04-10.6 4.04-14.65 0-4.04-4.04-4.04-10.6 0-14.65 4.04-4.04 10.6-4.04 14.65 0"/>
                                <path fill="url(#c-radial-4)" fill-rule="evenodd" d="M757.32 488.12a8.45 8.45 0 0 0-11.95 0 8.45 8.45 0 0 0 0 11.95 8.45 8.45 0 0 0 11.95 0 8.45 8.45 0 0 0 0-11.95m-5.4 5.4c.32.32.32.84 0 1.15s-.84.32-1.15 0-.32-.84 0-1.15c.32-.32.84-.32 1.15 0"/>
                                <path d="M758.26 487.18a9.66 9.66 0 0 0-3.82-2.36l-.2.59a9.05 9.05 0 0 1 3.58 2.21 9.13 9.13 0 0 1 2.45 4.44l.6-.14a9.77 9.77 0 0 0-2.62-4.75Z" class="c-cls-3"/>
                                <path d="m742.32 492.56-.61-.11c-.51 3.02.4 6.23 2.73 8.55a9.77 9.77 0 0 0 8.53 2.73l-.1-.61c-2.82.47-5.82-.39-7.99-2.56a9.13 9.13 0 0 1-2.55-8.01Z" class="c-cls-3"/>
                            </g>
                        </svg>
                    </div>
                    <h2 class="login-card-heading">Welcome Back</h2>
                    <p class="login-card-subtitle">Sign in to continue your musical journey.</p>

                    <asp:Label ID="lblMessage" runat="server" CssClass="message-label" Visible="false"></asp:Label>

                    <div class="form-group">
                        <label for="txtEmail">Email / Username</label>
                        <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" placeholder="you@example.com" />
                        <asp:RequiredFieldValidator ID="rfvEmail" runat="server" ControlToValidate="txtEmail" ErrorMessage="Required." Display="Dynamic" CssClass="error-msg" />
                    </div>

                    <div class="form-group">
                        <label for="txtPassword">Password</label>
                        <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" CssClass="form-control" placeholder="Enter your password" />
                        <asp:RequiredFieldValidator ID="rfvPassword" runat="server" ControlToValidate="txtPassword" ErrorMessage="Required." Display="Dynamic" CssClass="error-msg" />
                    </div>

                    <asp:Button ID="btnLogin" runat="server" Text="Sign In" CssClass="login-cta" OnClick="btnLogin_Click" />

                    <div class="login-card-footer">
                        <a href="register.aspx">Don't have an account? <strong>Sign Up</strong></a>
                    </div>
                </div>
            </div>

        </div>

    </form>
</body>
</html>
