//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace POSInventory.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Item
    {
        public long Item_No { get; set; }
        public string Item_Name { get; set; }
        public string Category { get; set; }
        public string Brand { get; set; }
        public Nullable<int> Retail_Price { get; set; }
        public Nullable<int> Cost_Price { get; set; }
        public Nullable<int> Threshhold_Quantity { get; set; }
        public Nullable<int> Quantity { get; set; }
    }
}
