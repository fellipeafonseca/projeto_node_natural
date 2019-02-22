<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Linguagem_Natural.aspx.cs" Inherits="Projeto.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <script src="app.js"></script>
   
    <title></title>
    <style type="text/css">
        .auto-style1 {
            width: 896px;
        }
    </style>
</head>
<body>

    <form id="form1" runat="server">
         

        <div style ="position: absolute; left: 5%;  font-style: italic; font-weight: bold; font-size: 15px; font-family: arial, sans-serif;">
        <table>
             <tr>
                <td class="auto-style1">
                    <asp:Image ID="Image1" runat="server" ImageUrl="~/Imagen/1.jpg" Width= "100" Height="100" />
                    </td>
                 </tr>
            <tr>
                <td class="auto-style1">
                   
                      <asp:Label ID="Label1" runat="server" BackColor="White" ForeColor="#000099" 
            Text="Descreva em linguagem natural as tabelas e campos que você deseja que façam parte da sua aplicação. Separe em parágrafos cada assunto (tabela) para um melhor preocessamento." Font-Bold="True" Font-Size="Larger"></asp:Label>
                         </td>

            </tr>

             <tr>
                <td class="auto-style1">

    <div style ="font-style: italic; font-weight: bold; font-size: 20px; font-family: arial, sans-serif;">
        <asp:TextBox ID="TextBox1" runat="server" Width="100%" Height="350" TextMode="MultiLine" EnableTheming="True"></asp:TextBox>
    </div>
                   
                </td>

            </tr>
            <tr>
            <td class="auto-style1">

             <div style ="position: absolute; font-style: italic; font-weight: bold; font-size: 15px; font-family: arial, sans-serif;">
        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" BackColor="#00CC00" Height="50px" Width="150px" ForeColor="White" Font-Bold="True" Font-Size="Larger" Text="Processar" />
                 </div>
 </td>

            </tr>
        </table>
      


    </div>







        <p>
       
        </p>

      

    </form>
</body>
</html>


