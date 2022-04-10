using MartianRobots.Domain;
using NUnit.Framework;
using System.Collections.Generic;

namespace MartianRobots.testing
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            List<Robot> listRobots = new List<Robot>();
            Robot a = new Robot(1, 1, Orientation.E, "RFRFRFRF");
            listRobots.Add(a);
            Robot b = new Robot(3, 2, Orientation.N, "FRRFLLFFRRFLL");
            listRobots.Add(b);
            Robot c = new Robot(0, 3, Orientation.W, "LLFFFRFLFL");
            listRobots.Add(c);

            var grid = new Grid(5, 3);
            var result = grid.ResolveGrid(listRobots);
            var expected = "1 1 E\n3 3 N LOST\n4 2 N\n";
            Assert.True(result == expected);
        }
        [Test]
        public void Test2()
        {
            List<Robot> listRobots = new List<Robot>();
            Robot a = new Robot(1, 1, Orientation.E, "RFRFRFRF");
            listRobots.Add(a);
            Robot b = new Robot(3, 2, Orientation.N, "FRRFLLFFRRFLL");
            listRobots.Add(b);
            Robot c = new Robot(0, 3, Orientation.W, "LLFFFRFLFL");
            listRobots.Add(c);

            var grid = new Grid(5, 3);
            var result = grid.ResolveGrid(listRobots);
            var expected = "1 1 E\n3 3 N LOST\n4 2 N\n";
            Assert.True(result == expected);
        }
    }
}