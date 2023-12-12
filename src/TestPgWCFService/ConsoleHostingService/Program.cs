using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;

namespace ConsoleHostingService
{
    class Program
    {
        static void Main(string[] args)
        {
            Uri baseAddress = new Uri("http://localhost:8004/");
            Type service = typeof(Tests.WCF.Services.ProductsImplementation);
            ServiceHost host = new ServiceHost(service, baseAddress);
                using (host)
                {
                    Type contract = typeof(Tests.WCF.Services.IProductsContract);
                    host.AddServiceEndpoint(contract, new WSHttpBinding(), "Products");
                    host.Open();
                    Console.WriteLine("Products service running.Press <ENTER> to quit.");
                    Console.ReadLine();
                    host.Close();
                }
            
        }
    }
}
