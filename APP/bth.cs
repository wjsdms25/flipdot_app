using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;
using System.Windows;

namespace APP
{
    public class bth : Window
    {
        SerialPort arduSerialPort = new SerialPort();  //시리얼 포트 생성
        public bth()
        {
            //InitializeComponent();
            arduSerialPort.PortName = "COM3";    //아두이노가 연결된 시리얼 포트 번호 지정
            arduSerialPort.BaudRate = 9600;       //시리얼 통신 속도 지정
            arduSerialPort.Open();                //포트 오픈
        }

        public void on_btn_Click()
        {
            arduSerialPort.Write("1"); //연결된 시리얼포트로 "1"의 값을 전달

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (true == arduSerialPort.IsOpen) //포트가 열려있다면
            {
                arduSerialPort.Close();        //포트를 닫는다
            }
        }

        public void off_btn_Click_()
        {
             arduSerialPort.Write("0"); //연결된 시리얼포트로 "0"의 값을 전달
        }


    }
}


