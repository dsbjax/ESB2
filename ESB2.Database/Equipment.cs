//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ESB2.Database
{
    using System;
    using System.Collections.Generic;
    
    public partial class Equipment
    {
        public int Id { get; set; }
        public string Nomenclature { get; set; }
        public string Description { get; set; }
        public EquipmentStatus EquipmentStatus { get; set; }
        public OperationalStatus OperationalStatus { get; set; }
        public int EquipmentGroupId { get; set; }
        public Nullable<int> StatusBoardStaticPageItemId { get; set; }
    }
}
