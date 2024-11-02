using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Tests.WCF.Services.Data;

namespace Tests.WCF.Services
{
    
    public class ProductsImplementation : IProductsContract
    {
        public List<Objects.Product> GetProductsByName(string name)
        {
                DataManager da = new DataManager();
                return da.GetProducts(name);   
        }
    }
}
