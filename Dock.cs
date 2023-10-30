
namespace WarehouseSimulation
{
    internal class Dock
    {
        public string? Id { get; set; }
        public double? totalSales { get; set; }
        public int totalCrates { get; set; }
        public int totalTrucks { get; set; }
        public int timeInUse { get; set; }
        public int timeNotInUse { get; set; }

        Queue<Truck> line = new Queue<Truck>();


        /// <summary>
        /// Add truck to the back of the line queue.
        /// </summary>
        /// <param name="truck">truck object</param>
        public void JoinLine(Truck truck)
        {
            line.Enqueue(truck);
        }

        /// <summary>
        /// Removes truck from the front of the line queue.
        /// </summary>
        /// <returns>Truck object dequeued</returns>
        public Truck SendOff()
        {
            return line.Dequeue();
        }

        public void ProcessIncrement()
        {
        //    if (line.Count > 0)
        //    {
        //        var currentTruck = line.Peek();
        //        var unloadedCrate = currentTruck.Unload();

        //        totalSales += unloadedCrate.price;
        //        totalCrates++;

        //        if (currentTruck.isEmpty) // Assuming you have a method to check if the truck is empty
        //        {
        //            SendOff();
        //            totalTrucks++;
        //        }

        //        timeInUse++;
        //    }
        //    else
        //    {
        //        timeNotInUse++;
        //    }
        }
    }
}
