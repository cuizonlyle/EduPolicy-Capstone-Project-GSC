using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using API.Models;
using System.Text;

namespace API.Controllers
{
    public class AddNewStaffController : ApiController
    {
        private HttpResponseMessage response;

        [HttpPost]
        [Route("api/staff/new", Name = "Add_Staff")]
        public HttpResponseMessage Add_Staff([FromUri] LoginModel staffNew)
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
                        sqlComm.CommandText = "SELECT COUNT(*) FROM auth_ppl WHERE u_k = @u_k AND role = 'Marshall'";
                        sqlComm.Parameters.Add(new MySqlParameter("@u_k", staffNew.u_k));

                        int count = Convert.ToInt32(sqlComm.ExecuteScalar());

                        if (count == 0)
                        {
                            sqlComm.Parameters.Clear();
                            DateTime now = DateTime.Now;
                            string dateCreated = now.ToString("yyyy-MM-dd");
                            string timeCreated = now.ToString("HH:mm:ss");
                            sqlComm.CommandText = "INSERT INTO auth_ppl(name, u_k, role, status, date_created, time_created) VALUES(@name, @u_k1, 'Marshall', 'Active', @date, @time)";
                            sqlComm.Parameters.Add(new MySqlParameter("@name", staffNew.name));
                            sqlComm.Parameters.Add(new MySqlParameter("@u_k1", staffNew.u_k));
                            sqlComm.Parameters.Add(new MySqlParameter("@date", dateCreated));
                            sqlComm.Parameters.Add(new MySqlParameter("@time", timeCreated));
                            sqlComm.ExecuteNonQuery();

                            response = Request.CreateResponse(HttpStatusCode.OK);
                            response.Content = new StringContent("Successfully Added.");
                            return response;
                        }
                        else
                        {
                            response = Request.CreateResponse(HttpStatusCode.BadRequest);
                            response.Content = new StringContent("This Key is assigned to someone else.");
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
