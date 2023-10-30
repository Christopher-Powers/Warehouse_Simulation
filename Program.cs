namespace WarehouseSimulation
{
	internal class Program
	{
		static void Main(string[] args)
		{
			// Testing Truck props
			Truck testTruck = new Truck();
            Console.WriteLine(testTruck.driverName);
			Console.WriteLine(testTruck.deliveryCompany);

			// Testing Crate props
			Crate testCrate = new Crate();
			Crate testCrate1 = new Crate();
			Crate testCrate2 = new Crate();
			Console.WriteLine(testCrate.Id);
            Console.WriteLine(testCrate.price);

			//Test Trucks Load method
            testTruck.Load(testCrate);
			testTruck.Load(testCrate1);
			testTruck.Load(testCrate2);
			Console.WriteLine("Crates loaded: ");
            foreach (Crate crate in testTruck.trailer)
			{
				Console.WriteLine(crate.Id);
				Console.WriteLine(crate.price);
                Console.WriteLine("");
            }

			//Test Truck unload
			testTruck.Unload();
            Console.WriteLine("Trucks after unloading:");
			foreach (Crate crate in testTruck.trailer)
			{
				Console.WriteLine(crate.Id);
				Console.WriteLine(crate.price);
				Console.WriteLine("");
			}

		}
	}
}