namespace MartianRobots.Domain
{
    public interface IRobotMovements
    {
        public void MoveForward();
        public void TurnRight();
        public void TurnLeft();

       /* Used to provide more functionality to the robot in the future.
        Notice that, in this option, the functionality will be added to 
        the robot "vertically" and no "horizontally".*/
    }
}
