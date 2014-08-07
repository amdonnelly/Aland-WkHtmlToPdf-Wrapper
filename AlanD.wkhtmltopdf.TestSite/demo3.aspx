<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="demo3.aspx.cs" Inherits="AlanD.wkhtmltopdf.TestSite.demo3" ValidateRequest="false" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Demo 3</title>
</head>
<body>
    <form id="form1" runat="server">
        <h2>Demo3 - Output url and direct html as one or separate PDF's</h2>
        <table>
            <tr>
                <td style="width:100px;"><asp:Label ID="lbl_source_1" runat="server" Text="URL Source" AssociatedControlID="fld_source_1"></asp:Label></td>
                <td><asp:TextBox ID="fld_source_1" runat="server" ></asp:TextBox></td>
            </tr>
            <tr>
                <td><asp:Label ID="lbl_source_2" runat="server" Text="HTML Source" AssociatedControlID="fld_source_2"></asp:Label></td>
                <td><asp:TextBox ID="fld_source_2" runat="server" TextMode="MultiLine" Rows="10" Columns="30"></asp:TextBox></td>
            </tr>
            <tr>
                <td><asp:Label ID="lbl_single" runat="server" Text="Single PDF" AssociatedControlID="chk_single"></asp:Label></td>
                <td>
                    <asp:CheckBox ID="chk_single" runat="server" />
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Button ID="btn_submit" runat="server" Text="Generate" OnClick="Btn_Submit_Click" />
                </td>
            </tr>

            <tr>
                <td colspan="2">&nbsp;</td>
            </tr>
            <tr>
                <td colspan="2">Results</td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:TextBox ID="fld_results" runat="server" TextMode="MultiLine" Rows="20" Columns="65"></asp:TextBox>
                </td>
            </tr>
        </table>


        
    </form>
</body>
</html>
