using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace APP
{
    public class DotButton
    {
        public BitmapImage black_dot = new BitmapImage();
        public BitmapImage white_dot = new BitmapImage();
        public int ByteNumber = 0;

        Image b_image = new Image();
        Image w_image = new Image();

        public bool isBlack = true;

        public Button myButton;

        public DotButton(Button myButton)
        {
            this.myButton = myButton;

            black_dot.BeginInit();
            black_dot.UriSource = new Uri(@"C:\Users\user\source\repos\APP\image\black_dot.png", UriKind.Absolute);
            black_dot.EndInit();

            b_image.Stretch = Stretch.None;
            b_image.Source = black_dot;

            white_dot.BeginInit();
            white_dot.UriSource = new Uri(@"C:\Users\user\source\repos\APP\image\white_dot.png", UriKind.Absolute);
            white_dot.EndInit();

            w_image.Stretch = Stretch.None;
            w_image.Source = white_dot;
        }

        public void ChangeColor(string targerColor)
        {
            switch(targerColor)
            {
                case "White":
                    myButton.Content = w_image;
                    isBlack = false;
                    ByteNumber = 1;
                    break;

                case "Black":
                    myButton.Content = b_image;
                    isBlack = true;
                    ByteNumber = 0;
                    break;
            }
        }

    }
}
