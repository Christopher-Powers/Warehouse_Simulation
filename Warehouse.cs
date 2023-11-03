
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
        private const int MAXDOCKS = 6;
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
            //Runs simulation, incremementing based on time and if the warehouse is empty
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
                            scenario = "Crate unloaded, truck still contains crates.";
                        }
                        else if (dock.line.Count > 1)
                        {
                            scenario = "Crate unloaded, truck empty. New truck docked.";
                        }
                        else
                        {
                            scenario = "Crate unloaded, truck empty. Dock empty.";
                        }

                        // Log the details
                        DateTime startTime = new DateTime(1, 1, 1, 9, 0, 0); // Workday starts at 9:00 AM
                        DateTime currentTime = startTime.AddMinutes(currentIncrement * 10);
                        string formattedTime = currentTime.ToString("hh:mm tt");


                        string logEntry = string.Format("{0,-5} | {1,-15} | {2,-16} | {3,-10} | {4,-11:C} | {5}",
                                formattedTime,
                                currentTruck.driverName,
                                currentTruck.deliveryCompany,
                                crate.Id,
                                crate.price,
                                scenario);
                        logs.Add(logEntry);

                        // Handle truck properties
                        dock.totalSales += crate.price;
                        dock.totalCrates++;
                        dock.timeInUse++;

                        // Truck is now empty
                        if (currentTruck.trailer.Count == 0) 
                        {
                            dock.totalTrucks++;
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
                Dock? shortestDock = docks.OrderBy(d => d.line.Count).FirstOrDefault();
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
            Truck truck = new Truck();
            //var truck = RandomlyGenerateTruck();
            entrance.Enqueue(truck);
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
            Console.WriteLine("------ Warehouse Simulation Report-------");
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

            //Print out logs in a tabular format
            Console.WriteLine("------------------------------- Crate Unloading Logs ------------------------------------");
            Console.WriteLine("{0,-8} | {1,-15} | {2,-16} | {3,-10} | {4,-9} | {5}",
                              "Time", "Driver", "Delivery Company", "Crate ID", "Crate Value", "Scenario");
            Console.WriteLine(new string('-', 95)); // Adjust the number of '-' to match the header's width.

            foreach (string log in logs)
            {
                // We will assume that log is a plain string formatted similarly to what was provided previously
                Console.WriteLine(log);
            }

            Console.WriteLine("-------------------------------------------------------------------------------");

        }
    }
}
