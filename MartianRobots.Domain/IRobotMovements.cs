using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MartianRobots.Domain
{
    public interface IRobotMovements
    {
        public void MoveForward();
        public void TurnRight();
        public void TurnLeft();

        

    }
}
