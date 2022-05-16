using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace WebApplication2.Pages
{
    public class job
    {
        public int Id;
        public string Localization;
        public string WorkerName;
    }

    public class bazaModel : PageModel
    {
        public List<job> job_list = new();

        public void OnGet()
        {
            try
            {
                string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename="+ AppDomain.CurrentDomain.BaseDirectory + "database.mdf" + ";Integrated Security=True;Connect Timeout=30";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "SELECT * FROM jobs";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using(SqlDataReader reader = command.ExecuteReader())
                        {
                            while(reader.Read())
                            {
                                job _job = new job();
                                _job.Id = reader.GetInt32(0);
                                _job.Localization = reader.GetString(1);
                                _job.WorkerName = reader.GetString(2);

                                job_list.Add(_job);
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
    }
}
