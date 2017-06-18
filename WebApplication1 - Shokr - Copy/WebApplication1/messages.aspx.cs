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
    public partial class messages : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string connStr = ConfigurationManager.ConnectionStrings["MyDbConn"].ToString();
            SqlConnection conn = new SqlConnection(connStr);
            String email = Session["account"].ToString();
            SqlCommand cmd = new SqlCommand("view_friends", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@email", email));
            conn.Open();
            SqlDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            while (rdr.Read())
            {
                String email2 = rdr.GetString(rdr.GetOrdinal("email"));
                String firstname = rdr.GetString(rdr.GetOrdinal("f_name"));
                String lastname = rdr.GetString(rdr.GetOrdinal("l_name"));
                Button h = new Button();
                h.Text = "Send Messages";
                h.ID = email2;
                h.Click += new EventHandler(this.send_Click);
                Label l = new Label();
                l.Text = firstname + " " + lastname + " ";
                form1.Controls.Add(l);
                form1.Controls.Add(h);
                form1.Controls.Add(new LiteralControl("<br></br>"));


            }

        }

        private void send_Click(object sender, EventArgs e)
        {
            Session["user"] = ((Button)sender).ID;
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
    }
}