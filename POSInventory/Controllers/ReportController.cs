using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using POSInventory.Models;

namespace POSInventory.Controllers
{
    public class ReportController : ApiController
    {
        POS_InventoryEntities1 db = new POS_InventoryEntities1();

        [HttpGet]
        public HttpResponseMessage SearchSale(int sid)
        {
            try
            {
                var sl = db.Sales_Detail.Where(s => s.Sale_Id == sid).OrderBy(s => s.Sale_Id).ToArray();
                int price = 0;
                int Saleid = 0;
                int itms = 0;
                for (int i = 0; i < sl.Length; i++)
                {
                    price = price + sl[i].Price.Value;
                    itms = itms + 1;
                    Saleid = sl[i].Sale_Id.Value;
                }
                ArrayList arrlst = new ArrayList();
                arrlst.Add(Saleid);
                arrlst.Add(itms);
                arrlst.Add(price);
                return Request.CreateResponse(HttpStatusCode.OK, arrlst);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        public HttpResponseMessage WholeSale()
        {
            try
            {
                var sl = db.Sales_Detail.OrderBy(s => s.Sale_Id).ToArray();
                int price = 0;
                int Saleid = 0;
                int itms = 0;
                for (int i = 0; i < sl.Length; i++)
                {
                    Saleid = sl[i].Sale_Id.Value;
                    itms = itms + 1;
                    price = price + sl[i].Price.Value;
                }
                string[] arr = new string[2];
                arr[0] = itms.ToString();
                arr[1] = price.ToString();
                return Request.CreateResponse(HttpStatusCode.OK, arr);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        public class Report
        {  public DateTime startdate { get; set; }
            public DateTime enddate { get; set; }
            public Int64 itemno { get; set; }
        }

        [HttpPost]
        public HttpResponseMessage SalereportWithDate(Report r1)
        {
            try
            {
                var sl2 = db.Sales_Detail.Where(s2 => s2.Sale_Date >= r1.startdate && s2.Sale_Date <= r1.enddate && s2.Item_No == r1.itemno).ToArray();
                if (sl2 != null)
                {
                    var arrlst2 = new List<Dictionary<string, object>>();
                    int price = 0;
                    long checkitemno = 0;
                    string checkdate = null;
                    for (int i = 0; i < sl2.Length;)
                    {
                        price = 0;
                        if (sl2[i].Sale_Date.Value.ToString() == checkdate && sl2[i].Item_No.Value == checkitemno)
                        {
                            i++;
                        }
                        else
                        {
                            checkdate = sl2[i].Sale_Date.Value.ToString();
                            checkitemno = sl2[i].Item_No.Value;
                            var dic = new Dictionary<string, object>();

                            for (int j = 0; j < sl2.Length; j++)
                            {
                                if (sl2[j].Sale_Date.Value.ToString() == checkdate && sl2[j].Item_No == checkitemno)
                                {
                                    price = price + sl2[j].Price.Value;

                                }
                            }
                            dic.Add("Date", sl2[i].Sale_Date.Value.ToShortDateString());
                            dic.Add("Itemno", sl2[i].Item_No);
                            dic.Add("Totalsale", price);
                            arrlst2.Add(dic);
                            i++;
                        }
                    }
                    return Request.CreateResponse(HttpStatusCode.OK, arrlst2);
                }
                else
                {

                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Not Found");
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        public HttpResponseMessage StockreportWithDate(Report r2)
        {
            try
            {
                var sl2 = db.Stock_Detail.Where(s2 => s2.Date >= r2.startdate && s2.Date <= r2.enddate && s2.Itemno == r2.itemno).ToArray();
                if (sl2 != null)
                {
                    var arrlst2 = new List<Dictionary<string, object>>();
                    int quantity = 0;
                    Int64 checkitemno = 0;
                    string checkdate = null;
                    for (int i = 0; i < sl2.Length;)
                    {
                        quantity = 0;
                        if (sl2[i].Date.Value.ToString() == checkdate && sl2[i].Itemno.Value == checkitemno)
                        {
                            i++;
                        }
                        else
                        {
                            checkdate = sl2[i].Date.Value.ToString();
                            checkitemno = sl2[i].Itemno.Value;
                            var dic = new Dictionary<string, object>();

                            for (int j = 0; j < sl2.Length; j++)
                            {
                                if (sl2[j].Date.Value.ToString() == checkdate && sl2[j].Itemno == checkitemno)
                                {
                                    quantity = quantity + sl2[j].Quantity.Value;

                                }
                            }
                            dic.Add("Date", sl2[i].Date.Value.ToShortDateString());
                            dic.Add("Itemno", sl2[i].Itemno);
                            dic.Add("Quantity", quantity);
                            arrlst2.Add(dic);
                            i++;
                        }
                    }
                    return Request.CreateResponse(HttpStatusCode.OK, arrlst2);
                }
                else
                {

                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Not Found");
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        } 
    }
}
