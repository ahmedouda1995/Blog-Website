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
    public partial class GameReview : System.Web.UI.Page
    {
        // awaiting session from user.. done
        int gameID = 3;
        String login_email = "hazem@mn.com";
        
        protected void Page_Load(object sender, EventArgs e)
        {
            String s = (String)Session["Game Review ID"];
            int gameReviewID = Int32.Parse(s);
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
                Label9.Text = game_name;

            }

            string connStr1 = ConfigurationManager.ConnectionStrings["DB1"].ToString();
            SqlConnection conn1 = new SqlConnection(connStr1);
            SqlCommand cmd1 = new SqlCommand("get_game_review_info", conn1);
            cmd1.CommandType = CommandType.StoredProcedure;
            cmd1.Parameters.Add(new SqlParameter("@game_review_id", gameReviewID));

            conn1.Open();
            SqlDataReader rdr1 = cmd1.ExecuteReader(CommandBehavior.CloseConnection);

            while (rdr1.Read())
            {
                String rev_content = rdr1.GetString(rdr1.GetOrdinal("content"));
                String rev_date = "" + rdr1.GetDateTime(rdr1.GetOrdinal("review_date"));
                String reviewer_email = rdr1.GetString(rdr1.GetOrdinal("email"));
                Label12.Text = rev_date;
                Label13.Text = reviewer_email;
                Label11.Text = rev_content;
            }


            string connStr2 = ConfigurationManager.ConnectionStrings["DB1"].ToString();
            SqlConnection conn2 = new SqlConnection(connStr2);
            SqlCommand cmd2 = new SqlCommand("list_comments_Game", conn2);
            cmd2.CommandType = CommandType.StoredProcedure;
            cmd2.Parameters.Add(new SqlParameter("@game_review_id", gameReviewID));


            conn2.Open();
            SqlDataReader rdr2 = cmd2.ExecuteReader(CommandBehavior.CloseConnection);
            while (rdr2.Read())
            {
                String cmnt_txt = rdr2.GetString(rdr2.GetOrdinal("comment_text"));
                String cmnt_email = rdr2.GetString(rdr2.GetOrdinal("email"));
                int comment_id = rdr2.GetInt32(rdr2.GetOrdinal("game_review_comment_id"));

                LinkButton link_email = new LinkButton();
                link_email.Text = cmnt_email + ":";
                link_email.ID = cmnt_email;
                link_email.Click += this.link_email_click;
                form1.Controls.Add(link_email);

                Label lbl_text = new Label();
                lbl_text.Text = cmnt_txt ;
                form1.Controls.Add(lbl_text);

                Button delete_comment = new Button();
                lbl_text.Text = " DELETE"+ " <br /> <br />";
                lbl_text.ID = "A" + comment_id;
                form1.Controls.Add(lbl_text);
                delete_comment.Click += this.deleteAComment;


            }



        }

        private void deleteAComment(object sender, EventArgs e)
        {
            Button b = sender as Button;
            int id_comment = Int32.Parse((b.ID).Substring(1));
            String s = (String)Session["Game Review ID"];
            int gameReviewID = Int32.Parse(s);
            string connStr2 = ConfigurationManager.ConnectionStrings["DB1"].ToString();
            SqlConnection conn2 = new SqlConnection(connStr2);
            SqlCommand cmd2 = new SqlCommand("delete_comments_Game", conn2);
            cmd2.CommandType = CommandType.StoredProcedure;
            cmd2.Parameters.Add(new SqlParameter("@game_id", gameID));
            cmd2.Parameters.Add(new SqlParameter("@game_email", Label13.Text));
            cmd2.Parameters.Add(new SqlParameter("@email", login_email));
            cmd2.Parameters.Add(new SqlParameter("@game_review_comment_id", id_comment));

            conn2.Open();
            cmd2.ExecuteNonQuery();
            conn2.Close();
            Response.Redirect("GameReview.aspx");

        }

        protected void link_email_click (object sender, EventArgs e)
        {
            LinkButton b = sender as LinkButton;
            String email = b.ID;
            Session["Commenter Email"] = email;
            // Responce.Redirect("Page from user compoment");  
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Session["user"] = Label13.Text;
            Response.Redirect("verifiedreviewerProfile.aspx");
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            String s = (String)Session["Game Review ID"];
            int gameReviewID = Int32.Parse(s);
            string connStr2 = ConfigurationManager.ConnectionStrings["DB1"].ToString();
            SqlConnection conn2 = new SqlConnection(connStr2);
            SqlCommand cmd2 = new SqlCommand("add_comment_game_review", conn2);
            cmd2.CommandType = CommandType.StoredProcedure;
            cmd2.Parameters.Add(new SqlParameter("@game_review_id", gameReviewID));
            cmd2.Parameters.Add(new SqlParameter("@game_id", gameID));
            cmd2.Parameters.Add(new SqlParameter("@game_email", Label13.Text));
            cmd2.Parameters.Add(new SqlParameter("@email", login_email));
            cmd2.Parameters.Add(new SqlParameter("@comment_text", TextBox1.Text));

            conn2.Open();
            cmd2.ExecuteNonQuery();
            conn2.Close();
            Response.Redirect("GameReview.aspx");
        }
    }
}