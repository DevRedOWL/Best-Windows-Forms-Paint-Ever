using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PluginInterface;

namespace MyPlugin
{
    [Version(2, 0, 0, 0)]
    public class AddGeoData : IPlugin
    {
        string IPlugin.Name => "Add Geo Data";
        string IPlugin.Author => "Daniel Varentsov";

        void IPlugin.Transform(Bitmap bitmap)
        {
            // Казалось бы простенький объект, но сколько в нем истории (смотреть ниже)
            LocationData data = new LocationData(bitmap.PropertyItems);

            // Графический контекст
            Graphics graphics = Graphics.FromImage(bitmap);

            // Выбор дизайна шрифта
            FontDialog fontDialog = new FontDialog() { ShowHelp = false, ShowColor = true };
            fontDialog.Color = System.Drawing.Color.Red;
            fontDialog.Font = new Font("Consolas", 14f, FontStyle.Bold);
            fontDialog.ShowDialog();

            // Выбор формата представления координат
            string getGpsFormatRequest = "Представить координаты в формате DD°MM′SS'?\n(Нет - Представление в десятичном формате)";
            DialogResult gpsFormat = data
                ? MessageBox.Show(getGpsFormatRequest, "Формат геолокации", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                : DialogResult.Ignore;

            // Форматирование строки геоданных
            string geoDataString = gpsFormat == DialogResult.Ignore ? "" : gpsFormat == DialogResult.Yes ? data : data.ToDecimalString(true);

            // А это было просто
            string timeDataString = DateTime.Now.ToLocalTime().ToString();

            // Отрисовка
            SizeF textSize = graphics.MeasureString(timeDataString + (geoDataString == "" ? "" : '\n' + geoDataString), fontDialog.Font);
            RectangleF textRectangle = new RectangleF(bitmap.Width - textSize.Width, bitmap.Height - textSize.Height, textSize.Width, textSize.Height);
            graphics.DrawString(timeDataString + (geoDataString == "" ? "" : '\n' + geoDataString), fontDialog.Font, new SolidBrush(fontDialog.Color),
                textRectangle, new StringFormat(StringFormatFlags.NoClip) { Alignment = StringAlignment.Far });
            // У этой штуки растеризатор не очень то и умный, а еще некоторые вин шрифты кривые, так что на мелких размерах отступы некрасивые
        }

        /* 
         * Интересные и полезные ссылки, которые я хочу сохранить на будущее:
         * https://docs.microsoft.com/en-us/dotnet/api/system.drawing.imaging.propertyitem.id
         * https://docs.microsoft.com/en-us/windows/win32/gdiplus/-gdiplus-constant-property-item-descriptions?redirectedfrom=MSDN
         * https://medium.com/@dannyc/get-image-file-metadata-in-c-using-net-88603e6da63f
         * https://stackoverflow.com/questions/4983766/getting-gps-data-from-an-images-exif-in-c-sharp

         * Было весело это исследовать, благодарю за интересный кейс.
         * Для начало необходимо было добыть объект из которого можно получить вообще геоданные.
         * У image я нашел GetPropertyItem который позволяет получить метаданные изображения, здорово, но штука весьма низкоуровневая
         * В итоге удалось найти список значений (Ну и разобраться как устроены метаданные в GDI+), 
         * которые нужно передавать в этот метод и описание возвращаемых значений
         * Работать с байт-массивом было, конечно, неприятно, но норм
         * Остается только вывести эти данные в нормальном виде, и понять, почему все норм, а секунды нет.
         * Пару часов спустя, я все таки понял, что все эти значения не просто intы, а части unsigned long,
         * а секунды вообще умножены на 100 для сохранения точности по GPS.
         * Ну с LatRef и LongRef Оказалось проще, это всего лишь charcode символа строны света.
         * Реализовал пересчет координат в десятичную систему, красота.
         * А теперь как передать то эту всю красоту в плагин, как вариант, можно сделать новый интерфейс
         * и для того чтобы у меня поддерживались плагины одногруппников реализовать поддержку разных интерфейсов.
         * Но более эффективным способом будет хранить геоданные в битмапе, но т.к.он инкапсулирует имедж из которого создается
         * необходимо ему навязать метаданные снова, ну это просто циклом и setPropertyItem можно сделать.
         * Поэтому для корректной работы плагинов, использующих метаданные пришлось внести изменения в логику загрузки изображения.      
         */
    }

