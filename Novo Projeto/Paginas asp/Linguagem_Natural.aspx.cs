using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Google.Apis;
using Novo_Projeto;

namespace Projeto
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        static string Host;
        static int port;
        static TcpClient client;
        static string api;

        Dictionary<char, int> items_criar;
        Dictionary<char, int> indices;
        Dictionary<string, List<string>> tables_fields;
        int indice;

        public WebForm1()
        {
            //Api Google
            api = "You API";
            indice = 0;
            items_criar = new Dictionary<char, int>();
            indices = new Dictionary<char, int>();
            tables_fields = new Dictionary<string, List<string>>();

            items_criar.Add('v', 0);
            items_criar.Add('p', 0);
            items_criar.Add('n', 0);
            items_criar.Add('a', 0);

            indices.Add('v', 0);
            indices.Add('p', 0);
            indices.Add('n', 0);
            indices.Add('a', 0);
        }

        static void OpenConnection()
        {
            if (client !=null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Connection is already open---");
            }
            else
            {
                try
                {
                    client = new TcpClient();
                    client.Connect(Host, port);
                    Console.WriteLine("Connection establised sucessfully...");

                }
                catch(Exception ex)
                {
                    client = null;
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Error: Conection could not be establish. Msg: "+ ex.Message);
                }

            }

        }

        static void ClosedConnection()
        {
            if (client == null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Connection is not open or already closed---");
                return;

            }

            try
            {
                client.Close();
            }
            catch(Exception ex)
            {
                Console.WriteLine("Error: " + ex);
                throw;
            }
            finally
            {

                client = null;
            }
        }

        static string SendData(string data)
        {
            if (client == null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("---Connection is not open or closed");
                return string.Empty;
            }

            //send
            NetworkStream newStream = client.GetStream();
            byte[] bytesToSend = System.Text.ASCIIEncoding.ASCII.GetBytes(data);
            Console.WriteLine("Sending: " + data);
            newStream.Write(bytesToSend, 0, bytesToSend.Length);

            //receive
            byte[] bytesToRead = new byte[client.ReceiveBufferSize];
            int bytesRead = default(int);
             bytesRead = newStream.Read(bytesToRead, 0, client.ReceiveBufferSize);
            
            // Console.WriteLine("Received: " + System.Text.Encoding.ASCII.GetString(bytesToRead, 0, bytesRead));
            return System.Text.Encoding.ASCII.GetString(bytesToRead, 0, bytesRead);
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            Host = "127.0.0.1";
            port = 9000;
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
  
        Novo_Projeto.Tradutor.LanguageServiceClient client = new Novo_Projeto.Tradutor.LanguageServiceClient();
         string texto_traduzido = client.Translate(api, TextBox1.Text, client.Detect(api, TextBox1.Text), "en");
          // Processa Linguagem Natural
         Process(texto_traduzido);
            //Manda Para  outra Pagina

            HttpCookie myCookie = new HttpCookie("MyTestCookie");
            DateTime now = DateTime.Now;

            // Set the cookie value.
            myCookie.Value = Formatar_Mensagem_Enviar();
            // Set the cookie expiration date.
            myCookie.Expires = now.AddMinutes(5);

            // Add the cookie.
            Response.Cookies.Add(myCookie);

            //Response.Write("<p> The cookie has been written.");


            //Response.Cookies["Valor"].Value = Formatar_Mensagem_Enviar();
            Response.Redirect("~/Tabelas_Campos.aspx");
          
        }

        public string Formatar_Mensagem_Enviar()
        {
            string mensagem = string.Empty;


            for (int i = 0; i < tables_fields["table"].Count; i++)
            {
                if (i == tables_fields["table"].Count - 1)
                {
                    mensagem += tables_fields["table"][i] + "|";
                }
                else
                {
                    mensagem += tables_fields["table"][i] + " ";
                }
            
            }


            for (int i = 0; i < tables_fields["field"].Count; i++)
            {
                if (i == tables_fields["field"].Count - 1)
                {
                    mensagem += tables_fields["field"][i] + "|";
                }
                else
                {
                    mensagem += tables_fields["field"][i] + " ";
                }

            }

            return mensagem;
        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        public void Process(string texto)
        {

            string  ret  = string.Empty;

            double nota_create = default(double);
            double nota_verbo_classificacao = default(double);
            double nota_prepo = default(double);
            List<string> list_sinonimos = new List<string>();
            string last_substantivo = string.Empty;

            tables_fields.Add("table", new List<string>());
            tables_fields.Add("field", new List<string>());
      

            var linhas =  texto.Split(new string[] { "\n", "\r", "" }, StringSplitOptions.None);
         

            OpenConnection();
           

            foreach (var linha in linhas)
            {
                //    var linhas_2 = linha.Split(new string[] { ",", ".", ";", "and" }, StringSplitOptions.None);

                var linhas_2 = linha.Split(new string[] { ".", ";", "with", ",this", ",that", ", this", ", that","who", "will", "be", "have"  }, StringSplitOptions.None);

                last_substantivo = string.Empty;

                foreach (var frase in linhas_2)
                {

                    if (!frase.ToString().Equals(string.Empty))
                    {


                        ret = string.Empty;

                        string classificação = Node_Natural(";" + frase.ToString(), ref ret);


                        var palavra = frase.Split(new string[] { ",", " ", "and" }, StringSplitOptions.None);

                        nota_create = default(double);
                        nota_verbo_classificacao = default(double);
                        nota_prepo = default(double);

                        foreach (var p in palavra)
                        {
                            if (!p.ToString().Equals(string.Empty))
                            {
                                switch (Node_Natural(p, ref ret))
                                {

                                    case "n": //substantivo
                                              //comparação

                                        list_sinonimos.Clear();
                                        Sinonimos(ret, ref list_sinonimos);
                                        double nota = default(double);
                                        ret = string.Empty;

                                        foreach (string item in list_sinonimos)
                                        {                                           
                                            double var = Convert.ToDouble(Node_Natural(item + ";" + classificação, ref ret).Replace('.', ','));

                                                if (var > nota)
                                                {
                                                    nota = var;

                                                }
                                                                                  
                                        }



                                        if (nota_create >= 0.4 && nota_verbo_classificacao >= 0.7
                                            || nota_create >= 0.0 && nota_verbo_classificacao >= 0.7)
                                        {
                                            tables_fields[classificação].Add(p);
                                        }



                                        if (nota >= 0.7)
                                        {
                                            nota_verbo_classificacao = nota;

                                            //if (last_substantivo != "")
                                            //{
                                            //    tables_fields[classificação].Add(p);
                                            //}
                                        }
                                        else
                                        {
                                            last_substantivo = p;
                                        }

                                        break;

                                    case "v": // verbo


                                        double var_aux = default(double);
                                        list_sinonimos.Clear();
                                        Sinonimos(ret, ref list_sinonimos);
                                        ret = string.Empty;

                                        //comparação



                                        foreach (string item in list_sinonimos)
                                        {

                                           
                                                var_aux = Convert.ToDouble(Node_Natural(item + ";create", ref ret).Replace('.',','));


                                                if (nota_create != 1
                                                     && var_aux > nota_create)
                                                {
                                                    nota_create = var_aux;
                                                }
                                            }

                                          //  double var = Convert.ToDouble(Node_Natural(item + ";" + classificação, ref ret));

                                            //if (var >= 0.7
                                            //    && var > nota_verbo_classificacao)
                                            //{
                                            //    nota_verbo_classificacao = var;
                                            //}

                                        

                                        break;

                                    case "num":

                                        break;

                                    case "p": //preposição
                                              //comparação
                                        ret = string.Empty;

                                        double nota_1 = Convert.ToDouble(Node_Natural(p + ";are", ref ret).Replace('.', ','));
                                        ret = string.Empty;

                                        double nota_2 = Convert.ToDouble(Node_Natural(p + ";is", ref ret).Replace('.', ','));

                                        nota_prepo = nota_1 > nota_2 ? nota_1 : nota_2;

                                        break;

                                    default:
                                        break;
                                }
                            }


                        }


                    }
                }


                if (linha!="")
                {
                    tables_fields["field"].Add(tables_fields["table"].Last());
                }
               
           
                
                
            }

            ClosedConnection();
        }

        public void Sinonimos(string par, ref List<string> ret)
        {
            //fazer um esquema de contagem de "v"(verbo) e "n" (substantivo) para ver se a palavra é mais verbo
            // ou mais substantivo
            var split_1 = par.Split('|'); 

          var var_split_2  = split_1[indice].Split(';');

            ret.AddRange(var_split_2[3].Split(','));

            

        }


        public string Node_Natural(string palavra, ref string ret) 
        {

            if (palavra.Length == 1)
            {
                return "";
            }

            OpenConnection();
            string g = SendData(palavra);

            ClosedConnection();


            var split = g.Split('|');
            ret = g;

            if (split.Length == 1)
            {
                return ret;
            }

            items_criar['v'] = 0;
            items_criar['p'] = 0;
            items_criar['n'] = 0;
            items_criar['a'] = 0;

            indices['v'] = 0;
            indices['p'] = 0;
            indices['n'] = 0;
            indices['a'] = 0;
            char aux = default(char);
            indice = 0;

            indices[Convert.ToChar(split[0].Split(';')[1].ToString())] = 0;
            aux = Convert.ToChar(split[0].Split(';')[1].ToString());
            int maior = 0;
            string retorno = split[0].Split(';')[1].ToString();

            
                for (int i = 0; i < split.Length; i++)
                {
                if (!split[i].ToString().Equals(string.Empty))
                {


                    var re = split[i].Split(';')[1].ToString();

                    items_criar[Convert.ToChar(re)] += 1;

                    if (Convert.ToChar(re) != aux)
                    {
                        indices[Convert.ToChar(re)] = i;
                        aux = Convert.ToChar(re);
                    }

                    if (items_criar[Convert.ToChar(re)] > maior)
                    {
                        maior = items_criar[Convert.ToChar(re)];
                        retorno = re;

                    }

                    if (Convert.ToChar(re) =='n' )
                    {
                        indice = i;
                        retorno = re;
                        return retorno;
                    }
                }
            }





            indice = indices[Convert.ToChar(retorno)];





            return retorno;
         
        }
    }
}