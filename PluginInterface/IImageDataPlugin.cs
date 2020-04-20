using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PluginInterface
{
    public interface IImageDataPlugin
    {
        string Name { get; }
        string Author { get; }
        void Transform(Bitmap bitmap, ImageData data);
    }

    public class ImageData
    {
        public string Latitude { get; private set; }
        public string Longitude { get; private set; }
        public string Altitude { get; private set; }

        public ImageData(string lat, string lng, string alt)
        {
            Latitude = lat;
            Longitude = lng;
            Altitude = alt;
        }
    }
}
