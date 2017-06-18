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
    public partial class friendrequests : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string connStr = ConfigurationManager.ConnectionStrings["MyDbConn"].ToString();
            SqlConnection conn = new SqlConnection(connStr);

            SqlCommand cmd = new SqlCommand("pendingFriends", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            String account = Session["account"].ToString();
            cmd.Parameters.Add(new SqlParameter("@email", account));
            conn.Open();
            SqlDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            while (rdr.Read())
            {
                String email = rdr.GetString(rdr.GetOrdinal("email1"));
                Label l1 = new Label();
                l1.Text = email + " wants to be your friends do you ";
                Label l3 = new Label();
                l3.Text = " or ";
                Label l2 = new Label();
                l2.Text = " ?";
                LinkButton h = new LinkButton();
                h.Text = "accpet";
                h.ID = "a" + email;

                h.Click += new EventHandler(this.acceptBtn_Click);
                LinkButton h2 = new LinkButton();
                h2.Text = "reject";
                h2.ID = "r" + email;

                h2.Click += new EventHandler(this.rejectBtn_Click);
                form1.Controls.Add(l1);
                form1.Controls.Add(h);
                form1.Controls.Add(l3);
                form1.Controls.Add(h2);
                form1.Controls.Add(l2);
                form1.Controls.Add(new LiteralControl("<br></br>"));

            }
        }

        private void rejectBtn_Click(object sender, EventArgs e)
        {
            string connStr = ConfigurationManager.ConnectionStrings["MyDbConn"].ToString();
            SqlConnection conn = new SqlConnection(connStr);

            SqlCommand cmd = new SqlCommand("accept_rejectFriendship", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            String account = Session["account"].ToString();
            String email2 = ((LinkButton)sender).ID.Substring(1);
            cmd.Parameters.Add(new SqlParameter("@email_reciever", account));
            cmd.Parameters.Add(new SqlParameter("@email_sender", email2));
            cmd.Parameters.Add(new SqlParameter("@accepts", false));
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
            Response.Redirect("friendrequests.aspx");
        }

        private void acceptBtn_Click(object sender, EventArgs e)
        {

            string connStr = ConfigurationManager.ConnectionStrings["MyDbConn"].ToString();
            SqlConnection conn = new SqlConnection(connStr);

            SqlCommand cmd = new SqlCommand("accept_rejectFriendship", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            String account = Session["account"].ToString();
            String email2 = ((LinkButton)sender).ID.Substring(1);
            cmd.Parameters.Add(new SqlParameter("@email_reciever", account));
            cmd.Parameters.Add(new SqlParameter("@email_sender", email2));
            cmd.Parameters.Add(new SqlParameter("@accepts", 1));
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
            Response.Redirect("friendrequests.aspx");
        }
        protected void btn_search_Click(object sender, EventArgs e)
        {
            Session["searchitem"] = searchitem.SelectedValue;
            Response.Redirect(String.Format("Search.aspx?search=" + txt_search.Text));
        }
        protected void btn_myprofile_Click(object sender, EventArgs e)
        {
            Session["user"] = Session["account"].ToString();
            Response.Redirect("myprofile.aspx");
        }
        protected void btn_logout_Click(object sender, EventArgs e)
        {

            Response.Redirect("homepage.aspx");
        }

        protected void btn_conference_Click(object sender, EventArgs e)
        {

        }

        protected void btn_community_Click(object sender, EventArgs e)
        {
            Response.Redirect("start.aspx");
        }
    }
}