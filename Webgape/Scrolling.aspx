<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeBehind="Scrolling.aspx.cs" Inherits="Webgape.Scrolling" %>


<html>
<head>

    <script type="text/javascript">
        function OnLoad() {
            var $container = $('.blog-grid1');

            var gutter = 30;
            var min_width = 345;
            $container.imagesLoaded(function () {
                $container.masonry({
                    itemSelector: '.post',
                    gutterWidth: gutter,
                    isAnimated: true,
                    columnWidth: function (containerWidth) {
                        var box_width = (((containerWidth - gutter) / 2) | 0);

                        if (box_width < min_width) {
                            box_width = (((containerWidth - gutter) / 2) | 0);
                        }

                        if (box_width < min_width) {
                            box_width = containerWidth;
                        }

                        $('.post').width(box_width);

                        return box_width;
                    }
                });
                $container.css('visibility', 'visible').parent().removeClass('loading');
            });
        }
    </script>
</head>
<body>
    <form id="frmscroll" runat="server">
        <div class="wrapper">
            <div id="navigation">
                <div class="intro">
                    <a>
                        <span style="float:left" class="meta-nav-prev">&nbsp;Page Number : <%=pagenumber%></span>
                        <span class="meta-nav-prev"> <%=displaytext%></span>
                    </a>
                </div>
            </div>
            <div class="blog-wrap">
                <div class="blog-grid1" runat="server">
                    <asp:Repeater ID="RepPost" runat="server" OnItemDataBound="RepPost_ItemDataBound">
                        <HeaderTemplate>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Literal ID="ltrPostData" runat="server"></asp:Literal>
                            <asp:Literal ID="ltrPostId" runat="server" Visible="false" Text='<%#Eval("PostID")%>'></asp:Literal>
                        </ItemTemplate>
                        <FooterTemplate>
                        </FooterTemplate>
                    </asp:Repeater>
                </div>
            </div>
        </div>
    </form>
    <input type="hidden" id="hdnscrollpostId" runat="server" />
</body>

</html>
