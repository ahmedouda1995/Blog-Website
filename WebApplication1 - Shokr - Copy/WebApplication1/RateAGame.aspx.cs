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
    public partial class RateAGame : System.Web.UI.Page
    {
        // awaiting session from user.. done
        int gameID = 3;
        String login_email = "hazem@mn.com";
        protected void Page_Load(object sender, EventArgs e)
        {
            login_email = (String)Session["account"];
            String gID = (String)Session["game"];
            gameID = Int32.Parse(gID);

            string connStr = ConfigurationManager.ConnectionStrings["DB1"].ToString();
            SqlConnection conn = new SqlConnection(connStr);
            SqlCommand cmd = new SqlCommand("view_game_info", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@game_id", gameID));

            conn.Open();
            SqlDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            while (rdr.Read())
            {
                String game_name = rdr.GetString(rdr.GetOrdinal("name"));
                Label8.Text = game_name;
            }

            string connStr1 = ConfigurationManager.ConnectionStrings["DB1"].ToString();
            SqlConnection conn1 = new SqlConnection(connStr1);
            SqlCommand cmd1 = new SqlCommand("overall_rating", conn1);
            cmd1.CommandType = CommandType.StoredProcedure;
            cmd1.Parameters.Add(new SqlParameter("@game_id", gameID));
            cmd1.Parameters.Add(new SqlParameter("@rating", SqlDbType.Int));
            cmd1.Parameters["@rating"].Direction = ParameterDirection.Output;
            conn1.Open();
            SqlDataReader rdr1 = cmd1.ExecuteReader(CommandBehavior.CloseConnection);

            rdr1.Read();
            Label7.Text = cmd1.Parameters["@rating"].Value.ToString();

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            if(TextBox1.Text == "" || TextBox2.Text == "" || TextBox3.Text == "" || TextBox4.Text == "")
            {
                Label9.Text = "Rating Invalid";
                Label9.Visible = true;
            }
            else
            {
                int graphics =Int32.Parse(TextBox1.Text);
                int interactivity = Int32.Parse(TextBox1.Text);
                int uniqueness = Int32.Parse(TextBox1.Text);
                int level_design = Int32.Parse(TextBox1.Text);

                string connStr = ConfigurationManager.ConnectionStrings["DB1"].ToString();
                SqlConnection conn = new SqlConnection(connStr);
                SqlCommand cmd = new SqlCommand("Rate_game", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@g_id", gameID));
                cmd.Parameters.Add(new SqlParameter("@m_email", login_email));
                cmd.Parameters.Add(new SqlParameter("@graphics_r", graphics));
                cmd.Parameters.Add(new SqlParameter("@interactivity_r", interactivity));
                cmd.Parameters.Add(new SqlParameter("@uniqueness_r", uniqueness));
                cmd.Parameters.Add(new SqlParameter("@designlvl_r", level_design));

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();

                string connStr1 = ConfigurationManager.ConnectionStrings["DB1"].ToString();
                SqlConnection conn1 = new SqlConnection(connStr1);
                SqlCommand cmd1 = new SqlCommand("overall_rating", conn1);
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.Add(new SqlParameter("@game_id", gameID));
                cmd1.Parameters.Add(new SqlParameter("@rating", SqlDbType.Int));
                cmd1.Parameters["@rating"].Direction = ParameterDirection.Output;

                conn1.Open();
                SqlDataReader rdr1 = cmd1.ExecuteReader(CommandBehavior.CloseConnection);

                rdr1.Read();
                Label7.Text = cmd1.Parameters["@rating"].Value.ToString();

                Label9.Text = "Rating Posted";
                Label9.Visible = true;
            
            }
        }
    }
}