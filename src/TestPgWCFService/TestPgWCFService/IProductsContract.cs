using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Tests.WCF.Services.Objects;

namespace Tests.WCF.Services
{
    [ServiceContract(Name="Products")]
    public interface IProductsContract
    {
        [OperationContract]
        List<Product> GetProductsByName(string name);
    }
}
