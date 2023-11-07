///////////////////////////////////////////////////////////////////////////////
//																			 	
// Authors: Jason Gun, GUNJC1@etsu.edu										 
//			Dustin Sway, ZDCS16@etsu.edu									 
//			Chris Powers, powersct@etsu.edu									 
// Course: CSCI-2210-001 - Data Structures									 
// Assignment: Project 3													 
// Description: Warehouse Simulation - Optimize cost efficency			     
//																			 
//																			 
///////////////////////////////////////////////////////////////////////////////

namespace WarehouseSimulation
{
    /// <summary>
    /// Program class serves as the entry point for the Warehouse simulation.
    /// </summary>
    internal class Program
    {
        /// <summary>
        /// Main entry point for the application.
        /// </summary>
        static void Main()
        {
            //=========TODO===============
            //1) Modify HandleTruckArrivals() to account for binomially
            //   distributed trucks
            //2) Test various efficencies by change MAXDOCKS const
            //3) Write brief recommendation based on findings
            //4) Modify GenerateReport() to print to external file
            //5) Spot check
            //6) Submit

            Warehouse warehouse = new Warehouse();
            warehouse.Run();
            Console.ReadLine();
        }
    }
}
