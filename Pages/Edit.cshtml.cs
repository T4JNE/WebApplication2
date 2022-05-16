using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace WebApplication2.Pages
{
    public class EditModel : PageModel
    {
        public job Job = new job();

        public void OnGet()
        {
            string id = Request.Query["Id"];

            try
            {
                string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=" + AppDomain.CurrentDomain.BaseDirectory + "database.mdf" + ";Integrated Security=True;Connect Timeout=30";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "SELECT * FROM jobs WHERE Id=@Id";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@Id", id);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if(reader.Read())
                            {
                                Job.Id = reader.GetInt32(0);
                                Job.Localization = reader.GetString(1);
                                Job.WorkerName = reader.GetString(2);
                                
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void OnPost()
        {
            Job.Id = Int32.Parse(Request.Form["Id"]);
            Job.WorkerName = Request.Form["WorkerName"];
            Job.Localization = Request.Form["Localization"];

            try
            {
                string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=" + AppDomain.CurrentDomain.BaseDirectory + "database.mdf" + ";Integrated Security=True;Connect Timeout=30";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "UPDATE jobs SET Miejscowosc=@Localization, Wykonawca=@WorkerName WHERE Id=@Id;";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@Localization", Job.Localization);
                        command.Parameters.AddWithValue("@WorkerName", Job.WorkerName);
                        command.Parameters.AddWithValue("@Id", Job.Id);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }

            Response.Redirect("baza");
        }
    }
}
