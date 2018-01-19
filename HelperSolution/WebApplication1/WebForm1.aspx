<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="WebApplication1.WebForm1" %>

<!DOCTYPE html>

<script runat="server">

    protected void Button1_Click(object sender, EventArgs e)
    {
        Label1.Text = "Panel refreshed at " + DateTime.Now + " " + this.GetSomeText();
       // Calendar1.
    }

    protected void chk_CheckedChanged(object sender, EventArgs e)
    {
        Label1.Text = chk.Checked.ToString();
    }

</script>

<html xmlns="http://www.w3.org/1999/xhtml">
    <head runat="server">
        <title>UpdatePanel Tutorial</title>
        <style type="text/css">
            #UpdatePanel1, #UpdatePanel2 { 
                width:300px; height:100px;
            }
        </style>
    </head>
    <body>
        <form id="form1" runat="server">
            <div>
                <asp:ScriptManager ID="ScriptManager1" runat="server">
                </asp:ScriptManager>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <fieldset>
                            <legend>UpdatePanel1</legend>
                            <asp:Label ID="Label1" runat="server" Text="Panel Created"></asp:Label><br />
                            <asp:Button ID="Button1" runat="server" Text="Refresh Panel 1" OnClick="Button1_Click" />
                            <asp:CheckBox ID="chk" runat="server" AutoPostBack="true" OnCheckedChanged="chk_CheckedChanged" />
                        </fieldset>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <fieldset>
                            <legend>UpdatePanel2</legend>
                            <asp:Calendar ID="Calendar1" runat="server"></asp:Calendar>
                        </fieldset>
                    </ContentTemplate>
                </asp:UpdatePanel>

            </div>
        </form>
    </body>
</html>
