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
    public partial class ReviewAGame : System.Web.UI.Page
    {
        int gameID = 3; //sessions done 
        String login_email = "mido@mn.com";
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
                Label1.Text = game_name;

            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string connStr = ConfigurationManager.ConnectionStrings["DB1"].ToString();
            SqlConnection conn = new SqlConnection(connStr);
            SqlCommand cmd = new SqlCommand("add_game_review", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@game_id", gameID));
            cmd.Parameters.Add(new SqlParameter("@content", TextBox1.Text));
            cmd.Parameters.Add(new SqlParameter("@email", login_email));
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
            Response.Redirect("ViewGame.aspx");
            Label2.Visible = true;

        }
    }
}