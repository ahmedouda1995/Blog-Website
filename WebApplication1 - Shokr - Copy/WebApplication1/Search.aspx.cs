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
    
    public partial class Search : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string connStr = ConfigurationManager.ConnectionStrings["MyDbConn"].ToString();
            SqlConnection conn = new SqlConnection(connStr);
            String searchname = Request.QueryString["search"].ToString();
            String item = Session["searchitem"].ToString();
            if (item.Equals("0"))
            {
                btn_verified_Click(searchname, conn);

            }
            else
            {
                if (item.Equals("1"))
                {
                    btn_development_team_Click(searchname, conn);

                }
                else
                {
                    if (item.Equals("2"))
                    {
                        btn_games_Click(searchname, conn);
                    }
                    else
                    {
                        if (item.Equals("3"))
                        {
                            btn_conference_Click(searchname, conn);
                        }
                        else
                        {
                            btn_community_Click(searchname, conn);
                        }

                    }

                }

            }
        }
        protected void btn_verified_Click(String name, SqlConnection conn)
        {
            SqlCommand cmd = new SqlCommand("search_verified", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@name", name));
            conn.Open();
            SqlDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            while (rdr.Read())
            {
                String email = rdr.GetString(rdr.GetOrdinal("email"));
                String fname = rdr.GetString(rdr.GetOrdinal("f_name"));
                String lname = rdr.GetString(rdr.GetOrdinal("l_name"));
                LinkButton h = new LinkButton();
                h.Text = fname + " " + lname;
                h.ID = email;
                h.Click += new EventHandler(this.verifiedBtn_Click);
                form_search.Controls.Add(h);
                form_search.Controls.Add(new LiteralControl("<br></br>"));


            }
        }

        private void verifiedBtn_Click(object sender, EventArgs e)
        {
            LinkButton b = (LinkButton)sender;
            Session["user"] = b.ID;
            Response.Redirect("verifiedreviewerProfile.aspx");
        }

        protected void btn_development_team_Click(String name, SqlConnection conn)
        {
            SqlCommand cmd = new SqlCommand("search_development", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@dname", name));
            conn.Open();
            SqlDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            while (rdr.Read())
            {
                String email = rdr.GetString(rdr.GetOrdinal("email"));
                String teamname = rdr.GetString(rdr.GetOrdinal("name"));

                LinkButton h = new LinkButton();
                h.Text = teamname;
                h.ID = email;
                h.Click += new EventHandler(this.developmentBtn_Click);
                form_search.Controls.Add(h);
                form_search.Controls.Add(new LiteralControl("<br></br>"));


            }
        }

        private void developmentBtn_Click(object sender, EventArgs e)
        {
            LinkButton b = (LinkButton)sender;
            Session["user"] = b.ID;
            Response.Redirect("developmentProfile.aspx");
        }

        protected void btn_games_Click(String name, SqlConnection conn)
        {
            SqlCommand cmd = new SqlCommand("search_game", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@gname", name));
            conn.Open();
            SqlDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            while (rdr.Read())
            {
                int id = rdr.GetInt32(rdr.GetOrdinal("game_id"));
                String gamename = rdr.GetString(rdr.GetOrdinal("name"));

                LinkButton h = new LinkButton();
                h.Text = gamename;
                h.ID = "" + id;
                h.Click += new EventHandler(this.gamesBtn_Click);

                form_search.Controls.Add(h);
                form_search.Controls.Add(new LiteralControl("<br></br>"));


            }

        }

        private void gamesBtn_Click(object sender, EventArgs e)
        {
            LinkButton b = (LinkButton)sender;
            Session["game"] = b.ID;
            Response.Redirect("ViewGame.aspx");
        }

        protected void btn_conference_Click(String name, SqlConnection conn)
        {
            SqlCommand cmd = new SqlCommand("search_conference", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@cname", name));
            conn.Open();
            SqlDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            while (rdr.Read())
            {
                int id = rdr.GetInt32(rdr.GetOrdinal("conference_id"));
                String conferencename = rdr.GetString(rdr.GetOrdinal("name"));

                LinkButton h = new LinkButton();
                h.Text = conferencename;
                h.ID = "" + id;
                h.Click += new EventHandler(this.conferenceBtn_Click);
                form_search.Controls.Add(h);
                form_search.Controls.Add(new LiteralControl("<br></br>"));


            }
        }

        private void conferenceBtn_Click(object sender, EventArgs e)
        {

            LinkButton b = (LinkButton)sender;
            Session["conf_id"] = b.ID;
            Response.Redirect("Conference1.aspx");

        }

        protected void btn_community_Click(String name, SqlConnection conn)
        {
            SqlCommand cmd = new SqlCommand("search_communties", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@comname", name));
            conn.Open();
            SqlDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            while (rdr.Read())
            {
                int id = rdr.GetInt32(rdr.GetOrdinal("community_id"));
                String communityname = rdr.GetString(rdr.GetOrdinal("name"));

                LinkButton h = new LinkButton();
                h.Text = communityname;
                h.ID = "" + id;
                h.Click += new EventHandler(this.communityBtn_Click);
                form_search.Controls.Add(h);
                form_search.Controls.Add(new LiteralControl("<br></br>"));


            }
        }

        private void communityBtn_Click(object sender, EventArgs e)
        {
            LinkButton b = (LinkButton)sender;
            Session["cid_for_user"] = b.ID;
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