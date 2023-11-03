
using System.Diagnostics;
using System.Drawing;

namespace WarehouseSimulation
{
    /// <summary>
    /// The Warehouse class represent a warehouse with an entrance queue of trucks
    /// that are fed into a dock line queue.  This class handles the majority of the
    /// functionality by handling the truck arrivals, assigning the trucks to docks and
    /// processing them once they are at the dock.  
    /// </summary>
    internal class Warehouse
    {
        List<Dock> docks;
        Queue<Truck> entrance;
        private List<string> logs;
        private const int SIMULATIONTIME = 48;
        private const int MAXDOCKS = 15;
        private bool isWarehouseEmpty;

        /// <summary>
        /// Default constructor initializing properties.
        /// </summary>
        public Warehouse()
        {
            docks = new List<Dock>(MAXDOCKS);
            entrance = new Queue<Truck>();
            logs = new List<string>();
            isWarehouseEmpty = GetIsWarehouseEmpty();

            // Initialize docks
            for (int i = 0; i < MAXDOCKS; i++)
            {
                docks.Add(new Dock());
            }
        }

        /// <summary>
        /// Abstracted run method to itterate through each time increment 
        /// in order to process each moving component of the warehouse based
        /// on the time incrementation. Generates report based on findings
        /// and logs.
        /// </summary>
        public void Run()
        {
            for (int increment = 0; increment < SIMULATIONTIME || isWarehouseEmpty;  increment++)
            {
                HandleTruckArrivals();

                AssignTrucksToDocks();

                ProcessDocks(increment);

                isWarehouseEmpty = GetIsWarehouseEmpty();
            }
            GenerateReport();
        }

        /// <summary>
        /// Processes all docks by unloading crates from trucks. Sends off
        /// empty trucks, and logs the activity of each individual dock.
        /// </summary>
        /// <remarks>Modified https://chat.openai.com/ method</remarks>
        /// <param name="currentIncrement">Current time increment</param>
        private void ProcessDocks(int currentIncrement)
        {
            foreach (var dock in docks)
            {
                if (dock.line.Count > 0)
                {
                    // Look at the first truck without removing it
                    Truck currentTruck = dock.line.Peek();
                    Crate crate = currentTruck.Unload();
                    if (crate != null)
                    {
                        // Determine logging senario
                        string scenario;
                        if (currentTruck.trailer.Count > 0)
                        {
                            scenario = "A crate was unloaded, but the truck still has more crates to unload.";
                        }
                        else if (dock.line.Count > 1)
                        {
                            scenario = "A crate was unloaded, and the truck has no more crates to " +
                                       "unload, and another truck is already in the Dock.";
                        }
                        else
                        {
                            scenario = "A crate was unloaded, and the truck has no more crates to " +
                                       "unload, but another truck is NOT already in the Dock.";
                        }

                        // Log the details
                        string logEntry = $"Time Increment: {currentIncrement}, " +
                                          $"Driver: {currentTruck.driverName}, " +
                                          $"Delivery Company: {currentTruck.deliveryCompany}, " +
                                          $"Crate ID: {crate.Id}, " +
                                          $"Crate Value: ${crate.price}, " +
                                          $"Scenario: {scenario}";
                        logs.Add(logEntry);

                        dock.totalSales += crate.price;
                        dock.totalCrates++;
                        dock.timeInUse++;

                        // Truck is now empty
                        if (currentTruck.trailer.Count == 0) 
                        {
                            dock.SendOff();
                        }
                    }
                }
                else
                {
                    dock.timeNotInUse++;
                }
            }
        }

        /// <summary>
        /// Assign trucks that are in the entrance queue to the dock with the shortest
        /// line.
        /// </summary>
        private void AssignTrucksToDocks()
        {
            // Distribute trucks in the entrance to available docks. 
            // Trucks go to the shortest line.
            while (entrance.Count > 0)
            {
                Dock shortestDock = docks.OrderBy(d => d.line.Count).FirstOrDefault();
                if (shortestDock != null)
                {
                    Truck truck = entrance.Dequeue();
                    shortestDock.JoinLine(truck);
                }
                else
                {
                    break; // No more docks available or they are all full
                }
            }
        }

