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
   
    public partial class topic_content : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            string connStr = ConfigurationManager.ConnectionStrings["MyDbConn"].ToString();
            SqlConnection conn = new SqlConnection(connStr);

            SqlCommand cmd1 = new SqlCommand("topic_content_database_1", conn);
            cmd1.CommandType = CommandType.StoredProcedure;

            int id1 = int.Parse(Session["tid"].ToString());

            cmd1.Parameters.Add(new SqlParameter("@topic_id", id1));



            int id = int.Parse(Session["tid"].ToString());


            conn.Open();
            string temail = "";
            SqlDataReader rdr = cmd1.ExecuteReader(CommandBehavior.CloseConnection);
            while (rdr.Read())
            {
                temail = rdr.GetString(rdr.GetOrdinal("email"));

            }

            LinkButton lbl_temail = new LinkButton();
            lbl_temail.ID = temail;
            lbl_temail.Text = "this topic was written by :" + temail;
            lbl_temail.ForeColor = System.Drawing.Color.Blue;
            lbl_temail.Click += userpage;
            form1.Controls.Add(lbl_temail);

            Label tmp = new Label();
            tmp.Text = "  <br /> <br />";
            form1.Controls.Add(tmp);
            conn.Close();

            SqlCommand cmd = new SqlCommand("topic_content_database", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add(new SqlParameter("@topic_id", id));

            conn.Open();
            string tname = "";
            string tdescription = "";
            rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            while (rdr.Read())
            {
                tname = rdr.GetString(rdr.GetOrdinal("name"));
                tdescription = rdr.GetString(rdr.GetOrdinal("topic_description"));
            }

            Label lbl_tname = new Label();
            lbl_tname.Text = "topic's name :" + tname + "  <br /> <br />";
            lbl_tname.ForeColor = System.Drawing.Color.Red;
            form1.Controls.Add(lbl_tname);

            Label lbl_tdesc = new Label();
            lbl_tdesc.Text = "topic's description :" + tdescription + "  <br /> <br />";
            lbl_tdesc.ForeColor = System.Drawing.Color.Red;
            form1.Controls.Add(lbl_tdesc);

            conn.Close();

            Label tmp1 = new Label();
            tmp1.Text = "The comments on this topic:  <br /> <br />";
            form1.Controls.Add(tmp1);

            SqlCommand cmd2 = new SqlCommand("topic_content_database_2", conn);
            cmd2.CommandType = CommandType.StoredProcedure;

            cmd2.Parameters.Add(new SqlParameter("@topic_id", id1));

            conn.Open();
            string commemail = "";
            string commcontent = "";

            rdr = cmd2.ExecuteReader(CommandBehavior.CloseConnection);
            while (rdr.Read())
            {
                int commid = rdr.GetInt32(rdr.GetOrdinal("comment_id"));
                commemail = rdr.GetString(rdr.GetOrdinal("email"));
                commcontent = rdr.GetString(rdr.GetOrdinal("content"));


                Label lbl_commemail = new Label();
                lbl_commemail.Text = commemail + "<br />";
                form1.Controls.Add(lbl_commemail);

                Label lbl_commcontent = new Label();
                lbl_commcontent.Text = commcontent + "<br />";
                form1.Controls.Add(lbl_commcontent);

                if (commemail == Session["account"].ToString())
                {
                    Button btn_delete = new Button();
                    btn_delete.ID = commid + "," + id;
                    btn_delete.Text = "delete this comment";
                    btn_delete.Click += del_comm_method;
                    form1.Controls.Add(btn_delete);
                }

                Label tmp5 = new Label();
                tmp5.Text = "<br /> <br />";
                form1.Controls.Add(tmp5);

            }

            Label tmp3 = new Label();
            tmp3.Text = "<br /> <br />";
            form1.Controls.Add(tmp3);

            Button btn_add_comm = new Button();
            btn_add_comm.ID = "cin" + id;
            btn_add_comm.Click += add_the_comm;
            btn_add_comm.Text = "add comment on this topic";
            btn_add_comm.ForeColor = System.Drawing.Color.Purple;
            form1.Controls.Add(btn_add_comm);

            Label tmp4 = new Label();
            tmp4.Text = "<br /> <br />";
            form1.Controls.Add(tmp4);

            if (Session["account"].ToString() == temail)
            {
                Button btn_delete_topic = new Button();
                btn_delete_topic.ID = id + "";
                btn_delete_topic.Text = "delete this topic";
                btn_delete_topic.Click += deltop;
                btn_delete_topic.ForeColor = System.Drawing.Color.Red;
                form1.Controls.Add(btn_delete_topic);
            }

        }

        protected void deltop(object sender, EventArgs e)
        {
            Button b = sender as Button;
            string x = b.ID;

            //Response.Redirect("delete_topic.aspx");

            string connStr = ConfigurationManager.ConnectionStrings["MyDbConn"].ToString();
            SqlConnection conn = new SqlConnection(connStr);

            SqlCommand cmd = new SqlCommand("delete_topic", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            int id = int.Parse(x);
            string email = Session["account"].ToString();


            cmd.Parameters.Add(new SqlParameter("@topic_id", id));
            cmd.Parameters.Add(new SqlParameter("@email", email));

            SqlParameter tmp = cmd.Parameters.Add("@tmp", SqlDbType.Int);
            tmp.Direction = ParameterDirection.Output;

            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();

            Response.Redirect("point_2.aspx");
        }

        protected void add_the_comm(object sender, EventArgs e)
        {
            Button b = sender as Button;
            Session["addcomm"] = b.ID.Substring(3);

            Response.Redirect("add_comm.aspx");
        }
        protected void del_comm_method(object sender, EventArgs e)
        {
            Button b = sender as Button;
            string x = b.ID;

            //Response.Redirect("del_comm.aspx");

            string connStr = ConfigurationManager.ConnectionStrings["MyDbConn"].ToString();
            SqlConnection conn = new SqlConnection(connStr);

            SqlCommand cmd = new SqlCommand("delete_comment_topic", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            string tmp1 = x;

            string[] tmp2 = tmp1.Split(',');

            int cid = int.Parse(tmp2[0]);
            int tid = int.Parse(tmp2[1]);
            string email = Session["account"].ToString();

            cmd.Parameters.Add(new SqlParameter("@email", email));
            cmd.Parameters.Add(new SqlParameter("@topic_id", tid));
            cmd.Parameters.Add(new SqlParameter("@topic_comment_id", cid));

            SqlParameter tmp3 = cmd.Parameters.Add("@tmp", SqlDbType.Int);
            tmp3.Direction = ParameterDirection.Output;

            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();

            if (tmp3.Value.ToString().Equals("1"))
            {

                Response.Write("done");

            }
            else
            {
                Response.Write("you can only delete a comment made by you");
            }

            Response.Redirect("topic_content.aspx");
        }

        protected void userpage(object sender, EventArgs e)
        {
            LinkButton b = sender as LinkButton;
            Session["user"] = b.ID;

            Response.Redirect("myprofile.aspx");
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