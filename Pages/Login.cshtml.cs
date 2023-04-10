using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace MyCompanion.Pages
{
    public class LoginModel : PageModel
    {
        public UserInfo userinfo = new UserInfo();
        public string errorMessage = "";
        public string successMessage = "";

        public void OnGet()
        {
        }
        public RedirectToPageResult OnPost()
        {
            string username = Request.Form["username"];
            string password = Request.Form["password"];
            int uid = 0;

            try
            {
                string connectionString = "Data Source=hosting-test1.database.windows.net;Initial Catalog=hosting-test1;User ID=major-admin;Password=Amity@123";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "select * from user_details where username=@username and passwd=@password;";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("username", username);
                        command.Parameters.AddWithValue("password", password);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {   
                                uid = reader.GetInt32(0);
                                /*userinfo.uid = reader.GetInt32(0);*/
                                if (uid > 0)
                                {
                                    /*RedirectToPage("/Option", new { uid = uid });*/
                                    successMessage = "User Found!";
                                }
                                if (uid == 0)
                                {
                                    errorMessage = "invalid username or password!";
                                    throw new Exception(errorMessage);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return RedirectToPage("");
            }

            username = "";
            password = "";

            /*Response.Redirect("/Option?uid=@uid");*/
            /*return RedirectToAction("/Option", new { uid = uid });*/
            return RedirectToPage("/Option", new { uid = uid });
        }
    }
}
