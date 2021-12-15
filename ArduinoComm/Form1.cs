using System;
using System.Windows.Forms;
using System.IO.Ports;



namespace ArduinoComm
{
    /*
    // Klasseutvidelse av Progressbar for å gjøre den vertikal istedet for horisontal
    // Hentes fra "Toolbox"
    class VerticalProgressbar : ProgressBar 
    {
        protected override CreateParams CreateParams
        {
              get 
              {
                CreateParams orientation = base.CreateParams;
                orientation.Style |= 0x04;
                return orientation;
              }
        }
    }
    */


    public partial class frmArduino : Form
    {
        // Data som kommer ifra arduinoen
        private string sData;
          

        public frmArduino() {
            InitializeComponent(); 
        }

        /// <summary>
        /// TrekkUtData 
        // Trekker ut verdien i en kommandolinje fra Arduino
        // Eksempel: data = "D1=22;D2=11;D3=44"
        // TrekkUtData(data,"D2") returnerer "11"
        /// </summary>
        /// <param name="data">
        /// Kommandolinje fra Arduino
        /// </param>
        /// <param name="varnavn">
        /// Hvilken verdi/variabelnavn som skal trekkes ut
        /// </param>
        /// <returns>
        /// Verdi i form av tekst
        /// </returns>
        private String TrekkUtData(String data, String varnavn)
        {
            String[] deldata = data.Split(';');
            
            foreach (var navn in deldata) {
                if (navn.StartsWith(varnavn + "="))
                    return navn.Substring(varnavn.Length+1);                
            }

            return "";
        }


        private void frmArduino_Load(object sender, EventArgs e)
        {
            cmbPort.Items.Clear();  

            try
            {
                if (SerialPort.GetPortNames().Length > 0)
                {
                    cmbPort.Items.AddRange(SerialPort.GetPortNames());
                    cmbPort.SelectedIndex = 0;
                    ArduinoPort.BaudRate = 9600;
                    ArduinoPort.PortName = cmbPort.Text;
                    cmbBaudrate.Text = "9600";
                    lblPort.Text = "Arduino på port: "+ ArduinoPort.PortName;
                    btnSerOpen.Enabled = true;
                    btnSerClose.Enabled = false;

                    btnSerOpen.Focus();
                }
                else
                {
                    lblPort.Text = "Ingen Arduino tilkoblet.";
                    btnSerOpen.Enabled = false;
                    btnSerClose.Enabled = false;

                    cmbPort.Focus();
                }
       
            }
            catch (Exception ex)
            {
                string melding = ex.Message;
                melding+="\nSjekk om port er opptatt!";
                MessageBox.Show(melding);
            }
        }

