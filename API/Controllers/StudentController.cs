using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using System.Configuration;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using System.Text;
using System.Data;
using API.Models;

namespace API.Controllers
{
    public class StudentController : ApiController
    {
        [HttpGet]
        [Route("api/loffense/list", Name = "Get_lOffense_List")]
        public IHttpActionResult Get_lOffense_List()
        {
            List<offensemodel> primaryKeys = new List<offensemodel>(); // Assuming the primary key column is of type string
            using (MySqlConnection sqlConn = new MySqlConnection(ConfigurationManager.ConnectionStrings["const"].ConnectionString))
            {
                if (sqlConn.State == ConnectionState.Closed)
                {
                    try
                    {
                        sqlConn.Open();
                        using (MySqlCommand msqlcom = new MySqlCommand("SELECT off_id, off_def FROM offense_type WHERE off_type = 'Light Offense'", sqlConn)) // Assuming "id" is the primary key column name
                        {
                            using (MySqlDataReader dtReader = msqlcom.ExecuteReader())
                            {
                                while (dtReader.Read())
                                {
                                    offensemodel dataObj = new offensemodel();
                                    dataObj.lo_id = dtReader["off_id"].ToString(); // Assuming "id" is the primary key column name
                                    dataObj.lo_def = dtReader["off_def"].ToString();
                                    primaryKeys.Add(dataObj);
                                }
                                return Ok(primaryKeys);
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
        [Route("api/lsoffense/list", Name = "Get_lsOffense_List")]
        public IHttpActionResult Get_lsOffense_List()
        {
            List<offensemodel> primaryKeys = new List<offensemodel>(); // Assuming the primary key column is of type string
            using (MySqlConnection sqlConn = new MySqlConnection(ConfigurationManager.ConnectionStrings["const"].ConnectionString))
            {
                if (sqlConn.State == ConnectionState.Closed)
                {
                    try
                    {
                        sqlConn.Open();
                        using (MySqlCommand msqlcom = new MySqlCommand("SELECT off_id, off_def FROM offense_type WHERE off_type = 'Less Serious Offense'", sqlConn)) // Assuming "id" is the primary key column name
                        {
                            using (MySqlDataReader dtReader = msqlcom.ExecuteReader())
                            {
                                while (dtReader.Read())
                                {
                                    offensemodel dataObj = new offensemodel();
                                    dataObj.lso_id = dtReader["off_id"].ToString(); // Assuming "id" is the primary key column name
                                    dataObj.lso_def = dtReader["off_def"].ToString();
                                    primaryKeys.Add(dataObj);
                                }
                                return Ok(primaryKeys);
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
        [Route("api/soffense/list", Name = "Get_sOffense_List")]
        public IHttpActionResult Get_sOffense_List()
        {
            List<offensemodel> primaryKeys = new List<offensemodel>(); // Assuming the primary key column is of type string
            using (MySqlConnection sqlConn = new MySqlConnection(ConfigurationManager.ConnectionStrings["const"].ConnectionString))
            {
                if (sqlConn.State == ConnectionState.Closed)
                {
                    try
                    {
                        sqlConn.Open();
                        using (MySqlCommand msqlcom = new MySqlCommand("SELECT off_id, off_def FROM offense_type WHERE off_type = 'Serious Offense'", sqlConn)) // Assuming "id" is the primary key column name
                        {
                            using (MySqlDataReader dtReader = msqlcom.ExecuteReader())
                            {
                                while (dtReader.Read())
                                {
                                    offensemodel dataObj = new offensemodel();
                                    dataObj.so_id = dtReader["off_id"].ToString(); // Assuming "id" is the primary key column name
                                    dataObj.so_def = dtReader["off_def"].ToString();
                                    primaryKeys.Add(dataObj);
                                }
                                return Ok(primaryKeys);
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
