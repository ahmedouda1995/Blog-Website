
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
    public partial class user_comm_link : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string connStr = ConfigurationManager.ConnectionStrings["MyDbConn"].ToString();
            SqlConnection conn = new SqlConnection(connStr);

            SqlCommand cmd = new SqlCommand("view_community_info", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            int cid = int.Parse(Session["cid_for_user"].ToString());

            cmd.Parameters.Add(new SqlParameter("@community_id", cid));

            conn.Open();
            SqlDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            while (rdr.Read())
            {


                string cname = rdr.GetString(rdr.GetOrdinal("name"));
                string cdescription = rdr.GetString(rdr.GetOrdinal("community_description"));

                Label lbl_cname = new Label();
                lbl_cname.Text = "community's name :" + cname;
                lbl_cname.ForeColor = System.Drawing.Color.Red;
                form1.Controls.Add(lbl_cname);

                Label blank2 = new Label();
                blank2.Text = "<br />";
                form1.Controls.Add(blank2);

                Label lbl_cdescription = new Label();
                lbl_cdescription.Text = "description :" + cdescription + "<br />";
                lbl_cdescription.ForeColor = System.Drawing.Color.DarkCyan;
                form1.Controls.Add(lbl_cdescription);

            }

            conn.Close();

            //

            SqlCommand cmd1 = new SqlCommand("point_2_2", conn);
            cmd1.CommandType = CommandType.StoredProcedure;

            conn.Open();

            cmd1.Parameters.Add(new SqlParameter("@cid", cid));
            SqlDataReader rdr1 = cmd1.ExecuteReader(CommandBehavior.CloseConnection);



            while (rdr1.Read())
            {
                int tid = rdr1.GetInt32(rdr1.GetOrdinal("topic_id"));
                string tname = rdr1.GetString(rdr1.GetOrdinal("name"));

                LinkButton hpr_tname = new LinkButton();
                hpr_tname.ID = "" + tid + "," + cid;
                hpr_tname.Text = "topic  :" + tname;
                hpr_tname.Click += tmpmethod;
                form1.Controls.Add(hpr_tname);

                Label blank = new Label();
                blank.Text = "  <br />";
                form1.Controls.Add(blank);
            }

            conn.Close();

            Label blank1 = new Label();
            blank1.Text = "  <br /> <br />";
            form1.Controls.Add(blank1);
        }




        protected void tmpmethod(object sender, EventArgs e)
        {
            LinkButton b = sender as LinkButton;
            string x = b.ID;
            string[] x1 = x.Split(',');
            Session["tid"] = x1[0];
            Session["new cid"] = x1[1];
            Response.Redirect("topic_content1.aspx");
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