
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
    public partial class third_point : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string connStr = ConfigurationManager.ConnectionStrings["MyDbConn"].ToString();
            SqlConnection conn = new SqlConnection(connStr);

            string connStr1 = ConfigurationManager.ConnectionStrings["MyDbConn"].ToString();
            SqlConnection conn1 = new SqlConnection(connStr1);

            SqlCommand cmd = new SqlCommand("third_point", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            conn.Open();
            SqlDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            while (rdr.Read())
            {
                int cid = rdr.GetInt32(rdr.GetOrdinal("community_id"));
                string cname = rdr.GetString(rdr.GetOrdinal("name"));

                Label lbl_cname = new Label();
                // lbl_cname.ID = "" + cid;
                lbl_cname.Text = cname + " :";
                lbl_cname.ForeColor = System.Drawing.Color.DarkRed;
                form1.Controls.Add(lbl_cname);

                SqlCommand cmd1 = new SqlCommand("third_point_side_kick", conn1);
                cmd1.CommandType = CommandType.StoredProcedure;

                cmd1.Parameters.Add(new SqlParameter("@cid", cid));

                SqlParameter joined = cmd1.Parameters.Add("@tmp", SqlDbType.Int);
                joined.Direction = ParameterDirection.Output;

                conn1.Open();
                cmd1.ExecuteNonQuery();
                conn1.Close();

                if (joined.Value.ToString().Equals("0"))
                {
                    Button cjoin = new Button();
                    cjoin.ID = "" + cid;
                    cjoin.Text = "join this community";
                    cjoin.ForeColor = System.Drawing.Color.Green;
                    cjoin.Click += this.set_title_pass;
                    form1.Controls.Add(cjoin);
                }


                Button c_add_del = new Button();
                // c_add_del.ID = "" + cid;
                c_add_del.Text = "add a topic on this comm.";
                c_add_del.ForeColor = System.Drawing.Color.Green;
                c_add_del.ID = "btn" + cid;
                c_add_del.Click += add_topic;
                form1.Controls.Add(c_add_del);

                Label tmp = new Label();
                tmp.Text = "<br /> <br />";
                form1.Controls.Add(tmp);





            }
            conn.Close();
        }

        protected void set_title_pass(object sender, EventArgs e)
        {
            Button b = sender as Button;
            int id = int.Parse(b.ID);

            //Response.Redirect("join_comm.aspx");

            string connStr = ConfigurationManager.ConnectionStrings["MyDbConn"].ToString();
            SqlConnection conn = new SqlConnection(connStr);

            SqlCommand cmd = new SqlCommand("join_comm", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            string email = Session["account"].ToString();


            cmd.Parameters.Add(new SqlParameter("@email", email));
            cmd.Parameters.Add(new SqlParameter("@community_id", id));

            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();

            Response.Write("you've successfully joined this community");
        }

        protected void add_topic(object sender, EventArgs e)
        {
            Button b = sender as Button;
            Session["add_id"] = b.ID.Substring(3);

            Response.Redirect("add_topic.aspx");

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