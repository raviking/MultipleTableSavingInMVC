using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MultipleTableSavingInMVC.Models;
using System.Data.SqlClient;

namespace MultipleTableSavingInMVC.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult SaveUser(TUser UserData)
        {
            int Userid = 0;
            try
            {
                if (UserData != null)
                {
                    using (DatabaseEntities db = new DatabaseEntities())
                    {
                        //saves the user data and returns userid from procedure.
                        var id = db.Database.SqlQuery<int>("sp_InsertUser @Name,@Email,@PhoneNumber",
                            new SqlParameter("Name",UserData.Name),
                            new SqlParameter("Email",UserData.Email),
                            new SqlParameter("PhoneNumber",UserData.PhoneNumber)).Single();
                        Userid = id;
                    }
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
            return Json(Userid, JsonRequestBehavior.AllowGet);
        }
        //saves Address data TAddress table
        public JsonResult SaveAddress(TAddress AddressData)
        {        
            bool result = false;
            try
            {
                if (AddressData != null)
                {
                    using (DatabaseEntities db = new DatabaseEntities())
                    {
                        db.TAddresses.Add(AddressData);
                        db.SaveChanges();
                        result = true;
                    }
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        //saves orderdata in TOrders table
        public JsonResult SaveOrders(TOrder OrdersData)
        {
            bool result = false;
            try
            {
                if (OrdersData != null)
                {
                    using (DatabaseEntities db = new DatabaseEntities())
                    {
                        db.TOrders.Add(OrdersData);
                        db.SaveChanges();
                        result = true;
                    }
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}