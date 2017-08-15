<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="Webgape.Admin.Dashboard" %>

<%@ MasterType VirtualPath="~/Admin/Admin.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="breadcrumbs" id="breadcrumbs">
        <ul class="breadcrumb">
            <li><i class="icon-home home-icon"></i><a href="index.html">Home</a> <span class="divider"><i class="icon-angle-right arrow-icon"></i></span></li>
            <li class="active">Dashboard</li>
        </ul>
        <!--.breadcrumb-->


        <!--#nav-search-->
    </div>
    <div class="page-content">
        <div class="page-header position-relative">
            <h1>Dashboard </h1>
        </div>
        <!--/.page-header-->
        <div class="row-fluid panel-box">
            <a href="Posts/PostList.aspx">
                <div class="infobox infobox-pink  ">
                    <div class="infobox-icon"><i class="icon-pencil"></i></div>
                    <div class="infobox-data">
                        <span class="infobox-data-number">
                            <asp:Literal ID="ltrtotalpost" runat="server"></asp:Literal></span>
                        <div class="infobox-content">Total Post(s)</div>
                    </div>
                </div>
            </a>
            <a href="Posts/PostList.aspx">
                <div class="infobox infobox-green  ">
                    <div class="infobox-icon"><i class="icon-pencil"></i></div>
                    <div class="infobox-data">
                        <span class="infobox-data-number">
                            <asp:Literal ID="ltractivepost" runat="server"></asp:Literal></span>
                        <div class="infobox-content">Active Post(s)</div>
                    </div>
                </div>
            </a>
            <a href="Posts/PostList.aspx">
                <div class="infobox infobox-red  ">
                    <div class="infobox-icon"><i class="icon-pencil"></i></div>
                    <div class="infobox-data">
                        <span class="infobox-data-number">
                            <asp:Literal ID="ltrpendingpost" runat="server"></asp:Literal></span>
                        <div class="infobox-content">Pending Post(s)</div>
                    </div>
                </div>
            </a>
            <a href="Profile/NotificationList.aspx">
                <div class="infobox infobox-blue  ">
                    <div class="infobox-icon"><i class="icon-bell-alt"></i></div>
                    <div class="infobox-data">
                        <span class="infobox-data-number">
                            <asp:Literal ID="ltrnotification" runat="server"></asp:Literal></span>
                        <div class="infobox-content">Notifications</div>
                    </div>
                    <!--<div class="badge badge-success"> +32% <i class="icon-arrow-up"></i> </div> -->
                </div>
            </a>
            <a href="Profile/Message.aspx">
                <div class="infobox infobox-purple">
                    <div class="infobox-icon"><i class="icon-envelope"></i></div>
                    <div class="infobox-data">
                        <span class="infobox-data-number">
                            <asp:Literal ID="ltrmessage" runat="server"></asp:Literal></span>
                        <div class="infobox-content">Messages</div>
                    </div>
                </div>
            </a>
            <a href="Profile/Earning.aspx">
                <div class="infobox infobox-orange">
                    <div class="infobox-icon"><i class="icon-rupee"></i></div>
                    <div class="infobox-data">
                        <span class="infobox-data-number">
                            <asp:Literal ID="ltrearnings" runat="server"></asp:Literal></span>
                        <div class="infobox-content">Total Earning</div>
                    </div>
                </div>
            </a>
            <a href="Profile/Point.aspx">
                <div class="infobox infobox-brown">
                    <div class="infobox-icon"><i class="icon-star"></i></div>
                    <div class="infobox-data">
                        <span class="infobox-data-number">
                            <asp:Literal ID="ltrpoints" runat="server"></asp:Literal></span>
                        <div class="infobox-content">Total Points</div>
                    </div>
                </div>
            </a>
        </div>
        <div class="row-fluid">
            <div class="span12">
                <!--PAGE CONTENT BEGINS-->
                <div class="span9">
                    <div class="widget-box transparent">
                        <div class="widget-header widget-header-flat">
                            <h4 class="lighter"><i class="icon-shopping-cart orange"></i>Top 10 Posts</h4>
                            <div class="widget-toolbar"><a href="#" data-action="collapse"><i class="icon-chevron-up"></i></a></div>
                        </div>
                        <div class="widget-body">
                            <div class="widget-main no-padding">
                                <asp:GridView ID="grdtop10post" runat="server" CellPadding="0" CssClass="table table-bordered table-striped link"
                                    Width="100%" AutoGenerateColumns="false" CellSpacing="1" GridLines="None" OnRowDataBound="grdtop10post_RowDataBound"
                                    EditRowStyle-HorizontalAlign="Center" HeaderStyle-CssClass="even-row" Style="margin-bottom: 0 !important;">
                                    <EmptyDataTemplate>
                                        <span style="color: red; font-size: large;">No Post added by you.
                                        <u>
                                            <asp:HyperLink ID="hladdpost" NavigateUrl="/Admin/Posts/Post.aspx" runat="server">Add Post</asp:HyperLink>
                                        </u>&nbsp; and earn money</span>
                                    </EmptyDataTemplate>
                                    <Columns>
                                        <asp:TemplateField HeaderText="#" >
                                            <ItemTemplate>
                                                <%# Container.DataItemIndex + 1 %>.
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Post Title" ItemStyle-Width="45%">
                                            <ItemTemplate>
                                                <a href="/admin/posts/post.aspx?postid=<%# Eval("PostID")%>&mode=edit" target="_blank"
                                                    style="font-weight: normal; font-size: 13px" title="Click to See Post">
                                                    <%# SetName(Convert.ToString(Eval("Title")))%></a>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Type" ItemStyle-Width="10%">
                                            <ItemTemplate>
                                                <a href="/admin/posts/post.aspx?postid=<%# Eval("PostID")%>&mode=edit" target="_blank"
                                                    style="font-weight: normal; font-size: 13px" title="Click to See Post">
                                                    <%# SetName(Convert.ToString(Eval("PostType")))%></a>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Create Date" ItemStyle-Width="10%">
                                            <ItemTemplate>
                                                <%# String.Format("{0:MM/dd/yyyy}", Eval("CreatedOn"))%>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Post Status" ItemStyle-Width="10%">
                                            <ItemTemplate>
                                                <asp:HiddenField ID="hdnActive" runat="server" Value='<%# (DataBinder.Eval(Container.DataItem,"Active")) %>' />
                                                <asp:Literal ID="ltrPost" runat="server"></asp:Literal>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Total Visit" ItemStyle-Width="10%">
                                            <ItemTemplate>
                                                <%# Eval("ViewCount")%>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Edit" ItemStyle-Width="5%">
                                            <ItemTemplate>
                                                <a href="/admin/posts/post.aspx?postid=<%# Eval("PostID")%>&mode=edit"
                                                    style="font-weight: normal; font-size: 11px" title="Click to Edit Post">
                                                    <span class="green"><i class="icon-edit bigger-160"></i></span>
                                                </a>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="View" ItemStyle-Width="5%">
                                            <ItemTemplate>
                                                <a href="/post/<%# Eval("PostID")%>" target="_blank"
                                                    <%--<a href="/post.aspx?pid=<%# Eval("PostID")%>" target="_blank"--%>
                                                    style="font-weight: normal; font-size: 11px" title="Click to View Post">
                                                    <span class="green"><i class="icon-eye-open bigger-160"></i></span>
                                                </a>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <AlternatingRowStyle CssClass="even-row" />
                                </asp:GridView>
                            </div>
                            <!--/widget-main-->
                        </div>
                        <!--/widget-body-->
                    </div>
                    <!--/widget-box-->

                </div>
                <div class="span3">
                    <div class="widget-box transparent">
                        <div class="widget-header widget-header-flat">
                            <h4 class="lighter"><i class="icon-mail-forward orange"></i>Shortcuts</h4>
                            <div class="widget-toolbar"><a href="#" data-action="collapse"><i class="icon-chevron-up"></i></a></div>
                        </div>
                        <div class="widget-body">
                            <div class="widget-main no-padding">
                                <ul class="nav nav-list nav-list1">
                                    <li><a href="Posts/PostList.aspx"><i class="icon-pencil"></i><span class="menu-text">Post List</span></a></li>
                                    <li><a href="Posts/Post.aspx"><i class="icon-plus"></i><span class="menu-text">Add Post</span></a></li>
                                    <li><a href="Settings/PostPageConfiguration.aspx"><i class="icon-wrench"></i><span class="menu-text">Post Setting</span></a></li>
                                    <li><a href="Profile/Profile.aspx"><i class="icon-user"></i><span class="menu-text">Profile</span></a></li>
                                    <li><a href="Profile/Message.aspx"><i class="icon-envelope"></i><span class="menu-text">Message</span></a></li>
                                    <li><a href="Profile/NotificationList.aspx"><i class="icon-bell-alt"></i><span class="menu-text">Notification</span></a></li>
                                    <li><a href="Profile/Earning.aspx"><i class="icon-rupee"></i><span class="menu-text">Earning</span></a></li>
                                    <li><a href="Profile/Point.aspx"><i class="icon-star"></i><span class="menu-text">Points</span></a></li>
                                    <li><a href="Posts/CommentList.aspx"><i class="icon-edit"></i><span class="menu-text">Comments</span></a></li>
                                    <li><a href="Users/UserList.aspx"><i class="icon-user"></i><span class="menu-text">User List </span></a></li>
                                    <li><a href="Content/SubscriptionList.aspx"><i class="icon-edit"></i><span class="menu-text">Subscription</span></a></li>
                                    <li><a href="Content/TestimonialList.aspx"><i class="icon-edit"></i><span class="menu-text">Testimonial</span></a></li>
                                    <li><a href="Settings/ProfilePageConfiguration.aspx"><i class="icon-wrench"></i><span class="menu-text">Profile Setting</span></a></li>
                                </ul>
                            </div>
                            <!--/widget-main-->
                        </div>
                        <!--/widget-body-->
                    </div>
                    <div class="widget-box transparent">
                        <div class="widget-header widget-header-flat">
                            <h4 class="lighter"><i class="icon-shopping-cart orange"></i>Top 10 Visited Posts</h4>
                            <div class="widget-toolbar"><a href="#" data-action="collapse"><i class="icon-chevron-up"></i></a></div>
                        </div>
                        <div class="widget-body">
                            <div class="widget-main no-padding">
                                <table class="table table-bordered table-striped">
                                    <thead>
                                        <tr>
                                            <th width="13%" align="center"># </th>
                                            <th width="55%">Product </th>
                                            <th width="32%" class="hidden-phone" style="text-align: right">Qty </th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr>
                                            <td>1</td>
                                            <td>Lorem Ipsum</td>
                                            <td align="right" class="hidden-phone" style="text-align: right">0</td>
                                        </tr>
                                        <tr>
                                            <td>2</td>
                                            <td>Lorem Ipsum</td>
                                            <td align="right" class="hidden-phone" style="text-align: right">0</td>
                                        </tr>
                                        <tr>
                                            <td>3</td>
                                            <td>Lorem Ipsum</td>
                                            <td align="right" class="hidden-phone" style="text-align: right">0</td>
                                        </tr>
                                        <tr>
                                            <td>4</td>
                                            <td>Lorem Ipsum</td>
                                            <td align="right" class="hidden-phone" style="text-align: right">0</td>
                                        </tr>
                                        <tr>
                                            <td>5</td>
                                            <td>Lorem Ipsum</td>
                                            <td align="right" class="hidden-phone" style="text-align: right">0</td>
                                        </tr>
                                        <tr>
                                            <td>6</td>
                                            <td>Lorem Ipsum</td>
                                            <td align="right" class="hidden-phone" style="text-align: right">0</td>
                                        </tr>
                                        <tr>
                                            <td>7</td>
                                            <td>Lorem Ipsum</td>
                                            <td align="right" class="hidden-phone" style="text-align: right">0</td>
                                        </tr>
                                        <tr>
                                            <td>8</td>
                                            <td>Lorem Ipsum</td>
                                            <td align="right" class="hidden-phone" style="text-align: right">0</td>
                                        </tr>
                                        <tr>
                                            <td>9</td>
                                            <td>Lorem Ipsum</td>
                                            <td align="right" class="hidden-phone" style="text-align: right">0</td>
                                        </tr>
                                        <tr>
                                            <td>10</td>
                                            <td>Lorem Ipsum</td>
                                            <td align="right" class="hidden-phone" style="text-align: right">0</td>
                                        </tr>
                                        <tr>
                                            <td align="right" style="text-align: right;" colspan="3"><i class="icon-list orange"></i><a href="#">View List</a></td>
                                        </tr>
                                    </tbody>
                                </table>
                            </div>
                            <!--/widget-main-->
                        </div>
                        <!--/widget-body-->
                    </div>
                </div>
            </div>
            <!--/.span-->
        </div>
        <!--/.row-fluid-->
    </div>

</asp:Content>
