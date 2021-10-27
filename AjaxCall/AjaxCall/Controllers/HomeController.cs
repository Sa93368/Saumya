//using Entity;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using AjaxCall.Models;
using System;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AjaxCall.Controllers
{
    public class HomeController : Controller
    {
        string conStr = "Data Source=.\\SQLEXPRESS ; Initial Catalog=employeeregistration; Integrated Security=True;";

        public ActionResult Index()
        {
            return View("Test");
        }

        public DataSet GetEmployeeData()
        {
            DataSet ds = new DataSet();
            try
            {
                using (SqlConnection connection = new SqlConnection(conStr))
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = connection;
                    cmd.CommandText = "GetEmployeeList";
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter adp = new SqlDataAdapter(cmd);
                    adp.Fill(ds);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                ds.Dispose();
            }
            return ds;
        }

        //public DataSet GetEmployeeData()
        //{
        //    DataSet ds = new DataSet();
        //    try
        //    {
        //        using (SqlConnection connection = new SqlConnection(conStr))
        //        {
        //            SqlCommand cmd = new SqlCommand();
        //            cmd.Connection = connection;
        //            cmd.CommandText = "GetEmployeeData";
        //            cmd.CommandType = CommandType.StoredProcedure;
        //            SqlDataAdapter adp = new SqlDataAdapter(cmd);
        //            adp.Fill(ds);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    finally
        //    {
        //        ds.Dispose();
        //    }
        //    return ds;
        //}

        public ActionResult SaveEmpData(string firstName,string address,string email,string gender,string phone,string username,string password)
        {

            int result = 0;
            SqlCommand cmd = new SqlCommand();
            try
            {
                using (SqlConnection connection = new SqlConnection(conStr))
                {
                    cmd.Connection = connection;
                    cmd.CommandText = "EmployeReg";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@name",firstName);
                    cmd.Parameters.AddWithValue("@address", address);
                    cmd.Parameters.AddWithValue("@email", email);
                    cmd.Parameters.AddWithValue("@gender", gender);
                    cmd.Parameters.AddWithValue("@phone", phone);
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@password", password);




                    connection.Open();
                    result = cmd.ExecuteNonQuery();
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                result = 0;
            }
            finally
            {
                cmd.Dispose();
            }
            return Json("Success");
        }
    }
 }
        