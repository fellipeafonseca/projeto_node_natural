<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Inicial.aspx.cs" Inherits="Novo_Projeto.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .auto-style1 {
            height: 30px;
        }
    </style>
</head>
   <body >
    <form id="form1" runat="server">
       
    <div style ="position: absolute; top: 50%; left: 50%; transform: translate(-50%, -50%);">
   <table>
       <tr >
           <td>
               <div style ="position: absolute; left: 50%; transform: translate(-50%, -50%); height: 34px; width: 636px; font-style: italic;
		font-weight: bold;
		font-size: 30px;
		font-family: arial, sans-serif;">

        <asp:Label ID="Label1" runat="server" BackColor="White" ForeColor="#000099" Text="Crie sua própria aplicação CRUD com Linguagem Natural" Font-Bold="True" Font-Size="Larger"></asp:Label>
                   </div>
               <br />

           </td>
       </tr>


         <tr >
           <td class="auto-style1">

               <asp:Image ID="Image1" runat="server" Height="416px" ImageUrl="~/Imagen/1.jpg" Width="747px" />
           </td>
       </tr>


        <tr >
           <td class="auto-style1">
               <br />
               <div style ="position: absolute; left: 50%; transform: translate(-50%, -50%); font-style: italic; font-weight: bold; font-size: 20px; font-family: arial, sans-serif;">
               <asp:Button ID="Button1" runat="server" Text="INICIAR" BackColor="#00CC00"  ForeColor="White" Font-Bold="True" Font-Size="Larger" Height="71px" Width="180px" PostBackUrl="~/Linguagem_Natural.aspx" />
          </div>
                    </td>
       </tr>
       
   </table>
        
       
   
</div>
    </form>
    
</body>
</html>
