namespace LuminousStudio.Web.ViewModels.Statistic
{
    using System.ComponentModel.DataAnnotations;

    public class StatisticVM
    {
        [Display(Name = "Count Clients")]
        public int CountClients { get; set; }

        [Display(Name = "Count Tiffany Lamps")]
        public int CountTiffanyLamps { get; set; }

        [Display(Name = "Count Orders")]
        public int CountOrders { get; set; }

        [Display(Name = "Total Sum Orders")]
        public decimal SumOrders { get; set; }
    }
}
