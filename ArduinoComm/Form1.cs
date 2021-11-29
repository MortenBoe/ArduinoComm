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



namespace ArduinoComm
{
    public partial class frmArduino : Form
    {
        public frmArduino()
        {
            InitializeComponent();
        }

        private float Data(String data, String varnavn)
        {
            String[] parts = data.Split(';');
            /*
            foreach (var navn in parts)
            {
                if navn.StartsWith "Temp="

            }
            
            */
            return 0;
        }
        private void frmArduino_Load(object sender, EventArgs e)
        {
            try
            {
                if (SerialPort.GetPortNames().Length > 0)
                {
                    lblPort.Text = "Arduino tilkoblet på port:";
                    cmbPort.Items.AddRange(SerialPort.GetPortNames());
                    cmbPort.SelectedIndex = 0;
                    ArduinoPort.BaudRate = 9600;

                    ArduinoPort.PortName = cmbPort.Text;
                }
                else
                    lblPort.Text = "Ingen Arduino tilkoblet.";
       
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnOn_Click(object sender, EventArgs e)
        {
            try {

                ArduinoPort.Open();
                ArduinoPort.Write("A");
                ArduinoPort.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


        }

        private void btnOff_Click(object sender, EventArgs e)
        {
            try
            {
                ArduinoPort.Open();
                ArduinoPort.Write("a");
                ArduinoPort.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void cmbPort_SelectedIndexChanged(object sender, EventArgs e)
        {
            ArduinoPort.PortName = cmbPort.Text;

        }

        private void btnLesData_Click(object sender, EventArgs e)
        {
             
        }




    }
}
/*
 * 
 
//Arduino-kode:

int PERIODE1 = 500;   
unsigned long TID1 = 0;

int PERIODE2 = 333;  
unsigned long TID2 = 0; 

bool LEDstatus = false;

 

void setup() {
    Serial.begin(9600);
    pinMode(13,OUTPUT);
}
 
void loop() 
{

      // leser innkomne data fra PC
      if (Serial.available()) {
        String d = Serial.readString();
        char c = d.charAt(0);
        
            switch(c) {
                case 'A':
                    PERIODE2 = 100;  // blinker i høyere takt
                    //digitalWrite(13,HIGH);  // Hvis man ønsker å skru på
                break;
                
                case 'a':
                    PERIODE2 = 500; // blinker i lavere takt
                     //digitalWrite(13,LOW);  // Hvis man ønsker å skru av
                break;
                
               // F.eks. S120 blir her omgjort til   value=120
               // case 'S':
                //   String x = data.substring(1);
                //   int value = x.toInt();
               // break;
             // 
             }
      }

      
   // PERIODE 1
    if (millis() >= TID1 + PERIODE1)
{
    TID1 += PERIODE1;

    // Utfør noe etter periode 1
    // Her skriver vi ut noen vilkårlige testdata til PC som kan være hentet fra ulike sensorer
    // F.eks: "A:;Temp=16;D1=39;D2=10.83"
    int temp = random(13, 25); // Tilfeldig temperatur
    int d1 = random(0, 255);  // tilfeldig tall

    String data = "A:";
    data += "Temp=";
    data += temp;
    data += ";D1=";
    data += d1;
    data += ";D2=";
    data += d1 / 3.6;

    Serial.println(data);  // skriver data til PC

}

// PERIODE2:
if (millis() >= TID2 + PERIODE2)
{
    TID2 += PERIODE2;

    // Vi lan LED13 blinke av/på 
    LEDstatus = !LEDstatus;
    digitalWrite(13, LEDstatus);
}
 
}
*/