    // Класс для доставания данных о геолокации
    public class LocationData
    {
        public struct LatLngFieldFormat
        {
            // Градусы
            public readonly double Deg;
            // Минуты
            public readonly double Min;
            // Секунды
            public readonly double Sec;

            // Конструктор
            public LatLngFieldFormat(byte[] dataArray)
            {
                Deg = BitConverter.ToUInt32(dataArray, 0);
                Min = BitConverter.ToUInt32(dataArray, 8);
                Sec = (double)BitConverter.ToUInt32(dataArray, 16) / 100;
            }

            // Преобразование в строку
            public static explicit operator string(LatLngFieldFormat l) => $"{l.Deg + "°"}{l.Min + "'"}{(l.Sec != 0 ? l.Sec + "\"" : "")}";

            // Преобразование в десятичные координаты (deg/sec/min to decimal)
            public static explicit operator double(LatLngFieldFormat l) => l.Deg + (l.Min / 60) + (l.Sec / 3600);

            // Преобразование в проверку на существование координат
            public static explicit operator bool(LatLngFieldFormat l) => (l.Deg != 0 || l.Min != 0 || l.Sec != 0);
        }

        // Широта
        public LatLngFieldFormat Latitude { get; private set; }
        public readonly char LatitudeRef;

        // Долгота
        public LatLngFieldFormat Longitude { get; private set; }
        public readonly char LongitudeRef;

        // Высота
        public double Altitude { get; private set; }

        // Конструктор
        public LocationData(System.Drawing.Imaging.PropertyItem[] propertyItems)
        {
            foreach (var item in propertyItems)
            {
                switch (item.Id)
                {
                    case 0x0001: LatitudeRef = (char)item.Value[0]; break;
                    case 0x0002: Latitude = new LatLngFieldFormat(item.Value); break;
                    case 0x0003: LongitudeRef = (char)item.Value[0]; break;
                    case 0x0004: Longitude = new LatLngFieldFormat(item.Value); break;
                    case 0x0005: /** Здесь дожна быть Altitude reference но все ее ставят как уровень моря **/break;
                    case 0x0006: Altitude = (double)BitConverter.ToUInt32(item.Value, 0) / 100; break;
                    default: break;
                }
            }
        }

        // Преобразование в строку
        public override string ToString()
        {
            if ((bool)Latitude || (bool)Longitude || Altitude != 0)
                return (string)Latitude + LatitudeRef + ' ' + (string)Longitude + LongitudeRef + ' ' + Altitude;
            else
                return "";
        }
        public static implicit operator string(LocationData l) => l.ToString();

        // Преобразование в десятичную строку
        /// <summary>Получение десятичного представление координат, содержащихся в данном объекте</summary>
        /// <param name="reference">Требуется ли указывать широту/долготу</param>
        /// <returns>Десятичное дробное значение координат GPS, если они определены</returns>
        public string ToDecimalString(bool reference = false)
        {
            if ((bool)Latitude || (bool)Longitude || Altitude != 0)
                return
                (reference ? $"{LatitudeRef}" : "") + $"{(double)Latitude:F6}".Replace(',', '.') + ", " +
                (reference ? $"{LongitudeRef}" : "") + $"{(double)Longitude:F6}".Replace(',', '.');
            else
                return "";
        }

        // Преобразование в bool для проверки
        public static implicit operator bool(LocationData l) => (bool)l.Latitude || (bool)l.Longitude || l.Altitude != 0;
    }
}
