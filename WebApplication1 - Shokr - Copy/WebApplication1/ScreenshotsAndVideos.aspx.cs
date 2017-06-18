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
    public partial class ScreenshotsAndVideos : System.Web.UI.Page
    {
       // awaiting session from user .. done
        int gameID = 3;

        string lbl1 = "";
        string lbl2 = "";
        protected void Page_Load(object sender, EventArgs e)
        {

            String gID = (String)Session["game"];
            gameID = Int32.Parse(gID);
            // DropDownList3.Items.Clear();
            // DropDownList2.Items.Clear();
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

         //   conn.Close();
            

            string connStr1 = ConfigurationManager.ConnectionStrings["DB1"].ToString();
            SqlConnection conn1 = new SqlConnection(connStr1);
            SqlCommand cmd1 = new SqlCommand("view_game_screenshots", conn1);
            cmd1.CommandType = CommandType.StoredProcedure;
            cmd1.Parameters.Add(new SqlParameter("@game_id", gameID));

            conn1.Open();
            SqlDataReader rdr1 = cmd1.ExecuteReader(CommandBehavior.CloseConnection);
            int i = 1;
            DropDownList3.Items.Clear();
            DropDownList3.TextChanged += this.change_desc;
            while (rdr1.Read())
            {
                //  int screenshotID = rdr1.GetInt32(rdr1.GetOrdinal("screenshot_id"));
                // DateTime screenshotDate = rdr1.GetDateTime(rdr1.GetOrdinal("submitted_date"));
                String screenshot_desc = rdr1.GetString(rdr1.GetOrdinal("screenshot_description"));
                ListItem newItem = new ListItem();
                newItem.Text = "Screenshot number: " + i;
                newItem.Value = screenshot_desc;
                DropDownList3.Items.Add(newItem);
                i++;
            }

           

            string connStr3 = ConfigurationManager.ConnectionStrings["DB1"].ToString();
            SqlConnection conn3 = new SqlConnection(connStr3);
            SqlCommand cmd3 = new SqlCommand("view_game_videos", conn3);
            cmd3.CommandType = CommandType.StoredProcedure;
            cmd3.Parameters.Add(new SqlParameter("@game_id", gameID));

            conn3.Open();
            SqlDataReader rdr3 = cmd3.ExecuteReader(CommandBehavior.CloseConnection);
            int j = 1;
            DropDownList2.Items.Clear();
            while (rdr3.Read())
            {
                // int videoID = rdr3.GetInt32(rdr3.GetOrdinal("video_id"));
                //  DateTime videoDate = rdr3.GetDateTime(rdr3.GetOrdinal("submitted_date"));
                String video_desc = rdr3.GetString(rdr3.GetOrdinal("video_description"));
                ListItem newItem = new ListItem();
                newItem.Text = "Video number: " + j;
                newItem.Value = video_desc;
                DropDownList2.Items.Add(newItem);
                j++;
            }

           

        }

        private void DropDownList3_TextChanged(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        protected void DropDownList3_SelectedIndexChanged(object sender, EventArgs e)
        {
            Label2.Text = DropDownList3.Text;
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            
            Label2.Text = DropDownList3.SelectedValue;
         //   DropDownList3.Items.Clear();
        }

        protected void Button2_Click(object sender, EventArgs e)
        {

            Label4.Text = DropDownList2.SelectedValue;
       //     DropDownList2.Items.Clear();
        }

        protected void change_desc(object sender, EventArgs e)
        {

            Label2.Text = "ff";
            //     DropDownList2.Items.Clear();
        }
    }
}