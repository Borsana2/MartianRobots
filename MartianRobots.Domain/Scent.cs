using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MartianRobots.Domain
{
    public class Scent
    {

        
        public int IdScent { get; private set; }
        public int PosScentY { get; private set; }
        public int PosSecntX { get; private set; }
        public Orientation OrientationScent { get; private set; }

        public Scent()
        {

        }
        public Scent(int posScentY, int posSecntX, Orientation orientationScent)
        {
            PosScentY = posScentY;
            PosSecntX = posSecntX;
            OrientationScent = orientationScent;
        }

        public override string ToString()
        {
            return "Scent: " + "Y axis: " + PosScentY + ", X axis: " + PosSecntX + ", Orietation: " + OrientationScent.ToString();
        }

        public void InsertScentToDb(Scent scent)
        {
            using (var ctx = new MartianRobotsContext())
            {
                var scentToDB = new Scent()
                {
                    IdScent = scent.IdScent,
                    PosScentY = scent.PosScentY,
                    PosSecntX = scent.PosSecntX,
                    OrientationScent = scent.OrientationScent
                    
                };

                ctx.Scents.Add(scentToDB);
                ctx.SaveChanges();
            }
        }

    }
}
