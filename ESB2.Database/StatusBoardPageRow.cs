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
    
    public partial class StatusBoardPageRow
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public StatusBoardPageRow()
        {
            this.StatusBoardPageRowItems = new HashSet<StatusBoardPageRowItem>();
        }
    
        public int Id { get; set; }
        public int StatusBoardPageId { get; set; }
        public int RowIndex { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StatusBoardPageRowItem> StatusBoardPageRowItems { get; set; }
    }
}