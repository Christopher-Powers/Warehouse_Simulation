# Warehouse Simulation Project

## Overview

The Warehouse Simulation project is a C# console application that simulates the operation of a warehouse with multiple loading docks. It models the arrival and processing of trucks carrying crates of goods over a period of 48 discrete time increments.

The goal of the simulation is to help a company determine the optimal number of loading docks for their new warehouse to maximize revenue, considering the costs of dock operations.

## Features

- Simulates truck arrivals with random cargo crates.
- Processes loading and unloading of crates at multiple docks.
- Tracks dock usage and generates a detailed performance report.
- Outputs a CSV log of crate unloading events.
- Offers a configurable number of docks for multiple simulation scenarios.

## Installation

1. Ensure you have Visual Studio 2022 installed.
2. Clone the repository to your local machine using Git:

    ```
    git clone [https://github.com/Christopher-Powers/Warehouse_Simulation]
    ```

3. Open the `.sln` file with Visual Studio 2022.

## Running the Simulation

To run the simulation:

1. Build the solution in Visual Studio.
2. Run the application. The console will display the simulation logs in real-time.
3. Once the simulation is complete, a summary report will be displayed in the console and written to a CSV file.

## Simulation Report

Upon completion, the simulation generates a report containing:

- Number of docks open during the simulation.
- Longest line at any loading dock during the simulation.
- Total number of trucks processed.
- Total number of crates unloaded.
- Total and average value of crates unloaded.
- Dock utilization statistics.
- Total revenue and operating costs.

## Modifying the Simulation

You can modify the simulation by:

- Adjusting the number of docks in the `Warehouse` constructor.
- Changing the crate and truck generation logic to simulate different traffic patterns.
- Implementing an optional graphical interface to visualize the simulation.

## Recommendation Summary

After running the simulation with different configurations, a summary of recommendations will be provided based on the performance outcomes to guide the company on the number of docks to implement in their new warehouse.

## Contributing

Contributions to the project are welcome. Please follow the standard fork, branch, and pull request workflow.

## License

This project is open-sourced under the MIT license. See the [LICENSE](LICENSE) file for details.

## Contact

For questions or support, please contact [powersct@etsu.edu].


Test_Jason
