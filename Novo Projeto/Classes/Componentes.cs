using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Novo_Projeto
{
    public class Componentes
    {

        public List<string> tabelas { get; set; }
        public Dictionary<string, List<string>> campos { get; set; }
        public Dictionary<string, List<string>> tipos_campos { get; set; }

    }
}
