using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Drawing;

namespace WebApplication1
{
    public partial class ReviewPage : System.Web.UI.Page
    {
        string Log_in_email;
        int R_id;
        int C_id;
        TextBox comment;
        protected void Page_Load(object sender, EventArgs e)
        {
            Log_in_email = (string)(Session["User_Email"]);

            String Rid = (string)(Session["rev_id"]);
            R_id = Int32.Parse(Rid);

            String Cid = (string)(Session["conferences_id"]);
            C_id = Int32.Parse(Cid);




            string connStr = ConfigurationManager.ConnectionStrings["MyDbConn"].ToString();
            SqlConnection conn = new SqlConnection(connStr);

            SqlCommand cmd = new SqlCommand("search_review", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@R_id", R_id));


            conn.Open();
            SqlDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);


            Label emtpy = new Label();
            emtpy.Text = "Review: " + " <br /> <br />";
            form1.Controls.Add(emtpy);
            int counter1 = 1000;
            while (rdr.Read())
            {

                string email = rdr.GetString(rdr.GetOrdinal("email"));
                string content = rdr.GetString(rdr.GetOrdinal("content"));
                DateTime review_date = rdr.GetDateTime(rdr.GetOrdinal("review_date"));


                LinkButton lbl_email = new LinkButton();
                lbl_email.Text = "(" + email + "): " + "  <br /> <br />";
                lbl_email.ID = counter1+"," + email;
                lbl_email.Click += this.patrick_click;
                form1.Controls.Add(lbl_email);
                counter1--;

                Label lbl_content = new Label();
                lbl_content.Text = content + " (" + review_date + ") " + " <br /> <br />";
                form1.Controls.Add(lbl_content);




            }

            Label emtpy1 = new Label();
            emtpy1.Text = "  <br /> <br />";
            form1.Controls.Add(emtpy1);

            Label lbl_comments = new Label();
            lbl_comments.Text = "Comments: " + "  <br /> <br />";
            form1.Controls.Add(lbl_comments);



            string connStr2 = ConfigurationManager.ConnectionStrings["MyDbConn"].ToString();
            SqlConnection conn2 = new SqlConnection(connStr2);

            SqlCommand cmd2 = new SqlCommand("list_comments_conference", conn2);
            cmd2.CommandType = CommandType.StoredProcedure;
            cmd2.Parameters.Add(new SqlParameter("@conference_review_id", R_id));


            conn2.Open();
            SqlDataReader rdr2 = cmd2.ExecuteReader(CommandBehavior.CloseConnection);

            int counter = 0;
            while (rdr2.Read())
            {
                int comm_id = rdr2.GetInt32(rdr2.GetOrdinal("comment_id"));
                string User_email = rdr2.GetString(rdr2.GetOrdinal("email"));
                string comment_text = rdr2.GetString(rdr2.GetOrdinal("comment_text"));
                DateTime comment_date = rdr2.GetDateTime(rdr2.GetOrdinal("comment_date"));//3'alta fashee5a


                LinkButton lbl_UEmail = new LinkButton();
                lbl_UEmail.Text = User_email + ": ";
                lbl_UEmail.ID = counter+"," + User_email;
                lbl_UEmail.Click += this.patrick_click;
                form1.Controls.Add(lbl_UEmail);
                counter++;

                Label lbl_comment_text = new Label();
                lbl_comment_text.Text = comment_text + " (" + comment_date + ") ";
                form1.Controls.Add(lbl_comment_text);

                if (User_email.Equals(Log_in_email))
                {
                    Button btn_delete = new Button();
                    btn_delete.Text = "DELETE ";
                    btn_delete.ID = User_email + "," + comm_id;
                    btn_delete.Click += this.Delete_Comment;
                    form1.Controls.Add(btn_delete);
                }


                Label emtpy44 = new Label();
                emtpy44.Text = "  <br /> <br />";
                form1.Controls.Add(emtpy44);


            }
            Label emtpy22 = new Label();
            emtpy22.Text = "  <br /> <br />";
            form1.Controls.Add(emtpy22);

            comment = new TextBox();
            comment.Width = 300;
            comment.Height = 70;
            form1.Controls.Add(comment);

            Label emtpy33 = new Label();
            emtpy33.Text = "  <br /> <br />";
            form1.Controls.Add(emtpy33);

            Button btn_adding = new Button();
            btn_adding.Text = "ADD COMMENT";
            btn_adding.Click += this.Add_comment_clicked;
            form1.Controls.Add(btn_adding);

        }

        protected void Add_comment_clicked(object sender, EventArgs e)
        {
            Button b = sender as Button;

            string connStr = ConfigurationManager.ConnectionStrings["MyDbConn"].ToString();
            SqlConnection conn = new SqlConnection(connStr);

            SqlCommand cmd = new SqlCommand("add_comment_conference_review", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add(new SqlParameter("@conference_id", C_id));
            cmd.Parameters.Add(new SqlParameter("@member_email", Log_in_email));
            cmd.Parameters.Add(new SqlParameter("@conferences_reviews_id", R_id));
            cmd.Parameters.Add(new SqlParameter("@comment_text", comment.Text));

            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
            Response.Redirect("ReviewPage.aspx");

        }

        protected void Delete_Comment(object sender, EventArgs e)
        {
            Button b = sender as Button;
            string x = b.ID;
            string[] x1 = x.Split(',');
            string UEMAIL = x1[0];
            string commentsid = x1[1];
            int comments_id = Int32.Parse(commentsid);

            string connStr00 = ConfigurationManager.ConnectionStrings["MyDbConn"].ToString();
            SqlConnection conn00 = new SqlConnection(connStr00);

            SqlCommand cmd00 = new SqlCommand("delete_comments_conference", conn00);
            cmd00.CommandType = CommandType.StoredProcedure;

            cmd00.Parameters.Add(new SqlParameter("@email", UEMAIL));
            cmd00.Parameters.Add(new SqlParameter("@conference_review_id", R_id));
            cmd00.Parameters.Add(new SqlParameter("@comment_id", comments_id));


            conn00.Open();
            cmd00.ExecuteNonQuery();
            conn00.Close();
            Response.Redirect("ReviewPage.aspx");

        }

        protected void patrick_click(object sender, EventArgs e)
        {
            LinkButton b = sender as LinkButton;
            int index = (b.ID).IndexOf(",");
            Session["user"] = (b.ID).Substring(index+1);
            Response.Redirect("myprofile.aspx");



        }
    }
}