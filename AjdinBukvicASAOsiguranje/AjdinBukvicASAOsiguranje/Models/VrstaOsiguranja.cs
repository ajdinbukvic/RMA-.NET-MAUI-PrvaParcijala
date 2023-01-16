using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AjdinBukvicASAOsiguranje.Models
{
    public class VrstaOsiguranja
    {
        public int Id { get; set; }
        public string Naziv { get; set; }
        public float OsiguranaSumaPoUsluzi { get; set; }
        public float Cijena { get; set; }
        public bool Ugovorena { get; set; }
        public float Kolicina { get; set; }

    }
}
