using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APP
{
    class DrawButton
    {
        public int ArrayNumber = 0;
        public String DrawPageString="" ;

        //int[][] DrawInformationArray = Enumerable.Repeat(Enumerable.Repeat(0, 7).ToArray(), 28).ToArray();
        int[,] DrawInformationArray = new int[28, 7] ;
        public DrawButton()
        {
           
        }

        public void SendButtonInformation(int ButtonIndex, int ButtonColor)
        {
            if (ButtonColor == 1)
            {
                DrawInformationArray[ButtonIndex %28 ,ButtonIndex / 28] = 1;
                //Console.WriteLine(ButtonIndex %28 + "," + ButtonIndex / 28+" "+ButtonIndex);
            }
          
        }
        public void ShowStringInformation()
        {
            DrawPageString = "";
            
            for(int i = 27; i >= 0  ; i--)
            {
                DrawPageString += "B";
                for (int j = 0; j < 7; j++)
                {
                    //Console.WriteLine("i"+i+"j"+j+"."+DrawInformationArray[i,j]);
                     DrawPageString = DrawPageString + DrawInformationArray[i,j];
                }
                if (i == 0)
                    break;
               DrawPageString += ",";
            }
            //Console.WriteLine(DrawPageString);

        }
        public void Initialize()
        {
            DrawPageString = "";
            for (int i = 0; i < 28; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    DrawInformationArray[i, j] = 0;
                    
                }
                
            }
            ShowStringInformation();
        }

        public void StringToDecimal()
        {

        }

    }
}





//switch (ButtonIndex % 7)
//{
//    case 0:
//        while (ArrayNumber < 7)//변수 변경 필요
//        {
//            DrawInformationArray[0, ArrayNumber] = ButtonColor;
//            ArrayNumber++;
//        }
//        break;

//}