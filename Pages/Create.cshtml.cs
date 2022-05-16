using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace WebApplication2.Pages
{
    public class CreateModel : PageModel
    {
        public job Job = new job();
        public void OnGet()
        {
        }

        public void OnPost()
        {
            Job.WorkerName = Request.Form["WorkerName"];
            Job.Localization = Request.Form["Localization"];

            try
            {
                string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=" + AppDomain.CurrentDomain.BaseDirectory + "database.mdf" + ";Integrated Security=True;Connect Timeout=30";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "INSERT INTO jobs (Miejscowosc, Wykonawca) VALUES (@Localization, @WorkerName);";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@Localization", Job.Localization);
                        command.Parameters.AddWithValue("@WorkerName", Job.WorkerName);

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
