using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EPiServer.Commerce.Models.ViewModels
{
    public class WarehouseInventoryViewModel
    {
        public string WarehouseCode { get; set; }
        public string WarehouseName { get; set; }
        public string WarehouseContact { get; set; }
        public string WarehouseAddress { get; set; }

        public decimal InventoryLevel { get; internal set; }
        public decimal ReservedLevel { get; internal set; }
        public bool IsAvailable { get; internal set; }
    }
}