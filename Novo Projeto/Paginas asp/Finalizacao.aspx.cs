using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

namespace Novo_Projeto
{
    public partial class Finalizacao : System.Web.UI.Page
    {
        
        List<string> list_tables;
        Dictionary<string, List<string>> tables_fields;
        Dictionary<string, List<string>> tables_types;
        Dictionary<string, List<string>> tipos_cSharp;
        Dictionary<string, List<string>> table_key;
        Dictionary<string, List<string>> tables_campo;

        protected void Page_Load(object sender, EventArgs e)
        {

            list_tables = new List<string>();
            tables_fields = new Dictionary<string, List<string>>();
            tipos_cSharp = new Dictionary<string, List<string>>();
            table_key = new Dictionary<string, List<string>>();
            tables_campo = new Dictionary<string, List<string>>();
           

            List<string> fields_aux = new List<string>();
            List<string> fields_aux_type = new List<string>();
            List<string> tipos_cSharp_aux_type = new List<string>();

            HttpCookie myCookie = new HttpCookie("MyTestCookie");
            DateTime now = DateTime.Now;
            myCookie = Request.Cookies["MyTestCookie"];
            string mensagem = "";

            // Read the cookie information and display it.
            if (myCookie != null)
                mensagem = myCookie.Value;

            myCookie.Expires = now.AddMinutes(-1);

            var itens = mensagem.Split('|');
            string tabela = string.Empty;


            for (int i = 0; i < itens.Length; i++)
            {

                if (!itens[i].Equals(""))
                {


                    var nova = itens[i].Split(',');
                    fields_aux = new List<string>();
                    tabela = "";

                    for (int j = 0; j < nova.Length; j++)
                    {

                        if (j == 0)
                        {
                            list_tables.Add(nova[j]);
                            tabela = nova[j];
                        }
                        else
                        {
                            fields_aux.Add(nova[j]);

                        }
                    }



                    tables_fields.Add(tabela, fields_aux);


                }
                }




            //pro

            myCookie = new HttpCookie("MyTestCookie_2");
             now = DateTime.Now;
            myCookie = Request.Cookies["MyTestCookie_2"];
             mensagem = "";

            // Read the cookie information and display it.
            if (myCookie != null)
                mensagem = myCookie.Value;

            myCookie.Expires = now.AddMinutes(-1);

            itens = mensagem.Split('|');
            tabela = string.Empty;


            for (int i = 0; i <  list_tables.Count; i++)
            {

                var nova = itens[i].Split(',');
                fields_aux = new List<string>();
                tabela = "";


               
                for (int j = 0; j < nova.Length; j++)
                {
                    fields_aux.Add(nova[j]);
                  
                }

                tipos_cSharp.Add(list_tables[i], fields_aux);

              

            }


            //key


            myCookie = new HttpCookie("MyTestCookie_1");
            now = DateTime.Now;
            myCookie = Request.Cookies["MyTestCookie_1"];
            mensagem = "";

            // Read the cookie information and display it.
            if (myCookie != null)
                mensagem = myCookie.Value;

            myCookie.Expires = now.AddMinutes(-1);

            itens = mensagem.Split('|');
            tabela = string.Empty;


            for (int i = 0; i < list_tables.Count; i++)
            {

                var nova = itens[i].Split(',');
                fields_aux = new List<string>();
                tabela = "";

                for (int j = 0; j < nova.Length; j++)
                {
                    fields_aux.Add(nova[j]);

                }

                table_key.Add(list_tables[i], fields_aux);



            }





            myCookie = new HttpCookie("MyTestCookie_3");
            now = DateTime.Now;
            myCookie = Request.Cookies["MyTestCookie_3"];
            mensagem = "";

            // Read the cookie information and display it.
            if (myCookie != null)
                mensagem = myCookie.Value;

            myCookie.Expires = now.AddMinutes(-1);

            itens = mensagem.Split('|');
            tabela = string.Empty;


            for (int i = 0; i < list_tables.Count; i++)
            {

                var nova = itens[i].Split(',');
                fields_aux = new List<string>();
                tabela = "";

                for (int j = 0; j < nova.Length; j++)
                {
                    fields_aux.Add(nova[j]);

                }

                tables_campo.Add(list_tables[i], fields_aux);



            }




        }

        protected void Button1_Click(object sender, EventArgs e)
        {

            string folder = txt_arquivos.Text; //nome do diretorio a ser criado

            //Se o diretório não existir...
            if (!Directory.Exists(folder))
            {

                //Criamos um com o nome folder
                Directory.CreateDirectory(folder);

            }



            ////declarando a variavel do tipo StreamWriter para 
            //abrir ou criar um arquivo para escrita 
            StreamWriter x = null;
            StreamWriter y = null;

            ////Colocando o caminho fisico e o nome do arquivo a ser criado
            //finalizando com .txt

            folder = txt_arquivos.Text + "\\Script_Banco_Dados"; //nome do diretorio a ser criado

            //Se o diretório não existir...

            if (!Directory.Exists(folder))
            {

                //Criamos um com o nome folder
                Directory.CreateDirectory(folder);

            }



            //aqui, exemplo de escrever no arquivo texto
            //como se fossemos criar um recibo de pagamento



            //folder = filePatch.Text + "\\Paginas HTML"; //nome do diretorio a ser criado

            ////Se o diretório não existir...

            //if (!Directory.Exists(folder))
            //{

            //    //Criamos um com o nome folder
            //    Directory.CreateDirectory(folder);

            //}
            //finalizando com .txt


            string CaminhoNome = "";
            Html_Pages html;
            Script_Table table;
            int i = 0;


            foreach (string key in list_tables)
            {
                CaminhoNome = txt_arquivos.Text + "\\Script_Banco_Dados\\Script_SQL.txt";

                //utilizando o metodo para criar um arquivo texto
                //e associando o caminho e nome ao metodo
                if (i ==0)
                {
                    y = File.CreateText(CaminhoNome);
                    i++;
                }
               

                table = new Script_Table();
                table.NameFields = tables_fields[key];
                table.NameTable = key;
                table.Criar_Tabela(y);
                //fechando o arquivo texto com o método .Close()
             



                CaminhoNome = txt_arquivos.Text + "\\" + key + ".aspx";

                //utilizando o metodo para criar um arquivo texto
                //e associando o caminho e nome ao metodo
                x = File.CreateText(CaminhoNome);

                html = new Html_Pages();

                html.labels = tables_campo[key];

                html.nomePagina = key;
                html.nome = key;
                html.chaveTabela = table_key[key][0];
                html.connectionString = txt_conection.Text;
                html.tiposCSchar = tipos_cSharp[key];
                html.nomeTabela = key;
                html.Criar_Pagina(x);
                x.Close();


                CaminhoNome = txt_arquivos.Text + "\\" + key + ".cs";


                //utilizando o metodo para criar um arquivo texto
                //e associando o caminho e nome ao metodo
                x = File.CreateText(CaminhoNome);
                html.Criar_Aspx_Cs(key, x);

                x.Close();

            }
            y.Close();


            //folder = filePatch.Text + "\\C Sharp"; //nome do diretorio a ser criado

            ////Se o diretório não existir...

            //if (!Directory.Exists(folder))
            //{

            //    //Criamos um com o nome folder
            //    Directory.CreateDirectory(folder);

            //}
            //finalizando com .txt




            //   MessageBox.Show("Arquivos Gerados com sucesso");
            Response.Write("<script>alert('Arquivos Gerados com sucesso');</script>");
            Response.Redirect("~/Inicial.aspx");

        }
    }
}