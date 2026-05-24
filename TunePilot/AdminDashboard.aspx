<%@ Page Title="Admin Dashboard" Language="C#" MasterPageFile="~/navbar.Master" AutoEventWireup="true" CodeBehind="AdminDashboard.aspx.cs" Inherits="TunePilot.AdminDashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:Label ID="dashboardTitlelbl" runat="server" Text="Admin Dashboard" Font-Bold="true" Font-Size="XX-Large" Font-Names="Montserrat"></asp:Label>

    <br /><br />

    <div style="display:flex; gap:20px;">

        <div style="flex:2;">

            <asp:Label ID="courseTitlelbl" runat="server" Text="Course Management" Font-Bold="true" Font-Size="Large" Font-Names="Montserrat"></asp:Label>

            <br /><br />

            <asp:DropDownList ID="sortCourseddl" runat="server" AutoPostBack="true" OnSelectedIndexChanged="sortCourseddl_SelectedIndexChanged">
                <asp:ListItem Text="Sort by..." Value="" />
                <asp:ListItem Text="Sort by Name" Value="title" />
                <asp:ListItem Text="Sort by Difficulty" Value="difficulty_level" />
                <asp:ListItem Text="Sort by Instrument" Value="instrument" />
            </asp:DropDownList>

            <asp:TextBox ID="searchCoursetb" runat="server" placeholder="Search name or ID"></asp:TextBox>

            <asp:Button ID="searchCoursebtn" runat="server" Text="Search" OnClick="searchCoursebtn_Click" />
            <asp:Button ID="addCoursebtn" runat="server" Text="+ Add Course" OnClick="addCoursebtn_Click" />

            <br /><br />

            <asp:GridView ID="coursesgv" runat="server"
                AutoGenerateColumns="false"
                DataKeyNames="course_id"
                AllowPaging="true"
                PageSize="10"
                OnPageIndexChanging="coursesgv_PageIndexChanging"
                OnSelectedIndexChanged="coursesgv_SelectedIndexChanged"
                OnRowDeleting="coursesgv_RowDeleting">
                <Columns>
                    <asp:BoundField DataField="course_id"        HeaderText="ID" />
                    <asp:BoundField DataField="title"            HeaderText="Course" />
                    <asp:BoundField DataField="instrument"       HeaderText="Instrument" />
                    <asp:BoundField DataField="difficulty_level" HeaderText="Difficulty" />
                    <asp:BoundField DataField="lesson_count"     HeaderText="Lessons" />
                    <asp:BoundField DataField="quiz_count"       HeaderText="Quizzes" />
                    <asp:CommandField ShowSelectButton="true"    SelectText="View" />
                    <asp:CommandField ShowDeleteButton="true"    DeleteText="Delete" />
                </Columns>
            </asp:GridView>

            <asp:Panel ID="courseDetailpnl" runat="server" Visible="false">

                <br />
                <asp:Label ID="courseDetailTitlelbl" runat="server" Text="Course Detail" Font-Bold="true" Font-Size="Large" Font-Names="Montserrat"></asp:Label>
                <br /><br />

                <asp:DetailsView ID="coursedv" runat="server"
                    AutoGenerateRows="false"
                    AutoGenerateEditButton="true"
                    DataKeyNames="course_id"
                    OnItemUpdating="coursedv_ItemUpdating"
                    OnModeChanging="coursedv_ModeChanging">
                    <Fields>
                        <asp:BoundField DataField="course_id"        HeaderText="ID"          ReadOnly="true" />
                        <asp:BoundField DataField="title"            HeaderText="Title" />
                        <asp:BoundField DataField="description"      HeaderText="Description" />
                        <asp:BoundField DataField="difficulty_level" HeaderText="Difficulty" />
                        <asp:BoundField DataField="instrument"       HeaderText="Instrument"  ReadOnly="true" />
                        <asp:BoundField DataField="created_at"       HeaderText="Created"     ReadOnly="true" />
                    </Fields>
                </asp:DetailsView>

                <br />
                <asp:Label ID="lessonsTitlelbl" runat="server" Text="Lessons" Font-Bold="true" Font-Size="Medium" Font-Names="Montserrat"></asp:Label>
                <br /><br />

                <asp:GridView ID="lessonsgv" runat="server"
                    AutoGenerateColumns="False"
                    DataKeyNames="lesson_id">
                    <Columns>
                        <asp:BoundField DataField="lesson_order"     HeaderText="#" />
                        <asp:BoundField DataField="title"            HeaderText="Lesson Title" />
                        <asp:BoundField DataField="duration_minutes" HeaderText="Duration (min)" />
                        <asp:HyperLinkField Text="Edit Contents" DataNavigateUrlFields="lesson_id" DataNavigateUrlFormatString="~/EditLesson.aspx?id={0}" />
                    </Columns>
                </asp:GridView>

            </asp:Panel>

        </div>

        <div style="flex:1;">

            <asp:Label ID="studentTitlelbl" runat="server" Text="Student Management" Font-Bold="true" Font-Size="Large" Font-Names="Montserrat"></asp:Label>

            <br /><br />

            <asp:TextBox ID="searchStudenttb" runat="server" placeholder="Search name or ID"></asp:TextBox>
            <asp:Button ID="searchStudentbtn" runat="server" Text="Search" OnClick="searchStudentbtn_Click" />
            <asp:Button ID="addStudentbtn" runat="server" Text="+ Add Student" OnClick="addStudentbtn_Click" />

            <br /><br />

            <asp:ListView ID="studentslv" runat="server"
                DataKeyNames="user_id"
                OnItemCommand="studentslv_ItemCommand">
                <ItemTemplate>
                    <div>
                        <asp:Label runat="server" Text='<%# Eval("first_name") + " " + Eval("last_name") %>'></asp:Label>
                        <asp:Label runat="server" Text='<%# "ID: " + Eval("user_id") %>'></asp:Label>
                        <asp:LinkButton ID="selectStudentbtn" runat="server" CommandName="Select" CommandArgument='<%# Eval("user_id") %>'>View</asp:LinkButton>
                    </div>
                </ItemTemplate>
            </asp:ListView>

            <asp:Panel ID="studentDetailpnl" runat="server" Visible="false">

                <br />
                <asp:Label ID="studentDetailTitlelbl" runat="server" Text="Student Detail" Font-Bold="true" Font-Size="Large" Font-Names="Montserrat"></asp:Label>
                <br /><br />

                <asp:DetailsView ID="studentdv" runat="server"
                    AutoGenerateRows="false"
                    AutoGenerateEditButton="true"
                    DataKeyNames="user_id"
                    OnItemUpdating="studentdv_ItemUpdating">
                    <Fields>
                        <asp:BoundField DataField="user_id"    HeaderText="ID"       ReadOnly="true" />
                        <asp:BoundField DataField="first_name" HeaderText="First Name" />
                        <asp:BoundField DataField="last_name"  HeaderText="Last Name" />
                        <asp:BoundField DataField="username"   HeaderText="Username" />
                        <asp:BoundField DataField="email"      HeaderText="Email" />
                        <asp:BoundField DataField="active"     HeaderText="Active"   ReadOnly="true" />
                        <asp:BoundField DataField="created_at" HeaderText="Joined"   ReadOnly="true" />
                    </Fields>
                </asp:DetailsView>

                <br />
                <asp:Label ID="enrollmentsTitlelbl" runat="server" Text="Enrollments" Font-Bold="true" Font-Size="Medium" Font-Names="Montserrat"></asp:Label>
                <br /><br />

                <asp:GridView ID="enrollmentsgv" runat="server"
                    AutoGenerateColumns="false"
                    DataKeyNames="enrollment_id">
                    <Columns>
                        <asp:BoundField DataField="title"       HeaderText="Course" />
                        <asp:BoundField DataField="enrolled_at" HeaderText="Enrolled" />
                        <asp:BoundField DataField="status"      HeaderText="Status" />
                    </Columns>
                </asp:GridView>

            </asp:Panel>

        </div>

    </div>

</asp:Content>