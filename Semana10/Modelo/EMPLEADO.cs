//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Semana10.Modelo
{
    using System;
    using System.Collections.Generic;
    
    public partial class EMPLEADO
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public EMPLEADO()
        {
            this.PEDIDO = new HashSet<PEDIDO>();
        }
    
        public int IdEmpleado { get; set; }
        public string ApeEmpleado { get; set; }
        public string NomEmpleado { get; set; }
        public string DirEmpleado { get; set; }
        public int idDistrito { get; set; }
        public string fonoEmpleado { get; set; }
        public int idCargo { get; set; }
        public string Estado { get; set; }
    
        public virtual CARGO CARGO { get; set; }
        public virtual DISTRITO DISTRITO { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PEDIDO> PEDIDO { get; set; }
    }
}
