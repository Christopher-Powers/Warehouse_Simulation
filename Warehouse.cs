
namespace WarehouseSimulation
{
    internal class Warehouse
    {
        List<Dock> docks = new List<Dock>();

        Queue<Truck> entrance = new Queue<Truck>();

        //Additional properties to run simulation (run())

        //private const int SIMULATIONTIME = 48;
        //private const int MAXDOCKS = 15;


        public void Run()
        {
            //for (int timeIncrement = 0; timeIncrement < SIMULATIONTIME; timeIncrement++)
            //{
            //    // Randomly decide if a truck arrives at the entrance
            //    if (rand.NextDouble() < 0.5) // 50% chance of a truck arrival. Adjust as needed
            //    {
            //        var truck = new Truck(); // A method to create a truck with random properties
            //        entrance.Enqueue(truck);
            //    }

            //    // Process each dock for the time increment
            //    foreach (var dock in docks)
            //    {
            //        if (dock.IsFree() && entrance.Count > 0) // Assuming IsFree method to check if dock is not occupied
            //        {
            //            var nextTruck = entrance.Dequeue();
            //            dock.JoinLine(nextTruck);
            //        }

            //        dock.ProcessIncrement();
            //    }
            //}
        }
    }
}