        private void btnOn_Click(object sender, EventArgs e)
        {
            try 
            {  // Sender ut kommandoen til arduinoen for at LED13 skal på
                ArduinoPort.Write("B13=ON");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnOff_Click(object sender, EventArgs e)
        {
            try
            {   // Sender ut kommandoen til arduinoen for at LED13 skal av
                ArduinoPort.Write("B13=OFF");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void cmbPort_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                // Hvis det er flere serieporter tilgjengelig må vi velge vår Arduino
                ArduinoPort.PortName = cmbPort.Text;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
 

        private void ArduinoPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            // Henter data fra arduinoen
            sData = ArduinoPort.ReadLine();

            // Trigger en hendelse for å behande dataene fra arduinoen
            this.BeginInvoke(new EventHandler(BehandleData));
        }

        //Behandler dataene fra arduinoen
        private void BehandleData(object sender, EventArgs e)
        {
            try
            {


                // Trekker ut innholdet ; her D1 og D2 som kommer ifra linjen skrevet Arduino
                textBox1.Text = TrekkUtData(sData, "D1");
                textBox2.Text = TrekkUtData(sData, "D2");

                // Legger til nytt punkt på diagram
                chart1.Series["D1"].Points.AddY(TrekkUtData(sData, "D1"));
                chart1.Series["D2"].Points.AddY(TrekkUtData(sData, "D2"));

                // Sørger for at vi har en begrenset lengde på x-aksen
                if (chart1.Series["D1"].Points.Count == 20)
                    chart1.Series["D1"].Points.RemoveAt(0);

                if (chart1.Series["D2"].Points.Count == 20)
                    chart1.Series["D2"].Points.RemoveAt(0);

                // Legger ti en verdi til "progressbar"
                // (Maximum- og minimum-verdier er forhåndsdefinert i progressbar1.)
                progressBar1.Value = Convert.ToInt32(TrekkUtData(sData, "D1"));

                // skriver ut rådata
                txtData.Text += sData;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnSerOpen_Click(object sender, EventArgs e)
        {
            if (ArduinoPort!=null)
                if (!ArduinoPort.IsOpen)
                {
                    try
                    {
                        ArduinoPort.PortName = cmbPort.Text;
                        ArduinoPort.Open();
                        btnSerOpen.Enabled = false;
                        btnSerClose.Enabled = true;
                        btnSerClose.Focus();
                    }
                    catch (Exception ex)
                    {
                        string melding = ex.Message;
                        melding+="\nSjekk om port er opptatt!";
                        MessageBox.Show(melding);
                    }
                }
        }

        private void btnSerClose_Click(object sender, EventArgs e)
        {
            try
            {
                if (ArduinoPort.IsOpen)
                {
                    ArduinoPort.Close();
                    btnSerOpen.Enabled = true;
                    btnSerClose.Enabled = false;
                    btnSerOpen.Focus();
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }

        }

        private void frmArduino_FormClosing(object sender, FormClosingEventArgs e)
        {
            try { 
                ArduinoPort.Close();
            }
            catch (Exception ex)  {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnBlink1_Click(object sender, EventArgs e)
        {
            try {
                // Sender ut kommandoen at LED13 skal blinke (her:333ms mellom hver blink)
                ArduinoPort.Write("B13=500");
            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }


          
        }


        private void cmbBaudrate_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                // Hvis vi skulle endre på overføringshastighet PC<->Arduino
                ArduinoPort.BaudRate = Convert.ToInt32(cmbBaudrate.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void frmArduino_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                ArduinoPort.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                // Sender ut kommandoen at LED13 skal blinke (her:100ms mellom hver blink)
                ArduinoPort.Write("B13=100");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
    }


}

/**
// ARDUINO-testkode
// Copy-and-Paste to Arduino IDE and run

#include <string.h>

//////////////////////////////////////////////////////////////
// Vi ønsker her å trigge ulike hendelser etter ulike perioder
int PERIODE1 = 1000;  // hvert sekund ; her overføring av data til PC
unsigned long TID1 = 0;

int PERIODE2 = 250;   //  hver 1/4 sekund skal LED13 blinke
unsigned long TID2 = 0;
//////////////////////////////////////////////////////////////////////


void setup()
{
    Serial.begin(9600);
    // Standard overføringshastighet på serieport-kommunikasjon

    pinMode(13, OUTPUT);
}

////////////////////////////////////////////////////////////7
// TrekkUtData 
// Trekker ut verdien i en kommandolinje fra PC
// i.e. data = "D1=2;D2=3.2;D3=5"
// TrekkUtData(data,"D1") returnerer "2"
//
String TrekkUtData(String data, String varn)
{
    int start = data.indexOf(varn+"=");
    int  semi = data.indexOf(";", start+1);

    if (start>=0)
        return data.substring(start + (varn.length() + 1), semi);
    else
        return "";
}


void loop()
{
    // Leser innkomne kommando fra PC
    if (Serial.available())
    {
        String sPC = Serial.readStringUntil('\n');

        // testdata- eksempel ; gjør om verdi til desimal-tall
        // Serial.println(TrekkUtData(sPC, "D2").toFloat());

        if (TrekkUtData(sPC, "B13").equals("ON"))
        {
            digitalWrite(13, HIGH);  //LED13 på
            PERIODE2 = 999999;  // Lager Periode2 høy
        }
        else if (TrekkUtData(sPC, "B13").equals("OFF"))
        {
            digitalWrite(13, LOW);   //LED13 av
            PERIODE2 = 999999;
        }
        else if (TrekkUtData(sPC, "B13").toInt() > 0)
            PERIODE2 = TrekkUtData(sPC, "B13").toInt();


    }


    // PERIODE 1 - innhenting og overføring av data til pc
    if (millis() >= TID1 + PERIODE1)
    {
        TID1 += PERIODE1;

        // Testdata:
        // Her skriver vi ut noen vilkårlige testdata til PC ,
        // F.eks: "D:D1=39;D2=10.83"
        int d2 = random(6, 14);    // Tilfeldige data fra  "temperatur-sensor"
        int d1 = random(0, 255);   // tilfeldig tall fra en eller annen sensor

        // Bygger opp en linje med testdata:
        String data = "D:";
        data+= ";D1=";
        data+= d1;
        data+= ";D2=";
        data+= d2*1.23;   // vi ganger med et vilkårlig desimaltall 
        
        // skriver testdata som kommando til PC 
        Serial.println(data);  

    }

    // PERIODE2:
    if (millis() >= TID2 + PERIODE2)
    {
        TID2 += PERIODE2;

        // LED13 blinke av/på vekselsvis etter hver PERIODE2
        digitalWrite(13, !digitalRead(13));
    }


}

**/