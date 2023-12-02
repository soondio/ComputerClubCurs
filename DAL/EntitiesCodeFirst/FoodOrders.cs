namespace DAL.EntitiesCodeFirst
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class FoodOrders
    {
        public int Id { get; set; }

        public int UserID { get; set; }

        public DateTime? OrderDateTime { get; set; }

        public decimal? TotalPrice { get; set; }
        public int FoodCount { get; set; }
        public int FoodId { get; set; }

        [StringLength(20)]
        public string OrderStatus { get; set; }

        public virtual Users Users { get; set; }
        public virtual Food Food { get; set; }
    }
}
