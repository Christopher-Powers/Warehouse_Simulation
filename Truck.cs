
using System.Threading.Channels;

namespace WarehouseSimulation
{
    /// <summary>
    /// Represents a truck with a driver name, delivery company, and a trailer.
    /// </summary>
    internal class Truck
    {
        public string driverName {  get; set; }
        public string deliveryCompany {  get; set; }

        public Stack<Crate> trailer = new Stack<Crate>();

        /// <summary>
        /// Initializes truck driverName, deliveryCompany, trailer properties
        /// to random values.
        /// </summary>
        public Truck()
        {
            driverName = GetRandomName();
            deliveryCompany = GetRandomCompany();
            trailer = GetRandomTrailer();
        }

        /// <summary>
        /// Gets one of twenty random names.
        /// </summary>
        /// <returns>A random name</returns>
        public string GetRandomName()
        {
            Random random = new Random();
            string[] fakeNames = new string[]
            {
                "John Smith", "Sarah Johnson", "Michael Brown", "Emily Davis", "David Wilson",
                "Jennifer Lee", "Robert Anderson", "Mary Jones", "William Martin", "Susan Taylor",
                "James Clark", "Karen White", "Joseph Harris", "Lisa Miller", "Richard Turner",
                "Patricia Moore", "Thomas Walker", "Linda Hall", "Christ Almighty", "Nancy King",
                "Dixon Knutz", "Cosmo McGee", "Tanjiro Kamado", "Muzan Kibutsuji", "Satoru Gojo",
                "Yuji Itadori", "Eren Yager", "Mikasa Ackerman", "Armin Arlert" 
            };
            return fakeNames[random.Next(fakeNames.Length)];
        }

        /// <summary>
        /// Gets one of ten delivery company names at random.
        /// </summary>
        /// <returns>A company name(string)</returns>
        public string GetRandomCompany()
        {
            Random random = new Random();
            string[] deliveryCompanies = new string[]
            {
                "UPS","FedEx","USPS", "FleetFulfil", 
                "DHL Express","Amazon Logistics","Walmart Delivery","FedEx Ground",
                "UPSF","OnTrac","USF Reddaway"
            };
            return deliveryCompanies[random.Next(deliveryCompanies.Length)];
        }

        /// <summary>
        /// Randomizes the number of crates added to the trucks trailer.
        /// </summary>
        /// <returns></returns>
        public Stack<Crate> GetRandomTrailer()
        {
            Random random = new Random();
            Stack<Crate> stackOfCrates = new Stack<Crate>();
            int randomNum = random.Next(1, 10);

            for (int i = 0; i < randomNum; i++)
            {
                Crate crate = new Crate();
                stackOfCrates.Push(crate);
            }
            return stackOfCrates;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool GetIsEmpty()
        {
            if(trailer.Count <= 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Adds a crate to the top of the Truck.trailer stack.
        /// </summary>
        /// <param name="crate">Crate object</param>
        public void Load(Crate crate)
        {
            trailer.Push(crate);
        }

        /// <summary>
        /// Pops crate of the top of the Truck.trailer stack.
        /// </summary>
        /// <returns>Crate popped off</returns>
        public Crate Unload()
        {
            return trailer.Pop();
        }

        public override string ToString()
        {
            string trailerString = "";
            foreach (Crate crate in trailer)
            {
                trailerString += crate.ToString() + "\n";
            }

            string truckString = $"{driverName}\n" +
                                    $"{deliveryCompany}\n" +
                                    $"{trailerString}\n" +
                                    "===============================";

            return truckString;
        }

    }
}
