namespace MartianRobots.Domain
{
    public class Scent
    {
        public int PosScentY { get; private set; }
        public int PosSecntX { get; private set; }
        public Orientation OrientationScent { get; private set; }

        public Scent(int posScentY, int posSecntX, Orientation orientationScent)
        {
            PosScentY = posScentY;
            PosSecntX = posSecntX;
            OrientationScent = orientationScent;
        }

        public override string ToString()
        {
            return "Scent: " + "X axis: " + PosSecntX + ", Y axis: " + PosScentY + ", Orietation: " + OrientationScent.ToString();
        }

    }
}
