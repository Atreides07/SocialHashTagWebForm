<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="SocialHashTagWebForm.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Repeater runat="server" ID="MessagesRepeater">
                <ItemTemplate>
                    <div>
                        <iframe title="YouTube video player" class="youtube-player" type="text/html"
                            width="640" height="390" src='<%# Eval("VideoUrl") %>'
                            frameborder="0" allowfullscreen></iframe>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </form>
</body>
</html>
