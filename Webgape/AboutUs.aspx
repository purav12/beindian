﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AboutUs.aspx.cs" Inherits="Webgape.AboutUs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="wrapper">
        <div class="content box">
            <h1 class="title">About Us</h1>
            <h3>Comming Soon...</h3>
        </div>
        <div class="sidebar box">
            <div class="sidebox widget">
                <h3 class="widget-title">Connect with us</h3>
                <p>Like, Comment, share and connect with us , you can find us on</p>
                <p>
                    <span class="lite1">Facebook:</span>&nbsp; <a href="<%=FacebookLink%>" target="_blank"><u><%=FacebookLink%></u></a><br/>
                    <span class="lite1">You-Tube:</span>&nbsp;<a href="<%=YoutubeLink%>" target="_blank"><u>Youtube Channel</u></a><br/>
                    <span class="lite1">Twitter:</span>&nbsp; <a href="<%=TwitterLink%>" target="_blank"><u><%=TwitterLink%></u></a>
                    <%--<span class="lite1">E-mail:</span>&nbsp; <a href="mailto:admin@puravam.com?Subject=Contact%20Us" target="_blank"><u>admin@puravam.com</u></a>--%>
                </p>
            </div>
            <div class="sidebox widget">
                <h3 class="widget-title">Frequently Asked Questions</h3>
                <p>Comming soon..... </p>
            </div>
        </div>
        <div class="clear">
        </div>
    </div>
</asp:Content>
