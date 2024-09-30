Training Details Console Application

Overview
This console application readsdata from a JSON file (`trainings.txt`) and generates output JSON files with following Task.

Task 1: Counts of how many people completed each training.
Task 2: List of people who completed specified trainings in a given fiscal year.
Task 3: List of people with expired or soon-to-expire trainings, with expiration status.

Prerequisites
Visual Studio 2019/2022 or higher.
.NET Core SDK installed.
The Newtonsoft.Json NuGet package (already included in the project via NuGet).

 Steps to Run the Application
1. Clone the Repository
Clone or download the repository to your local machine.

2. Open the Project in Visual Studio
Open Visual Studio.
Select File -> Open -> Project/Solution.
Navigate to the project folder and select the .sln (Solution) file.

3. Build the Project
In Visual Studio, right-click the solution name in the Solution Explorer and select Build or press Ctrl+Shift+B.

4. Run the Project
Press F5 to run the application or click the Start button in Visual Studio.

The app will read the trainings.txt file and produce three output files in the root project folder:
task1_output.json
task_output.json
task3_output.json

6. Check Output Files


