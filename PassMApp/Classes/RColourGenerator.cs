using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace PassMApp
{
    public class RColourGenerator
    {
        public Brush RandomBrush { get { return generateRandomColour(); } set { } }
        private static Random randomColour = new Random();
        private static uint[] uintColours =
        {
                0xFF34AADC,0xFFFF2D55,0xFF007AFF,0xFFFF9500,0xFF4CD964,
                0xFFFFCC00,0xFF5856D6,0xFFFF3B30,0xFFFF4981,0xFFFF3A2D
             };

        public RColourGenerator()
        {
            RandomBrush = generateRandomColour();
        }

        

        private static Color ConvertColour(uint uintCol)
        {
            byte A = (byte)((uintCol & 0xFF000000) >> 24);
            byte R = (byte)((uintCol & 0x00FF0000) >> 16);
            byte G = (byte)((uintCol & 0x0000FF00) >> 8);
            byte B = (byte)((uintCol & 0x000000FF) >> 0);
            return Color.FromArgb(A, R, G, B); ;
        }

        public static Brush generateRandomColour()
        {
            return new SolidColorBrush(ConvertColour(uintColours[randomColour.Next(0, 9)]));
        }
    }
}
