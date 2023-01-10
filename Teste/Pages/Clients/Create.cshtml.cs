using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using System.Reflection;
using System.Text.RegularExpressions;

namespace Teste.Pages.Clients
{
    public class CreateModel : PageModel
    {
        public ClientInfo clientInfo = new ClientInfo();
        public String erroMessage = "";
        public String sucessMessage = "";
        public void OnGet()
        {

        }
        public void OnPost()
        {
            clientInfo.name = Request.Form["name"];
            clientInfo.email = Request.Form["email"];
            clientInfo.phone = Request.Form["phone"];

            if(clientInfo.name.Length == 0 || clientInfo.email.Length == 0 || clientInfo.phone.Length == 0)
            {
                erroMessage = " All the fields are required";
                return;
            }
            // if(bool clientInfo.email = true)
            // {
               // erroMessage = " E-mail not valid";
               // return;
            // }



            //Save the new client
            try
            {
                String connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\Cláudio\\Documents\\Teste.mdf;Integrated Security=True;Connect Timeout=30";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "INSERT INTO clients" +
                                "(name, email, phone) VALUES" +
                                "(@name, @email, @phone);";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@name", clientInfo.name);
                        command.Parameters.AddWithValue("@email", clientInfo.email);
                        command.Parameters.AddWithValue("@phone", clientInfo.phone);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch(Exception ex)
            {
                erroMessage = ex.Message;
                return;
            }
            Response.Redirect("/Clients/Index");
        }
    }
    // public class ValidateEmail
    //{
    //public static bool IsValidEmail(string email)
    //{
    //Regex emailRegex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$"), RegexOptions.Ignorecase);
    //return emailRegex.IsMatch(email);
    //}
}
