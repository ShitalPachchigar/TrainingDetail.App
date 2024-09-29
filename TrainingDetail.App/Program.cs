using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TrainingDetail.App.Classs;

namespace TrainingDetail.App
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                //Relative File Paths to ensure Compatability on ant server
                string AppDirectory = Directory.GetCurrentDirectory();
                string inputFile = Path.Combine(AppDirectory, @"..\..\..\trainings.txt");

                //Load And Parse Json Data
                var jsonData = File.ReadAllText(inputFile);
                // var people = JsonConvert.DeserializeObject<List<People>>(jsonData);
                var people = JsonConvert.DeserializeObject<List<People>>(jsonData, new JsonSerializerSettings
                {
                    DateFormatString = "M/D/yyyy", // Specify the date format
                    NullValueHandling = NullValueHandling.Ignore
                });


                // -------------Task 1------------------------


                // List of completed training with a count of people who completed that training.
                var trainingCount = people
                        .SelectMany(p => p.completions
                            .GroupBy(c => c.name)
                            .Select(g => g.OrderByDescending(c => c.timestamp).First()))
                        .GroupBy(c => c.name)
                        .Select(g => new Task1Output { trainingName = g.Key, Count = g.Count() })
                        .ToList();
                //write output to json file
                string outputTask1Path = Path.Combine(AppDirectory, @"..\..\..\task1_output.json");
                File.WriteAllText(outputTask1Path, JsonConvert.SerializeObject(trainingCount, Formatting.Indented));


                //---------------Task2------------------------------


                //List of all people that completed specified trainings in the given fiscal year
                string[] specifiedTrainings = { "Electrical Safety for Labs", "X-Ray Safety", "Laboratory Safety Training" };
                int fiscalYear = 2024;
                DateTime fiscalYearStartDate = new DateTime(fiscalYear - 1, 7, 1);
                DateTime fiscalYearEndDate = new DateTime(fiscalYear, 6, 30);

                var trainingsInFiscalYear = people
                    .SelectMany(p => p.completions
                        .GroupBy(c => c.name)
                        .Select(g => g.OrderByDescending(c => c.timestamp).First()), (p, c) => new { p.name, c })
                    .Where(pc => specifiedTrainings.Contains(pc.c.name)
                                 && pc.c.timestamp >= fiscalYearStartDate
                                 && pc.c.timestamp <= fiscalYearEndDate)
                    .GroupBy(pc => pc.c.name).Select(g => new Task2Output { trainingName = g.Key, People = g.Select(pc => pc.name).Distinct().ToList() })
                    .ToList();

                //Write output to a JSON file
                string outputTask2Path = Path.Combine(AppDirectory, @"..\..\..\task2_output.json");
                File.WriteAllText(outputTask2Path, JsonConvert.SerializeObject(trainingsInFiscalYear, Formatting.Indented));


                //---------------Task3------------------------------
                // List of people with trainings that have expired or will expire soon based on the given date
                DateTime givenDate = new DateTime(2023, 10, 1);
                var expiredOrSoon = people
                    .Select(p => new Task3Output
                    {
                        name = p.name,
                        expiredOrSoon = p.completions
                            .GroupBy(c => c.name)
                            .Select(g => g.OrderByDescending(c => c.timestamp).First())
                            .Where(c => c.expires != null)
                            .Select(c => new ExpiringTraining
                            {
                                trainingName = c.name,
                                expiresOn = c.expires.Value.ToString(@"MM/dd/yyyy"),
                                status = c.expires < givenDate ? "Expired" :
                                         (c.expires <= givenDate.AddMonths(1) ? "Expires Soon" : null)
                            })
                            .Where(c => c.status != null)
                            .ToList()
                    })
                    .Where(p => p.expiredOrSoon.Any())
                    .ToList();

                // Write output to a JSON file
                string outputTask3Path = Path.Combine(AppDirectory, @"..\..\..\task3_output.json");
                File.WriteAllText(outputTask3Path, JsonConvert.SerializeObject(expiredOrSoon, Formatting.Indented));

            }
            catch (Exception ex)
            {
                Console.WriteLine("Something went wrong .." + ex.Message);
          
            }
        }
    }
}
