using System;
using System.Collections.Generic;

namespace ITS_Middleware.Models.Entities
{
    public partial class Usuario
    {
        public int id { get; set; }
        public string nombre { get; set; } = null!;
        public DateTime fechaAlta { get; set; }
        public string puesto { get; set; } = null!;
        public string? email { get; set; }
        public string pass { get; set; } = null!;
        public bool Activo { get; set; }
    }
}
