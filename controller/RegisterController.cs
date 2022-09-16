using FinalTestMVC.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FinalTestMVC.Controllers
{
    public class RegisterController : Controller
    {
        DBConnectionClass DBObj = new DBConnectionClass();
        // GET: Register
        public ActionResult RegisterUser()
        {
            return View();
        }
        [HttpPost]
        public ActionResult RegisterUser(ClientModel obbj)
        {
            try
            {
                ClientModel usr = new ClientModel();
                SqlParameter[] param = new SqlParameter[2];
                param[0] = new SqlParameter("@UserName", obbj.UserName);
                param[1] = new SqlParameter("@Action", "Login");
                SqlDataReader sdr = DBObj.ExecuteReaderSP("SP_EmployeeDetails", param);
                if (sdr.HasRows)
                {
                    sdr.Read();
                    usr.UserName = sdr["UserName"].ToString();
                    if (obbj.UserName == usr.UserName)
                    {
                        ViewBag.Msg = "User Already Exists!!";
                        ModelState.Clear();
                        return View();
                    }

                }
                sdr.Close();
                SqlParameter[] param1 = new SqlParameter[8];
                param1[0] = new SqlParameter("@UserName", obbj.UserName);
                param1[1] = new SqlParameter("@Salary", obbj.Salary);
                param1[2] = new SqlParameter("@Email", obbj.Email);
                param1[3] = new SqlParameter("@StateID", obbj.StateID);
                param1[4] = new SqlParameter("@CityID", obbj.CityID);
                param1[5] = new SqlParameter("@Gender", obbj.Gender);
                param1[6] = new SqlParameter("@PassCode", obbj.PassCode);
                param1[7] = new SqlParameter("@Action", "InsertUpdateEmp");
                int sd1 = DBObj.ExecuteNonQuerySP("SP_EmployeeDetails", param1);
                if (sd1 > 0)
                {
                    ViewBag.Msg = "Registered Successfully!!";
                    ModelState.Clear();
                    return View();
                }
                else
                {
                    ViewBag.Msg = "User not Registered!!";
                    ModelState.Clear();
                    return View();
                }
            }
            catch (Exception ex)
            {

                ViewBag.Msg = "User not Registered!!";
                return RedirectToAction("RegisterUser");
            }
        }
    }
}