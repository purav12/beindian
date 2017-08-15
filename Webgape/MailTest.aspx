<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MailTest.aspx.cs" Inherits="WebReminds.MailTest" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table>
        <tr>
            <td>MailUserName</td>
            <td>
                <asp:TextBox ID="MailUserName" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>MailPassword</td>
            <td>
                <asp:TextBox ID="MailPassword" runat="server"></asp:TextBox></td>
        </tr>
        <tr>
            <td>MailHost</td>
            <td>
                <asp:TextBox ID="MailHost" runat="server"></asp:TextBox></td>
        </tr>
        <tr>
            <td>MailFrom</td>
            <td>
                <asp:TextBox ID="MailFrom" runat="server"></asp:TextBox></td>
        </tr>
        <tr>
            <td>MailFromDisplay</td>
            <td>
                <asp:TextBox ID="MailFromDisplay" runat="server"></asp:TextBox></td>
        </tr>
        <tr>
            <td>MailTo</td>
            <td>
                <asp:TextBox ID="MailTo" runat="server"></asp:TextBox></td>
        </tr>
        <tr>
            <td>Port</td>
            <td>
                <asp:TextBox ID="Port" runat="server"></asp:TextBox></td>
        </tr>
        <tr>
            <td>MailSubject</td>
            <td>
                <asp:TextBox ID="MailSubject" runat="server"></asp:TextBox></td>
        </tr>
        <tr>
            <td>MailBody</td>
            <td>
                <asp:TextBox ID="MailBody" runat="server"></asp:TextBox></td>
        </tr>
        <tr>
            <td>

            </td>
            <td>
                <asp:Button ID="btnsendmail" runat="server" Text="Send Mail" OnClick="btnsendmail_Click" />
            </td>
        </tr>

    </table>
</asp:Content>
