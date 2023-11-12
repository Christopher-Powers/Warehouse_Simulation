
namespace WarehouseSimulation
{
    /// <summary>
    /// Represent a Crate with an Id and price.
    /// </summary>
    internal class Crate
    {
        public string Id { get; set; }
        public double price { get; set; }
        public int incrimentWhenUnloaded { get; set; }

        /// <summary>
        /// Represents a crate with an Id and price.
        /// </summary>
        public Crate()
        {
            Id = GetUniqueId();
            price = GetRandomDouble(50, 500);
            incrimentWhenUnloaded = 0;
        }

        /// <summary>
        /// Expanded use of the Random.NextDouble() function to fit crate 
        /// requirements.
        /// https://stackoverflow.com/questions/1064901/random-number-between-2-double-numbers
        /// </summary>
        /// <param name="minimum">Minimum value</param>
        /// <param name="maximum">Maximum value</param>
        /// <returns>Random double value based on min and max</returns>
        public double GetRandomDouble(double minimum, double maximum)
        {
            Random random = new Random();
            return random.NextDouble() * (maximum - minimum) + minimum;
        }

        /// <summary>
        /// Gets an 8 character unique ID for the crate.
        /// https://stackoverflow.com/questions/11313205/generate-a-unique-id
        /// </summary>
        /// <returns>Unique Id</returns>
        public string GetUniqueId()
        {
            return Guid.NewGuid().ToString("N").Substring(0, 8).ToUpper();
        }

        /// <summary>
        /// Overidden ToString to print properties
        /// </summary>
        /// <returns>Crate properties in a string</returns>
        public override string ToString()
        {
            return $"Id = {Id}, Price = {price}";
        }
    }
}
