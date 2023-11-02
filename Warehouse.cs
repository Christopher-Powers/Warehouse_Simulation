
using System.Drawing;

namespace WarehouseSimulation
{
    internal class Warehouse
    {
        List<Dock> docks;
        Queue<Truck> entrance;
        private const int SIMULATIONTIME = 48;
        private const int MAXDOCKS = 15;

        public Warehouse()
        {
            docks = new List<Dock>(MAXDOCKS);
            entrance = new Queue<Truck>();

            // Initialize docks
            for (int i = 0; i < MAXDOCKS; i++)
            {
                docks.Add(new Dock());
            }
        }

        public void Run()
        {
            for(int increment = 0; increment < SIMULATIONTIME; increment++)
            {
                //1 Handle Truck arrivals
                var truck = RandomlyGenerateTruck();
                Console.WriteLine(truck.ToString());
                entrance.Enqueue(truck);

                AssignTrucksToDocks();

                ProcessDocks();
            }

            
        }

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

        private void ProcessDocks()
        {
            foreach (var dock in docks)
            {
                //Testing - remove when done
                dock.ToString();

                if (dock.line.Count > 0)
                {
                    Truck currentTruck = dock.line.Peek(); // Look at the first truck without removing it
                    Crate crate = currentTruck.Unload();
                    if (crate != null)
                    {
                        // Log the crate to a CSV or some data structure
                        //LogCrate(currentTime, currentTruck, crate);

                        dock.totalSales += crate.price;
                        dock.totalCrates++;
                        dock.timeInUse++;

                        if (currentTruck.trailer.Count == 0) // Truck is now empty
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

        private void AssignTrucksToDocks()
        {
            // Distribute trucks in the entrance to available docks. This is a naive implementation where each truck goes to the shortest line.
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

        public void PrintTotalSales()
        {
            double? totalWarehouseSales = 0;
            foreach(var dock in docks)
            {
                totalWarehouseSales += dock.totalSales;
            }

            Console.WriteLine(totalWarehouseSales);
        }
    }
}
