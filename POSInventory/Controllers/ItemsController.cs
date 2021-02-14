using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using POSInventory.Models;
using System.Data.Entity;

namespace POSInventory.Controllers
{
    public class ItemsController : ApiController
    {
        POS_InventoryEntities1 db = new POS_InventoryEntities1();

        [HttpGet]
        public HttpResponseMessage AllItems()
        {
            try
            {
                var Item = db.Items.OrderBy(i => i.Category).ToList();
                return Request.CreateResponse(HttpStatusCode.OK, Item);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        public HttpResponseMessage AddItem(Item item)
        {
            try
            {
                var x = db.Items.Find(item.Item_No);
                if (x == null)
                {
                    db.Items.Add(item);
                    db.SaveChanges();

                    var Item = db.Items.FirstOrDefault(i => i.Item_No == item.Item_No);
                    return Request.CreateResponse(HttpStatusCode.OK, Item);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.Found, "Item already exist");
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        public HttpResponseMessage ModifyItem(Item item)
        {
            try
            {
                var origional = db.Items.Find(item.Item_No);
                    db.Entry(origional).CurrentValues.SetValues(item);
                    db.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.OK, "Item is Modified");
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        public HttpResponseMessage DeleteItem(int ItemNo)
        {
            try
            {
                var origional = db.Items.Find(ItemNo);
                if (origional == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, "Item not found");
                }


                db.Entry(origional).State = System.Data.Entity.EntityState.Deleted;
                db.SaveChanges();

                return Request.CreateResponse(HttpStatusCode.OK, "Item is Deleted");
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

     /*   [HttpPost]
        public HttpResponseMessage deductItem(Item item)
        {
            try
            {
                var origional = db.Items.Find(item.Item_No);
                if (origional == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, "Item not found");
                }

                origional.Quantity = item.Quantity;
                db.SaveChanges();

                return Request.CreateResponse(HttpStatusCode.OK, "Item is Modified");
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }*/

            public class item
        {
            public Int64 ItemNo { get; set; }
        }

        [HttpPost]
        public HttpResponseMessage SearchItem(item ItemNo)
        {
            try
            {
                if (ItemNo.ItemNo != 0)
                {
                    var item1 = db.Items.FirstOrDefault(u => u.Item_No == ItemNo.ItemNo).Quantity;
                    if (item1>0)
                    {
                        var item2 = db.Items.Where(s => s.Item_No == ItemNo.ItemNo).ToList();
                        if (item2 != null)
                        {
                            return Request.CreateResponse(HttpStatusCode.OK, item2);
                        }
                        else
                        {
                            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Not found");
                        }
                    }
                    else
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Out of stock");
                    }
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK, "Please Enter itemno");
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }

        }

        [HttpPost]
        public HttpResponseMessage GetFrequentlyItem()
        {
            try
            {
                var Fitem = db.Items.Where(f => f.Item_No == 123 || f.Item_No == 456 || f.Item_No == 789 || f.Item_No == 987 || f.Item_No == 321 || f.Item_No == 654).ToList();
                if (Fitem != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, Fitem);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, "Data not fetched");
                }
            }catch(Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        public HttpResponseMessage checkquantity(Item item)
        {

            try
            {
                    var item1 = db.Items.FirstOrDefault(u => u.Item_No == item.Item_No);
                if (item1.Quantity < item.Quantity)
                {
                    return Request.CreateResponse(HttpStatusCode.Found, "No more stock");
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK, item1);
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }

        }
    }
}
