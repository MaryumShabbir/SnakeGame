using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Windows.Media;

namespace SnakeGame
{
    public static class Images
    {
        public readonly static ImageSource Empty = LoadImage("Empty.png");
        public readonly static ImageSource Body = LoadImage("Green.png");
        public readonly static ImageSource head = LoadImage("Snake.png");
        public readonly static ImageSource food = LoadImage("C:\\Users\\Admin\\source\\repos\\SnakeGame\\Assets\\Food.png");
        public readonly static ImageSource DeadBody = LoadImage("DarkGreen.png");
        public readonly static ImageSource DeadHead = LoadImage("DeadSnake.png");
        private static ImageSource LoadImage(string fileName)
        {
            return new BitmapImage(new Uri($"Assests/{fileName}", UriKind.Relative));
        }
    }
}
