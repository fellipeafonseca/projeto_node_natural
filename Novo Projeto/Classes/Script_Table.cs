using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Novo_Projeto
{
   public class Script_Table
    {

        public string NameTable { get; set; }
        public List<string> NameFields { get; set; }
  

        public string chave;

        public void Criar_Tabela(StreamWriter writer)
        {
            writer.WriteLine("");
            writer.WriteLine("CREATE TABLE " + NameTable);
            writer.WriteLine("(");

            for (int i = 0; i < NameFields.Count; i++)
            {
                if (i == NameFields.Count - 1)
                {
                    writer.WriteLine(NameFields[i]);
                }
                else
                {
                   
                    writer.WriteLine(NameFields[i] + ",");
                }
            }

            writer.WriteLine(");");
        }
    }
}
