using System;
using System.Collections.Generic;
using System.Web.Services;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.Script.Serialization; // This using is to return in JSON mode.


namespace AngularWS
{
    /// <summary>
    /// Summary description for ProductsService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class ProductsService : System.Web.Services.WebService
    {

        [WebMethod]
        public void getProducts()
        {
            List<Product> ProductList = new List<Product>();
            string cs = ConfigurationManager.ConnectionStrings["SQLServerConection"].ConnectionString;
            using (SqlConnection con = new SqlConnection(cs))
            {
                //SqlCommand  cmd = new SqlCommand("Select * from Student",con );
                SqlCommand cmd = new SqlCommand("sp_cal_total_percentage", con);
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    Product product = new Product();
                    product.id = Convert.ToInt16(dr["id"]);
                    product.name = Convert.ToString(dr["name"]).Trim();
                    product.Subject1 = Convert.ToString(dr["Subject1"]);
                    product.Subject2 = Convert.ToString(dr["Subject2"]);
                    product.Subject3 = Convert.ToString(dr["Subject3"]);
                    product.Subject4 = Convert.ToString(dr["Subject4"]);
                    product.Subject5 = Convert.ToString(dr["Subject5"]);
                    product.total    = Convert.ToInt16(dr["total"]);
                    product.percentage  = Convert.ToDecimal(dr["percentage"]); 
                    ProductList.Add(product);
                }
            }
            JavaScriptSerializer js = new JavaScriptSerializer();
            Context.Response.Write(js.Serialize(ProductList));

        }
    }
}
