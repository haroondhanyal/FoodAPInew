using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using POSInventory.Models;

namespace POSInventory.Controllers
{
    public class LoginController : ApiController
    {
        POS_InventoryEntities1 db = new POS_InventoryEntities1();

        [HttpPost]
        public HttpResponseMessage UserLogin(Login user)
        {
            try
            {
                var userFound = db.Logins.FirstOrDefault(u => u.U_Name == user.U_Name && u.Password == user.Password);

                if (userFound == null)
                    return Request.CreateResponse(HttpStatusCode.NotFound, "Invalid Username and/or password");

                return Request.CreateResponse(HttpStatusCode.OK, userFound);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
