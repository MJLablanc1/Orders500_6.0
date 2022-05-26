using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Orders500.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Orders500.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        // GET: api/<OrderController>/orders
        [Route("orders")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderData>>> GetOrderData()
        {
            var context = new OrdersDBContext();
            return await context.OrdersTables.Select(x => new OrderData
            {
                OrderId = x.OrdersId,
                StoreId = x.StoreId,
                SalesId = x.SalesPersonId,               
                CdId = x.CdId,
                PricePaid = x.PricePaid,
                Date = x.Date

            }).ToListAsync();
        }

        // GET: api/<OrderController>/store
        [Route("store")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StoreDropdownEntry>>> GetStorePData()
        {
            var context = new OrdersDBContext();
            return await context.StoreTables.Select(x => new StoreDropdownEntry
            {
                city = x.City,
                StoreID = x.StoreId,

            }).ToListAsync();
        }

        // GET: api/<OrderController>/cds
        [Route("cds")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CDsDropdownEntry>>> GetCdData()
        {
            var context = new OrdersDBContext();
            return await context.CdTables.Select(x => new CDsDropdownEntry
            {
                cdName = x.Cdname,
                listPrice = x.ListPrice,
                cdID = x.CdId,

            }).ToListAsync();
        }

        // GET: api/<OrderController>/sales
        [Route("sales")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SalesPersonDropdownEntry>>> GetSalesPData()
        {
            var context = new OrdersDBContext();
            return await context.SalesPersonTables.Select(x => new SalesPersonDropdownEntry
            {
                last = x.LastName,
                first = x.FirstName,
                salesPersonId = x.SalesPersonId,

            }).ToListAsync();
        }

        // GET: api/<OrderController>/preformance
        [Route("preformance/{id:int}")]
        [HttpGet("{id}")]
        public PerfomanceData GetStoreSales(int id)
        {
            var context = new OrdersDBContext();
            var saleList = (from order in context.OrdersTables
                            where order.StoreId == id
                            select new { order.PricePaid, order.Store.City });
            int totalSales = 0;
            foreach (var sale in saleList)
            {
                totalSales += sale.PricePaid;
            }

            var temp = new PerfomanceData();
            temp.cityName = saleList.First().City;
            temp.totalSalesAmount = totalSales;

            return temp;
        }

        // GET: api/<OrderController>/Highest
        [Route("highest")]
        [HttpGet]
        public List <HighestSalesData> GetHighestSales()
        {
            List<HighestSalesData> highestSales = new List<HighestSalesData>();
            List<string> validCities = new List<string>();

            var context = new OrdersDBContext();
            var saleQ = (from order in context.OrdersTables
                            where order.PricePaid > 13
                            select order.Store.City);

            //Change the query to a list for better indexing
            var saleList = saleQ.ToList();            
            
            //Set up valid city for checking agaisnt the query list
            foreach (var city in saleQ)
            {                
                if (!validCities.Contains(city))
                {
                    validCities.Add(city);
                }
            }

            //Switching between each valid city at a time,
            //Then check the entire query list for
            //matching cities and count those
            //Lastly push the city name and total sales into
            //the model and return list
            foreach (var city in validCities)
            {
                var temp = new HighestSalesData();
                int totalSalesOver13 = 0;
                for (int i = 0; i < saleList.Count(); i++)
                {
                    if (saleList[i].ToString() == city.ToString())
                    {
                        totalSalesOver13++;
                    }
                }
                temp.city = city;
                temp.salesNumber = totalSalesOver13;
                highestSales.Add(temp);
            }

            return highestSales;

        }

        // POST api/<OrderController>
        [Route("post")]
        [HttpPost]
        public void Post([FromBody] newOrder oneOrder)
        {
            var context = new OrdersDBContext();

            OrdersTable newOrder = new OrdersTable();
            newOrder.StoreId = oneOrder.StoreId;
            newOrder.SalesPersonId = oneOrder.SalesPersonId;
            newOrder.CdId = oneOrder.CdId;
            newOrder.PricePaid = oneOrder.PricePaid;
            newOrder.Date = DateTime.Now.ToString();

            try
            {
                context.OrdersTables.Add(newOrder);
                context.SaveChanges();
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }
        }



        // GET api/<OrderController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<OrderController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<OrderController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<OrderController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
    public class PerformanceTemp
    {
        public int cityId { get; set; }
        public int pricePaid { get; set; }
    }

    public class newOrder
    {
        public int OrdersId { get; set; }
        public int StoreId { get; set; }
        public int SalesPersonId { get; set; }
        public int CdId { get; set; }
        public int PricePaid { get; set; }
    }
}

