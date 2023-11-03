
namespace WarehouseSimulation
{
    /// <summary>
    /// The dock class represents a single loading dock in within the warehouse.
    /// It manages a queue of trucks that are waiting to be unloaded.  It has
    /// various properties that aid in track statistics about the dock and its
    /// efficency.
    /// </summary>
    internal class Dock
    {
        public string Id { get; set; }
        public double totalSales { get; set; }
        public int totalCrates { get; set; }
        public int totalTrucks { get; set; }
        public int timeInUse { get; set; }
        public int timeNotInUse { get; set; }

        public Queue<Truck> line;

        /// <summary>
        /// Dock constructor to initializes all properties to a zero or null value
        /// to be determined in Warehouse.
        /// </summary>
        public Dock()
        {
            Id = GetUniqueId();
            totalSales = 0;
            totalCrates = 0;
            totalTrucks = 0;
            timeInUse = 0;
            timeNotInUse = 0;
            line = new Queue<Truck>();
        }

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
            if (line.Count > 0)
            {
                return line.Dequeue();
            }
            return null;
        }

        /// <summary>
        /// Gets a unique ID for the Dock
        /// </summary>
        /// <returns>Unique ID string</returns>
        public string GetUniqueId()
        {
            string uniqueId = Guid.NewGuid().ToString("N");
            string shortendId = uniqueId.Substring(0, 5).ToUpper();
            return shortendId;
        }

        /// <summary>
        /// Overidden ToString to print Dock properties
        /// </summary>
        /// <returns>Dock properties string</returns>
        public override string ToString()
        {
            string truckString = "";
            foreach (Truck truck in line)
            {
                truckString += ">>" + truck.ToString() + ",\n";
            }

            return $"Dock Id: {Id}\n" +
                $"Total Sales: {totalSales}\n" +
                $"Total Crates: {totalCrates}\n" +
                $"Total Trucks: {totalTrucks}\n" +
                $"Time in Use: {timeInUse}\n";
        }
    }
}
