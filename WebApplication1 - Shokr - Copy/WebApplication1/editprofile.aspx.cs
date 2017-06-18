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
    public partial class editprofile : System.Web.UI.Page
    {
        String email;
        SqlParameter type;
        protected void Page_Load(object sender, EventArgs e)
        {
            string connStr = ConfigurationManager.ConnectionStrings["MyDbConn"].ToString();
            SqlConnection conn = new SqlConnection(connStr);
            email = Session["account"].ToString();
            SqlCommand cmd2 = new SqlCommand("getusertype", conn);
            cmd2.CommandType = CommandType.StoredProcedure;
            cmd2.Parameters.Add(new SqlParameter("@email", email));
            type = cmd2.Parameters.Add("@type", SqlDbType.Int);
            type.Direction = ParameterDirection.Output;
            conn.Open();
            cmd2.ExecuteNonQuery();
            conn.Close();
            if (type.Value.ToString().Equals("0"))
            {
                normaluser();

            }
            else
            {
                if (type.Value.ToString().Equals("1"))
                {
                    verifiedreviewer();

                }
                else
                {
                    developmentteam();
                }

            }


        }
        protected void submit(object o, EventArgs e)
        {
            string connStr = ConfigurationManager.ConnectionStrings["MyDbConn"].ToString();
            SqlConnection conn = new SqlConnection(connStr);
            if (type.Value.ToString().Equals("0"))
            {
                String fname = txt1.Text;
                String lname = txt2.Text;
                String dob = txt3.Text;
                SqlCommand cmd = new SqlCommand("update_NormalUsers", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@email", email));
                cmd.Parameters.Add(new SqlParameter("@f_name", fname));
                cmd.Parameters.Add(new SqlParameter("@l_name", lname));
                cmd.Parameters.Add(new SqlParameter("@date_birth", dob));
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
                Session["user"] = email;
                Response.Redirect("normaluserProfile.aspx");

            }
            else
            {
                if (type.Value.ToString().Equals("1"))
                {

                    String fname = txt1.Text;
                    String lname = txt2.Text;
                    String years = txt3.Text;
                    SqlCommand cmd = new SqlCommand("update_verfiedReviewer", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@email", email));
                    cmd.Parameters.Add(new SqlParameter("@f_name", fname));
                    cmd.Parameters.Add(new SqlParameter("@l_name", lname));
                    cmd.Parameters.Add(new SqlParameter("@years_experience", years));
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    Session["user"] = email;
                    Response.Redirect("verifiedreviewerProfile.aspx");
                }
                else
                {

                    String name = txt1.Text;
                    String foundationdate = txt2.Text;
                    String company = txt3.Text;
                    SqlCommand cmd = new SqlCommand("update_development_teams", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@email", email));
                    cmd.Parameters.Add(new SqlParameter("@name", name));
                    cmd.Parameters.Add(new SqlParameter("@foundation_date", foundationdate));
                    cmd.Parameters.Add(new SqlParameter("@company", company));
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    Session["user"] = email;
                    Response.Redirect("developmentProfile.aspx");
                }

            }

        }
        private void developmentteam()
        {
            lbl1.Text = "Name: ";
            lbl2.Text = "Date of Foundation(month/day/year): ";

            lbl3.Text = "Company: ";
        }

        private void normaluser()
        {
            lbl1.Text = "First Name: ";
            lbl2.Text = "Last Name: ";
            lbl3.Text = "Birthday(month/day/year): ";

        }

        private void verifiedreviewer()
        {

            lbl1.Text = "First Name: ";
            lbl2.Text = "Last Name: ";
            lbl3.Text = "Years of Experience: ";

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