using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PluginInterface;

namespace MyPlugin
{
    [Version(2, 0, 0, 0)]
    public class AddGeoData : IImageDataPlugin
    {
        string IImageDataPlugin.Name => "Add Geo Data";
        string IImageDataPlugin.Author => "Daniel Varentsov";

        void IImageDataPlugin.Transform(Bitmap bitmap, ImageData data)
        {
            
        }
    }
}
