using Microsoft.EntityFrameworkCore;

namespace Market.Models
{
    public class SalesReport
    {
        public int ID { get; set; }
        public string productName { get; set; }
        public int price { get; set; }
        public int Quantity { get; set; }
        public int totalPayment { get; set; }
        public User user { get; set; }
        public int userID { get; set; }

        public void SaveSalesData(MarketDBContext db,List<CartItem> salesData,int currentId)
        {
            if (salesData != null)
            {

                foreach (var sales in salesData)
                {
                    SalesReport salesReport = new SalesReport();
                    int total = sales.Quantity * sales.Product.price;
                    salesReport.productName = sales.Product.name;
                    salesReport.price = sales.Product.price;
                    salesReport.Quantity = sales.Quantity;
                    salesReport.totalPayment = total;
                    salesReport.userID = currentId;
                    db.sales.Add(salesReport);
                    new CartItem().SaveChanges(db);
                }
            }
        }

        public List<SalesReport> GetSalesData(MarketDBContext db)
        {
            var ReportData = db.sales.Include(a => a.user).ToList();
            return ReportData;
        }
    }
}
