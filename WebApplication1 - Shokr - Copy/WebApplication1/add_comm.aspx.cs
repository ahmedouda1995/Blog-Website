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
    public partial class add_comm : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void add_comm_method1(object sender, EventArgs e)
        {
            string connStr = ConfigurationManager.ConnectionStrings["MyDbConn"].ToString();
            SqlConnection conn = new SqlConnection(connStr);

            SqlCommand cmd = new SqlCommand("add_comment_topic1", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            string email = Session["account"].ToString();
            string content = textbox3.Text;

            int tid = int.Parse(Session["addcomm"].ToString());
            int cids = int.Parse(Session["new cid"].ToString());

            cmd.Parameters.Add(new SqlParameter("@cid", cids));
            cmd.Parameters.Add(new SqlParameter("@topic_id", tid));
            cmd.Parameters.Add(new SqlParameter("@email", email));
            cmd.Parameters.Add(new SqlParameter("@content", content));

            SqlParameter c = cmd.Parameters.Add("@tmp", SqlDbType.Int);
            c.Direction = ParameterDirection.Output;

            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();

            if (c.Value.ToString().Equals("1"))
            {

                Response.Write("done");

                Response.Redirect("topic_content1.aspx");
            }
            else
            {
                Response.Write("sorry, but you are not a member in this community");
            }


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