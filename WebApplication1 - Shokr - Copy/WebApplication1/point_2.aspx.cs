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
    public partial class point_2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string connStr = ConfigurationManager.ConnectionStrings["MyDbConn"].ToString();
            SqlConnection conn = new SqlConnection(connStr);

            SqlCommand cmd = new SqlCommand("point_2_1", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            conn.Open();
            SqlDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            string connStr1 = ConfigurationManager.ConnectionStrings["MyDbConn"].ToString();
            SqlConnection conn1 = new SqlConnection(connStr1);

            while (rdr.Read())
            {

                int cid = rdr.GetInt32(rdr.GetOrdinal("community_id"));
                string cname = rdr.GetString(rdr.GetOrdinal("name"));
                string cdescription = rdr.GetString(rdr.GetOrdinal("community_description"));

                LinkButton lbl_cname = new LinkButton();
                lbl_cname.Text = "community's name :" + cname;
                lbl_cname.ID = "l" + cid;
                lbl_cname.Click += action;
                lbl_cname.ForeColor = System.Drawing.Color.Red;
                form1.Controls.Add(lbl_cname);

                Label blank2 = new Label();
                blank2.Text = "<br />";
                form1.Controls.Add(blank2);

                Label lbl_cdescription = new Label();
                lbl_cdescription.Text = "description :" + cdescription + "<br />";
                lbl_cdescription.ForeColor = System.Drawing.Color.DarkCyan;
                form1.Controls.Add(lbl_cdescription);

                //
                SqlCommand cmd1 = new SqlCommand("point_2_2", conn1);
                cmd1.CommandType = CommandType.StoredProcedure;

                conn1.Open();

                cmd1.Parameters.Add(new SqlParameter("@cid", cid));
                SqlDataReader rdr1 = cmd1.ExecuteReader(CommandBehavior.CloseConnection);



                while (rdr1.Read())
                {
                    int tid = rdr1.GetInt32(rdr1.GetOrdinal("topic_id"));
                    string tname = rdr1.GetString(rdr1.GetOrdinal("name"));

                    Label hpr_tname = new Label();
                    hpr_tname.Text = "topic  :" + tname;
                    form1.Controls.Add(hpr_tname);

                    Label blank = new Label();
                    blank.Text = "  <br />";
                    form1.Controls.Add(blank);
                }

                conn1.Close();

                Label blank1 = new Label();
                blank1.Text = "  <br /> <br />";
                form1.Controls.Add(blank1);
            }
        }

        // protected void tmpmethod(object sender, EventArgs e)
        //{
        //  LinkButton b = sender as LinkButton;
        //Session["tid"] = b.ID;

        //Response.Redirect("topic_content.aspx");
        //}

        protected void action(object sender, EventArgs e)
        {
            LinkButton b = sender as LinkButton;
            Session["cid_for_user"] = (b.ID).Substring(1);
            Response.Redirect("user_comm_link.aspx");
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