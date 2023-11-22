using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using BusinessObject;
namespace eStore.Models
{
    public class LoginModel
    {
        SqlConnection con = new SqlConnection("Data Source=(local); Initial Catalog=EStore; Integrated Security=True; uid=sa; pwd=123456; Trusted_Connection=False");

        private class AdminAccount : Member
        {
            public new string ToString => Email + " " + Password;
            public override bool Equals(object? obj)
            {
                return obj is AdminAccount account &&
                       Email == account.Email &&
                       Password == account.Password;
            }

            public override int GetHashCode()
            {
                return HashCode.Combine(Email, Password);
            }
        }
        private Boolean isAdminAccount(AdminAccount admin) // Check admin account, combine with json
        {
            IConfiguration config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
            IConfigurationSection jsonObject = config.GetSection("admin");
            AdminAccount admin1 = new AdminAccount
            {
                Email = jsonObject["email"],
                Password = jsonObject["password"]
            };
            return admin1.Equals(admin);
        }
        public int LoginCheck(Member member)
        {
            try {
                AdminAccount acc = new AdminAccount
                {
                    Email = member.Email,
                    Password = member.Password,
                };
                if (isAdminAccount(acc))
                {
                    return 2;
                }
                else
                {
                    SqlCommand comm = new SqlCommand("Sp_Login", con); // sử dụng Store Procedure 
                    comm.CommandType = CommandType.StoredProcedure;
                    comm.Parameters.AddWithValue("@Email", member.Email); // apply values
                    comm.Parameters.AddWithValue("@Password", member.Password);
                    SqlParameter objectLogin = new SqlParameter();
                    objectLogin.ParameterName = "@IsValid"; // check if the adding sequence is valid
                    objectLogin.SqlDbType = SqlDbType.Int;
                    objectLogin.Direction = ParameterDirection.Output;
                    comm.Parameters.Add(objectLogin);
                    con.Open();
                    comm.ExecuteNonQuery();
                    int res = Convert.ToInt32(objectLogin.Value);
                    con.Close();
                    return res;
                }               
            } catch (Exception e) {
                return 0;
            }           
        }
    }
}
