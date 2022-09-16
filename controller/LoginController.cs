using FinalTestMVC.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace FinalTestMVC.Controllers
{
    public class LoginController : Controller
    {
        DBConnectionClass DBObj = new DBConnectionClass();
        // GET: Login
        public ActionResult LoginClient()
        {
            return View();
        }
        [HttpPost]
        public ActionResult LoginClient(ClientModel User,string ReturnUrl)
        {
            ClientModel usr = new ClientModel();
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@UserName",User.UserName);
            param[1] = new SqlParameter("@Action", "Login");
            SqlDataReader sdr = DBObj.ExecuteReaderSP("SP_EmployeeDetails",param);
            if (sdr.HasRows)
            {
                sdr.Read();
                usr.UserID = int.Parse(sdr["UserID"].ToString());
                usr.UserName = sdr["UserName"].ToString();
                usr.PassCode = sdr["PassCode"].ToString();
                usr.type = sdr["Type"].ToString();
                if (usr.UserName == User.UserName && usr.PassCode == User.PassCode && usr.type == "Admin")
                {
                    FormsAuthentication.SetAuthCookie(usr.UserName, false);
                    if (ReturnUrl != null)
                    {
                        Session["id"] = usr.UserID;
                        Session["UserName"] = usr.UserName;
                        return Redirect(ReturnUrl);
                    }
                    else
                    {
                        Session["id"] = usr.UserID;
                        Session["UserName"] = usr.UserName;
                        return RedirectToAction("WelcomeADM", "Home");
                    }
                }
                if (usr.UserName == User.UserName && usr.PassCode == User.PassCode && usr.type == "User")
                {
                    FormsAuthentication.SetAuthCookie(usr.UserName, false);
                    if (ReturnUrl != null)
                    {
                        Session["id"] = usr.UserID;
                        Session["UserName"] = usr.UserName;
                        return Redirect(ReturnUrl);
                    }
                    else
                    {
                        Session["id"] = usr.UserID;
                        Session["UserName"] = usr.UserName;
                        return RedirectToAction("WelcomeUSR", "Home");
                    }
                }
                else
                {
                    ViewBag.Msg = "Invalid Credentials!!!";
                    ModelState.Clear();
                    return View();
                }


            }
            else
            {
                ViewBag.Msg = "Invalid Credentials!!!";
                ModelState.Clear();
                return View();
            }
        }

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            Session["id"] = null;
            Session["UserName"] = null;
            return RedirectToAction("LoginClient");
        }
    }
}