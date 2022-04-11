using Newtonsoft.Json;
using System;
using System.Linq;


namespace MartianRobots.Domain
{
    public class Robot : IRobotMovements
    {
        [JsonProperty]
        public int XCoordinate { get; private set; }
        [JsonProperty]
        public int YCoordinate { get; private set; }
        [JsonProperty]
        public Orientation Orientation { get; private set; }
        [JsonProperty]
        public String RobotPath { get; private set; }
        public int ExploredSurface { get; private set; }
        public bool IsLost { get; private set; }

        public Robot(int xCoordinate, int yCoordinate, Orientation orientation, string robotPath)
        {
            XCoordinate = xCoordinate;
            YCoordinate = yCoordinate;
            Orientation = orientation;
            RobotPath = robotPath;
            ExploredSurface = 0;
            IsLost = false;
        }

        public String ExecutePath(Grid grid)
        {
            for (int i = 0; i < RobotPath.Length; i++)
            {
                switch (RobotPath[i])
                {
                    case 'F':
                        if (IsThereScent(grid))
                        {
                            continue;
                        }
                        else
                        {
                            //we clone the robot before the forward movement,
                            //in case the next move it gets out of the grid.
                            //Thus, we still can record it's correct atributes on the grid.
                            var recordRobot = (Robot)MemberwiseClone();
                            
                            MoveForward();
                            ExploredSurface += 1;

                            if (AmILost(grid))
                            {
                                recordRobot.IsLost = true;
                                grid.FinalRobotsState.Add(recordRobot);
                                grid.AddScent(recordRobot);
                                return recordRobot.SetResult();
                            }
                        }
                        break;
                    case 'R':
                        TurnRight();
                        break;
                    case 'L':
                        TurnLeft();
                        break;
                    default:
                        //Not supported command.
                        //Every command introduced that doesn't match a instruction (F,R,L) is ignored.
                        continue;
                }
            }

            grid.FinalRobotsState.Add(this);
            return SetResult();
        }

        public void MoveForward()
        {
            switch (Orientation)
            {
                case Orientation.N:
                    YCoordinate += 1;
                    break;
                case Orientation.E:
                    XCoordinate += 1;
                    break;
                case Orientation.S:
                    YCoordinate += -1;
                    break;
                default:
                    //Orientation.West
                    XCoordinate += -1;
                    break;
            }
        }

        public void TurnRight()
        {
            switch (Orientation)
            {
                case Orientation.N:
                    Orientation = Orientation.E;
                    break;
                case Orientation.E:
                    Orientation = Orientation.S;
                    break;
                case Orientation.S:
                    Orientation = Orientation.W;
                    break;
                default:
                    Orientation = Orientation.N;
                    break;
            }
        }

        public void TurnLeft()
        {
            switch (Orientation)
            {
                case Orientation.N:
                    Orientation = Orientation.W;
                    break;
                case Orientation.E:
                    Orientation = Orientation.N;
                    break;
                case Orientation.S:
                    Orientation = Orientation.E;
                    break;
                default:
                    Orientation = Orientation.S;
                    break;
            }
        }

        private bool AmILost(Grid grid)
        {
            return YCoordinate >= grid.MaxY || YCoordinate < 0 || XCoordinate >= grid.MaxX || XCoordinate < 0;
        }

        private bool IsThereScent(Grid grid)
        {
            return grid.ListScents.Any(item => item.PosScentY == YCoordinate && item.PosSecntX == XCoordinate && item.OrientationScent == Orientation);
        }

        private String SetResult()
        {
            return XCoordinate + " " + YCoordinate + " " + Orientation.ToString() + (IsLost ? " LOST" : "");
        }

        public override string ToString()
        {
            return "Robot: " + "Y axis: " + YCoordinate + ", X axis: " + XCoordinate + ", Orietation: " + Orientation.ToString() + ", Robot Path: " + RobotPath + ", State: " + (IsLost ? "LOST" : "NOT LOST") + ", Explored Surface: " + ExploredSurface + " grid squares";
        }
    }
}


