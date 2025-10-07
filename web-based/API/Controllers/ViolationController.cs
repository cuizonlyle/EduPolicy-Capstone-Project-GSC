using API.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;

namespace API.Controllers
{
    public class ViolationController : ApiController
    {
        HttpResponseMessage response;

        [HttpPost]
        [Route("api/student/violation_add", Name = "Post_Violation_Add")]
        public HttpResponseMessage Post_Violation_Add([FromUri] offensemodel record)
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
                        sqlComm.CommandText = "SELECT COUNT(*) FROM auth_ppl WHERE u_k = @u_k";
                        sqlComm.Parameters.Add(new MySqlParameter("@u_k", record.u_k));

                        int count = Convert.ToInt32(sqlComm.ExecuteScalar());
                        SQLCON.Close();

                        if (count == 1)
                        {
                            SQLCON.Open();
                            sqlComm.Parameters.Clear();
                            DateTime now = DateTime.Now;
                            string loginDate = now.ToString("yyyy-MM-dd");
                            string loginTime = now.ToString("HH:mm:ss");
                            sqlComm.CommandText = "INSERT INTO violation_logs VALUES (@student_id, @last_name, @first_name, @middle_name, @gender, @date, @time, @off_id, @u_k, 'Unsettled', @prior_no)";
                            sqlComm.Parameters.Add(new MySqlParameter("@student_id", record.studentid));
                            sqlComm.Parameters.Add(new MySqlParameter("@last_name", record.last_name));
                            sqlComm.Parameters.Add(new MySqlParameter("@first_name", record.first_name));
                            sqlComm.Parameters.Add(new MySqlParameter("@middle_name", record.middle_name));
                            sqlComm.Parameters.Add(new MySqlParameter("@gender", record.gender));
                            sqlComm.Parameters.Add(new MySqlParameter("@date", loginDate));
                            sqlComm.Parameters.Add(new MySqlParameter("@time", loginTime));
                            sqlComm.Parameters.Add(new MySqlParameter("@off_id", record.off_id));
                            sqlComm.Parameters.Add(new MySqlParameter("@u_k", record.u_k));
                            sqlComm.Parameters.Add(new MySqlParameter("@prior_no", record.prior_no));
                            sqlComm.ExecuteNonQuery(); //EXECUTE MYSQL QUEUE STRING

                            response = Request.CreateResponse(HttpStatusCode.OK);
                            response.Content = new StringContent("Submitted Successfully");
                            return response;
                        }
                        else
                        {
                            response = Request.CreateResponse(HttpStatusCode.BadRequest);
                            response.Content = new StringContent("I.D not found.");
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


        [HttpPut]
        [Route("api/violation/status", Name = "Put_Violation_Status")]
        public HttpResponseMessage Put_Violation_Status([FromUri] offensemodel updatestat)
        {
            HttpResponseMessage response;
            string connectionString = ConfigurationManager.ConnectionStrings["const"].ConnectionString;

            using (MySqlConnection SQLCON = new MySqlConnection(connectionString))
            {
                try
                {
                    // Open the connection if it's closed
                    SQLCON.Open();

                    // Prepare the command for updating the violation status
                    using (MySqlCommand sqlComm = new MySqlCommand())
                    {
                        sqlComm.Connection = SQLCON;
                        sqlComm.CommandText = "UPDATE violation_logs SET status = @status WHERE prior_no = @prior_no";
                        sqlComm.Parameters.AddWithValue("@status", updatestat.status);
                        sqlComm.Parameters.AddWithValue("@prior_no", updatestat.prior_no);

                        // Execute the update query and check the number of affected rows
                        int rowsAffected = sqlComm.ExecuteNonQuery();
                        if (rowsAffected == 1)
                        {
                            // Prepare for logging the status change
                            DateTime now = DateTime.Now;
                            string loginDate = now.ToString("yyyy-MM-dd");
                            string loginTime = now.ToString("HH:mm:ss");

                            sqlComm.Parameters.Clear();
                            sqlComm.CommandText = "INSERT INTO status_logs VALUES (@prior_no1, @date, @time, @username)";
                            sqlComm.Parameters.AddWithValue("@prior_no1", updatestat.prior_no);
                            sqlComm.Parameters.AddWithValue("@date", loginDate);
                            sqlComm.Parameters.AddWithValue("@time", loginTime);
                            sqlComm.Parameters.AddWithValue("@username", updatestat.username);
                            sqlComm.ExecuteNonQuery();

                            // Return success response
                            response = Request.CreateResponse(HttpStatusCode.OK);
                            response.Content = new StringContent("Status Changed.");
                        }
                        else
                        {
                            // If no rows were affected, return an error response
                            response = Request.CreateResponse(HttpStatusCode.BadRequest);
                            response.Content = new StringContent("No records were found.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Handle any exceptions by returning an internal server error response
                    response = Request.CreateResponse(HttpStatusCode.InternalServerError);
                    response.Content = new StringContent("There is an error in performing this action: " + ex.Message, Encoding.Unicode);
                }
                finally
                {
                    // Ensure the connection is closed and disposed
                    if (SQLCON.State == ConnectionState.Open)
                        SQLCON.Close();
                    SQLCON.Dispose();
                }
            }
            return response;
        }

        [HttpPut]
        [Route("api/sanction/status", Name = "Put_Sanction_Status")]
        public HttpResponseMessage Put_Sanction_Status([FromUri] offensemodel updatestat)
        {
            HttpResponseMessage response;
            string connectionString = ConfigurationManager.ConnectionStrings["const"].ConnectionString;

            using (MySqlConnection SQLCON = new MySqlConnection(connectionString))
            {
                try
                {
                    // Open the connection if it's closed
                    SQLCON.Open();

                    // Prepare the command for updating the violation status
                    using (MySqlCommand sqlComm = new MySqlCommand())
                    {
                        sqlComm.Connection = SQLCON;
                        sqlComm.CommandText = "UPDATE sanction_logs SET sanct_status = 'Resolved' WHERE prior_no = @prior_no";
                        sqlComm.Parameters.AddWithValue("@prior_no", updatestat.prior_no);

                        // Execute the update query and check the number of affected rows
                        int rowsAffected = sqlComm.ExecuteNonQuery();
                        if (rowsAffected == 1)
                        {
                            // Prepare for logging the status change
                            DateTime now = DateTime.Now;
                            string loginDate = now.ToString("yyyy-MM-dd");
                            string loginTime = now.ToString("HH:mm:ss");

                            sqlComm.Parameters.Clear();
                            sqlComm.CommandText = "INSERT INTO status_logs1 VALUES (@prior_no1, @date, @time, @username)";
                            sqlComm.Parameters.AddWithValue("@prior_no1", updatestat.prior_no);
                            sqlComm.Parameters.AddWithValue("@date", loginDate);
                            sqlComm.Parameters.AddWithValue("@time", loginTime);
                            sqlComm.Parameters.AddWithValue("@username", updatestat.username);
                            sqlComm.ExecuteNonQuery();

                            // Return success response
                            response = Request.CreateResponse(HttpStatusCode.OK);
                            response.Content = new StringContent("Status Changed.");
                        }
                        else
                        {
                            // If no rows were affected, return an error response
                            response = Request.CreateResponse(HttpStatusCode.BadRequest);
                            response.Content = new StringContent("No records were found.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Handle any exceptions by returning an internal server error response
                    response = Request.CreateResponse(HttpStatusCode.InternalServerError);
                    response.Content = new StringContent("There is an error in performing this action: " + ex.Message, Encoding.Unicode);
                }
                finally
                {
                    // Ensure the connection is closed and disposed
                    if (SQLCON.State == ConnectionState.Open)
                        SQLCON.Close();
                    SQLCON.Dispose();
                }
            }
            return response;
        }

        [HttpGet]
        [Route("api/violation/info", Name = "Get_Violation_Info")]
        public IHttpActionResult Get_Violation_Info()
        {
            List<ObligationModel> stats = new List<ObligationModel>();

            using (MySqlConnection sqlConn = new MySqlConnection(ConfigurationManager.ConnectionStrings["const"].ConnectionString))
            {
                try
                {
                    sqlConn.Open();

                    string query = @"
    SELECT 
        violation_logs.studentid,
        CONCAT(violation_logs.last_name, ' ', violation_logs.first_name) AS fullname,
        violation_logs.status,
        DATE_FORMAT(violation_logs.date_of_v, '%M-%d-%Y') AS date_of_v, 
        violation_logs.time_of_v, 
        violation_logs.off_id, 
        offense_type.off_def, 
        offense_type.off_type,
        auth_ppl.name, 
        auth_ppl.role,
        violation_logs.prior_no
    FROM 
        violation_logs
    INNER JOIN 
        offense_type ON violation_logs.off_id = offense_type.off_id
    INNER JOIN 
        auth_ppl ON violation_logs.u_k = auth_ppl.u_k";

                    using (MySqlCommand msqlcom = new MySqlCommand(query, sqlConn))
                    {
                        using (MySqlDataReader dtReader = msqlcom.ExecuteReader())
                        {
                            if (dtReader.HasRows)
                            {
                                while (dtReader.Read())
                                {
                                    ObligationModel dataObj = new ObligationModel
                                    {
                                        studentid = dtReader["studentid"].ToString(),
                                        fullname = dtReader["fullname"].ToString(),
                                        status = dtReader["status"].ToString(),
                                        date_of_v = Convert.ToDateTime(dtReader["date_of_v"]).ToString("MMMM-dd-yyyy"),
                                        time_of_v = dtReader["time_of_v"].ToString(),
                                        off_id = dtReader["off_id"].ToString(),
                                        off_def = dtReader["off_def"].ToString(),
                                        off_type = dtReader["off_type"].ToString(),
                                        name = dtReader["name"].ToString(),
                                        role = dtReader["role"].ToString(),
                                        prior_no = dtReader["prior_no"].ToString()
                                    };

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
                    return Content(HttpStatusCode.InternalServerError, ex.Message);
                }
            }
        }

        [HttpGet]
        [Route("api/sanction/info", Name = "Get_Sanction_Info")]
        public IHttpActionResult Get_Sanction_Info([FromUri] ObligationModel assignedto)
        {
            List<ObligationModel> stats = new List<ObligationModel>();

            using (MySqlConnection sqlConn = new MySqlConnection(ConfigurationManager.ConnectionStrings["const"].ConnectionString))
            {
                try
                {
                    sqlConn.Open();

                    string query = @"
                SELECT 
                    offense_type.off_def, 
                    sanction_type.sanction_def, 
                    CONCAT(violation_logs.first_name, ' ', violation_logs.last_name) AS fullname, 
                    auth_ppl.name, 
                    sanction_logs.assigned_date, 
                    sanction_logs.sanct_status, 
                    sanction_logs.prior_no 
                FROM 
                    sanction_logs
                INNER JOIN 
                    offense_type ON sanction_logs.off_id = offense_type.off_id
                INNER JOIN 
                    sanction_type ON sanction_logs.sanction_id = sanction_type.sanction_id
                INNER JOIN 
                    violation_logs ON sanction_logs.prior_no = violation_logs.prior_no
                INNER JOIN 
                    auth_ppl ON sanction_logs.username = auth_ppl.username
                WHERE
                    violation_logs.studentid = @studentid";

                    using (MySqlCommand msqlcom = new MySqlCommand(query, sqlConn))
                    {
                        msqlcom.Parameters.AddWithValue("@studentid", assignedto.studentid);

                        using (MySqlDataReader dtReader = msqlcom.ExecuteReader())
                        {
                            if (dtReader.HasRows)
                            {
                                while (dtReader.Read())
                                {
                                    ObligationModel dataObj = new ObligationModel
                                    {
                                        off_def = dtReader["off_def"].ToString(),
                                        sanction_def = dtReader["sanction_def"].ToString(),
                                        fullname = dtReader["fullname"].ToString(),
                                        name = dtReader["name"].ToString(),
                                        assigned_date = Convert.ToDateTime(dtReader["assigned_date"]).ToString("MMMM-dd-yyyy"),
                                        sanct_status = dtReader["sanct_status"].ToString(),
                                        prior_no = dtReader["prior_no"].ToString(),
                                    };
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
                    return Content(HttpStatusCode.InternalServerError, ex.Message);
                }
            }
        }

        [HttpPost]
        [Route("api/student/sanction_add", Name = "Post_Sanction_Add")]
        public HttpResponseMessage Post_Sanction_Add([FromUri] offensemodel sa)
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
                        sqlComm.CommandText = @"
                                                SELECT COUNT(*) 
                                                FROM sanction_logs
                                                WHERE prior_no = @prior_no";
                        sqlComm.Parameters.Add(new MySqlParameter("@prior_no", sa.prior_no));

                        int count = Convert.ToInt32(sqlComm.ExecuteScalar());

                        if (count > 0)
                        {
                            response = Request.CreateResponse(HttpStatusCode.BadRequest);
                            response.Content = new StringContent("This violation number is already assigned to someone.");
                            return response;
                        }
                        else
                        {
                            sqlComm.Parameters.Clear();
                            DateTime now = DateTime.Now;
                            string loginDate = now.ToString("yyyy-MM-dd");
                            string loginTime = now.ToString("HH:mm:ss");
                            sqlComm.CommandText = "INSERT INTO sanction_logs VALUES (@sanction_id, @prior_no1, @username, @date, @time, @off_id, 'Unresolved')";
                            sqlComm.Parameters.Add(new MySqlParameter("@sanction_id", sa.sanction_id));
                            sqlComm.Parameters.Add(new MySqlParameter("@prior_no1", sa.prior_no));
                            sqlComm.Parameters.Add(new MySqlParameter("@username", sa.username));
                            sqlComm.Parameters.Add(new MySqlParameter("@date", loginDate));
                            sqlComm.Parameters.Add(new MySqlParameter("@time", loginTime));
                            sqlComm.Parameters.Add(new MySqlParameter("@off_id", sa.off_id));
                            sqlComm.ExecuteNonQuery(); //EXECUTE MYSQL QUEUE STRING

                            response = Request.CreateResponse(HttpStatusCode.OK);
                            response.Content = new StringContent("Assigned Successfully.");
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

        [HttpGet]
        [Route("api/light/list", Name = "Get_Light_Sanction")]
        public IHttpActionResult Get_Light_Sanction()
        {
            List<SanctionModel> lightoffense = new List<SanctionModel>(); // Assuming the primary key column is of type string
            using (MySqlConnection sqlConn = new MySqlConnection(ConfigurationManager.ConnectionStrings["const"].ConnectionString))
            {
                if (sqlConn.State == ConnectionState.Closed)
                {
                    try
                    {
                        sqlConn.Open();
                        using (MySqlCommand msqlcom = new MySqlCommand("SELECT sanction_id, sanction_def FROM sanction_type WHERE sanction_type = 'Light Offense'", sqlConn)) // Assuming "id" is the primary key column name
                        {
                            using (MySqlDataReader dtReader = msqlcom.ExecuteReader())
                            {
                                while (dtReader.Read())
                                {
                                    SanctionModel dataObj = new SanctionModel();
                                    dataObj.sanction_id = dtReader["sanction_id"].ToString(); // Assuming "id" is the primary key column name
                                    dataObj.sanction_def = dtReader["sanction_def"].ToString();
                                    lightoffense.Add(dataObj);
                                }
                                return Ok(lightoffense);
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
        [Route("api/less/list", Name = "Get_Less_Sanction")]
        public IHttpActionResult Get_Less_Sanction()
        {
            List<SanctionModel> lessoffense = new List<SanctionModel>(); // Assuming the primary key column is of type string
            using (MySqlConnection sqlConn = new MySqlConnection(ConfigurationManager.ConnectionStrings["const"].ConnectionString))
            {
                if (sqlConn.State == ConnectionState.Closed)
                {
                    try
                    {
                        sqlConn.Open();
                        using (MySqlCommand msqlcom = new MySqlCommand("SELECT sanction_id, sanction_def FROM sanction_type WHERE sanction_type = 'Less Serious Offense'", sqlConn)) // Assuming "id" is the primary key column name
                        {
                            using (MySqlDataReader dtReader = msqlcom.ExecuteReader())
                            {
                                while (dtReader.Read())
                                {
                                    SanctionModel dataObj = new SanctionModel();
                                    dataObj.sanction_id = dtReader["sanction_id"].ToString(); // Assuming "id" is the primary key column name
                                    dataObj.sanction_def = dtReader["sanction_def"].ToString();
                                    lessoffense.Add(dataObj);
                                }
                                return Ok(lessoffense);
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
        [Route("api/serious/list", Name = "Get_Serious_Sanction")]
        public IHttpActionResult Get_Serious_Sanction()
        {
            List<SanctionModel> serious = new List<SanctionModel>(); // Assuming the primary key column is of type string
            using (MySqlConnection sqlConn = new MySqlConnection(ConfigurationManager.ConnectionStrings["const"].ConnectionString))
            {
                if (sqlConn.State == ConnectionState.Closed)
                {
                    try
                    {
                        sqlConn.Open();
                        using (MySqlCommand msqlcom = new MySqlCommand("SELECT sanction_id, sanction_def FROM sanction_type WHERE sanction_type = 'Serious Offense'", sqlConn)) // Assuming "id" is the primary key column name
                        {
                            using (MySqlDataReader dtReader = msqlcom.ExecuteReader())
                            {
                                while (dtReader.Read())
                                {
                                    SanctionModel dataObj = new SanctionModel();
                                    dataObj.sanction_id = dtReader["sanction_id"].ToString(); // Assuming "id" is the primary key column name
                                    dataObj.sanction_def = dtReader["sanction_def"].ToString();
                                    serious.Add(dataObj);
                                }
                                return Ok(serious);
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
