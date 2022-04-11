using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace MartianRobots.Domain
{
    public class Grid
    {
        [JsonProperty]
        public int MaxY { get; private set; }
        [JsonProperty]
        public int MaxX { get; private set; }
        public List<Scent> ListScents { get; private set; } 
        public List<Robot> FinalRobotsState { get; private set; }

        public Grid()
        {
            ListScents = new List<Scent>();
            FinalRobotsState = new List<Robot>();
        }

        public Grid(int maxX, int maxY)
        {
            //we add an extra number so it can match the row/column '0'
            MaxX = (maxX + 1);
            MaxY = (maxY + 1);

            ListScents = new List<Scent>();
            FinalRobotsState = new List<Robot>();
        }

        public String ResolveGrid(List<Robot> listRobots)
        {
            String gridResult = "";

            foreach (var robot in listRobots)
            {
                gridResult += robot.ExecutePath(this) + "\n" ;

            }
            return gridResult;
        }

        public void AddScent(Robot robot)
        {
            Scent recordScent = new Scent(robot.YCoordinate, robot.XCoordinate, robot.Orientation);
            ListScents.Add(recordScent);
        }

        public override string ToString()
        {
            return "Superior Y axis: " + (MaxY-1) + ", Superior X axis: " + (MaxX-1) + ", Scents: " + ListScents.Count;
        }

    }

}


