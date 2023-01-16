using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AjdinBukvicASAOsiguranje.Models
{
    public class KlasaOsiguranja
    {
        public int Id { get; set; }
        public string NazivKlase { get; set; }
        public int FaktorOsiguraneSume { get; set; }
        public int FaktorCijene { get; set; }
        public bool Ugovorena { get; set; }
    }
}
