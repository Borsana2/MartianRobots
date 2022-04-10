using MartianRobots.Domain;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace MartianRobots
{
    class Program
    {
        private static string _pathRobot = @"./NewRobots.json";
        private static string _pathGrid = @"./NewGrid.json";
       
        static void Main(string[] args)
        {
            List<Robot> listRobots = new List<Robot>();
            Grid grid = new Grid();


            //Choosing data source 
            if (StartMenu().Equals("1"))
            {
                grid = IntroduceGrid();
                listRobots = IntroduceRobots();
            }
            else
            {
                listRobots = DeserializeJsonRobotFile();
                grid = DeserializeJsonGridFile();
            }

            Console.WriteLine("----------------------------");
            Console.WriteLine("MARTIAN ROBOTS");
            Console.WriteLine("----------------------------");
            Console.WriteLine(grid.ResolveGrid(listRobots));

            ShowScentReport(grid);
        }

        private static String GetRobotsFromJsonFile()
        {
            String robotsJson;
            using (var reader = new StreamReader(_pathRobot))
            {
                robotsJson = reader.ReadToEnd();
            }

            return robotsJson;
        }

        private static List<Robot> DeserializeJsonRobotFile()
        {
            var robotsFromJsonFile = GetRobotsFromJsonFile();
            return JsonConvert.DeserializeObject<List<Robot>>(robotsFromJsonFile);

        }

        private static String GetGridFromJsonFile()
        {
            String gridJson;
            using (var reader = new StreamReader(_pathGrid))
            {
                gridJson = reader.ReadToEnd();
            }

            return gridJson;
        }

        private static Grid DeserializeJsonGridFile()
        {
            var gridFromJsonFile = GetGridFromJsonFile();
            return JsonConvert.DeserializeObject<Grid>(gridFromJsonFile);

        }

        private static void ShowScentReport(Grid grid)
        {
            Console.WriteLine("Scents report: ");
            if (grid.ListScents == null)
            {
                Console.WriteLine("No Scents reported");
            }
            else
            {
                foreach (var Scent in grid.ListScents)
                {
                    Console.WriteLine(Scent.ToString());
                }
            }
        }

        private static String StartMenu()
        {
            Console.WriteLine("How do you want to introduce the Robots?");
            Console.WriteLine("1 - Manualy / 2 - From Json repository");

            return Console.ReadLine();
        }

        private static List<Robot> IntroduceRobots()
        {
            List<Robot> listRobots = new List<Robot>();
            //variable declaration
            String answerAnotherRobot;
            String secondLineInstructions = "";
            int xAxis;
            int yAxis;
            
            Orientation orientation = Orientation.N;


            Console.WriteLine("Is time to build your Robots");
            Console.WriteLine("---------------------------");
            Console.WriteLine("We will use the axis to place your robot on the grid");

            do
            { 
                yAxis = InsertAxis("Y");
                xAxis = InsertAxis("X");

                orientation = InsertOrientation();

                secondLineInstructions = InsertInstructions();

                var robotAux = new Robot(xAxis, yAxis, orientation, secondLineInstructions);
                listRobots.Add(robotAux);

                Console.WriteLine("Robot introduced successfully");

                //Another robot?

                Console.WriteLine("--------------------------------------------");
                Console.WriteLine("Do you want to isert another Robot?");
                Console.WriteLine("1 - yes / else - no");
                answerAnotherRobot = Console.ReadLine().Trim();
                
            } while (answerAnotherRobot.Equals("1"));

            return listRobots;
        }

        private static string InsertInstructions()
        {
            String instructions = "";
          
            do
            {
                Console.WriteLine("Insert instructions for movement:");
                instructions = Console.ReadLine().ToUpper().Trim();
                if (instructions.Length > 100)
                {
                    Console.WriteLine("The instructions String lenght must be under 100 characters");                   
                }

            } while (instructions.Length > 100);

            return instructions;
        }

        private static Orientation InsertOrientation()
        {
            String orientationInput = "";
            Orientation orientation = Orientation.E;
            do
            {
                Console.WriteLine("Please, insert the robot orientation: ");
                orientationInput = Console.ReadLine().Trim().ToUpper();

                if (IsCorrectOrientation(orientationInput))
                {
                    switch (orientationInput.ToUpper())
                    {
                        case "N":
                            orientation = Orientation.N;
                            break;
                        case "E":
                            orientation = Orientation.E;
                            break;
                        case "S":
                            orientation = Orientation.S;
                            break;
                        default:
                            //w
                            orientation = Orientation.W;
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("The orientation character must be 'N (north), E (east), S (south) or W (west)'");
                }

            } while (!IsCorrectOrientation(orientationInput));

            return orientation;
        }

        private static int InsertAxis(String axisType)
        {
            int axis;
            do
            {
                Console.WriteLine($"Please, insert the {axisType} axis: ");
                if (!int.TryParse(Console.ReadLine().Trim(), out axis))
                {
                    Console.WriteLine("Please, introduce a valid number");
                }
                
            } while (!CheckCoordinateLength(axis));
            return axis;

        }

        private static bool IsCorrectOrientation(string orientationInput)
        {
            return orientationInput.Equals("N") || orientationInput.Equals("E") || orientationInput.Equals("W") || orientationInput.Equals("S");
        }

        private static Grid IntroduceGrid()
        {
          


            Console.WriteLine("Welcome to Martian Robots!");
            Console.WriteLine("---------------------------");
            Console.WriteLine("Please, configure your grid");

            int yGridAxis = InsertAxis("Y");
            int xGridAxis = InsertAxis("X");
            

            Console.WriteLine("Your grid is ready. The size is: " + yGridAxis + " rows and " + xGridAxis + " columns.");

            var grid = new Grid(xGridAxis, yGridAxis);
            return grid;



        }

        private static bool CheckCoordinateLength(int positionAux)
        {
            if (positionAux > 50)
            {
                Console.WriteLine("Sorry, the number needs to be under 51");
                return false;
            }
            else
            {
                Console.WriteLine("Number entered correctly");
                return true;
            }




        }



    }

}

