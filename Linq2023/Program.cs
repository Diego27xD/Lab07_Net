using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linq2023
{
    public class Program
    {
        public static DataClasses2DataContext context = new DataClasses2DataContext();
        static void Main(string[] args)
        {
            //DataSource();
            //Filtering();
            //Ordering();
            //Grouping();
            //Grouping2();
            //Joining();

            DataSourceLambda();
            //FilteringLambda();
            //OrderingLambda();
            //GroupingLambda();
            //Grouping2Lambda();
            //JoiningLambda();
            Console.Read();

        }
        static void IntroToLINQ()
        {
            int[] numbers = new int[7] { 0, 1, 2, 3, 4, 5, 6 };

            var numQuery = 
                from num in numbers
                where ( num % 0) == 0
                select num;

            foreach (int num in numQuery)
            {
                Console.Write("{0,1}", num);
            }
        }

        
        static void DataSource()
        {
            var queryAllCustomers = from cust in context.clientes
                                    select cust;

            foreach (var item in queryAllCustomers)
            {
                Console.WriteLine(item.NombreCompañia);
            }
        }

        static void DataSourceLambda()
        {
            var queryAllCustomers = context.clientes;

            foreach (var item in queryAllCustomers)
            {
                Console.WriteLine(item.NombreCompañia);
            }
        }

        static void Filtering()
        {
            var queryLondonCustomers = from cust in context.clientes
                                       where cust.Ciudad == "Londres"
                                       select cust;
            foreach (var item in queryLondonCustomers)
            {
                Console.WriteLine(item.Ciudad);
            }
        }

        static void FilteringLambda()
        {
            var queryLondonCustomers = context.clientes.Where(cust => cust.Ciudad == "Londres");

            foreach (var item in queryLondonCustomers)
            {
                Console.WriteLine(item.Ciudad);
            }
        }

        static void Ordering()
        {
            var queryLondonCustomer3 = (from cust in context.clientes
                                       where cust.Ciudad == "Londres"
                                       orderby cust.NombreCompañia ascending
                                       select cust).ToList();
            foreach(var item in queryLondonCustomer3)
            {
                Console.WriteLine(item.NombreCompañia);
            }
        }

        static void OrderingLambda()
        {
            var queryLondonCustomer3 = context.clientes
                .Where(cust => cust.Ciudad == "Londres")
                .OrderBy(cust => cust.NombreCompañia)
                .ToList();

            foreach (var item in queryLondonCustomer3)
            {
                Console.WriteLine(item.NombreCompañia);
            }
        }

        static void Grouping()
        {
            var queryCustomersByCity = from cust in context.clientes
                                       group cust by cust.Ciudad;

            foreach (var customerGroup in queryCustomersByCity)
            {
                Console.WriteLine(customerGroup.Key);

                foreach (clientes customer in customerGroup)
                {
                    Console.WriteLine("{0}", customer.NombreCompañia);
                }
            }
        }

        static void GroupingLambda()
        {
            var queryCustomersByCity = context.clientes
                .GroupBy(cust => cust.Ciudad);

            foreach (var customerGroup in queryCustomersByCity)
            {
                Console.WriteLine(customerGroup.Key);

                foreach (var customer in customerGroup)
                {
                    Console.WriteLine("{0}", customer.NombreCompañia);
                }
            }
        }

        static void Grouping2()
        {
            var custQuery = from cust in context.clientes
                            group cust by cust.Ciudad into custGroup
                            where custGroup.Count() > 2
                            orderby custGroup.Key
                            select custGroup;
            foreach (var item in custQuery)
            {
                Console.WriteLine(item.Key);
            }
        }

        static void Grouping2Lambda()
        {
            var custQuery = context.clientes
                .GroupBy(cust => cust.Ciudad)
                .Where(custGroup => custGroup.Count() > 2)
                .OrderBy(custGroup => custGroup.Key);

            foreach (var item in custQuery)
            {
                Console.WriteLine(item.Key);
            }
        }

        static void Joining()
        {
            var innerJoinQuery = from cust in context.clientes
                                 join dist in context.Pedidos on cust.idCliente equals dist.IdCliente
                                 select new { CustomerName = cust.NombreCompañia, DistributorName = dist.PaisDestinatario };
            foreach (var item in innerJoinQuery)
            {
                Console.WriteLine(item.CustomerName);
            }
        }

        static void JoiningLambda()
        {
            var innerJoinQuery = context.clientes
                .Join(context.Pedidos,
                    cust => cust.idCliente,
                    dist => dist.IdCliente,
                    (cust, dist) => new { CustomerName = cust.NombreCompañia, DistributorName = dist.PaisDestinatario });

            foreach (var item in innerJoinQuery)
            {
                Console.WriteLine(item.CustomerName);
            }
        }


    }
}
