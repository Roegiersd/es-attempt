using EventStore.ClientAPI;
using System;
using Newtonsoft.Json;

namespace EventStreamTest
{
    class Program
    {
        private const string CUSTOMERSTREAMPREFIX = "Customer_";
        private const string ADDRESSSTREAMPREFIX = "Address_";
        static void Main(string[] args)
        {
            var p = new Program();
            p.DoSomething();
        }

        public void DoSomething()
        {
            try
            {
                var conn = EventStoreConnection.Create(new Uri("conn stre"),
                    "InputFromFileConsoleApp");
                conn.ConnectAsync();
                AddCustomers(ref conn);
                AddressAdded(ref conn);
                conn.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            Console.ReadKey();
        }


        //public void AddCustomers(ref IEventStoreConnection conn)
        //{
        //    for (int i = 0; i <= 10000; i++)
        //    {
        //        var x = new Customer { FirstName = "John" + i.ToString(), Lastname = "Doe" };
        //        var data = System.Text.Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(x));
        //        byte[] md = null;
        //        var eventData = new EventData(Guid.NewGuid(), "Customer_Created", true, data, md);
        //        conn.AppendToStreamAsync("Customers", 1, eventData).Wait();
        //        Console.WriteLine(String.Format("Added{0}", i));
        //    }
        //}

        public void AddCustomers(ref IEventStoreConnection conn)
        {
            for (int i = 0; i <= 10000; i++)
            {
                var x = new Customer { FirstName = "John" + i.ToString(), Lastname = "Doe", Id = i + 1 };
                var data = System.Text.Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(x));
                byte[] md = null;
                var eventData = new EventData(Guid.NewGuid(), "Customer_Created", true, data, md);
                conn.AppendToStreamAsync(CUSTOMERSTREAMPREFIX + (x.Id).ToString(), ExpectedVersion.NoStream, eventData).Wait();
                Console.WriteLine(String.Format("Added{0}", i));
            }
        }

        public void AddressAdded(ref IEventStoreConnection conn)
        {
            for (int i = 2000; i <= 8900; i++)
            {
                var address = new Address { City = "Ghent", Street = "DeadEnd", Id = i + 1 };
                var customer = new Customer { FirstName = "John" + i.ToString(), Lastname = "Doe", Id = i + 1, primaryAddress = address.Id };
                var addressData = System.Text.Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(address));
                var customerData = System.Text.Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(customer));
                var eventData = new EventData(Guid.NewGuid(), "AddressCreated", true, addressData, null);
                conn.AppendToStreamAsync(CUSTOMERSTREAMPREFIX + (customer.Id).ToString(), 0, eventData).Wait();
                eventData = new EventData(Guid.NewGuid(), "Primary_Address_Changed", true, customerData, null);
                conn.AppendToStreamAsync(CUSTOMERSTREAMPREFIX + (customer.Id).ToString(), 1, eventData).Wait();

                Console.WriteLine(String.Format("Added{0}", i));
            }
        }
    }

    public class Customer
    {
        public string FirstName { get; set; }
        public string Lastname { get; set; }
        public Int64 Id { get; set; }
        public Int64 primaryAddress { get; set; }
    }

    public class Address
    {
        public string City { get; set; }
        public string Street { get; set; }
        public Int64 Id { get; set; }
    }
}
