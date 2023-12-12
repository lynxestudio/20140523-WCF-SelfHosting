using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Npgsql;
using Tests.WCF.Services.Objects;

namespace Tests.WCF.Services.Data
{
    public sealed class DataManager
    {
        public List<Product> GetProducts(string name)
        {
            StringBuilder commandText = new StringBuilder("SELECT id, ")
            .Append("product_code, ")
            .Append("product_name ")
            .Append("FROM Products ")
            .AppendFormat("WHERE product_name Like '%{0}%'", name);
            List<Product> productList = null;
            Product p = null;
                using (NpgsqlDataReader reader = PostgreSQLCommand.GetReader(commandText.ToString(),
                    null,
                    CommandType.Text))
                {
                    productList = new List<Product>();
                    while (reader.Read())
                    {
                        p = new Product();
                        p.Product_ID = Convert.ToInt32(reader["id"]);
                        p.ProductCode = reader["product_code"].ToString();
                        p.ProductName = reader["product_name"].ToString();
                        productList.Add(p);
                    }
                }
            return productList;
        }
    }
}
