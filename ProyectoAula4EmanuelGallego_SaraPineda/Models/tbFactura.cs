//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ProyectoAula4EmanuelGallego_SaraPineda.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class tbFactura
    {
        public int IdFactura { get; set; }
        public int IdAgua { get; set; }
        public int IdEnergia { get; set; }
        public int IdPrecios { get; set; }
        public decimal PagoAgua { get; set; }
        public decimal PagoEnergia { get; set; }
        public decimal PagoTotal { get; set; }
    
        public virtual tbAgua tbAgua { get; set; }
        public virtual tbEnergia tbEnergia { get; set; }
        public virtual tbPrecio tbPrecio { get; set; }
    }
}