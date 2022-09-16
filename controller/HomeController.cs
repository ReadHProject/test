using FinalTestMVC.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FinalTestMVC.Controllers
{
    public class HomeController : Controller
    {
        DBConnectionClass DBObj = new DBConnectionClass();
        [Authorize]
        // GET: Home
        public ActionResult WelcomeADM()
        {
            if (Session["id"] == null)
            {
                TempData["Msg"] = "Your Session is Expired.!!";
                return RedirectToAction("LoginClient", "Login");
            }
            return View();
        }
        [Authorize]
        public ActionResult WelcomeUSR()
        {
            if (Session["id"] == null)
            {
                TempData["Msg"] = "Your Session is Expired.!!";
                return RedirectToAction("LoginClient", "Login");
            }
            return View();
        }
        public ActionResult GetAllData()
        {
            List<ClientModel> lst = new List<ClientModel>();
            SqlParameter[] para = new SqlParameter[2];
            para[0] = new SqlParameter("@Action", "GetAllEmp");
            DataSet ds = DBObj.ExecuteDataSetSP("SP_EmployeeDetails", para);
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                lst.Add(new ClientModel()
                {
                    UserID = int.Parse(dr["UserID"].ToString()),
                    UserName = dr["UserName"].ToString(),
                    Salary = Convert.ToDecimal(dr["Salary"].ToString()),
                    Email = dr["Email"].ToString(),
                    CityName = dr["CityName"].ToString(),
                    StateName = dr["StateName"].ToString(),
                    Gender = dr["Gender"].ToString(),
                });
            }

            return Json(lst,JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetSpecififcData(int id)
        {
            List<ClientModel> lst = new List<ClientModel>();
            SqlParameter[] para = new SqlParameter[2];
            para[0] = new SqlParameter("@UserID", id);
            para[1] = new SqlParameter("@Action", "GetSpecificEmp");
            DataSet ds = DBObj.ExecuteDataSetSP("SP_EmployeeDetails", para);
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                lst.Add(new ClientModel()
                {
                    UserID = int.Parse(dr["UserID"].ToString()),
                    UserName = dr["UserName"].ToString(),
                    Salary = Convert.ToDecimal(dr["Salary"].ToString()),
                    Email = dr["Email"].ToString(),
                    PassCode = dr["PassCode"].ToString(),
                    CityID = int.Parse(dr["CityID"].ToString()),
                    StateID = int.Parse(dr["StateID"].ToString()),
                    CityName = dr["CityName"].ToString(),
                    StateName = dr["StateName"].ToString(),
                    Gender = dr["Gender"].ToString(),
                });
            }

            return Json(lst, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetState()
        {
            List<ClientModel> lst = new List<ClientModel>();
            SqlParameter[] para = new SqlParameter[1];
            para[0] = new SqlParameter("@Action", "GetState");
            DataSet ds = DBObj.ExecuteDataSetSP("SP_EmployeeDetails", para);
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                lst.Add(new ClientModel()
                {
                    StateID = int.Parse(dr["StateID"].ToString()),
                    StateName = dr["StateName"].ToString(),
                });
            }

            return Json(lst, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetCity(string id)
        {
            List<ClientModel> lst = new List<ClientModel>();
            SqlParameter[] para = new SqlParameter[2];
            para[0] = new SqlParameter("@StateID", id);
            para[1] = new SqlParameter("@Action", "GetCity");
            DataSet ds = DBObj.ExecuteDataSetSP("SP_EmployeeDetails", para);
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                lst.Add(new ClientModel()
                {
                    CityID = int.Parse(dr["CityID"].ToString()),
                    CityName = dr["CityName"].ToString(),
                });
            }

            return Json(lst, JsonRequestBehavior.AllowGet);
        }

        public ActionResult updateData(string UserID, string UserName, string PassCode, string Salary, string ddstate, string ddcity, string Email,string ddGender)
        {
            try
            {
                SqlParameter[] param1 = new SqlParameter[8];
                param1[0] = new SqlParameter("@UserName", UserName);
                param1[1] = new SqlParameter("@Salary", Convert.ToDecimal(Salary));
                param1[2] = new SqlParameter("@Email", Email);
                param1[3] = new SqlParameter("@StateID",Convert.ToInt16(ddstate));
                param1[4] = new SqlParameter("@CityID", Convert.ToInt16(ddcity));
                param1[5] = new SqlParameter("@Gender", ddGender);
                param1[6] = new SqlParameter("@PassCode",PassCode);
                param1[7] = new SqlParameter("@Action", "InsertUpdateEmp");
                int sd1 = DBObj.ExecuteNonQuerySP("SP_EmployeeDetails", param1);
                if (UserID != null)
                {
                    if (sd1 > 0)
                    {
                        string msg = "Row Updated Successfully!!!";
                        return Json(msg, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        string msg = "Error While Updating Row!!";
                        return Json(msg, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    if (sd1 > 0)
                    {
                        string msg = "Row Inserted Successfully!!!";
                        return Json(msg, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        string msg = "Error while Inserted Data!!!";
                        return Json(msg, JsonRequestBehavior.AllowGet);
                    }
                }
                
            }
            catch (Exception ex)
            {

                ViewBag.Msg = "Error!!";
                return RedirectToAction("RegisterUser");
            }
        }

        public ActionResult DeleteData(string EID)
        {
            SqlParameter[] para = new SqlParameter[2];
            para[0] = new SqlParameter("@UserID", Convert.ToInt16(EID));
            para[1] = new SqlParameter("@Action", "DeleteData");
            int ds = DBObj.ExecuteNonQuerySP("SP_EmployeeDetails", para);
            if (ds > 0)
            {
                string msg = "Row Deleted Successfully!!!";
                return Json(msg, JsonRequestBehavior.AllowGet);
            }
            else
            {
                string msg = "Error While Deleting Row!!!";
                return Json(msg, JsonRequestBehavior.AllowGet);
            }

        }

        //USER//
        public ActionResult getsesion()
        {
            List<ClientModel> lst = new List<ClientModel>();
            ClientModel emp = new ClientModel();
            if (Session["id"] != null)
            {
                emp.UserID = Convert.ToUInt16(Session["id"].ToString());
                lst.Add(emp);
            }
            return Json(lst, JsonRequestBehavior.AllowGet);
        }
    }
}

