﻿<%@ Page Async="true" Language="C#" AutoEventWireup="true" CodeBehind="FbAdmin.aspx.cs" Inherits="SocialHashTagWebForm.FbAdmin" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:LinkButton ID="AuthButtonLinkButton" runat="server" OnClick="OnClick" Text="Авторизоваться"></asp:LinkButton>
            <asp:Repeater runat="server" ID="MessagesRepeater">
                <ItemTemplate>
                    <div style='display: <%# Eval("Display") %>'>
                        <%# Eval("Id") %>
                        <br />

                        <iframe title="YouTube video player" class="youtube-player" type="text/html"
                            width="640" height="390" src='<%# Eval("VideoEmbebbedUrl") %>'
                            frameborder="0" allowfullscreen></iframe>

                        <asp:LinkButton  runat="server" Text="Одобрить" CommandArgument='<%# Eval("Id") %>' OnClick="Like_Click"></asp:LinkButton>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </form>
</body>
</html>
