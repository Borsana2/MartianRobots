using MartianRobots.Domain;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MartianRobots
{
    class Program
    {
        private static readonly string _pathRobot = @"./Samples/NewRobots.json";
        private static readonly string _pathGrid = @"./Samples/NewGrid.json";
        static void Main(string[] args)
        {
            List<Robot> listRobots = new List<Robot>();
            Grid grid = new Grid();

            Console.WriteLine("----------------------------");
            Console.WriteLine("-------MARTIAN ROBOTS-------");
            Console.WriteLine("----------------------------");
            //Choosing data source.
            if (StartMenu().Equals("1"))
            {
                grid = IntroduceGrid();
                listRobots = IntroduceRobots(grid);
            }
            else
            {
                listRobots = DeserializeJsonRobotFile();
                grid = DeserializeJsonGridFile();
            }

            //Resolving the grid
            Console.WriteLine("----------------------------");
            Console.WriteLine("----------RESULTS-----------");
            Console.WriteLine("----------------------------");
            Console.WriteLine(grid.ResolveGrid(listRobots));

            //Show additional data.
            ExtraInformationReport(grid);
        }

        private static String StartMenu()
        {
            Console.WriteLine("How do you want to introduce the Robots?");
            Console.WriteLine("1 - Manualy / Else - From Json repository");

            return Console.ReadLine();
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

        private static List<Robot> IntroduceRobots(Grid grid)
        {
            List<Robot> listRobots = new List<Robot>();
            int xAxis;
            int yAxis;

            Console.WriteLine("\nIs time to build your Robots");
            Console.WriteLine("---------------------------");
            Console.WriteLine("We will use the axis to place your robot on the grid");

            do
            {
                do
                {
                    xAxis = InsertAxis("X");
                    yAxis = InsertAxis("Y");
                } while (IsOutofTheGrid(yAxis, xAxis, grid));

                Orientation orientation = InsertOrientation();
                string secondLineInstructions = InsertRobotInstructions();

                var robotAux = new Robot(xAxis, yAxis, orientation, secondLineInstructions);
                listRobots.Add(robotAux);

                Console.WriteLine("Robot introduced successfully");

                Console.WriteLine("--------------------------------------------");
                Console.WriteLine("Do you want to isert another Robot?");
                Console.WriteLine("1 - yes / else - no");

            } while (Console.ReadLine().Trim().Equals("1"));

            return listRobots;
        }

        private static bool IsOutofTheGrid(int yAxis, int xAxis, Grid grid)
        {
            if (yAxis > (grid.MaxY-1) || xAxis > (grid.MaxX-1) || yAxis < 0 || xAxis < 0)
            {
                Console.WriteLine("You placed your robot out of the grid");
                return true;
            }
            return false;
        }

        private static string InsertRobotInstructions()
        {
            string instructions;
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

        private static bool IsCorrectOrientation(string orientationInput)
        {
            return orientationInput.Equals("N") || orientationInput.Equals("E") || orientationInput.Equals("W") || orientationInput.Equals("S");
        }

        private static int InsertAxis(String axisType)
        {
            int axis;
            bool isInt;
            do
            {
                Console.WriteLine($"Please, insert the {axisType} axis: ");
                isInt = int.TryParse(Console.ReadLine().Trim(), out axis);
                if (!isInt)
                {
                    Console.WriteLine("Please, introduce a valid number");
                }
            } while (!CheckAxisLength(axis) || !isInt);

            return axis;
        }

        private static bool CheckAxisLength(int positionAux)
        {
            if (positionAux > 50 || positionAux < 0) 
            {
                Console.WriteLine("We need a number between 0 and 50, inclusive");
                return false;
            }
            return true;
        }

        private static Grid IntroduceGrid()
        {
            int yGridAxis;
            int xGridAxis;
            Console.WriteLine("Welcome to Martian Robots!");
            Console.WriteLine("---------------------------");
            Console.WriteLine("Please, configure your grid");

            do
            {
                xGridAxis = InsertAxis("X");
                yGridAxis = InsertAxis("Y");
            } while (!IsARectangle(yGridAxis, xGridAxis));


            Console.WriteLine("Your grid is ready. The size is: " + (yGridAxis + 1) + " rows and " + (xGridAxis + 1) + " columns.");

            var grid = new Grid(xGridAxis, yGridAxis);
            return grid;
        }

        private static bool IsARectangle(int yGridAxis, int xGridAxis)
        {
            if (yGridAxis == xGridAxis)
            {
                Console.WriteLine("Sorry, the grid needs to be shaped like a rectangle");
                return false;
            }
            return true;
        }

        private static void ExtraInformationReport(Grid grid)
        {
            int totalExploredSurface = 0;

            Console.WriteLine("Information report: ");
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

            Console.WriteLine("There is " + grid.ListScents.Count + " Robots lost");

            foreach (var finalRobot in grid.ListRobots)
            {
                totalExploredSurface += finalRobot.ExploredSurface;  
            }

            Console.WriteLine("The avarage explored surface per robot is: " + 
                             (totalExploredSurface / grid.ListRobots.Count()) + " grid squares");
           
        }

    }

}

