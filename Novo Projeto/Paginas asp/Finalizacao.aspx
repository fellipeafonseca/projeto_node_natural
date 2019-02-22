<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Finalizacao.aspx.cs" Inherits="Novo_Projeto.Finalizacao" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .auto-style1 {
            width: 184px;
            height: 70px;
        }
        .auto-style2 {
            height: 70px;
            width: 396px;
        }
        .auto-style4 {
            width: 184px;
        }
        .auto-style5 {
            width: 396px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
                 
                 
        <div style ="position: absolute; left: 5%;  font-style: italic; font-weight: bold; font-size: 15px; font-family: arial, sans-serif;">
        <table>
              <tr>
                <td class="auto-style1">
             <asp:Image ID="Image1"  runat="server" ImageUrl="~/Imagen/1.jpg" Width= "100" Height="100" />
</td>

              </tr>
             <tr>
                <td class="auto-style1">
                    <asp:Label ID="Label1" runat="server" Text="Connection String"></asp:Label>

                </td>

                 <td class="auto-style2">
                       <asp:TextBox ID="txt_conection" runat="server" Width="410px" ></asp:TextBox>

                </td>
            </tr>

            <tr>
                <td class="auto-style4">
                           <asp:Label ID="Label2" runat="server" Text="Caminho Arquivos"></asp:Label>

                </td>

                 <td class="auto-style5">
                        <asp:TextBox ID="txt_arquivos" runat="server" Width="410px"></asp:TextBox>

                </td>
            </tr>

              <tr>
                <td class="auto-style4">
                    <div style ="position: absolute; font-style: italic; font-weight: bold; font-size: 10px; font-family: arial, sans-serif;">
            <br />
                         <asp:Button ID="Button1" runat="server" BackColor="#00CC00" ForeColor="White" Font-Bold="True" Font-Size="Larger" Text="Gerar Arquivos" OnClick="Button1_Click" />
                        </div>

</td>
                  </tr>
        </table>
         </div>
     

 
      

       
    </div>
    </form>
</body>
</html>
