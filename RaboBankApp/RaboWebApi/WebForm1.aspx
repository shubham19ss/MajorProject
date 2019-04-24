<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="RaboWebApi.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
   <div align="center" >
        <table border="1" style="background-color: #3C5A99">
         
            <tr align="center">
             <td colspan="4">
        
                 <asp:Button Id="LoginBtn" runat="server" Text="GETDATA" BackColor="#FFCCFF" BorderColor="Black" BorderWidth="1px" OnClick="LoginBtn_Click"  />
             </td>
             </tr>
 </table>
       <asp:GridView ID="GridViewRole" runat="server">
            <Columns>
        <asp:BoundField ItemStyle-Width="150px" DataField="Rid" HeaderText="Rolecode"/>
        <asp:BoundField ItemStyle-Width="150px" DataField="RName" HeaderText="RoleName"/>
        
    </Columns>
       </asp:GridView>
    </div>
    </form>
</body>
</html>