        // TBD ==> this method will be modified to include seperate binomial dist
        // function and generate approriate truck absed on the time of day then queue
        // them up at entrance.
        public void HandleTruckArrivals()
        {
            //Change out RandomlyGenerateTruck to a binomial distributed function
            var truck = RandomlyGenerateTruck();
            entrance.Enqueue(truck);
        }

        // Get rid of this method --> merge with HandleTruckArrival
        // to reduce coupling.  Handle binomial distribution their.
        public Truck RandomlyGenerateTruck()
        {
            //Random random = new Random();
            //int chance = random.Next(0, 100);

            //if (chance > 50)
            //{
            Truck truck = new Truck();
            return truck;
            //}
            //return null;
        }

        /// <summary>
        /// Checks if each dock and the entrance is empty.
        /// </summary>
        /// <returns>if the warehouse is empty - bool</returns>
        public bool GetIsWarehouseEmpty()
        {
            return docks.All(dock => dock.line.Count == 0) && entrance.Count == 0;
        }

        /// <summary>
        /// Retrieve information based on warehouse properties like the total operating
        /// cost per dock and total revenue to print results.  Also prints logs accumulated
        /// in the ProcessDocks method.
        /// </summary>
        public void GenerateReport()
        {
            //Aggregated values
            int totalTrucksProcessed = docks.Sum(d => d.totalTrucks);
            int totalCratesUnloaded = docks.Sum(d => d.totalCrates);
            double totalValueOfCrates = docks.Sum(d => d.totalSales);
            double totalDockTimeInUse = docks.Sum(d => d.timeInUse);
            double totalDockTimeNotInUse = docks.Sum(d => d.timeNotInUse);
            double totalCostOfOperatingDocks = totalDockTimeInUse * 100;  // Assuming $100 per time increment
            double totalRevenue = totalValueOfCrates - totalCostOfOperatingDocks;

            //Calculate averages
            double averageCrateValue = totalValueOfCrates / totalCratesUnloaded;
            double averageValuePerTruck = totalValueOfCrates / totalTrucksProcessed;
            double averageDockUsageTime = totalDockTimeInUse / docks.Count;

            //Calculate the longest line at any loading dock
            int longestLine = docks.Max(d => d.line.Count);

            //Print out the report
            Console.WriteLine("------ Warehouse Simulation Report ------");
            Console.WriteLine($"Number of docks open: {docks.Count}");
            Console.WriteLine($"Longest line at any dock: {longestLine}");
            Console.WriteLine($"Total number of trucks processed: {totalTrucksProcessed}");
            Console.WriteLine($"Total number of crates unloaded: {totalCratesUnloaded}");
            Console.WriteLine($"Total value of unloaded crates: ${totalValueOfCrates}");
            Console.WriteLine($"Average value of each crate: ${averageCrateValue}");
            Console.WriteLine($"Average value of each truck: ${averageValuePerTruck}");
            Console.WriteLine($"Total time docks were in use: {totalDockTimeInUse} time increments");
            Console.WriteLine($"Total time docks were not in use: {totalDockTimeNotInUse} time increments");
            Console.WriteLine($"Average dock usage time: {averageDockUsageTime} time increments");
            Console.WriteLine($"Total cost of operating docks: ${totalCostOfOperatingDocks}");
            Console.WriteLine($"Total revenue: ${totalRevenue}");
            Console.WriteLine("-----------------------------------------");

            //Print out logs
            Console.WriteLine("--------- Crate Unloading Logs ----------");
            foreach (string log in logs)
            {
                Console.WriteLine(log);
            }
            Console.WriteLine("-----------------------------------------");
        }
    }
}
