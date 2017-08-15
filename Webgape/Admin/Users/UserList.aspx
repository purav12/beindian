<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="UserList.aspx.cs" Inherits="Webgape.Admin.Users.UserList" %>

<%@ MasterType VirtualPath="~/Admin/Admin.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function SearchValidation() {
            if (document.getElementById('<%=txtSearch.ClientID%>') != null && document.getElementById('<%= txtSearch.ClientID%>').value == '') {
                jAlert('Please enter search value.', 'Message', '<%=txtSearch.ClientID%>');
                return false;
            }
            return true;
        }
    </script>
    <script type="text/javascript">
        function checkondelete(id) {
            alert(id);
            jConfirm('Are you sure want to delete selected User ?', 'Confirmation', function (r) {
                if (r == true) {
                    document.getElementById("ContentPlaceHolder1_btndelprice").click();
                    return true;
                }
                else {
                    return false;
                }
            });
            return false;
        }
    </script>
    <script type="text/javascript">
        function Checktxt() {
            var strseacrh = document.getElementById('<%=txtSearch.ClientID%>').value;
            if (strseacrh == '') {
                document.getElementById('<%=txtSearch.ClientID%>').style.backgroundImage = 'none';
            }
        }
    </script>
    <script runat="server">
        string _ReturnUrlname;
        public string SetRegisterImage(bool _Value)
        {
            if (_Value == true)
            {
                _ReturnUrlname = "../Images/yes.png";
            }
            else
            {
                _ReturnUrlname = "../Images/no.png";
            }
            return _ReturnUrlname;
        }
    </script>
    <script runat="server">
        string _ReturnUrld;
        public string SetImagefordelete(bool _Value)
        {
            if (_Value == false)
            {
                _ReturnUrld = "../Images/yes.png";
            }
            else
            {
                _ReturnUrld = "../Images/no.png";
            }
            return _ReturnUrld;
        }
    </script>
    <script src="../Js/jquery-1.3.2.js"></script>
    <script src="../../style/js/jquery-1.7.2.min.js"></script>
    <link href="../Css/jquery.alerts.css" rel="stylesheet" />
    <script src="../Js/jquery-alerts.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="breadcrumbs" id="breadcrumbs">
        <ul class="breadcrumb">
            <li><i class="icon-home home-icon"></i><a href="/Admin/Dashboard.aspx">Home</a> <span class="divider">
                <i class="icon-angle-right arrow-icon"></i></span></li>
            <li class="active">User List </li>
        </ul>
        <!--.breadcrumb-->
        <div class="nav-search" id="nav-search" style="display: none;">
            <form class="form-search">
                <span class="input-icon"></span>
            </form>
        </div>
        <!--#nav-search-->
    </div>
    <div class="page-content">
        <div class="page-header position-relative">
            <h1>User List
            </h1>
        </div>
        <div class="row-fluid">
            <div class="span12">
                <div class="widget-header header-color-dark">
                </div>
                <div class="row-fluid">
                    <div class="span12">
                        <div class="widget-header">
                            <h4>Search By</h4>
                        </div>
                        <div class="widget-body">
                            <div class="widget-main">
                                <div class="row-fluid">
                                    <div class="span6">
                                    </div>
                                    <div class="span6">
                                        <div class="row-fluid">
                                            <div class="pagination no-margin right" style="width: 400px">
                                                <span>Search : </span>
                                                <p>
                                                    <table>
                                                        <tr>
                                                            <td></td>
                                                            <td>
                                                                <asp:TextBox ID="txtSearch" runat="server" CssClass="order-textfield" Style="margin-bottom: 0px"
                                                                    Width="160px" onkeyup="Checktxt();"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:Button ID="btnSearch" class="btn btn-mini btn-info btn-padding" Text="Search" runat="server" OnClick="btnSearch_Click" OnClientClick="return SearchValidation();" />
                                                                <asp:Button ID="btnShowall" class="btn btn-mini btn-info btn-padding" Text="Show All" runat="server" OnClick="btnShowall_Click" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td></td>
                                                            <td colspan="2">
                                                                <span style="float: right; padding-top: 3px; padding-right: 5px; font-size: 12px; font-family: Arial,Helvetica,sans-serif;">Total Record : 
                                                                    <% =Usercount%>
                                                                </span>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </p>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row-fluid margin-top">
                    <div class="span12">
                        <table id="sample-table-2" width="100%" cellpadding="0" cellspacing="0" border="0">
                            <tr>
                                <td style="padding-top: 0px; padding-bottom: 0px;">
                                    <asp:GridView ID="grdUser" runat="server" AutoGenerateColumns="False" CellPadding="2"
                                        GridLines="None" EmptyDataText="No Record(s) Found." EmptyDataRowStyle-ForeColor="Red" ShowHeaderWhenEmpty="true"
                                        EmptyDataRowStyle-HorizontalAlign="Center" CssClass="table table-border-display background-default" ViewStateMode="Enabled" Width="100%"
                                        AllowPaging="True" PageSize="10" RowStyle-ForeColor="Black" HeaderStyle-ForeColor="#3c2b1b"
                                        PagerSettings-Mode="NumericFirstLast" OnRowCommand="grdUser_RowCommand"
                                        OnRowDataBound="grdUser_RowDataBound" CellSpacing="1" BorderStyle="solid"
                                        BorderWidth="1" BorderColor="#e7e7e7">
                                        <EmptyDataTemplate>
                                            <span style="color: Red; font-size: 12px; text-align: center;">No Record(s) Found !</span>
                                        </EmptyDataTemplate>
                                        <Columns>
                                            <asp:TemplateField Visible="false">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <HeaderTemplate>
                                                    User ID
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lbluserID" runat="server" Text='<%# Eval("AdminID") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <HeaderTemplate>
                                                    Name
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:HiddenField ID="hdnFrmid" runat="server" Value='<%#Eval("AdminID") %>' />
                                                    <asp:Literal ID="lblFirstName" runat="server" Text='<%# Eval("FullName")%>'></asp:Literal>
                                                    <%--<asp:Label ID="lblFirstName" runat="server" Text='<%# Eval("FullName")%>'></asp:Label>--%>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <HeaderTemplate>
                                                    User Name
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblusername" runat="server" Text='<%# Eval("UserName")%>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <HeaderTemplate>
                                                    Admin Type
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblLastName" runat="server" Text='<%# Eval("AdminType") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>


                                            <asp:TemplateField ItemStyle-Width="9%">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <HeaderTemplate>
                                                    Create Date
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblcreatedon" runat="server" Text='<%# Bind("CreateDate","{0:MM/dd/yyyy}") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Registered">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <img src='<%# SetRegisterImage(Convert.ToBoolean(DataBinder.Eval(Container.DataItem,"IsRegistered"))) %>'
                                                        alt="" />
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" CssClass="tdpro-checkbox" />
                                                <HeaderStyle HorizontalAlign="Center" CssClass="tdpro-checkbox" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Complete">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <img src='<%# SetRegisterImage(Convert.ToBoolean(DataBinder.Eval(Container.DataItem,"IsComplete"))) %>'
                                                        alt="" />
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" CssClass="tdpro-checkbox" />
                                                <HeaderStyle HorizontalAlign="Center" CssClass="tdpro-checkbox" />
                                            </asp:TemplateField>

                                              <asp:TemplateField ItemStyle-Width="9%">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <HeaderTemplate>
                                                    Views
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblviewcount" runat="server" Text='<%# Eval("ViewCount") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Send Message">
                                                <ItemTemplate>
                                                    <asp:Button ID="btnSendMessage" runat="server" Text="Send Message" CommandName="SendMessage" ToolTip="Send Message" CssClass="btn btn-small btn-success"
                                                        CommandArgument='<%# DataBinder.Eval(Container.DataItem,"AdminID") %>' OnClick="btnSendMessage_Click" />
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" CssClass="tdpro-checkbox" />
                                                <HeaderStyle HorizontalAlign="Center" CssClass="tdpro-checkbox" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="View" ItemStyle-Width="5%">
                                                <ItemTemplate>
                                                    <a href="/User.aspx?UId=<%# Eval("AdminID")%>" target="_blank"
                                                        style="font-weight: normal; font-size: 11px" title="Click to View Post">
                                                        <span class="green"><i class="icon-eye-open bigger-160"></i></span>
                                                    </a>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Operations" Visible="false">
                                                <ItemTemplate>
                                                    <asp:LinkButton runat="server" ID="btnEditUser" ToolTip="Edit" CommandName="edit"
                                                        AlternateText="Edit Price" CommandArgument='<%# DataBinder.Eval(Container.DataItem,"AdminID") %>'><span class="green"> <i class="icon-edit bigger-160"></i> </span></asp:LinkButton>
                                                    <asp:LinkButton runat="server" ID="btndelUser" ToolTip="Delete" CommandName="delete"
                                                        AlternateText="Delete User" CommandArgument='<%# DataBinder.Eval(Container.DataItem,"AdminID") %>' message="Are you sure want to delete selected Customer ?" OnClientClick='return confirm(this.getAttribute("message"))'>
                                                                 <span><i class="icon-trash bigger-160 red"></i></span></asp:LinkButton>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" CssClass="tdpro-checkbox" />
                                                <HeaderStyle HorizontalAlign="Center" CssClass="tdpro-checkbox" />
                                            </asp:TemplateField>
                                        </Columns>
                                        <PagerSettings Mode="NumericFirstLast" Position="TopAndBottom" PageButtonCount="30"></PagerSettings>
                                        <PagerStyle HorizontalAlign="right" CssClass="numbering" />
                                        <RowStyle HorizontalAlign="left" CssClass="eve-row" ForeColor="#000000" />
                                        <AlternatingRowStyle CssClass="odd-row" />
                                        <EmptyDataRowStyle HorizontalAlign="Center" ForeColor="Red"></EmptyDataRowStyle>
                                        <HeaderStyle ForeColor="White" Font-Bold="false" />
                                    </asp:GridView>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
