using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using POSInventory.Models;

namespace POSInventory.Controllers
{
    public class StockController : ApiController
    {
        POS_InventoryEntities1 db = new POS_InventoryEntities1();

        /*  [HttpGet]
          public HttpResponseMessage AllStockHistory()
          {
              try
              {
                  var stock = db.Items.OrderBy(s => s.It).ToList();
                  return Request.CreateResponse(HttpStatusCode.OK, stock);
              }
              catch (Exception ex)
              {
                  return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
              }
          } */



        [HttpPost]
        public HttpResponseMessage AddStock(Stock_Detail stockdetail)
        {
            try
            {
                var x=db.Items.Find(stockdetail.Itemno);
                if (x == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound);
                }
                else
                {
                    x.Quantity = x.Quantity+ stockdetail.Quantity;
                    db.Entry(x).State = EntityState.Modified;
                    var abc = stockdetail;
                    db.Stock_Detail.Add(stockdetail);
                    db.SaveChanges();
                    return Request.CreateResponse(HttpStatusCode.OK, x);
                }    
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }


        public class item
        {
            public int id { get; set; }
        }


        [HttpPost]
        public HttpResponseMessage SearchStockByItemNo(item id)
        {
            try
            {
                if (id.id != 0)
                {
                    var stock = db.Items.Where(s => s.Item_No == id.id).OrderBy(s => s.Quantity).ToList();
                    return Request.CreateResponse(HttpStatusCode.OK, stock);
                }
                else
                {
                    var stock = db.Items.OrderBy(s => s.Quantity).ToList();
                    return Request.CreateResponse(HttpStatusCode.OK, stock);
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
