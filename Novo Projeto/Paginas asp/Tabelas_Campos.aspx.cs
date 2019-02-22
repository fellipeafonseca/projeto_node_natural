using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Novo_Projeto
{
    public partial class Tabelas_Fields : System.Web.UI.Page
    {
        List<string> list_tables;
        Dictionary<string, List<string>> tables_fields;
        Dictionary<string, List<string>> tables_keys;
        Dictionary<string, List<string>> types_csharp;

        protected void Page_Load(object sender, EventArgs e)
        {
           
            list_tables = new List<string>();
            tables_fields = new Dictionary<string, List<string>>();
            tables_keys = new Dictionary<string, List<string>>();
            types_csharp = new Dictionary<string, List<string>>();


           // string mensagem = Request.Cookies["Valor"].Value;

            //Response.Cookies["Valores"].Value = Request.Cookies["Valor"].Value;

            HttpCookie myCookie = new HttpCookie("MyTestCookie");
            DateTime now = DateTime.Now;
            myCookie = Request.Cookies["MyTestCookie"];
            string mensagem = "";

            // Read the cookie information and display it.
            if (myCookie != null)
                mensagem =  myCookie.Value;

        //    myCookie.Expires = now.AddMinutes(-1);

            var itens = mensagem.Split('|');
            List<string> fields_aux = new List<string>();

            for (int i = 0; i < itens.Length; i++)
            {
                
                var nova = itens[i].Split(' ');

                for (int j = 0; j < nova.Length; j++)
                {
                    //Primeiro Tabelas
                    if (i == 0)
                    {
                        list_tables.Add(nova[j]);
                    }

                    //Depois Campos
                    else
                    {

                        if (list_tables.Contains(nova[j]))
                        {
                            tables_fields.Add(nova[j], fields_aux);
                            fields_aux = new List<string>();

                        }
                        else
                        {
                            fields_aux.Add(nova[j]);
                        }
                    }
                }
            }



            //Montando Layout Page



         
            int count = 1;
            int x, y;

            x = 0;
            y = 0;

            foreach (string key in list_tables)
            {

                this.Page.Form.Controls.Add(new LiteralControl("<br />"));

                this.Page.Form.Controls.Add(new LiteralControl("<table>"));

                this.Page.Form.Controls.Add(new LiteralControl("<tr>"));
                this.Page.Form.Controls.Add(new LiteralControl("<td>"));
                this.Page.Form.Controls.Add(Gera_Label("Table: ","_" + (count++).ToString()));
                this.Page.Form.Controls.Add(new LiteralControl("</td>"));

              

                this.Page.Form.Controls.Add(new LiteralControl("<td>"));
                this.Page.Form.Controls.Add(Gera_Txt(key, "_" + x.ToString()));
                this.Page.Form.Controls.Add(new LiteralControl("</td>"));
                this.Page.Form.Controls.Add(new LiteralControl("</tr>"));

                this.Page.Form.Controls.Add(new LiteralControl("<br />"));

                this.Page.Form.Controls.Add(new LiteralControl("<tr>"));
                this.Page.Form.Controls.Add(new LiteralControl("<td>"));
                this.Page.Form.Controls.Add(Gera_Label("Field","_" + (count++).ToString()));
                this.Page.Form.Controls.Add(new LiteralControl("</td>"));

                this.Page.Form.Controls.Add(new LiteralControl("<td>"));
                this.Page.Form.Controls.Add(Gera_Label("Type Data", "_"+ (count++).ToString()));
                this.Page.Form.Controls.Add(new LiteralControl("</td>"));


                this.Page.Form.Controls.Add(new LiteralControl("<td>"));
                this.Page.Form.Controls.Add(Gera_Label("Length","_" + (count++).ToString()));
                this.Page.Form.Controls.Add(new LiteralControl("</td>"));

                this.Page.Form.Controls.Add(new LiteralControl("<td>"));
                this.Page.Form.Controls.Add(Gera_Label("Key","_" + (count++).ToString()));
                this.Page.Form.Controls.Add(new LiteralControl("</td>"));
                this.Page.Form.Controls.Add(new LiteralControl("</tr>"));


                y = 0;
                foreach (string item in tables_fields[key])
                {
                    this.Page.Form.Controls.Add(new LiteralControl("<tr>"));
                    this.Page.Form.Controls.Add(new LiteralControl("<td>"));
                    this.Page.Form.Controls.Add(Gera_Txt(item,"_" + x.ToString() + "_" + y.ToString()));
                    this.Page.Form.Controls.Add(new LiteralControl("</td>"));
                  

                    this.Page.Form.Controls.Add(new LiteralControl("<td>"));
                    this.Page.Form.Controls.Add(Gera_DDL("_" + x.ToString() + "_" + y.ToString()));
                    this.Page.Form.Controls.Add(new LiteralControl("</td>"));
                  

                    this.Page.Form.Controls.Add(new LiteralControl("<td>"));
                    this.Page.Form.Controls.Add(Gera_Txt("", "_"+x.ToString() + "_" + y.ToString() + "_" + y.ToString()));
                    this.Page.Form.Controls.Add(new LiteralControl("</td>"));
                  
                    this.Page.Form.Controls.Add(new LiteralControl("<td>"));
                    this.Page.Form.Controls.Add(Gera_Check("_" + x.ToString() + "_" + y.ToString()));
                    this.Page.Form.Controls.Add(new LiteralControl("</td>"));
                    this.Page.Form.Controls.Add(new LiteralControl("</tr>"));
                    y += 1;
                }
                this.Page.Form.Controls.Add(new LiteralControl("</tr>"));
                this.Page.Form.Controls.Add(new LiteralControl("</table>"));
                x += 1;
            }


            Button b = new Button();
            b.ID = "btn_1";
            b.Text = "Processar";
            b.Click += B_Click;
            b.Attributes.Add("runat", "server");
            this.Page.Form.Controls.Add(b);

            //Montando a Pagina
        }
        public void Formatar_Mensagem_Enviar(ref string mensagem, ref string mensagem_1, ref string mensagem_2, ref string mensagem_3)
        {

            string au_key = "";

            TextBox aux;
            DropDownList drop;
            CheckBox check;


           
            List<string> list_key = new List<string>();

            for (int i = 0; i < list_tables.Count; i++)
            {

                aux = (TextBox)this.Page.Form.FindControl("txt_" + i.ToString());

              //  if (i == list_tables.Count - 1)
              //  {
                    mensagem += aux.Text + ",";
             //   }



                for (int j = 0; j < tables_fields[list_tables[i]].Count; j++)
                {

                    aux = (TextBox)this.Page.Form.FindControl("txt_" + i.ToString() + "_" + j.ToString());

                    mensagem_3 += aux.Text+",";
                    mensagem += aux.Text + " ";
                    au_key = aux.Text;

                    drop = (DropDownList)this.Page.Form.FindControl("drop_" + i.ToString() + "_" + j.ToString());
                    mensagem += drop.SelectedItem.Text + " ";



                    if (drop.SelectedItem.Text.Equals("INT"))
                    {
                        mensagem_2 += "int,";
                    }


                    if (drop.SelectedItem.Text.Equals("DECIMAL"))
                    {
                        mensagem_2 += "double,";
                    }



                    if (drop.SelectedItem.Text.Equals("VARCHAR"))
                    {
                        mensagem_2 += "string,";
                    }


                    if (drop.SelectedItem.Text.Equals("DATETIME"))
                    {
                        mensagem_2 += "DateTime,";
                    }


                    aux = (TextBox)this.Page.Form.FindControl("txt_" + i.ToString() + "_" + j.ToString() + "_" + j.ToString());
                    if (!aux.Text.Equals(string.Empty))
                    {
                        mensagem += "(" + aux.Text + ") ";
                    }

                    check = (CheckBox)this.Page.Form.FindControl("check_" + i.ToString() + "_" + j.ToString());

                    if (check.Checked)
                    {
                        mensagem += "PRIMARY KEY,";

                        mensagem_1 += au_key + ",";

                    }
                    else { mensagem += ","; }


                }


                mensagem_2 = mensagem_2.Remove(mensagem_2.Length - 1);
                mensagem_2 += "|";

                mensagem_3 = mensagem_3.Remove(mensagem_3.Length - 1);
                mensagem_3 += "|";

                mensagem_1 = mensagem_1.Remove(mensagem_1.Length - 1);
                mensagem_1 += "|";

                mensagem = mensagem.Remove(mensagem.Length - 1);
                mensagem += "|";
            }




        }


        private void B_Click(object sender, EventArgs e)
        {
            string mensagem = string.Empty;
            string mensagem_1 = "";
            string mensagem_2 = "";
            string mensagem_3 = "";

            // Set the cookie value.
            Formatar_Mensagem_Enviar(ref mensagem, ref mensagem_1, ref mensagem_2, ref mensagem_3);

            HttpCookie myCookie = new HttpCookie("MyTestCookie");
            DateTime now = DateTime.Now;
            myCookie.Value = mensagem;
            // Set the cookie expiration date.
        //    myCookie.Expires = now.AddMinutes(5);


            // Add the cookie.
            Response.Cookies.Add(myCookie);


            myCookie = new HttpCookie("MyTestCookie_1");
            now = DateTime.Now;
            myCookie.Value = mensagem_1;
            // Set the cookie expiration date.
     //       myCookie.Expires = now.AddMinutes(5);


            // Add the cookie.
            Response.Cookies.Add(myCookie);


            myCookie = new HttpCookie("MyTestCookie_2");
            now = DateTime.Now;
            myCookie.Value = mensagem_2;
            // Set the cookie expiration date.
            //            myCookie.Expires = now.AddMinutes(5);
            // Add the cookie.
            Response.Cookies.Add(myCookie);


            myCookie = new HttpCookie("MyTestCookie_3");
            now = DateTime.Now;
            myCookie.Value = mensagem_3;




            // Add the cookie.
            Response.Cookies.Add(myCookie);


            Response.Redirect("~/Finalizacao.aspx");
        }


       

        public Label Gera_Label(string valor, string id)
        {
            Label lbl = new Label();

         
            lbl.ID = "lbl" + id;
            lbl.Text = valor;
            lbl.Attributes.Add("runat", "server");
            return (lbl);
        }

        public DropDownList Gera_DDL(string id)
        {
           DropDownList drop_down_list_data_types = new DropDownList();
            drop_down_list_data_types.ID = "drop" + id;
            drop_down_list_data_types.Attributes.Add("runat", "server");

            
            drop_down_list_data_types.Items.Add("INT");
            drop_down_list_data_types.Items.Add("NUMERIC");
            drop_down_list_data_types.Items.Add("DECIMAL");
            drop_down_list_data_types.Items.Add("VARCHAR");
            drop_down_list_data_types.Items.Add("DATETIME");

            return drop_down_list_data_types;
        }

        public CheckBox Gera_Check(string id)
        {
            CheckBox   check_box_key = new CheckBox();
            check_box_key.ID = "check" + id;
            check_box_key.Attributes.Add("runat", "server");

            return check_box_key;
        }

      

        public TextBox Gera_Txt(string valor, string id)
        {
            TextBox lbl = new TextBox();


            lbl.ID = "txt" + id;
           lbl.Text = valor;
            lbl.Attributes["runat"] = "server";
            return (lbl);


            
        }
    }
}