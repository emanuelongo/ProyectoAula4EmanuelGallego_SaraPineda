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
    
    public partial class tbPrecio
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbPrecio()
        {
            this.tbFacturas = new HashSet<tbFactura>();
        }
    
        public int IdPrecios { get; set; }
        public string Servicio { get; set; }
        public decimal Precio { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbFactura> tbFacturas { get; set; }
    }
}