<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ChangePassword.aspx.cs" Inherits="RaboBankApp.ChangePassword" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div align="center">
    <h1><marquee>RABO BANK SERVICES</marquee></h1>
</div>
            <div></div></div>
        <div></div>
        <div></div>
        <div></div>
    <div align="center" >
        <table border="1" style="background-color: #3C5A99">
         <tr align="center">
             <td colspan="4">
        <asp:Label ID="Login" runat="server" Text="Change Password Page" ForeColor="White"></asp:Label>
             </td>
             </tr>
             <tr>
             <td>
        <asp:Label ID="Label2" runat="server" Text="Enter OldPassword:" ForeColor="White"></asp:Label>
             </td>
                 <td>
                   <asp:TextBox ID="oldpass" runat="server" ></asp:TextBox>

                 </td>
             </tr>
             <tr>
             <td>
        <asp:Label ID="Label3" runat="server" Text="Enter NewPassword:" ForeColor="White"></asp:Label>
             </td>
                  <td>
                   <asp:TextBox ID="newpass" runat="server" ></asp:TextBox>

                 </td>
             </tr>
             <tr>
             <td>
        <asp:Label ID="Label1" runat="server" Text="Confirm Password:" ForeColor="White"></asp:Label>
             </td>
                  <td>
                   <asp:TextBox ID="repass" runat="server" ></asp:TextBox>

                 </td>
             </tr>
            <tr align="center">
             <td colspan="4">
        
                 <asp:Button Id="ChangeBtn" runat="server" Text="CHANGE" BackColor="#FFCCFF" BorderColor="Black" BorderWidth="1px"  />
             </td>
             </tr>
 </table>
    </div>
    </form>
</body>
</html>
