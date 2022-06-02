using System;
using System.Collections.Generic;

namespace ITS_Middleware.Models.Entities
{
    public partial class Proyecto
    {
        public int id { get; set; }
        public string? nombre { get; set; }
        public string? descripcion { get; set; }
        public string usuario { get; set; } = null!;
        public string? tipoCifrado { get; set; }
        public string? metodoAutenticacion { get; set; }
        public string password { get; set; } = null!;
        public bool activo { get; set; }
    }
}
