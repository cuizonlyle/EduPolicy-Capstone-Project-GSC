using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using API.Models;
using MySql.Data.MySqlClient;

namespace API.Controllers
{
    public class LoginController : ApiController
    {
        private HttpResponseMessage response;
        DataSet ds = new DataSet();

        [HttpPost]
        [Route("api/marshall/login", Name = "Get_Marshall_Login")]
        public HttpResponseMessage Get_Marshall_Login([FromUri] LoginModel m)
        {

            using (MySqlConnection SQLCON = new MySqlConnection(ConfigurationManager.ConnectionStrings["const"].ConnectionString))
            {
                try
                {
                    if (SQLCON.State == ConnectionState.Closed)
                    {
                        SQLCON.Open();
                        MySqlCommand sqlComm = new MySqlCommand();
                        sqlComm.Connection = SQLCON;
                        sqlComm.CommandText = "SELECT COUNT(*) FROM auth_ppl WHERE u_k = @u_k AND status = 'Active'";
                        sqlComm.Parameters.Add(new MySqlParameter("@u_k", m.u_k));

                        int count = Convert.ToInt32(sqlComm.ExecuteScalar());

                        if (count == 1)
                        {
                            response = Request.CreateResponse(HttpStatusCode.OK);
                            response.Content = new StringContent("Successfully Logged in");
                            return response;
                        }
                        else
                        {
                            response = Request.CreateResponse(HttpStatusCode.BadRequest);
                            response.Content = new StringContent("Incorrect Key or this Marshall is Inactive");
                            return response;
                        }

                    }
                    else
                    {
                        response = Request.CreateResponse(HttpStatusCode.InternalServerError);
                        response.Content = new StringContent("Unable to connect to the database server", Encoding.UTF8);
                        return response;
                    }
                }
                catch (Exception ex)
                {
                    response = Request.CreateResponse(HttpStatusCode.OK);
                    response.Content = new StringContent(ex.ToString(), Encoding.UTF8);
                    return response;
                }
                finally
                {
                    SQLCON.Close();
                    SQLCON.Dispose();
                }
            }
        }


        [HttpPost]
        [Route("api/admin/login", Name = "Get_Adm_Login")]
        public HttpResponseMessage Get_Adm_Login([FromUri] LoginModel adm)
        {

            using (MySqlConnection SQLCON = new MySqlConnection(ConfigurationManager.ConnectionStrings["const"].ConnectionString))
            {
                try
                {
                    if (SQLCON.State == ConnectionState.Closed)
                    {
                        SQLCON.Open();
                        MySqlCommand sqlComm = new MySqlCommand();
                        sqlComm.Connection = SQLCON;
                        sqlComm.CommandText = "SELECT COUNT(*) FROM auth_ppl WHERE username = @username AND userpassword = @userpassword AND role = 'Admin'";
                        sqlComm.Parameters.Add(new MySqlParameter("@username", adm.username));
                        sqlComm.Parameters.Add(new MySqlParameter("@userpassword", adm.userpassword));

                        int count = Convert.ToInt32(sqlComm.ExecuteScalar());

                        if (count == 1)
                        {
                            sqlComm.Parameters.Clear();
                            DateTime now = DateTime.Now;
                            string loginDate = now.ToString("yyyy-MM-dd");
                            string loginTime = now.ToString("HH:mm:ss");
                            sqlComm.CommandText = "INSERT INTO adminlogin_logs VALUES(@username1, @login_date, @login_time)";
                            sqlComm.Parameters.Add(new MySqlParameter("@username1", adm.username));
                            sqlComm.Parameters.Add(new MySqlParameter("@login_date", loginDate));
                            sqlComm.Parameters.Add(new MySqlParameter("@login_time", loginTime));
                            sqlComm.ExecuteNonQuery();

                            response = Request.CreateResponse(HttpStatusCode.OK);
                            response.Content = new StringContent("Successfully Logged in");
                            return response;
                        }
                        else
                        {
                            response = Request.CreateResponse(HttpStatusCode.BadRequest);
                            response.Content = new StringContent("(Avoid unauthorized access) Double check password.");
                            return response;
                        }

                    }
                    else
                    {
                        response = Request.CreateResponse(HttpStatusCode.InternalServerError);
                        response.Content = new StringContent("Unable to connect to the database server", Encoding.UTF8);
                        return response;
                    }
                }
                catch (Exception ex)
                {
                    response = Request.CreateResponse(HttpStatusCode.OK);
                    response.Content = new StringContent(ex.ToString(), Encoding.UTF8);
                    return response;
                }
                finally
                {
                    SQLCON.Close();
                    SQLCON.Dispose();
                }
            }
        }

    }
}
