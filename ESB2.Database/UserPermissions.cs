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
    
    [Flags]
    public enum UserPermissions : int
    {
        ReportsOnly = 1,
        StatusUpdates = 2,
        EquipmentMangement = 4,
        StatusBoardManagement = 8,
        UserManagement = 16,
        Admin = 32
    }
}
