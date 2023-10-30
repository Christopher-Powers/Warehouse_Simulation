
namespace WarehouseSimulation
{
	internal class Crate
	{ 
		public string? Id { get; set; }
		public double? price { get; set; }

        public Crate()
        {
            Id = GetUniqueId();
			price = GetRandomDouble(50, 500);
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
			string uniqueId = Guid.NewGuid().ToString("N");
			string shortendId = uniqueId.Substring(0, 8).ToUpper();
			return shortendId;
		}

	}
}
