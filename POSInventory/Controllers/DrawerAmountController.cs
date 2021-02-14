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
    public class DrawerAmountController : ApiController
    {
        POS_InventoryEntities1 db = new POS_InventoryEntities1();

        [HttpPost]
        public HttpResponseMessage PutDraweramount(DrawerAmount draweramount)
        {
            try
            {
                    var x = db.DrawerAmounts.Add(draweramount);
                    db.SaveChanges();
                    return Request.CreateResponse(HttpStatusCode.OK, "Data added successfully! "+x);
               
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }


        [HttpPost]
        public HttpResponseMessage GetDraweramount()
        {
            try
            {
                var x = db.DrawerAmounts.FirstOrDefault();
                db.SaveChanges();

                return Request.CreateResponse(HttpStatusCode.OK, x);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }


        [HttpPost]
        public HttpResponseMessage PutDrawer(Drawer drawer)
        {
            try
            {
                var x = db.Drawers.Add(drawer);
                db.SaveChanges();

                return Request.CreateResponse(HttpStatusCode.OK, x);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        public HttpResponseMessage UpdateDrawer(Drawer drawer)
        {
            try
            {
                var x = db.Drawers.FirstOrDefault(xn => xn.Counter_name == drawer.Counter_name);
                x.Drawer_amount = drawer.Drawer_amount;
                db.Entry(x).State = EntityState.Modified;
                db.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.OK, x);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        public HttpResponseMessage UpdateDrawerfromtotalsale(Drawer drawer)
        {
            try
            {
                var x = db.Drawers.FirstOrDefault(xn => xn.Counter_name == drawer.Counter_name);
                x.Drawer_amount =x.Drawer_amount+drawer.Drawer_amount;
                db.Entry(x).State = EntityState.Modified;
                db.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.OK, x);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        public HttpResponseMessage Compareamount(Drawer drawer)
        {
            try
            {
                var x = db.Drawers.Where(xn => xn.Drawer_amount > xn.limit).ToList();
                db.SaveChanges();
                if (x != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, x);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No Data");
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        //[HttpPost]
        //public HttpResponseMessage GetDraweramount(Drawer drawer)
        //{
        //    try
        //    {
        //        var x = db.Drawers.FirstOrDefault();
        //        db.SaveChanges();

        //        return Request.CreateResponse(HttpStatusCode.OK, x);
        //    }
        //    catch (Exception ex)
        //    {
        //        return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
        //    }
        //}
    }
}
