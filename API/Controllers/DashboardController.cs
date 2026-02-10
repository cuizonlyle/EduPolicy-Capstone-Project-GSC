using System;
using System.Collections.Generic;

using System.Configuration;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using API.Models;
using MySql.Data.MySqlClient;

namespace API.Controllers
{
    public class DashboardController : ApiController
    {
        //Get Student Dashboard
        [HttpGet]
        [Route("api/student/dashboard", Name = "Get_Student_Dashboard")]
        public IHttpActionResult Get_Student_Dashboard([FromUri] LoginModel s)
        {
            List<LoginModel> stats = new List<LoginModel>();
            using (MySqlConnection sqlConn = new MySqlConnection(ConfigurationManager.ConnectionStrings["const"].ConnectionString))
            {
                if (sqlConn.State == ConnectionState.Closed)
                {
                    try
                    {

                        sqlConn.Open();
                        using (MySqlCommand msqlcom = new MySqlCommand("SELECT last_name, first_name FROM stdnt_list WHERE id = @username", sqlConn))
                        {
                            msqlcom.Parameters.Add(new MySqlParameter("@username", s.username));
                            using (MySqlDataReader dtReader = msqlcom.ExecuteReader())
                            {
                                if (dtReader.HasRows)

                                {

                                    while (dtReader.Read())

                                    {
                                        LoginModel dataObj = new LoginModel();
                                        dataObj.last_name = dtReader["last_name"].ToString();
                                        dataObj.first_name = dtReader["first_name"].ToString();

                                        stats.Add(dataObj);
                                    }
                                    return Ok(stats);
                                }
                                else
                                {
                                    return NotFound();
                                }
                            }
                        }
                    }

                    catch (Exception ex)
                    {
                        return Content(HttpStatusCode.InternalServerError, ex);
                    }
                }
                else
                {
                    return InternalServerError();
                }
            }
        }

        [HttpGet]
        [Route("api/marshall/incharge", Name = "Get_Marshall_inCharge")]
        public IHttpActionResult Get_Marshall_inCharge([FromUri] LoginModel s)
        {
            List<LoginModel> stats = new List<LoginModel>();
            using (MySqlConnection sqlConn = new MySqlConnection(ConfigurationManager.ConnectionStrings["const"].ConnectionString))
            {
                if (sqlConn.State == ConnectionState.Closed)
                {
                    try
                    {

                        sqlConn.Open();
                        using (MySqlCommand msqlcom = new MySqlCommand("SELECT role, name FROM auth_ppl WHERE u_k = @u_k AND status = 'Active'", sqlConn))
                        {
                            msqlcom.Parameters.Add(new MySqlParameter("@u_k", s.u_k));
                            using (MySqlDataReader dtReader = msqlcom.ExecuteReader())
                            {
                                if (dtReader.HasRows)

                                {

                                    while (dtReader.Read())

                                    {
                                        LoginModel dataObj = new LoginModel();
                                        dataObj.role = dtReader["role"].ToString();
                                        dataObj.name = dtReader["name"].ToString();

                                        stats.Add(dataObj);
                                    }
                                    return Ok(stats);
                                }
                                else
                                {
                                    return NotFound();
                                }
                            }
                        }
                    }

                    catch (Exception ex)
                    {
                        return Content(HttpStatusCode.InternalServerError, ex);
                    }
                }
                else
                {
                    return InternalServerError();
                }
            }
        }

        
        [HttpGet]
        [Route("api/admin/incharge", Name = "Get_Adm_inCharge")]
        public IHttpActionResult Get_Adm_inCharge([FromUri] LoginModel adm)
        {
            List<LoginModel> stats = new List<LoginModel>();
            using (MySqlConnection sqlConn = new MySqlConnection(ConfigurationManager.ConnectionStrings["const"].ConnectionString))
            {
                if (sqlConn.State == ConnectionState.Closed)
                {
                    try
                    {

                        sqlConn.Open();
                        using (MySqlCommand msqlcom = new MySqlCommand("SELECT name FROM auth_ppl WHERE username = @username", sqlConn))
                        {
                            msqlcom.Parameters.Add(new MySqlParameter("@username", adm.username));
                            using (MySqlDataReader dtReader = msqlcom.ExecuteReader())
                            {
                                if (dtReader.HasRows)

                                {

                                    while (dtReader.Read())

                                    {
                                        LoginModel dataObj = new LoginModel();
                                        dataObj.name = dtReader["name"].ToString();

                                        stats.Add(dataObj);
                                    }
                                    return Ok(stats);
                                }
                                else
                                {
                                    return NotFound();
                                }
                            }
                        }
                    }

                    catch (Exception ex)
                    {
                        return Content(HttpStatusCode.InternalServerError, ex);
                    }
                }
                else
                {
                    return InternalServerError();
                }
            }
        }

        [HttpGet]
        [Route("api/marshall/list", Name = "Get_Marshall_List")]
        public IHttpActionResult Get_Marshall_List()
        {
            List<LoginModel> stats = new List<LoginModel>();
            using (MySqlConnection sqlConn = new MySqlConnection(ConfigurationManager.ConnectionStrings["const"].ConnectionString))
            {
                if (sqlConn.State == ConnectionState.Closed)
                {
                    try
                    {

                        sqlConn.Open();
                        using (MySqlCommand msqlcom = new MySqlCommand("SELECT username, name, role, status FROM auth_ppl WHERE role = 'Marshall'", sqlConn))
                        {
                            using (MySqlDataReader dtReader = msqlcom.ExecuteReader())
                            {
                                if (dtReader.HasRows)

                                {

                                    while (dtReader.Read())

                                    {
                                        LoginModel dataObj = new LoginModel();
                                        dataObj.name = dtReader["name"].ToString();
                                        dataObj.role = dtReader["role"].ToString();
                                        dataObj.status = dtReader["status"].ToString();
                                        stats.Add(dataObj);
                                    }
                                    return Ok(stats);
                                }
                                else
                                {
                                    return NotFound();
                                }
                            }
                        }
                    }

                    catch (Exception ex)
                    {
                        return Content(HttpStatusCode.InternalServerError, ex);
                    }
                }
                else
                {
                    return InternalServerError();
                }
            }
        }


    }
}

    
