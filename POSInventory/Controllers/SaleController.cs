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
    public class SaleController : ApiController
    {
        POS_InventoryEntities1 db = new POS_InventoryEntities1();

        [HttpPost]
        public HttpResponseMessage Sale(Sales_Detail sale)
        {
            try
            {
                var dt = sale.Sale_Date;
                db.Sales_Detail.Add(sale);
                
                var x = db.Items.FirstOrDefault(xn => xn.Item_No == sale.Item_No);
                x.Quantity = x.Quantity - sale.Item_Quantity;
                db.Entry(x).State = EntityState.Modified; 
                db.SaveChanges();

                return Request.CreateResponse(HttpStatusCode.OK, sale);
            }catch(Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
