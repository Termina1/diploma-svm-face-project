using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace svmFace
{
    public static class Extensions
    {
        public static Vector toGrayScaleVector(this Bitmap pic) {
            Vector vector = new Vector();
            pic.eachPixel((pixel) => {
                vector.Add(pixel.R * 0.3 + pixel.G * 0.59 + pixel.B * 0.11);
                return 0;
            });
            return vector;
        }

        public static void eachPixel(this Bitmap pic, Func<Color, int> callback) {
            for (int i = 0; i < pic.Height; i++) {
                for (int j = 0; j < pic.Width; j++) {
                    callback(pic.GetPixel(j, i));
                }
            }
        }

        public static Bitmap resize(this Bitmap map, int width, int height)
        {
            var remap = new Bitmap(width, height);
            var g = Graphics.FromImage(remap as Image);
            g.DrawImage(map, 0, 0, width, height);
            g.Dispose();
            map.Dispose();
            return remap;
        }


        public static string toTrain(this List<double> ls, int label) {
            var line = new StringBuilder();
            line.Append(label);
            int i = 1;
            ls.ForEach((dig) =>
            {
                line.Append(" " + i + ":" + dig);
                i++;
            });
            return line.ToString();
        }
    }
}
