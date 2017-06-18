using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace WebApplication1
{
    public partial class HomePage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void login(object sender, EventArgs e)
        {
            string connStr = ConfigurationManager.ConnectionStrings["MyDbConn"].ToString();
            SqlConnection conn = new SqlConnection(connStr);

            SqlCommand cmd = new SqlCommand("loginProcedure", conn);

            cmd.CommandType = CommandType.StoredProcedure;

            string username = txt_username.Text;
            string password = txt_password.Text;
            cmd.Parameters.Add(new SqlParameter("@email", username));

            SqlParameter name = cmd.Parameters.Add("@password", SqlDbType.VarChar, 15);
            name.Value = password;

            // output parm
            SqlParameter count = cmd.Parameters.Add("@count", SqlDbType.Int);
            count.Direction = ParameterDirection.Output;

            conn.Open();
            cmd.ExecuteNonQuery();


            if (count.Value.ToString().Equals("1"))
            {
                Session["account"] = username;
                Session["user"] = username;
                Response.Redirect("myprofile.aspx");
            }
            else
            {
                Response.Write("The password or the username is incorrect");
            }
            conn.Close();
        }
        protected void sign_up(object sender, EventArgs e)
        {
            string connStr = ConfigurationManager.ConnectionStrings["MyDbConn"].ToString();
            SqlConnection conn = new SqlConnection(connStr);

            SqlCommand cmd = new SqlCommand("sign_up", conn);
            SqlCommand cmd2 = new SqlCommand("checkmember", conn);
            cmd2.CommandType = CommandType.StoredProcedure;
            cmd.CommandType = CommandType.StoredProcedure;
            string username = email.Text;
            string password = sign_password.Text;
            string pregenre = preferredGenre.SelectedValue;
            string type = usertype.SelectedValue;
            cmd2.Parameters.Add(new SqlParameter("@email", username));
            SqlParameter accept = cmd2.Parameters.Add("@count", SqlDbType.Int);
            accept.Direction = ParameterDirection.Output;
            conn.Open();
            cmd2.ExecuteNonQuery();
            conn.Close();
            if (accept.Value.ToString().Equals("0"))
            {
                cmd.Parameters.Add(new SqlParameter("@email", username));

                SqlParameter name = cmd.Parameters.Add("@password", SqlDbType.VarChar, 15);
                name.Value = password;
                SqlParameter genre = cmd.Parameters.Add("@preferred_genre", SqlDbType.VarChar, 15);
                genre.Value = pregenre;
                SqlParameter userType = cmd.Parameters.Add("@type", SqlDbType.VarChar, 15);
                userType.Value = type;

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            else
            {
                Response.Write("This email has already signed up!!");
            }


        }
    }
}