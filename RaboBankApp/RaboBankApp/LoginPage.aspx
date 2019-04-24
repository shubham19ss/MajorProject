<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LoginPage.aspx.cs" Inherits="RaboBankApp.LoginPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
       
        <div></div>
        <div></div>
        <div>
            <div></div>
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
        <asp:Label ID="Login" runat="server" Text="Login page" ForeColor="White"></asp:Label>
             </td>
             </tr>
             <tr>
             <td>
        <asp:Label ID="Label2" runat="server" Text="Enter UserId:" ForeColor="White"></asp:Label>
             </td>
                 <td>
                   <asp:TextBox ID="Userid" runat="server" ></asp:TextBox>

                 </td>
             </tr>
             <tr>
             <td>
        <asp:Label ID="Label3" runat="server" Text="Enter Password:" ForeColor="White"></asp:Label>
             </td>
                  <td>
                   <asp:TextBox ID="Pass" runat="server" ></asp:TextBox>

                 </td>
             </tr>
            <tr align="center">
             <td colspan="4">
        
                 <asp:Button Id="LoginBtn" runat="server" Text="LOGIN" BackColor="#FFCCFF" BorderColor="Black" BorderWidth="1px" OnClick="LoginBtn_Click"  />
             </td>
             </tr>
 </table>
    </div>
    </form>
</body>
</html>
