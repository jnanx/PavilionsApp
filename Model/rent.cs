//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PavilionsApp.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class rent
    {
        public int rentID { get; set; }
        public int tenantID { get; set; }
        public int employeeID { get; set; }
        public int pavilionID { get; set; }
        public int rentStatusID { get; set; }
        public System.DateTime startRent { get; set; }
        public System.DateTime endRent { get; set; }
    
        public virtual employee employees { get; set; }
        public virtual pavilion pavilions { get; set; }
        public virtual rentStatus rentStatuses { get; set; }
        public virtual tenant tenants { get; set; }
    }
}
