using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Web;
using System.Web.Services.Description;

namespace LMS1701.EA.SOAPAPI
{
    public class Program
    {
        static void main()
        {
            using (ServiceHost host = new ServiceHost(typeof(IService1)))
            {
                host.Open();
                Console.WriteLine("Host started @ " +DateTime.Now.ToString());
                Console.ReadLine();

            }
        }
      
    }
}