using System.Collections.Generic;
using System.Linq;

namespace WebApiTest.Model
{
    public class StockServices
    {
        public static List<Stocks> stockList=new List<Stocks>()
        {
            new Stocks { Id = 1, Name = "台積電", UnitPrice = 602.56M },
            new Stocks { Id = 2, Name = "元大0050", UnitPrice = 102.33M },
            new Stocks { Id = 3, Name = "富邦00900", UnitPrice = 27.21M },
             new Stocks { Id = 4, Name = "台尼", UnitPrice = 42.25M }
        };

        public List<Stocks> QueryAll()
        {
            return stockList;
        }

        public Stocks SpecificStock(int id)
        {
            return stockList.Where(s=>s.Id==id).FirstOrDefault();
        }

        public bool Create(Stocks stock)
        {
            stockList.Add(stock);
            return true;
        }

        public bool Update(Stocks stock)
        {
            var item = stockList.Where(s=>s.Id==stock.Id).FirstOrDefault();
            if (item != null)
            {
                item.UnitPrice=stock.UnitPrice;
            }
            return true;
        }

        public bool Delete(int Id)
        {
            var item = stockList.Where(s => s.Id == Id).FirstOrDefault();
            if (item!=null)
            {
                stockList.Remove(item);
            }
            return true;
        }
    }
}
