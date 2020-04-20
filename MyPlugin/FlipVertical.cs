using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PluginInterface;

namespace MyPlugin
{
    [Version(1, 0, 0, 2)]
    public class FlipVertical : IPlugin
    {
        string IPlugin.Name => "Flip Vertical";
        string IPlugin.Author => "Vyacheslav Lanin";

        void IPlugin.Transform(Bitmap bitmap)
        {
            for (int i = 0; i < bitmap.Width; ++i)
                for (int j = 0; j < bitmap.Height / 2; ++j)
                {
                    Color color = bitmap.GetPixel(i, j);
                    bitmap.SetPixel(i, j, bitmap.GetPixel(i, bitmap.Height - j - 1));
                    bitmap.SetPixel(i, bitmap.Height - j - 1, color);
                }
        }
    }
}
