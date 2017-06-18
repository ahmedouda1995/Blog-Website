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
using Label = System.Web.UI.WebControls.Label;
using Button = System.Web.UI.WebControls.Button;
using System.Windows.Forms;



namespace WebApplication1
{
    public partial class Conference1 : System.Web.UI.Page
    {
        string Log_in_email = "in1";
        int id;
        DropDownList list_available_games;
        protected void Page_Load(object sender, EventArgs e)
        {
            Log_in_email = (string)(Session["account"]);

            String cid = (string)(Session["conf_id"]);
            id = Int32.Parse(cid);

            string connStr = ConfigurationManager.ConnectionStrings["MyDbConn"].ToString();
            SqlConnection conn = new SqlConnection(connStr);

            SqlCommand cmd = new SqlCommand("preview_conference", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@conference_id", id));


            conn.Open();
            SqlDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            while (rdr.Read())
            {

                string conferencename = rdr.GetString(rdr.GetOrdinal("name"));
                string venue = rdr.GetString(rdr.GetOrdinal("venue"));
                DateTime starting_date = rdr.GetDateTime(rdr.GetOrdinal("starting_date"));


                Label lbl_name = new Label();
                lbl_name.Text = "Conference: " + conferencename + "  ";
                form1.Controls.Add(lbl_name);

                Button btn_attend = new Button();
                btn_attend.Text = "Attend";
                btn_attend.Click += this.Attend_btn_clicked;
                form1.Controls.Add(btn_attend);

                Label lbl_empty = new Label();
                lbl_empty.Text = "  <br /> <br />";
                form1.Controls.Add(lbl_empty);

                Label lbl_start = new Label();
                lbl_start.Text = "Starts At: " + starting_date + "  <br /> <br />";
                form1.Controls.Add(lbl_start);


                Label lbl_place = new Label();
                lbl_place.Text = "In: " + venue + "  <br /> <br />";
                form1.Controls.Add(lbl_place);




            }

            Label emtpy = new Label();
            emtpy.Text = "  <br /> <br />";
            form1.Controls.Add(emtpy);


            Label lbl_dev = new Label();
            lbl_dev.Text = "Development Teams: " + "  <br /> <br />";
            form1.Controls.Add(lbl_dev);

            string connStr1 = ConfigurationManager.ConnectionStrings["MyDbConn"].ToString();
            SqlConnection conn1 = new SqlConnection(connStr1);

            SqlCommand cmd1 = new SqlCommand("development_team_conference", conn1); //*****ALTERED*****
            cmd1.CommandType = CommandType.StoredProcedure;
            cmd1.Parameters.Add(new SqlParameter("@conference_id", id));


            conn1.Open();
            SqlDataReader rdr1 = cmd1.ExecuteReader(CommandBehavior.CloseConnection);

            int i = 1;

            while (rdr1.Read())
            {

                string TeamName = rdr1.GetString(rdr1.GetOrdinal("development_name"));
                string Gname = rdr1.GetString(rdr1.GetOrdinal("game_name"));
                string TeamEmail = rdr1.GetString(rdr1.GetOrdinal("Email"));
                int gameidd = rdr1.GetInt32(rdr1.GetOrdinal("game_id"));


                Label lbl_TName = new Label();
                lbl_TName.Text = i + "." + TeamName + "  <br /> <br />";
                form1.Controls.Add(lbl_TName);
                i++;


                Label lbl_Temail = new Label();
                lbl_Temail.Text = "Email: " + TeamEmail + "  <br /> <br />";
                form1.Controls.Add(lbl_Temail);


                LinkButton lbl_Gname = new LinkButton();
                lbl_Gname.Text = "Game Name: " + Gname + "  <br /> <br />";
                lbl_Gname.ID = "C" + gameidd;
                lbl_Gname.Click += this.Go_shokr;
                //lbl_Gname.PostBackUrl = "D.aspx";
                form1.Controls.Add(lbl_Gname);

            }
            if (i == 1) form1.Controls.Remove(lbl_dev);


            string connStr2 = ConfigurationManager.ConnectionStrings["MyDbConn"].ToString();
            SqlConnection conn2 = new SqlConnection(connStr2);

            SqlCommand cmd2 = new SqlCommand("games_at_conference", conn2); //*****ALTERED*****
            cmd2.CommandType = CommandType.StoredProcedure;
            cmd2.Parameters.Add(new SqlParameter("@conference_id", id));


            conn2.Open();
            SqlDataReader rdr2 = cmd2.ExecuteReader(CommandBehavior.CloseConnection);

            Label emtpy1 = new Label();
            emtpy1.Text = "  <br /> <br />";
            form1.Controls.Add(emtpy1);

            Label lbl_list = new Label();
            lbl_list.Text = "List of Games in the Conference: ";
            form1.Controls.Add(lbl_list);
            if (this.check_type() == 1)
            {
                list_available_games = new DropDownList();
                list_available_games.Width = 100;
                this.Add_to_dropbox();
                form1.Controls.Add(list_available_games);


                Button Add_a_game = new Button();
                Add_a_game.Text = "ADD GAME";
                Add_a_game.Click += this.present_game;
                form1.Controls.Add(Add_a_game);

            }
            Label emtpy222 = new Label();
            emtpy222.Text = "  <br /> <br />";
            form1.Controls.Add(emtpy222);

            int j = 1;
            while (rdr2.Read())
            {

                string Game_name = rdr2.GetString(rdr2.GetOrdinal("name"));
                int game_id = rdr2.GetInt32(rdr2.GetOrdinal("game_id"));

                LinkButton G_name = new LinkButton();
                G_name.Text = j + ". " + Game_name + "  <br /> <br />";
                G_name.ID = "P" + game_id;
                G_name.Click += this.Go_shokr; 
               ///////// G_name.PostBackUrl = "D.aspx";
                form1.Controls.Add(G_name);
                j++;
            }

            if (j == 1) form1.Controls.Remove(lbl_list);


            string connStr4 = ConfigurationManager.ConnectionStrings["MyDbConn"].ToString();
            SqlConnection conn4 = new SqlConnection(connStr4);

            SqlCommand cmd4 = new SqlCommand("Check_Attended", conn4);
            cmd4.CommandType = CommandType.StoredProcedure;

            cmd4.Parameters.Add(new SqlParameter("@email", Log_in_email));
            cmd4.Parameters.Add(new SqlParameter("@conference_id", id));

            SqlParameter check = cmd4.Parameters.Add("@checker", SqlDbType.Int);
            check.Direction = ParameterDirection.Output;

            conn4.Open();
            cmd4.ExecuteNonQuery();
            conn4.Close();

            Label emtpy2 = new Label();
            emtpy2.Text = "  <br /> <br />";
            form1.Controls.Add(emtpy2);

            Label lbl_Revlist = new Label();
            lbl_Revlist.Text = "List of Reviews: ";
            form1.Controls.Add(lbl_Revlist);

            if (check.Value.ToString().Equals("1"))
            {

                Button btn_add_rev = new Button();
                btn_add_rev.Text = "Add Review";
                btn_add_rev.Click += this.adding_rev_btn_click;
                form1.Controls.Add(btn_add_rev);
            }

            Label emtpy3 = new Label();
            emtpy3.Text = "  <br /> <br />";
            form1.Controls.Add(emtpy3);


            string connStr3 = ConfigurationManager.ConnectionStrings["MyDbConn"].ToString();
            SqlConnection conn3 = new SqlConnection(connStr3);

            SqlCommand cmd3 = new SqlCommand("view_conference_reviews", conn3);
            //*****ALTERED***** 3amalt 2 add new proc we 2 alter de w games_at_conf bas till nw
            cmd3.CommandType = CommandType.StoredProcedure;
            cmd3.Parameters.Add(new SqlParameter("@conference_id", id));


            conn3.Open();
            SqlDataReader rdr3 = cmd3.ExecuteReader(CommandBehavior.CloseConnection);


            int k = 1;
            while (rdr3.Read())
            {

                int review_id = rdr3.GetInt32(rdr3.GetOrdinal("conferences_reviews_id"));

                LinkButton G_review = new LinkButton();
                G_review.Text = k + ". " + "Review " + k + "  <br /> <br />";
                G_review.ID = "r" + review_id;
                G_review.Click += this.LinkButton_Click;
                form1.Controls.Add(G_review);
                k++;

            }

            //  if (k == 1) form1.Controls.Remove(lbl_Revlist);

        }

        private void Go_shokr(object sender, EventArgs e)
        {
            LinkButton b = sender as LinkButton ;
            Session["game"] = (b.ID).Substring(1);
            Response.Redirect("ViewGame.aspx");
        }

        protected void LinkButton_Click(object sender, EventArgs e)
        {
            LinkButton b = sender as LinkButton;
            Session["rev_id"] = (b.ID).Substring(1);
            Session["User_Email"] = Log_in_email;
            Session["conferences_id"] = "" + id;
            Response.Redirect("ReviewPage.aspx");


        }

        protected void Attend_btn_clicked(object sender, EventArgs e)
        {

            string connStr = ConfigurationManager.ConnectionStrings["MyDbConn"].ToString();
            SqlConnection conn = new SqlConnection(connStr);

            SqlCommand cmd = new SqlCommand("Attend_Conference", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add(new SqlParameter("@email", Log_in_email));
            cmd.Parameters.Add(new SqlParameter("@conference_id", id));

            SqlParameter flag = cmd.Parameters.Add("@out", SqlDbType.Int);
            flag.Direction = ParameterDirection.Output;

            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();

            if (flag.Value.ToString().Equals("1"))
            {
                MessageBox.Show("Successfully Attended");
            }
            else
            {
                MessageBox.Show("Already Attended!!");
            }
            Response.Redirect("Conference1.aspx");

        }


        protected void adding_rev_btn_click(object sender, EventArgs e)
        {
            Button b = sender as Button;
            Session["conference_id"] = "" + id;
            Session["MEmail"] = Log_in_email;
            Response.Redirect("ReviewAdd.aspx");


        }
        protected int check_type()
        {

            string connStr = ConfigurationManager.ConnectionStrings["MyDbConn"].ToString();
            SqlConnection conn = new SqlConnection(connStr);

            SqlCommand cmd = new SqlCommand("Member_Type", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add(new SqlParameter("@email", Log_in_email));

            SqlParameter flag = cmd.Parameters.Add("@Type", SqlDbType.Int);
            flag.Direction = ParameterDirection.Output;

            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();

            if (flag.Value.ToString().Equals("2"))
                return 1;
            else
                return 0;
        }

        protected void Add_to_dropbox()
        {

            string connStr = ConfigurationManager.ConnectionStrings["MyDbConn"].ToString();
            SqlConnection conn = new SqlConnection(connStr);

            SqlCommand cmd = new SqlCommand("Available_non_presented_games", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add(new SqlParameter("@email", Log_in_email));

            conn.Open();
            SqlDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            while (rdr.Read())
            {
                string game_name = rdr.GetString(rdr.GetOrdinal("name"));
                int game_id = rdr.GetInt32(rdr.GetOrdinal("game_id"));

                ListItem game_item = new ListItem();
                game_item.Text = game_name;
                game_item.Value = "" + game_id;
                list_available_games.Items.Add(game_item);

            }

        }

        protected void present_game(object sender, EventArgs e)
        {
            Button b = sender as Button;

            string gid = list_available_games.SelectedValue;
            int g_id = Int32.Parse(gid);

            string connStr = ConfigurationManager.ConnectionStrings["MyDbConn"].ToString();
            SqlConnection conn = new SqlConnection(connStr);

            SqlCommand cmd = new SqlCommand("Development_TeamPresents", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add(new SqlParameter("@email", Log_in_email));
            cmd.Parameters.Add(new SqlParameter("@game_id", g_id));
            cmd.Parameters.Add(new SqlParameter("@conference_id", id));


            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
            Response.Redirect("Conference1.aspx");

        }
    }
}