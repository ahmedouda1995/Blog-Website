
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
    public partial class verifiedreviewerProfile : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string connStr = ConfigurationManager.ConnectionStrings["MyDbConn"].ToString();
            SqlConnection conn = new SqlConnection(connStr);

            SqlCommand cmd = new SqlCommand("getinfoVerifiedReviewer", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            String username = Session["user"].ToString();
            String account = Session["account"].ToString();
            if (!account.Equals(username))
            {
                form1.Controls.Remove(btn_edit);

            }
            cmd.Parameters.Add(new SqlParameter("@email", username));
            conn.Open();
            SqlDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            lbl_email.Text = username;
            String first_name = " ";
            String last_name = " ";
            String years = " ";
            String date = " ";
            String preferred_genre = " ";
            while (rdr.Read())
            {
                if (!rdr.IsDBNull((rdr.GetOrdinal("first_name"))))
                {
                    first_name = rdr.GetString(rdr.GetOrdinal("first_name"));
                }
                if (!rdr.IsDBNull(rdr.GetOrdinal("last_name")))
                {
                    last_name = rdr.GetString(rdr.GetOrdinal("last_name"));
                }


                if (!rdr.IsDBNull(rdr.GetOrdinal("years")))
                {
                    years = Convert.ToString(rdr.GetInt32(rdr.GetOrdinal("years")));
                }

                if (!rdr.IsDBNull(rdr.GetOrdinal("d_o_s")))
                {
                    date = Convert.ToString(rdr.GetDateTime(rdr.GetOrdinal("d_o_s")));
                }
                if (!rdr.IsDBNull(rdr.GetOrdinal("genre")))
                {
                    preferred_genre = rdr.GetString(rdr.GetOrdinal("genre"));
                }
            }
            lbl_fname.Text = first_name;
            lbl_lname.Text = last_name;
            lbl_years.Text = years;
            lbl_dos.Text = date;
            lbl_genre.Text = preferred_genre;
            conn.Close();

        }

        protected void show_messages(object sender, EventArgs e)
        {
            Response.Redirect("messages.aspx");
        }
        protected void show_friends(object sender, EventArgs e)
        {
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
            Response.Redirect("ConferencesHomePage.aspx");
        }

        protected void btn_community_Click(object sender, EventArgs e)
        {
            Response.Redirect("start.aspx");

        }
        protected void edit_profile(object sender, EventArgs e)
        {
            Response.Redirect("editprofile.aspx");
        }

    }
}