using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PluginInterface
{
    public interface IPlugin
    {
        string Name { get; }
        string Author { get; }
        void Transform(Bitmap bitmap);
    }

    public class VersionAttribute : Attribute
    {
        public int Major { get; private set; }

        // Ну надо же было хоть что то изменить в этой жизни
        public int MinorFirst { get; private set; }
        public int MinorSecond { get; private set; }
        public int MinorThird { get; private set; }

        public VersionAttribute(int major, int minorFirst, int minorSecond, int minorThird)
        {
            Major = major;
            MinorFirst = minorFirst;
            MinorSecond = minorSecond;
            MinorThird = minorThird;
        }
    }

}

