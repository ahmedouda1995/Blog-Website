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
//using System.Windows.Forms;

namespace WebApplication1
{
  

    public partial class ConferencesHomePage : System.Web.UI.Page
    {
        string Log_in_email = "";

        protected void Page_Load(object sender, EventArgs e)
        {

            Log_in_email = (string)Session["account"]; ;

            string connStr = ConfigurationManager.ConnectionStrings["MyDbConn"].ToString();
            SqlConnection conn = new SqlConnection(connStr);

            SqlCommand cmd = new SqlCommand("list_conferences", conn);
            cmd.CommandType = CommandType.StoredProcedure;


            conn.Open();
            SqlDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);


            //name_getter = new Button();
            //name_getter.Text = "initial";
            //form1.Controls.Add(name_getter);
            //name_getter.Visible = false;

            int j = 0;
            while (rdr.Read())
            {

                string conferencename = rdr.GetString(rdr.GetOrdinal("name"));
                int conferenceID = rdr.GetInt32(rdr.GetOrdinal("conference_id"));



                Button confname = new Button();
                confname.Text = conferencename;
                confname.ID = "A" + conferenceID;
                form1.Controls.Add(confname);
                confname.Width = 500;
                confname.Click += this.btnConfirm_Click;
                form1.Controls.Add(confname);

                Label emtpy = new Label();
                emtpy.Text = "  <br /> <br />";
                form1.Controls.Add(emtpy);
                j++;

            }



        }

        protected void btnConfirm_Click(object sender, EventArgs e)
        {
            Button b = sender as Button;
            Session["conf_id"] = (b.ID).Substring(1); // session can't be equals to 1
            Session["Log_in_email"] = Log_in_email;
            Response.Redirect("Conference1.aspx");


        }
    }
}