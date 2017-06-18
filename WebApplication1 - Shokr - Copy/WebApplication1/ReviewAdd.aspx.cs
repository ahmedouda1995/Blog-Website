using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Drawing;
namespace WebApplication1
{
    public partial class ReviewAdd : System.Web.UI.Page
    {
        string Log_in_email;
        int C_id;
        protected void Page_Load(object sender, EventArgs e)
        {
            Log_in_email = (string)(Session["MEmail"]);

            String cid = (string)(Session["conference_id"]);
            C_id = Int32.Parse(cid);


            Label emtpy = new Label();
            emtpy.Text = "  <br /> <br />";
            form1.Controls.Add(emtpy);

            Button ADD = new Button();
            ADD.Text = "ADD REVIEW";
            ADD.Click += this.Add_btn_clicked;
            form1.Controls.Add(ADD);


        }
        protected void Add_btn_clicked(object sender, EventArgs e)
        {
            Button b = sender as Button;

            string connStr = ConfigurationManager.ConnectionStrings["MyDbConn"].ToString();
            SqlConnection conn = new SqlConnection(connStr);

            SqlCommand cmd = new SqlCommand("member_add_conference_review_to_a_conference", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add(new SqlParameter("@email", Log_in_email));
            cmd.Parameters.Add(new SqlParameter("@conference_id", C_id));
            cmd.Parameters.Add(new SqlParameter("@content", txt_Review.Text));

            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
            Response.Redirect("Conference1.aspx");

        }
    }
}