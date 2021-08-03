using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace DAL
{
    public class ProductDAL
    {
        private IConfiguration _configuration;
        public ProductDAL(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        //string _constr = _constr.CName;

        public IEnumerable<Product> GetAllProducts()
        {
            List<Product> lstProduct = new List<Product>();
            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("DbConnection")))
            {
                SqlCommand cmd = new SqlCommand("spGetAllProduct", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    Product product = new Product();
                    product.ProductId = Convert.ToInt32(rdr["ProductId"]);
                    product.Name = rdr["Name"].ToString();
                    product.Description = rdr["Description"].ToString();
                    product.UnitPrice = Convert.ToDecimal(rdr["UnitPrice"]);
                    product.CategoryId = Convert.ToInt32(rdr["CategoryId"]);
                    lstProduct.Add(product);
                }
                con.Close();
            }
            return lstProduct;
        }

        public IEnumerable<Category> GetAllCategories()
        {
            List<Category> lstCategory = new List<Category>();
            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("DbConnection")))
            {
                SqlCommand cmd = new SqlCommand("spGetAllCategories", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    Category category = new Category();
                    category.CategoryId = Convert.ToInt32(rdr["CategoryId"]);
                    category.Name = rdr["Name"].ToString();
                    
                    lstCategory.Add(category);
                }
                con.Close();
            }
            return lstCategory;
        }

        public void AddProduct(Product product)
        {
            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("DbConnection")))
            {
                SqlCommand cmd = new SqlCommand("spAddProduct", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Name", product.Name);
                cmd.Parameters.AddWithValue("@Description", product.Description);
                cmd.Parameters.AddWithValue("@UnitPrice", product.UnitPrice);
                cmd.Parameters.AddWithValue("@CategoryId", product.CategoryId);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        public void UpdateProduct(Product product)
        {
            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("DbConnection")))
            {
                SqlCommand cmd = new SqlCommand("spUpdateProduct", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ProductId", product.ProductId);
                cmd.Parameters.AddWithValue("@Name", product.Name);
                cmd.Parameters.AddWithValue("@Description", product.Description);
                cmd.Parameters.AddWithValue("@UnitPrice", product.UnitPrice);
                cmd.Parameters.AddWithValue("@CategoryId", product.CategoryId);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        public Product GetProductData(int? id)
        {
            Product Product = new Product();

            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("DbConnection")))
            {
                string sqlQuery = "SELECT * FROM Products WHERE ProductId= " + id;
                SqlCommand cmd = new SqlCommand(sqlQuery, con);
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    Product.ProductId = Convert.ToInt32(rdr["ProductId"]);
                    Product.Name = rdr["Name"].ToString();
                    Product.Description = rdr["Description"].ToString();
                    Product.UnitPrice = Convert.ToDecimal(rdr["UnitPrice"]);
                    Product.CategoryId = Convert.ToInt32(rdr["CategoryId"]);
                }
            }
            return Product;
        }

        public void DeleteProduct(int? id)
        {
            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("DbConnection")))
            {
                SqlCommand cmd = new SqlCommand("spDeleteProduct", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ProductId", id);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }
    }

}
