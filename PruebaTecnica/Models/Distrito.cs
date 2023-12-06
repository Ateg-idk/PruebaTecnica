using System;
using System.Collections.Generic;

namespace PruebaTecnica.Models
{
    public partial class Distrito
    {
        public Distrito()
        {
            Trabajadores = new HashSet<Trabajadore>();
        }

        public int Id { get; set; }
        public int? IdProvincia { get; set; }
        public string? NombreDistrito { get; set; }

        public virtual Provincium? IdProvinciaNavigation { get; set; }
        public virtual ICollection<Trabajadore> Trabajadores { get; set; }
    }
}
