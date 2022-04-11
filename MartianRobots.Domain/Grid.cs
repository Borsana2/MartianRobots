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
        [JsonProperty]
        public List<Scent> ListScents { get; private set; }
        [JsonProperty]
        public List<Robot> ListRobots { get; private set; }

        public Grid()
        {
            
        }

        public Grid(int maxX, int maxY)
        {
            //we add an extra number so it can match de row/column 0
            MaxX = (maxX + 1);
            MaxY = (maxY + 1);

            ListScents = new List<Scent>();
            ListRobots = new List<Robot>();
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

     

    }

}


