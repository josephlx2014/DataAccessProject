 using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DataAccessProject
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var connectionFromConfiguration = WebConfigurationManager.ConnectionStrings["DBConnection"];
            using (SqlConnection dbConnection = new SqlConnection(connectionFromConfiguration.ConnectionString)) 
            {
                try
                {
                    dbConnection.Open();
                    ltConnectionMessage.Text = "Connection Succesful";

                    try {

                        SqlCommand cmd = new SqlCommand("SELECT hex, name FROM Color",dbConnection);
                        SqlDataReader reader = cmd.ExecuteReader();

                        if (reader.HasRows) {
                            while (reader.Read()) {
                                ltOutput.Text += string.Format("<li style=\"color:#{0}\">{1}</li>", reader.GetString(0),reader.GetString(1));
                            }
                        }

                    }
                    catch (SqlException ex){

                        ltOutput.Text = "<li> Error: " + ex.Message + "</li>";
                    }

                }
                catch (Exception ex)
                {
                    ltConnectionMessage.Text = "Connection failed: " + ex.Message;
                   
                }

                finally 
                {
                    dbConnection.Close();
                    dbConnection.Dispose();
                }

            }
        }
    }
}