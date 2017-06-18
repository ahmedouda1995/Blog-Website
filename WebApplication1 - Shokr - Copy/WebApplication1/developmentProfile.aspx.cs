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
    public partial class developmentProfile : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string connStr = ConfigurationManager.ConnectionStrings["MyDbConn"].ToString();
            SqlConnection conn = new SqlConnection(connStr);

            SqlCommand cmd = new SqlCommand("getinfoDevelopment", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            String username = Session["account"].ToString();
            String account = Session["user"].ToString();
            if (!account.Equals(username))
            {
                form1.Controls.Remove(btn_edit);

            }
            cmd.Parameters.Add(new SqlParameter("@email", username));
            conn.Open();
            SqlDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            lbl_email.Text = username;
            String name = " ";

            String company = " ";
            String date = " ";
            String preferred_genre = " ";
            while (rdr.Read())
            {
                if (!rdr.IsDBNull((rdr.GetOrdinal("name"))))
                {
                    name = rdr.GetString(rdr.GetOrdinal("name"));
                }
                if (!rdr.IsDBNull(rdr.GetOrdinal("company")))
                {
                    company = rdr.GetString(rdr.GetOrdinal("company"));
                }



                if (!rdr.IsDBNull(rdr.GetOrdinal("d_o_f")))
                {
                    date = Convert.ToString(rdr.GetDateTime(rdr.GetOrdinal("d_o_f")));
                }
                if (!rdr.IsDBNull(rdr.GetOrdinal("genre")))
                {
                    preferred_genre = rdr.GetString(rdr.GetOrdinal("genre"));
                }
            }
            lbl_name.Text = name;

            lbl_company.Text = company;
            lbl_dof.Text = date;
            lbl_genre.Text = preferred_genre;
            conn.Close();

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
        protected void show_messages(object sender, EventArgs e)
        {
            Response.Redirect("messages.aspx");
        }
        protected void show_friends(object sender, EventArgs e)
        {
            Response.Redirect("friendrequests.aspx");
        }
    }
}