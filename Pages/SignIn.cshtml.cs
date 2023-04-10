using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace MyCompanion.Pages
{
    public class SignInModel : PageModel
    {
        public UserInfo userinfo = new UserInfo();
        public string errorMessage = "";
        public string successMessage = "";
        public void OnGet()
        {
        }

        public void OnPost()
        {
            userinfo.name = Request.Form["name"];
            userinfo.email = Request.Form["email"];
            userinfo.password = Request.Form["password"];
            userinfo.username = Request.Form["username"];
            userinfo.phone = Request.Form["phone"];
            userinfo.aadhar = Request.Form["aadhar"];
            userinfo.city = Request.Form["city"];
            userinfo.pin = Request.Form["pin"];

            if (userinfo.name.Length == 0)
            {
                errorMessage = "All the Fields are Required!";
                return;
            }
            /* (TODO) all the test conditions are to be put here */

            /* Saving the new user into the database */
            try
            {
                string connectionString = "Data Source=hosting-test1.database.windows.net;Initial Catalog=hosting-test1;User ID=major-admin;Password=Amity@123";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "INSERT INTO user_details (name, email, phone, aadhar, city, pin, username, passwd) VALUES " +
                        "(@name, @email, @phone, @aadhar, @city, @pin, @username, @password);";
                    /*                    string sql = "insert into user_details (name, email, phone, aadhar, city, pin, username, passwd) values('vxcvewq', 'fdsfscxvx', 'vbvbb', 'dasdasdas', 'dasdasd', 'dasdasd', 'dasdasdada', 'dsdasda');";
                    */
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("name", userinfo.name);
                        command.Parameters.AddWithValue("email", userinfo.email);
                        command.Parameters.AddWithValue("password", userinfo.password);
                        command.Parameters.AddWithValue("username", userinfo.username);
                        command.Parameters.AddWithValue("phone", userinfo.phone);
                        command.Parameters.AddWithValue("aadhar", userinfo.aadhar);
                        command.Parameters.AddWithValue("city", userinfo.city);
                        command.Parameters.AddWithValue("pin", userinfo.pin);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

            userinfo.name = "";
            userinfo.email = "";
            userinfo.password = "";
            userinfo.username = "";
            userinfo.phone = "";
            userinfo.aadhar = "";
            userinfo.city = "";
            userinfo.pin = "";

            successMessage = "New User Added Successfully!";

            Response.Redirect("/Option");
        }
    }
}

public class UserInfo
{
    public int uid;
    public string username;
    public string email;  
    public string password;
    public string name;
    public string phone;
    public string city;
    public string aadhar;
    public string pin;
}