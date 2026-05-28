<%@ Page Title="Admin Dashboard" Language="C#" MasterPageFile="~/navbar.Master" AutoEventWireup="true" CodeBehind="AdminDashboard.aspx.cs" Inherits="TunePilot.AdminDashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="dashboard-header admin-dashboard-header">
        <h1><asp:Label ID="dashboardTitlelbl" runat="server" Text="Admin Dashboard"></asp:Label></h1>
        <span class="role-badge">Management Console</span>
    </div>

    <div class="admin-dashboard-wrapper">
        <div class="admin-switcher" role="group" aria-label="Admin management views">
            <asp:Button ID="studentSwitchbtn" runat="server" Text="Student Management" OnClick="studentSwitchbtn_Click" CssClass="admin-switch-btn active" />
            <asp:Button ID="courseSwitchbtn" runat="server" Text="Course Management" OnClick="courseSwitchbtn_Click" CssClass="admin-switch-btn" />
        </div>

        <asp:PlaceHolder ID="StudentManagementPlaceholder" runat="server" Visible="true">
            <section class="section-card admin-management-card">
                <div class="section-header admin-section-header">
                    <div>
                        <h2>Student Management</h2>
                        <p>Search, review, and edit student accounts.</p>
                    </div>
                    <asp:Button ID="addStudentbtn" runat="server" Text="+ Add Student" OnClick="addStudentbtn_Click" CssClass="admin-action-btn" />
                </div>

                <div class="admin-toolbar">
                    <asp:TextBox ID="searchStudenttb" runat="server" placeholder="Search name or ID" CssClass="form-control admin-search"></asp:TextBox>
                    <asp:Button ID="searchStudentbtn" runat="server" Text="Search" OnClick="searchStudentbtn_Click" CssClass="admin-action-btn secondary" />
                </div>

                <asp:GridView ID="studentsgv" runat="server"
                    AutoGenerateColumns="false"
                    DataKeyNames="user_id"
                    AllowPaging="true"
                    PageSize="10"
                    OnPageIndexChanging="studentsgv_PageIndexChanging"
                    OnSelectedIndexChanged="studentsgv_SelectedIndexChanged"
                    CssClass="admin-table">
                    <Columns>
                        <asp:BoundField DataField="user_id" HeaderText="ID" />
                        <asp:BoundField DataField="first_name" HeaderText="First Name" />
                        <asp:BoundField DataField="last_name" HeaderText="Last Name" />
                        <asp:BoundField DataField="username" HeaderText="Username" />
                        <asp:BoundField DataField="email" HeaderText="Email" />
                        <asp:CommandField ShowSelectButton="true" SelectText="View" />
                    </Columns>
                </asp:GridView>

                <asp:Panel ID="studentDetailpnl" runat="server" Visible="false" CssClass="admin-detail-panel">
                    <div class="section-header compact">
                        <h2>Student Detail</h2>
                    </div>

                    <asp:DetailsView ID="studentdv" runat="server"
                        AutoGenerateRows="false"
                        AutoGenerateEditButton="true"
                        DataKeyNames="user_id"
                        OnItemUpdating="studentdv_ItemUpdating"
                        OnModeChanging="studentdv_ModeChanging"
                        CssClass="admin-table admin-detail-table">
                        <Fields>
                            <asp:BoundField DataField="user_id" HeaderText="ID" ReadOnly="true" />
                            <asp:BoundField DataField="first_name" HeaderText="First Name" />
                            <asp:BoundField DataField="last_name" HeaderText="Last Name" />
                            <asp:BoundField DataField="username" HeaderText="Username" />
                            <asp:BoundField DataField="email" HeaderText="Email" />
                            <asp:BoundField DataField="active" HeaderText="Active" ReadOnly="true" />
                            <asp:BoundField DataField="created_at" HeaderText="Joined" ReadOnly="true" />
                        </Fields>
                    </asp:DetailsView>

                    <div class="section-header compact">
                        <h2>Enrollments</h2>
                    </div>

                    <asp:GridView ID="enrollmentsgv" runat="server"
                        AutoGenerateColumns="false"
                        DataKeyNames="enrollment_id"
                        CssClass="admin-table">
                        <Columns>
                            <asp:BoundField DataField="title" HeaderText="Course" />
                            <asp:BoundField DataField="enrolled_at" HeaderText="Enrolled" />
                        </Columns>
                    </asp:GridView>
                </asp:Panel>
            </section>
        </asp:PlaceHolder>

        <asp:PlaceHolder ID="CourseManagementPlaceholder" runat="server" Visible="false">
            <section class="section-card admin-management-card">
                <div class="section-header admin-section-header">
                    <div>
                        <h2>Course Management</h2>
                        <p>Manage courses, lessons, quizzes, and exams.</p>
                    </div>
                    <asp:Button ID="addCoursebtn" runat="server" Text="+ Add Course" OnClick="addCoursebtn_Click" CssClass="admin-action-btn" />
                </div>

                <div class="admin-toolbar">
                    <asp:DropDownList ID="sortCourseddl" runat="server" AutoPostBack="true" OnSelectedIndexChanged="sortCourseddl_SelectedIndexChanged" CssClass="form-control admin-sort">
                        <asp:ListItem Text="Sort by..." Value="" />
                        <asp:ListItem Text="Sort by Name" Value="title" />
                        <asp:ListItem Text="Sort by Difficulty" Value="difficulty_level" />
                        <asp:ListItem Text="Sort by Instrument" Value="instrument" />
                    </asp:DropDownList>
                    <asp:TextBox ID="searchCoursetb" runat="server" placeholder="Search name or ID" CssClass="form-control admin-search"></asp:TextBox>
                    <asp:Button ID="searchCoursebtn" runat="server" Text="Search" OnClick="searchCoursebtn_Click" CssClass="admin-action-btn secondary" />
                </div>

                    <asp:GridView ID="coursesgv" runat="server"
                        AutoGenerateColumns="false"
                        DataKeyNames="course_id"
                        AllowPaging="true"
                        PageSize="10"
                        OnPageIndexChanging="coursesgv_PageIndexChanging"
                        OnSelectedIndexChanged="coursesgv_SelectedIndexChanged"
                        OnRowDeleting="coursesgv_RowDeleting"
                        CssClass="admin-table">
                        <Columns>
                            <asp:BoundField DataField="course_id" HeaderText="ID" />
                            <asp:BoundField DataField="title" HeaderText="Course" />
                            <asp:BoundField DataField="instrument" HeaderText="Instrument" />
                            <asp:TemplateField HeaderText="Difficulty">
                                <ItemTemplate>
                                    <%# Eval("difficulty_level") %>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:DropDownList ID="difficultyEditddl" runat="server" SelectedValue='<%# Bind("difficulty_level") %>'>
                                        <asp:ListItem Text="Beginner" Value="beginner" />
                                        <asp:ListItem Text="Intermediate" Value="intermediate" />
                                        <asp:ListItem Text="Advanced" Value="advanced" />
                                    </asp:DropDownList>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="lesson_count" HeaderText="Lessons" />
                            <asp:BoundField DataField="quiz_count" HeaderText="Quizzes" />
                            <asp:CommandField ShowSelectButton="true" SelectText="View" />
                            <asp:CommandField ShowDeleteButton="true" DeleteText="Delete" />
                        </Columns>
                    </asp:GridView>

                    <asp:Panel ID="courseDetailpnl" runat="server" Visible="false" CssClass="admin-detail-panel">
                        <asp:Label ID="courseMessagelbl" runat="server" Visible="false" ForeColor="Red" CssClass="message-label"></asp:Label>

                        <div class="section-header compact">
                            <h2>Course Detail</h2>
                        </div>

                        <asp:DetailsView ID="coursedv" runat="server"
                            AutoGenerateRows="false"
                            AutoGenerateEditButton="true"
                            DataKeyNames="course_id"
                            OnItemUpdating="coursedv_ItemUpdating"
                            OnModeChanging="coursedv_ModeChanging"
                            CssClass="admin-table admin-detail-table">
                            <Fields>
                                <asp:BoundField DataField="course_id" HeaderText="ID" ReadOnly="true" />
                                <asp:BoundField DataField="title" HeaderText="Title" />
                                <asp:BoundField DataField="description" HeaderText="Description" />
                                <asp:BoundField DataField="difficulty_level" HeaderText="Difficulty" />
                                <asp:BoundField DataField="instrument" HeaderText="Instrument" ReadOnly="true" />
                                <asp:BoundField DataField="created_at" HeaderText="Created" ReadOnly="true" />
                            </Fields>
                        </asp:DetailsView>

                        <div class="section-header compact">
                            <h2>Lessons</h2>
                        </div>

                        <asp:GridView ID="lessonsgv" runat="server"
                            AutoGenerateColumns="False"
                            DataKeyNames="lesson_id"
                            CssClass="admin-table">
                            <Columns>
                                <asp:BoundField DataField="lesson_order" HeaderText="#" />
                                <asp:BoundField DataField="title" HeaderText="Lesson Title" />
                                <asp:BoundField DataField="duration_minutes" HeaderText="Duration (min)" />
                                <asp:HyperLinkField Text="Edit Contents" DataNavigateUrlFields="lesson_id" DataNavigateUrlFormatString="~/EditLesson.aspx?id={0}" />
                            </Columns>
                        </asp:GridView>

                        <div class="admin-button-row">
                            <asp:Button ID="addLessonbtn" runat="server" Text="+ Add Lesson" OnClick="addLessonbtn_Click" CssClass="admin-action-btn" />
                            <asp:Button ID="addExambtn" runat="server" Text="+ Add Exam" OnClick="addExambtn_Click" CssClass="admin-action-btn secondary" />
                        </div>
                    </asp:Panel>
            </section>
        </asp:PlaceHolder>
    </div>
</asp:Content>
