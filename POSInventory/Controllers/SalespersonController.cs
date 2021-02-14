using POSInventory.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace POSInventory.Controllers
{
    public class SalespersonController : ApiController
    {
        POS_InventoryEntities1 db = new POS_InventoryEntities1();
        [HttpPost]
        public HttpResponseMessage Addsalespersondetail(Salespersondetail salespersondetail2)
        {
            try
            {
                    db.Salespersondetails.Add(salespersondetail2);
                    db.SaveChanges();
                    return Request.CreateResponse(HttpStatusCode.OK, salespersondetail2);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        public class Salespersondetail2
        {
            public String Date { get; set; }
        }

        [HttpPost]
        public HttpResponseMessage Searchsalepersondetail(Salespersondetail2 Date)
        {
            try
            {
                    var salesperson = db.Salespersondetails.Where(s => s.Date == Date.Date).OrderBy(s => s.Date).ToList();
                    return Request.CreateResponse(HttpStatusCode.OK, salesperson);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
