using System;

namespace JPEG
{
    public static partial class MyCringeDCT
    {
        public static void Setup()
	    {
		    for (var i = 0; i < width; i++)
		    {
			    for (var j = 0; j < height; j++)
			    {
				    DCTCosCache[i, j] = (float)Math.Cos(i * (float)piCoef * (2f * j + 1f));
				    IDCTCosCache[i, j] = (float)Math.Cos((2f * i + 1f) * piCoef * j);
			    }
		    }
		    
		    // DCTCosCache[0, 0] = (float)Math.Cos(0 * (float)piCoef * (2f * 0 + 1f));
		    // DCTCosCache[1, 0] = (float)Math.Cos(1 * (float)piCoef * (2f * 0 + 1f));
		    // DCTCosCache[2, 0] = (float)Math.Cos(2 * (float)piCoef * (2f * 0 + 1f));
		    // DCTCosCache[3, 0] = (float)Math.Cos(3 * (float)piCoef * (2f * 0 + 1f));
		    // DCTCosCache[4, 0] = (float)Math.Cos(4 * (float)piCoef * (2f * 0 + 1f));
		    // DCTCosCache[5, 0] = (float)Math.Cos(5 * (float)piCoef * (2f * 0 + 1f));
		    // DCTCosCache[6, 0] = (float)Math.Cos(6 * (float)piCoef * (2f * 0 + 1f));
		    // DCTCosCache[7, 0] = (float)Math.Cos(7 * (float)piCoef * (2f * 0 + 1f));
		    //
		    // DCTCosCache[0, 1] = (float)Math.Cos(0 * (float)piCoef * (2f * 1 + 1f));
		    // DCTCosCache[1, 1] = (float)Math.Cos(1 * (float)piCoef * (2f * 1 + 1f));
		    // DCTCosCache[2, 1] = (float)Math.Cos(2 * (float)piCoef * (2f * 1 + 1f));
		    // DCTCosCache[3, 1] = (float)Math.Cos(3 * (float)piCoef * (2f * 1 + 1f));
		    // DCTCosCache[4, 1] = (float)Math.Cos(4 * (float)piCoef * (2f * 1 + 1f));
		    // DCTCosCache[5, 1] = (float)Math.Cos(5 * (float)piCoef * (2f * 1 + 1f));
		    // DCTCosCache[6, 1] = (float)Math.Cos(6 * (float)piCoef * (2f * 1 + 1f));
		    // DCTCosCache[7, 1] = (float)Math.Cos(7 * (float)piCoef * (2f * 1 + 1f));
		    //
		    // DCTCosCache[0, 2] = (float)Math.Cos(0 * (float)piCoef * (2f * 2 + 1f));
		    // DCTCosCache[1, 2] = (float)Math.Cos(1 * (float)piCoef * (2f * 2 + 1f));
		    // DCTCosCache[2, 2] = (float)Math.Cos(2 * (float)piCoef * (2f * 2 + 1f));
		    // DCTCosCache[3, 2] = (float)Math.Cos(3 * (float)piCoef * (2f * 2 + 1f));
		    // DCTCosCache[4, 2] = (float)Math.Cos(4 * (float)piCoef * (2f * 2 + 1f));
		    // DCTCosCache[5, 2] = (float)Math.Cos(5 * (float)piCoef * (2f * 2 + 1f));
		    // DCTCosCache[6, 2] = (float)Math.Cos(6 * (float)piCoef * (2f * 2 + 1f));
		    // DCTCosCache[7, 2] = (float)Math.Cos(7 * (float)piCoef * (2f * 2 + 1f));
		    //
		    // DCTCosCache[0, 3] = (float)Math.Cos(0 * (float)piCoef * (2f * 3 + 1f));
		    // DCTCosCache[1, 3] = (float)Math.Cos(1 * (float)piCoef * (2f * 3 + 1f));
		    // DCTCosCache[2, 3] = (float)Math.Cos(2 * (float)piCoef * (2f * 3 + 1f));
		    // DCTCosCache[3, 3] = (float)Math.Cos(3 * (float)piCoef * (2f * 3 + 1f));
		    // DCTCosCache[4, 3] = (float)Math.Cos(4 * (float)piCoef * (2f * 3 + 1f));
		    // DCTCosCache[5, 3] = (float)Math.Cos(5 * (float)piCoef * (2f * 3 + 1f));
		    // DCTCosCache[6, 3] = (float)Math.Cos(6 * (float)piCoef * (2f * 3 + 1f));
		    // DCTCosCache[7, 3] = (float)Math.Cos(7 * (float)piCoef * (2f * 3 + 1f));
		    //
		    // DCTCosCache[0, 4] = (float)Math.Cos(0 * (float)piCoef * (2f * 4 + 1f));
		    // DCTCosCache[1, 4] = (float)Math.Cos(1 * (float)piCoef * (2f * 4 + 1f));
		    // DCTCosCache[2, 4] = (float)Math.Cos(2 * (float)piCoef * (2f * 4 + 1f));
		    // DCTCosCache[3, 4] = (float)Math.Cos(3 * (float)piCoef * (2f * 4 + 1f));
		    // DCTCosCache[4, 4] = (float)Math.Cos(4 * (float)piCoef * (2f * 4 + 1f));
		    // DCTCosCache[5, 4] = (float)Math.Cos(5 * (float)piCoef * (2f * 4 + 1f));
		    // DCTCosCache[6, 4] = (float)Math.Cos(6 * (float)piCoef * (2f * 4 + 1f));
		    // DCTCosCache[7, 4] = (float)Math.Cos(7 * (float)piCoef * (2f * 4 + 1f));
		    //
		    // DCTCosCache[0, 5] = (float)Math.Cos(0 * (float)piCoef * (2f * 5 + 1f));
		    // DCTCosCache[1, 5] = (float)Math.Cos(1 * (float)piCoef * (2f * 5 + 1f));
		    // DCTCosCache[2, 5] = (float)Math.Cos(2 * (float)piCoef * (2f * 5 + 1f));
		    // DCTCosCache[3, 5] = (float)Math.Cos(3 * (float)piCoef * (2f * 5 + 1f));
		    // DCTCosCache[4, 5] = (float)Math.Cos(4 * (float)piCoef * (2f * 5 + 1f));
		    // DCTCosCache[5, 5] = (float)Math.Cos(5 * (float)piCoef * (2f * 5 + 1f));
		    // DCTCosCache[6, 5] = (float)Math.Cos(6 * (float)piCoef * (2f * 5 + 1f));
		    // DCTCosCache[7, 5] = (float)Math.Cos(7 * (float)piCoef * (2f * 5 + 1f));
		    //
		    // DCTCosCache[0, 6] = (float)Math.Cos(0 * (float)piCoef * (2f * 6 + 1f));
		    // DCTCosCache[1, 6] = (float)Math.Cos(1 * (float)piCoef * (2f * 6 + 1f));
		    // DCTCosCache[2, 6] = (float)Math.Cos(2 * (float)piCoef * (2f * 6 + 1f));
		    // DCTCosCache[3, 6] = (float)Math.Cos(3 * (float)piCoef * (2f * 6 + 1f));
		    // DCTCosCache[4, 6] = (float)Math.Cos(4 * (float)piCoef * (2f * 6 + 1f));
		    // DCTCosCache[5, 6] = (float)Math.Cos(5 * (float)piCoef * (2f * 6 + 1f));
		    // DCTCosCache[6, 6] = (float)Math.Cos(6 * (float)piCoef * (2f * 6 + 1f));
		    // DCTCosCache[7, 6] = (float)Math.Cos(7 * (float)piCoef * (2f * 6 + 1f));
		    //
		    // DCTCosCache[0, 7] = (float)Math.Cos(0 * (float)piCoef * (2f * 7 + 1f));
		    // DCTCosCache[1, 7] = (float)Math.Cos(1 * (float)piCoef * (2f * 7 + 1f));
		    // DCTCosCache[2, 7] = (float)Math.Cos(2 * (float)piCoef * (2f * 7 + 1f));
		    // DCTCosCache[3, 7] = (float)Math.Cos(3 * (float)piCoef * (2f * 7 + 1f));
		    // DCTCosCache[4, 7] = (float)Math.Cos(4 * (float)piCoef * (2f * 7 + 1f));
		    // DCTCosCache[5, 7] = (float)Math.Cos(5 * (float)piCoef * (2f * 7 + 1f));
		    // DCTCosCache[6, 7] = (float)Math.Cos(6 * (float)piCoef * (2f * 7 + 1f));
		    // DCTCosCache[7, 7] = (float)Math.Cos(7 * (float)piCoef * (2f * 7 + 1f));
		    
		    //====================================================================
		    // IDCTCosCache[0, 0] = (float)Math.Cos((2f * 0 + 1f) * piCoef * 0);
		    // IDCTCosCache[1, 0] = (float)Math.Cos((2f * 1 + 1f) * piCoef * 0);
		    // IDCTCosCache[2, 0] = (float)Math.Cos((2f * 2 + 1f) * piCoef * 0);
		    // IDCTCosCache[3, 0] = (float)Math.Cos((2f * 3 + 1f) * piCoef * 0);
		    // IDCTCosCache[4, 0] = (float)Math.Cos((2f * 4 + 1f) * piCoef * 0);
		    // IDCTCosCache[5, 0] = (float)Math.Cos((2f * 5 + 1f) * piCoef * 0);
		    // IDCTCosCache[6, 0] = (float)Math.Cos((2f * 6 + 1f) * piCoef * 0);
		    // IDCTCosCache[7, 0] = (float)Math.Cos((2f * 7 + 1f) * piCoef * 0);
		    //
		    // IDCTCosCache[0, 1] = (float)Math.Cos((2f * 0 + 1f) * (float)piCoef * 1);
		    // IDCTCosCache[1, 1] = (float)Math.Cos((2f * 1 + 1f) * (float)piCoef * 1);
		    // IDCTCosCache[2, 1] = (float)Math.Cos((2f * 2 + 1f) * (float)piCoef * 1);
		    // IDCTCosCache[3, 1] = (float)Math.Cos((2f * 3 + 1f) * (float)piCoef * 1);
		    // IDCTCosCache[4, 1] = (float)Math.Cos((2f * 4 + 1f) * (float)piCoef * 1);
		    // IDCTCosCache[5, 1] = (float)Math.Cos((2f * 5 + 1f) * (float)piCoef * 1);
		    // IDCTCosCache[6, 1] = (float)Math.Cos((2f * 6 + 1f) * (float)piCoef * 1);
		    // IDCTCosCache[7, 1] = (float)Math.Cos((2f * 7 + 1f) * (float)piCoef * 1);
		    //
		    // IDCTCosCache[0, 2] = (float)Math.Cos((2f * 0 + 1f) * (float)piCoef * 2);
		    // IDCTCosCache[1, 2] = (float)Math.Cos((2f * 1 + 1f) * (float)piCoef * 2);
		    // IDCTCosCache[2, 2] = (float)Math.Cos((2f * 2 + 1f) * (float)piCoef * 2);
		    // IDCTCosCache[3, 2] = (float)Math.Cos((2f * 3 + 1f) * (float)piCoef * 2);
		    // IDCTCosCache[4, 2] = (float)Math.Cos((2f * 4 + 1f) * (float)piCoef * 2);
		    // IDCTCosCache[5, 2] = (float)Math.Cos((2f * 5 + 1f) * (float)piCoef * 2);
		    // IDCTCosCache[6, 2] = (float)Math.Cos((2f * 6 + 1f) * (float)piCoef * 2);
		    // IDCTCosCache[7, 2] = (float)Math.Cos((2f * 7 + 1f) * (float)piCoef * 2);
		    //
		    // IDCTCosCache[0, 3] = (float)Math.Cos((2f * 0 + 1f) * (float)piCoef * 3);
		    // IDCTCosCache[1, 3] = (float)Math.Cos((2f * 1 + 1f) * (float)piCoef * 3);
		    // IDCTCosCache[2, 3] = (float)Math.Cos((2f * 2 + 1f) * (float)piCoef * 3);
		    // IDCTCosCache[3, 3] = (float)Math.Cos((2f * 3 + 1f) * (float)piCoef * 3);
		    // IDCTCosCache[4, 3] = (float)Math.Cos((2f * 4 + 1f) * (float)piCoef * 3);
		    // IDCTCosCache[5, 3] = (float)Math.Cos((2f * 5 + 1f) * (float)piCoef * 3);
		    // IDCTCosCache[6, 3] = (float)Math.Cos((2f * 6 + 1f) * (float)piCoef * 3);
		    // IDCTCosCache[7, 3] = (float)Math.Cos((2f * 7 + 1f) * (float)piCoef * 3);
		    //
		    // IDCTCosCache[0, 4] = (float)Math.Cos((2f * 0 + 1f) * (float)piCoef * 4);
		    // IDCTCosCache[1, 4] = (float)Math.Cos((2f * 1 + 1f) * (float)piCoef * 4);
		    // IDCTCosCache[2, 4] = (float)Math.Cos((2f * 2 + 1f) * (float)piCoef * 4);
		    // IDCTCosCache[3, 4] = (float)Math.Cos((2f * 3 + 1f) * (float)piCoef * 4);
		    // IDCTCosCache[4, 4] = (float)Math.Cos((2f * 4 + 1f) * (float)piCoef * 4);
		    // IDCTCosCache[5, 4] = (float)Math.Cos((2f * 5 + 1f) * (float)piCoef * 4);
		    // IDCTCosCache[6, 4] = (float)Math.Cos((2f * 6 + 1f) * (float)piCoef * 4);
		    // IDCTCosCache[7, 4] = (float)Math.Cos((2f * 7 + 1f) * (float)piCoef * 4);
		    //
		    // IDCTCosCache[0, 5] = (float)Math.Cos((2f * 0 + 1f) * (float)piCoef * 5);
		    // IDCTCosCache[1, 5] = (float)Math.Cos((2f * 1 + 1f) * (float)piCoef * 5);
		    // IDCTCosCache[2, 5] = (float)Math.Cos((2f * 2 + 1f) * (float)piCoef * 5);
		    // IDCTCosCache[3, 5] = (float)Math.Cos((2f * 3 + 1f) * (float)piCoef * 5);
		    // IDCTCosCache[4, 5] = (float)Math.Cos((2f * 4 + 1f) * (float)piCoef * 5);
		    // IDCTCosCache[5, 5] = (float)Math.Cos((2f * 5 + 1f) * (float)piCoef * 5);
		    // IDCTCosCache[6, 5] = (float)Math.Cos((2f * 6 + 1f) * (float)piCoef * 5);
		    // IDCTCosCache[7, 5] = (float)Math.Cos((2f * 7 + 1f) * (float)piCoef * 5);
		    //
		    // IDCTCosCache[0, 6] = (float)Math.Cos((2f * 0 + 1f) * (float)piCoef * 6);
		    // IDCTCosCache[1, 6] = (float)Math.Cos((2f * 1 + 1f) * (float)piCoef * 6);
		    // IDCTCosCache[2, 6] = (float)Math.Cos((2f * 2 + 1f) * (float)piCoef * 6);
		    // IDCTCosCache[3, 6] = (float)Math.Cos((2f * 3 + 1f) * (float)piCoef * 6);
		    // IDCTCosCache[4, 6] = (float)Math.Cos((2f * 4 + 1f) * (float)piCoef * 6);
		    // IDCTCosCache[5, 6] = (float)Math.Cos((2f * 5 + 1f) * (float)piCoef * 6);
		    // IDCTCosCache[6, 6] = (float)Math.Cos((2f * 6 + 1f) * (float)piCoef * 6);
		    // IDCTCosCache[7, 6] = (float)Math.Cos((2f * 7 + 1f) * (float)piCoef * 6);
		    //
		    // IDCTCosCache[0, 7] = (float)Math.Cos((2f * 0 + 1f) * (float)piCoef * 7);
		    // IDCTCosCache[1, 7] = (float)Math.Cos((2f * 1 + 1f) * (float)piCoef * 7);
		    // IDCTCosCache[2, 7] = (float)Math.Cos((2f * 2 + 1f) * (float)piCoef * 7);
		    // IDCTCosCache[3, 7] = (float)Math.Cos((2f * 3 + 1f) * (float)piCoef * 7);
		    // IDCTCosCache[4, 7] = (float)Math.Cos((2f * 4 + 1f) * (float)piCoef * 7);
		    // IDCTCosCache[5, 7] = (float)Math.Cos((2f * 5 + 1f) * (float)piCoef * 7);
		    // IDCTCosCache[6, 7] = (float)Math.Cos((2f * 6 + 1f) * (float)piCoef * 7);
		    // IDCTCosCache[7, 7] = (float)Math.Cos((2f * 7 + 1f) * (float)piCoef * 7);
	    }
    }
}