# CSCI 2210 – Data Structures
## Project 3 – Warehouse Simulation Activity

### Overview
In this assignment, you will demonstrate your understanding and proficiency in the data structures we have discussed in this class up to this point: arrays, lists, stacks, queues, and priority queues. 

### Procedure
1. **Prerequisites:**
   - Visual Studio 2022
   - Access to a Github account

### Part 1: The Situation
You have been contracted by a local company about to build a new warehouse processing center. The main task is to develop simulations of the warehouse to assist in determining its necessary capacity. The company aims to identify the optimum number of docks required to maximize total revenue.

Details of the simulation are as follows:

- Trucks arrive with random cargo crates loaded into their trailers.
- Each truck’s trailer contains a random number of crates.
- Each truck arrives at a random time across 48 discrete time increments.
- Trailers are loaded in a manner where the first crate placed into the trailer is the last one to leave.
- On arriving at the warehouse, trucks are directed to a gate entrance for further processing.
- Every time increment, a truck can be added to a loading dock.
- At a loading dock, a truck can have one crate unloaded per time increment. Once emptied, it's immediately replaced by the next truck in line for that dock.

> **Note:** Each dock costs approximately $100 to operate per time increment.

Post simulation, the system should generate a comprehensive report detailing:

- Number of docks open during the simulation
- Longest line at any loading dock during the simulation
- Total trucks processed during the simulation
- Total crates unloaded during the simulation
- Total value of the crates that were unloaded
- Average value of each crate and truck unloaded
- Total time docks were in use/not in use
- Total cost of operating each dock
- Overall revenue of the warehouse (total value of crates – total operating cost)

### Part 2: Creating Classes 
This project comprises four primary classes: `Crate.cs`, `Truck.cs`, `Dock.cs`, and `Warehouse.cs`. 

- **The Crate Class**
  - Represents a single shipment inside of a truck.
  - Properties: `Id`, `Price` (ranging from $50 to $500)

- **The Truck Class**
  - Represents a single truck at the loading dock.
  - Properties: `driver`, `deliveryCompany`, `Trailer` (a stack of crates)
  - Methods: `Load(Crate crate)`, `Unload()`

- **The Dock Class**
  - Represents an individual warehouse loading dock.
  - Properties include `Id`, `Line` (queue of trucks), `TotalSales`, `TotalCrates`, `TotalTrucks`, `TimeInUse`, `TimeNotInUse`
  - Methods: `JoinLine(Truck truck)`, `SendOff()`

- **The Warehouse Class**
  - Represents the entire warehouse facility and manages the warehouse simulation.
  - Properties: `Docks` (List of docks with a maximum limit of 15), `Entrance` (queue of trucks), and other necessary properties for the simulation.
  - Methods: `Run()` and other required methods to execute the simulation.

### Part 3: Running the Simulation
While executing the simulation, each crate, as it's unloaded from the truck, should be logged into a CSV file detailing:

- Time increment of unloading
- Truck driver’s name
- Delivery company’s name
- Crate’s identification number
- Crate’s value 
- Status (based on three possible scenarios)

### Part 4: Modifying the Simulation
Participants can choose one of two modifications:

1. Update the simulation to consider non-uniform truck arrivals throughout the day.
2. Incorporate a visual aid, animation, or GUI to display the simulation in progress.

### Part 5: Offer a Recommendation
After multiple simulations with varying dock quantities, provide a concise recommendation on the number of docks the company should construct for their new warehouse.

### Part 6: Submit
Ensure all project files are hosted on a public GitHub repository. Include:

- Your recommendation summary
- All project-related files (`.sln`, `.csproj`, `.cs`, etc.)

Finally, submit the GitHub repository link to the dropbox on D2L.
