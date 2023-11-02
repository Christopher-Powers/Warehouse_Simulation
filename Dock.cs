
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

        public Queue<Truck> line;

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

        public string GetUniqueId()
        {
            string uniqueId = Guid.NewGuid().ToString("N");
            string shortendId = uniqueId.Substring(0, 5).ToUpper();
            return shortendId;
        }

        public override string ToString()
        {
            string truckString = "";
            foreach(Truck truck in line)
            {
                truckString += ">>" + truck.ToString() +",\n";
            }

            return $"Dock Id: {Id}\n" +
                $"Total Sales: {totalSales}\n" +
                $"Total Crates: {totalCrates}\n" +
                $"Total Trucks: {totalTrucks}\n" +
                $"Time in Use: {timeInUse}\n";
        }
    }
}
