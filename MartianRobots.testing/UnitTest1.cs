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
            Robot a = new Robot(0, 1, Orientation.E, "FFFFFF");
            listRobots.Add(a);
            Robot b = new Robot(3, 2, Orientation.N, "FRFFRFFLF");
            listRobots.Add(b);
            Robot c = new Robot(0, 0, Orientation.S, "RRRRF");
            listRobots.Add(c);

            var grid = new Grid(5, 3);
            var result = grid.ResolveGrid(listRobots);
            var expected = "5 1 E LOST\n5 1 E\n0 0 S LOST\n";
            Assert.True(result == expected);
        }

        [Test]
        public void Test3()
        {
            List<Robot> listRobots = new List<Robot>();
            Robot a = new Robot(4, 4, Orientation.W, "FFLFFLFFLFFL");
            listRobots.Add(a);
            Robot b = new Robot(2, 0, Orientation.N, "FRFFFF");
            listRobots.Add(b);
            Robot c = new Robot(1, 2, Orientation.S, "FFLFFFLFRFF");
            listRobots.Add(c);

            var grid = new Grid(5, 4);
            var result = grid.ResolveGrid(listRobots);
            var expected = "4 4 W\n5 1 E LOST\n5 1 E\n";
            Assert.True(result == expected);
        }

        [Test]
        public void Test4()
        {
            List<Robot> listRobots = new List<Robot>();
            Robot a = new Robot(1, 3, Orientation.W, "RFFFRL");
            listRobots.Add(a);
            Robot b = new Robot(3, 1, Orientation.N, "FLLFRRLFFR");
            listRobots.Add(b);
            Robot c = new Robot(2, 2, Orientation.S, "FLRHHHHJRFL5");
            listRobots.Add(c);

            var grid = new Grid(6, 3);
            var result = grid.ResolveGrid(listRobots);
            var expected = "1 3 N LOST\n1 1 N\n1 1 S\n";
            Assert.True(result == expected);
        }
    }
}