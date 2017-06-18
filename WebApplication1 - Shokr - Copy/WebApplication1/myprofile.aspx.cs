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
    public partial class myprofile : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string connStr = ConfigurationManager.ConnectionStrings["MyDbConn"].ToString();
            SqlConnection conn = new SqlConnection(connStr);
            SqlCommand cmd2 = new SqlCommand("getusertype", conn);
            cmd2.CommandType = CommandType.StoredProcedure;
            String username = Session["user"].ToString();
            cmd2.Parameters.Add(new SqlParameter("@email", username));

            SqlParameter type = cmd2.Parameters.Add("@type", SqlDbType.Int);
            type.Direction = ParameterDirection.Output;

            conn.Open();
            cmd2.ExecuteNonQuery();
            if (type.Value.ToString().Equals("0"))

                Response.Redirect("normaluserProfile.aspx");
            else
                if (type.Value.ToString().Equals("1"))

                Response.Redirect("verifiedReviewerProfile.aspx");
            else

                Response.Redirect("developmentProfile.aspx");

            conn.Close();
        }
    }
}