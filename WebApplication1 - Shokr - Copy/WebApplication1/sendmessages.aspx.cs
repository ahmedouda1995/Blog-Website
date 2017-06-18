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
    
    public partial class sendmessages : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string connStr = ConfigurationManager.ConnectionStrings["MyDbConn"].ToString();
            SqlConnection conn = new SqlConnection(connStr);
            String username = Session["user"].ToString();
            String account = Session["account"].ToString();
            SqlCommand cmd = new SqlCommand("view_message", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@email1", username));
            cmd.Parameters.Add(new SqlParameter("@email2", account));

            conn.Open();
            SqlDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            while (rdr.Read())
            {
                String firstname = rdr.GetString(rdr.GetOrdinal("f_name"));

                String messages = rdr.GetString(rdr.GetOrdinal("text_message"));

                String date = Convert.ToString(rdr.GetDateTime(rdr.GetOrdinal("sent_date")));
                Label l = new Label();
                l.Text = firstname + ": " + messages + " </br>sent at" + date + " <br></br>";
                form2.Controls.Add(l);


            }
            conn.Close();
        }

        protected void send_Click(object sender, EventArgs e)
        {
            string connStr = ConfigurationManager.ConnectionStrings["MyDbConn"].ToString();
            SqlConnection conn = new SqlConnection(connStr);
            String username = Session["user"].ToString();
            String account = Session["account"].ToString();
            SqlCommand cmd = new SqlCommand("send_message", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@email_sender", account));
            cmd.Parameters.Add(new SqlParameter("@email_receiver", username));
            String message = txt_msg.Text;
            cmd.Parameters.Add(new SqlParameter("@message", message));
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
            Response.Redirect("sendmessages.aspx");
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
            Response.Redirect("ConferencesHomePage.aspx");

        }

        protected void btn_community_Click(object sender, EventArgs e)
        {
            Response.Redirect("start.aspx");
        }

        protected void back_Click(object sender, EventArgs e)
        {
            Response.Redirect("messages.aspx");
        }
    }
}