// ARDUINO-testkode
// Copy-and-Paste 

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
