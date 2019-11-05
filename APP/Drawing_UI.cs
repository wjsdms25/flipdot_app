using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace APP

{
  
    public class Drawing_UI 
    {
        private Grid monitorGrid;
        public List<DotButton> dotButtons;

        // Class의 생성자
        public Drawing_UI(Grid monitorGrid)
        {
            this.monitorGrid = monitorGrid;

            dotButtons = new List<DotButton>();
        }

        public void ButtonDraw()
        {
            for (int row = 1; row < 8; row++)
            {
                for (int col = 1; col < 29; col++)
                {
                    Button flipDot = new Button();
                    flipDot.HorizontalAlignment = HorizontalAlignment.Center;
                    flipDot.VerticalAlignment = VerticalAlignment.Center;
                    flipDot.Margin = new Thickness(0);
                    flipDot.Height = 25;
                    flipDot.Width = 25;
                    flipDot.BorderThickness = new Thickness(0);
                    //flipDot.Background = Brushes.Transparent.Color.Opacity
                    
                    // 버튼 뒷배경 색 변경 아래 코드에서 가능합니다!
                    flipDot.Background = Brushes.LightGray;
                    Grid.SetColumn(flipDot, col);
                    Grid.SetRow(flipDot, row);

                    Image dotImage = new Image();
                    dotImage.Margin = new Thickness(0);
                    dotImage.Width = 240;
                    dotImage.Height = 80;
                    BitmapImage tmp = new BitmapImage();
                    tmp.BeginInit();
                    tmp.UriSource = new Uri(@"C:\Users\user\source\repos\APP\image\black_dot.png", UriKind.Absolute);
                    tmp.EndInit();
                    dotImage.Stretch = Stretch.None;
                    
                    dotImage.Source = tmp;

                    flipDot.Content = dotImage;

                    monitorGrid.Children.Add(flipDot);

                    DotButton dotBtn = new DotButton(flipDot);
                    dotButtons.Add(dotBtn);
                }
            }
        }


    }
}
