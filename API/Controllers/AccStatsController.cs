using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using API.Models;
using System.Data;
using System.Text;

namespace API.Controllers
{
    public class AccStatsController : ApiController
    {
        HttpResponseMessage response;

        [HttpPut]
        [Route("api/staff/change_status", Name = "Put_Staff_Status")]
        public HttpResponseMessage Put_Staff_Status([FromUri] LoginModel staff)
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
                        sqlComm.CommandText = "SELECT COUNT(*) FROM auth_ppl WHERE u_k = @u_k AND role = 'Admin'";
                        sqlComm.Parameters.Add(new MySqlParameter("@u_k", staff.u_k));

                        int count = Convert.ToInt32(sqlComm.ExecuteScalar());
                        SQLCON.Close();

                        if (count == 1)
                        {
                            SQLCON.Open();
                            sqlComm.Parameters.Clear();
                            DateTime now = DateTime.Now;
                            string loginDate = now.ToString("yyyy-MM-dd");
                            string loginTime = now.ToString("HH:mm:ss");
                            sqlComm.CommandText = "INSERT INTO status_logs2 VALUES (@u_k1, @date, @time, @name)";
                            sqlComm.Parameters.Add(new MySqlParameter("@u_k1", staff.u_k));
                            sqlComm.Parameters.Add(new MySqlParameter("@date", loginDate));
                            sqlComm.Parameters.Add(new MySqlParameter("@time", loginTime));
                            sqlComm.Parameters.Add(new MySqlParameter("@name", staff.name));
                            sqlComm.ExecuteNonQuery(); //EXECUTE MYSQL QUEUE STRING

                            sqlComm.Parameters.Clear();

                            // Second Query: UPDATE (Modify accordingly)
                            sqlComm.CommandText = "UPDATE auth_ppl SET status = 'Inactive' WHERE name = @name2 AND role ='Marshall'";
                            sqlComm.Parameters.Add(new MySqlParameter("@name2", staff.name)); // Adjust condition
                            sqlComm.ExecuteNonQuery();

                            response = Request.CreateResponse(HttpStatusCode.OK);
                            response.Content = new StringContent("Successfully changed status.");
                            return response;
                        }
                        else
                        {
                            response = Request.CreateResponse(HttpStatusCode.BadRequest);
                            response.Content = new StringContent("Unauthorized Key.");
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
                    response = Request.CreateResponse(HttpStatusCode.InternalServerError);

                    response.Content = new StringContent("There is an error in performing this action: " + ex.ToString(), Encoding.Unicode);

                    return response;
                }
                finally //ALWAYS CLOSE AND DISPOSE THE CONNECTION AFTER USING
                {
                    SQLCON.Close();
                    SQLCON.Dispose();

                }
            }
        }
    }
}
