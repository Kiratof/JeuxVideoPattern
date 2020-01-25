using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameCodeur
{
    class Util
    {


        static Random RandomGen = new Random();
        
        public static void SetRandomSeed(int pSeed)
        {
            RandomGen = new Random(pSeed);
        }

        public static int GetInt(int pMin, int pMax)
        {
            return RandomGen.Next(pMin, pMax);
        }
        

        //Colision
        public static bool CollideByBox(IActor p1, IActor p2)
        {
            return p1.BoundingBox.Intersects(p2.BoundingBox);

        }

    }
}
