using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;
using System.Xml.Linq;
using System.IO.Ports;
using System.IO;
using System.Net;
using System.Windows.Threading;
//using System.Windows.Localizability;

//using System.DataTime;


namespace APP
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        private SerialPort ardSerialPort = new SerialPort();

        public string sw_weather = "off";
        public string sw_time = "off";
        public string sw_text = "off";
        public string sw_draw = "off";
        public string InfoWeather, Infotext, Infoimage;

        public string init_textbox = "";
        public string init_textblock = "";
        public string input_text = "";
        public string allInfoToArdu;
        public string current_time;
        public string strWeather = ",";
        public string str_wf;
        public string wfKr;
        public string PaintNum="0";

        private Drawing_UI drawing_UI;
        private int ButtonColor;
        private DrawButton drawButton;


        public MainWindow()
        {
            InitializeComponent();

            // Class 인스턴스 생성 & UI 그려내기
            drawing_UI = new Drawing_UI(MonitorGrid);
            drawing_UI.ButtonDraw();

            // 각 Client Monitor의 버튼 클릭 이벤트를 등록
            SetIndividualButtonEvent();

            drawButton = new DrawButton();


            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Tick += Timer_Tick;
            timer.Start();

            InputMethod.SetIsInputMethodEnabled(this.textbox, false);//한글막음.

            ardSerialPort.PortName = "COM4";
            ardSerialPort.BaudRate = 9600;
            ardSerialPort.Open();

        }

        private void ToggleDotImage(object sender, RoutedEventArgs e)
        {
            Button clickedButton = (Button)sender;
            int index = -1;

            for(int btnIndex = 0; btnIndex < drawing_UI.dotButtons.Count; btnIndex++)
            {
                if (drawing_UI.dotButtons[btnIndex].myButton == clickedButton)
                    index = btnIndex;
            }

            if (drawing_UI.dotButtons[index].isBlack)
            {
                //Console.WriteLine("Black");
                //Console.WriteLine(index);
                
                drawing_UI.dotButtons[index].ChangeColor("White");
                
            }
            else
            {
                //Console.WriteLine("White");
                //Console.WriteLine(index);
                
                drawing_UI.dotButtons[index].ChangeColor("Black");
            }

        }



        private void SetIndividualButtonEvent()
        {
            for (int index = 0; index < drawing_UI.dotButtons.Count; index++)
            {
                // 마우스 클릭 이벤트
                drawing_UI.dotButtons[index].myButton.Click += ToggleDotImage;
            }
        }

        private void DrawPageSaveButton_Click(object sender, RoutedEventArgs e)
        {
            
            for (int index=0; index < drawing_UI.dotButtons.Count; index++)
            {
                //지우기
                //Console.WriteLine(index+","+drawing_UI.dotButtons[index].ByteNumber);
                //buttoncolor를 타겟컬러로 합침
                ButtonColor = drawing_UI.dotButtons[index].ByteNumber;
               
                //Console.WriteLine(drawButton.D);
                drawButton.SendButtonInformation(index,ButtonColor);
                
            }
            drawButton.ShowStringInformation();
            
        }


        private void Timer_Tick(object sender, EventArgs e)
        {
            clock_button.Text = DateTime.Now.ToLongTimeString();
            clock.Content = DateTime.Now.ToLongTimeString();

        }

        private void draw_click(object sender, RoutedEventArgs e)
        {
            Main_page.Visibility = Visibility.Hidden;
            Text_page.Visibility = Visibility.Hidden;
            Draw_page.Visibility = Visibility.Visible;
        }

        private void text_click(object sender, RoutedEventArgs e)
        {
            Main_page.Visibility = Visibility.Hidden;
            Text_page.Visibility = Visibility.Visible;
            Draw_page.Visibility = Visibility.Hidden;
            TextPageInitialize();
        }


        private void Switch_weather_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

            if (switch_weather.Toggled1 == true)
            {
                sw_weather = "on";
                WeatherXml();
            }
            else
            {
                sw_weather = "off";
            }
        }

        private void Switch_time_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (switch_time.Toggled1 == true)
            {
                sw_time = "on";
            }
            else
            {
                sw_time = "off";
            }
        }

        private void Switch_text_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (switch_text.Toggled1 == true)
            {
                sw_text = "on";
            }
            else
            {
                sw_text = "off";
            }
        }

        private void Switch_draw_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (switch_draw.Toggled1 == true)
            {
                sw_draw = "on";
            }
            else
            {
                sw_draw = "off";
            }
        }
        //public byte[] StrByte;
        //private void StringToByte(string str)
        //{
        //    StrByte = Encoding.UTF8.GetBytes(str);
            
        //}

        private void UpdateButton_click(object sender, RoutedEventArgs e)
        {
            allInfoToArdu = "";
            Infotext = sw_text + "," + input_text;//text함수에서 다시 정리해주기
            InfoWeather = sw_weather + "," + strWeather;
            if (sw_draw == "on")
            {
                Infoimage = sw_draw + ",["+ drawButton.DrawPageString;
            }
            else
            {
                Infoimage = sw_draw + ",";

            }
           // string a = "101";
            //StringToByte(a);
            //byte[] buffer = { 000111 };
            allInfoToArdu = "<" + InfoWeather + "," + sw_time + "," + Infotext + "," + Infoimage +">";
            ardSerialPort.Write(allInfoToArdu);
           // ardSerialPort.Encoding = System.Text.Encoding.UTF8;
            
           // ardSerialPort.Write(a);
        }

        //text page
        private void back_click(object sender, RoutedEventArgs e)
        {
            Main_page.Visibility = Visibility.Visible;
            Text_page.Visibility = Visibility.Hidden;
            Draw_page.Visibility = Visibility.Hidden;
            TextPageInitialize();
        }

        private void save_click(object sender, RoutedEventArgs e)
        {
            textblock.Text = textbox.Text;
            input_text = textblock.Text;
            text_input_button.Text = input_text;
        }
        public void TextPageInitialize()
        {
            textblock.Text = init_textblock;
            textbox.Text = init_textbox;
        }

        //변경 (계속해서 시간을 셀 수 있도록)
        private void Show_Current_time()
        {

            current_time = DateTime.Now.ToString("HH:mm:ss");
            if (sw_time == "on\\")//이미 켜져있다면, 원래거 없애버리고
            {
                sw_time = sw_time + current_time;
            }

        }

        private void Back_Click_1(object sender, RoutedEventArgs e)
        {
            Main_page.Visibility = Visibility.Visible;
            Text_page.Visibility = Visibility.Hidden;
            Draw_page.Visibility = Visibility.Hidden;
            TextPageInitialize();
        }//draw_page->main page로 돌아가기

        private void WeatherXml()
        {
            String strUrl = "http://www.kma.go.kr/wid/queryDFSRSS.jsp?zone=4711325000";
            UriBuilder ub = new UriBuilder(strUrl);
            //ub.Query = "strLd=109";//지역정보 변경하고 싶을 때 사용하기
            HttpWebRequest request;
            request = HttpWebRequest.Create(ub.Uri) as HttpWebRequest;
            request.BeginGetResponse(new AsyncCallback(GetResponse), request);

        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void InitializationButton_Click(object sender, RoutedEventArgs e)
        {
            for (int btnIndex = 0; btnIndex < drawing_UI.dotButtons.Count; btnIndex++)
            {
                if (!drawing_UI.dotButtons[btnIndex].isBlack)
                    drawing_UI.dotButtons[btnIndex].ChangeColor("Black");
            }
            drawButton.Initialize();
        }

        private void GetResponse(IAsyncResult ar)
        {
            HttpWebRequest wr = (HttpWebRequest)ar.AsyncState;
            HttpWebResponse wp = (HttpWebResponse)wr.EndGetResponse(ar);

            Stream stream = wp.GetResponseStream();
            StreamReader reader = new StreamReader(stream);

            String strRead = reader.ReadToEnd();

            XElement xmlMain = XElement.Parse(strRead);
            XElement xmlBody = xmlMain.Descendants("body").First();
            XElement xmlData = xmlBody.Element("data");

            String wfEn = xmlData.Element("wfEn").Value;
            String sTemp = xmlData.Element("temp").Value;
            wfKr = xmlData.Element("wfKor").Value;
            getWeather(wfEn);

            //strwfEn = strwfEn.Replace("<br/><br/>", "\n");

            strWeather = str_wf + "," + sTemp;

        }


        private void getWeather(string wfEn)
        {
            if (wfEn == "Clear")
            {
                str_wf = "Sunny";
            }
            else if (wfEn == "Cloudy" || wfEn == "Mostly Cloudy")
            {
                str_wf = "Cloudy";
            }
            else if (wfEn == "Shower")
            {
                str_wf = "Rainy";
            }
            else if (wfEn == "Snow")//눈은 어떻게 보내주는지 몰라서
            {
                str_wf = "Snowy";
            }
            else
            {
                str_wf = "Sunny";
            }

        }



    }
}







