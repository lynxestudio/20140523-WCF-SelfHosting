using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tests.WCF.Services;
using System.ServiceModel;
using Tests.WCF.Services.Objects;

namespace ConsoleHostingClient
{
    class Program
    {
        static void Main(string[] args)
        {
            IProductsContract proxy = GetServiceProxy();
            Console.Write("Search by name > ");
            string textToSearch = Console.ReadLine();
            List<Product> products = proxy.GetProductsByName(textToSearch);
            if (products != null)
            {
                products.ForEach(item =>
                {
                    Console.WriteLine("Item Id: {0}", item.Product_ID);
                    Console.WriteLine("Item code: {0}", item.ProductCode);
                    Console.WriteLine("Item name: {0}", item.ProductName);
                    Console.WriteLine("+-------------------------------------+");
                });
            }
            else
                Console.WriteLine("No found.");
            Console.WriteLine("\nPress <ENTER> to quit.");
            Console.ReadLine();
        }

        static IProductsContract GetServiceProxy() 
        {
            ChannelFactory<IProductsContract> factory = new ChannelFactory<IProductsContract>(new WSHttpBinding()
                , new EndpointAddress("http://localhost:8004/Products"));
            IProductsContract proxy = factory.CreateChannel();
            return proxy;
        }
    }
}
