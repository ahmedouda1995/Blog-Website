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
    public partial class ViewGame : System.Web.UI.Page
    {
        // awaiting session from user.. done
        int gameID = 3;
        String login_email = "mido@mn.com";

        protected void Page_Load(object sender, EventArgs e)
        {
            // bengarab el 3 lines dool
            login_email = (String)Session["account"];
            String gID = (String)Session["game"];
            gameID = Int32.Parse(gID);

            DropDownList2.Items.Clear();
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
                DateTime game_releaseDate = rdr.GetDateTime(rdr.GetOrdinal("release_date"));
                int game_ageLimit = rdr.GetInt32(rdr.GetOrdinal("age_limit"));

                Label2.Text = "" + game_releaseDate;
                Label1.Text = game_name;
                Label3.Text = "" + game_ageLimit;
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
            Label9.Text = cmd1.Parameters["@rating"].Value.ToString();

            string connStr2 = ConfigurationManager.ConnectionStrings["DB1"].ToString();
            SqlConnection conn2 = new SqlConnection(connStr2);
            SqlCommand cmd2 = new SqlCommand("get_game_type", conn2);
            cmd2.CommandType = CommandType.StoredProcedure;
            cmd2.Parameters.Add(new SqlParameter("@game_id", gameID));
            cmd2.Parameters.Add(new SqlParameter("@game_type", SqlDbType.VarChar, 20));
            cmd2.Parameters["@game_type"].Direction = ParameterDirection.Output;

            conn2.Open();
            SqlDataReader rdr2 = cmd2.ExecuteReader(CommandBehavior.CloseConnection);

            rdr2.Read();
            Label4.Text = cmd2.Parameters["@game_type"].Value.ToString();

            string connStr3 = ConfigurationManager.ConnectionStrings["DB1"].ToString();
            SqlConnection conn3 = new SqlConnection(connStr3);
            SqlCommand cmd3 = new SqlCommand("get_game_type_info", conn3);
            cmd3.CommandType = CommandType.StoredProcedure;
            cmd3.Parameters.Add(new SqlParameter("@game_id", gameID));
            cmd3.Parameters.Add(new SqlParameter("@game_type_info", SqlDbType.VarChar, 50));
            cmd3.Parameters["@game_type_info"].Direction = ParameterDirection.Output;

            conn3.Open();
            SqlDataReader rdr3 = cmd3.ExecuteReader(CommandBehavior.CloseConnection);

            rdr3.Read();
            Label10.Text = cmd3.Parameters["@game_type_info"].Value.ToString();

            Label12.Text = "";

            string connStr4 = ConfigurationManager.ConnectionStrings["DB1"].ToString();
            SqlConnection conn4 = new SqlConnection(connStr4);
            SqlCommand cmd4 = new SqlCommand("get_development_teams_names", conn4);
            cmd4.CommandType = CommandType.StoredProcedure;
            cmd4.Parameters.Add(new SqlParameter("@game_id", gameID));
            conn4.Open();
            SqlDataReader rdr4 = cmd4.ExecuteReader(CommandBehavior.CloseConnection);
            while (rdr4.Read())
            {
                String dev_name = rdr4.GetString(rdr4.GetOrdinal("name"));
                String dev_email = rdr4.GetString(rdr4.GetOrdinal("email"));
                Label12.Text += dev_name + " / " + dev_email + "<br/> <br/>";
            }

            string connStr5 = ConfigurationManager.ConnectionStrings["DB1"].ToString();
            SqlConnection conn5 = new SqlConnection(connStr5);
            SqlCommand cmd5 = new SqlCommand("view_game_reviews", conn5);
            cmd5.CommandType = CommandType.StoredProcedure;
            cmd5.Parameters.Add(new SqlParameter("@game_id", gameID));
            conn5.Open();
            SqlDataReader rdr5 = cmd5.ExecuteReader(CommandBehavior.CloseConnection);
            int i = 1;
            while (rdr5.Read())
            {
                String rev_title ="Review number: " + i ;
                String review_id = ""+ rdr5.GetInt32(rdr5.GetOrdinal("game_review_id"));
                ListItem item = new ListItem();
                item.Value = review_id;
                item.Text = rev_title;
                DropDownList2.Items.Add(item);
                i++;
            }

        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void BulletedList1_Click(object sender, BulletedListEventArgs e)
        {
              
        }
        protected void bengarab(object sender, EventArgs e)
        {
            Label1.Text = "msh fifa";
        }
     
        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect("ScreenshotsAndVideos.aspx");
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            Response.Redirect("RateAGame.aspx");
        }

        protected void Button5_Click(object sender, EventArgs e)
        {
            string connStr = ConfigurationManager.ConnectionStrings["DB1"].ToString();
            SqlConnection conn = new SqlConnection(connStr);
            SqlCommand cmd = new SqlCommand("check_verified_reviewer", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@email", login_email));
            cmd.Parameters.Add(new SqlParameter("@verified", SqlDbType.Bit));
            cmd.Parameters["@verified"].Direction = ParameterDirection.Output;
            conn.Open();
            SqlDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            rdr.Read();
            Boolean b =(Boolean) cmd.Parameters["@verified"].Value;
            if (b)
            {
                Response.Redirect("ReviewAGame.aspx");
            }
            else
            {
                Label11.Visible = true;
            }
        }

        protected void Button6_Click(object sender, EventArgs e)
        {
            string connStr = ConfigurationManager.ConnectionStrings["DB1"].ToString();
            SqlConnection conn = new SqlConnection(connStr);
            SqlCommand cmd = new SqlCommand("recommend_game", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("email_send", login_email));
            cmd.Parameters.Add(new SqlParameter("email_receive", DropDownList3.SelectedItem.Text));
            cmd.Parameters.Add(new SqlParameter("game_name", Label1.Text));
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();

        }

        protected void SqlDataSource1_Selecting(object sender, SqlDataSourceSelectingEventArgs e)
        {

        }

        protected void Button7_Click(object sender, EventArgs e)
        {
            String review_id = DropDownList2.SelectedItem.Value;
            Session["Game Review ID"] = review_id;
            Response.Redirect("GameReview.aspx");
        }

        protected void DropDownList3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}