using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace MyCompanion.Pages
{
    public class OptionModel : PageModel
    {
        public UserInfo userInfo = new UserInfo();
        public void OnGet()
        {
            string uid = Request.Query["uid"];

            try
            {
                string connectionString = "Data Source=hosting-test1.database.windows.net;Initial Catalog=hosting-test1;User ID=major-admin;Password=Amity@123";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "select * from user_details where uid=@uid;";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("uid", uid);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                userInfo.name = reader.GetString(1);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
