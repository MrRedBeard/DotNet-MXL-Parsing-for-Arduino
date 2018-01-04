using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MusicXMLParser
{
    class Note
    {
        public string voice { get; set; }
        public string noteString { get; set; }
        public int duration { get; set; }
        public int frequency { get; set; }
    }
    

    public class Pitch
    {
        public Dictionary<string, int> pitches = new Dictionary<string, int>();

        public string ArduinoBottom =
@"
void setup() {
  
  for (int thisNote = 0; thisNote < sizeof(melody) / sizeof(int); thisNote++)
  {    
    tone(11, melody[thisNote], noteDurations[thisNote] * .7);    
    delay(noteDurations[thisNote]);    
    noTone(11);
  }
}

void loop() {
  // no need to repeat the melody.
}
";

        public Pitch()
        {
            pitches.Add("NOTE_B0", 31);
            pitches.Add("NOTE_C1", 33);
            pitches.Add("NOTE_CS1", 35);
            pitches.Add("NOTE_D1", 37);
            pitches.Add("NOTE_DS1", 39);
            pitches.Add("NOTE_E1", 41);
            pitches.Add("NOTE_F1", 44);
            pitches.Add("NOTE_FS1", 46);
            pitches.Add("NOTE_G1", 49);
            pitches.Add("NOTE_GS1", 52);
            pitches.Add("NOTE_A1", 55);
            pitches.Add("NOTE_AS1", 58);
            pitches.Add("NOTE_B1", 62);
            pitches.Add("NOTE_C2", 65);
            pitches.Add("NOTE_CS2", 69);
            pitches.Add("NOTE_D2", 73);
            pitches.Add("NOTE_DS2", 78);
            pitches.Add("NOTE_E2", 82);
            pitches.Add("NOTE_F2", 87);
            pitches.Add("NOTE_FS2", 93);
            pitches.Add("NOTE_G2", 98);
            pitches.Add("NOTE_GS2", 104);
            pitches.Add("NOTE_A2", 110);
            pitches.Add("NOTE_AS2", 117);
            pitches.Add("NOTE_B2", 123);
            pitches.Add("NOTE_C3", 131);
            pitches.Add("NOTE_CS3", 139);
            pitches.Add("NOTE_D3", 147);
            pitches.Add("NOTE_DS3", 156);
            pitches.Add("NOTE_E3", 165);
            pitches.Add("NOTE_F3", 175);
            pitches.Add("NOTE_FS3", 185);
            pitches.Add("NOTE_G3", 196);
            pitches.Add("NOTE_GS3", 208);
            pitches.Add("NOTE_A3", 220);
            pitches.Add("NOTE_AS3", 233);
            pitches.Add("NOTE_B3", 247);
            pitches.Add("NOTE_C4", 262);
            pitches.Add("NOTE_CS4", 277);
            pitches.Add("NOTE_D4", 294);
            pitches.Add("NOTE_DS4", 311);
            pitches.Add("NOTE_E4", 330);
            pitches.Add("NOTE_F4", 349);
            pitches.Add("NOTE_FS4", 370);
            pitches.Add("NOTE_G4", 392);
            pitches.Add("NOTE_GS4", 415);
            pitches.Add("NOTE_A4", 440);
            pitches.Add("NOTE_AS4", 466);
            pitches.Add("NOTE_B4", 494);
            pitches.Add("NOTE_C5", 523);
            pitches.Add("NOTE_CS5", 554);
            pitches.Add("NOTE_D5", 587);
            pitches.Add("NOTE_DS5", 622);
            pitches.Add("NOTE_E5", 659);
            pitches.Add("NOTE_F5", 698);
            pitches.Add("NOTE_FS5", 740);
            pitches.Add("NOTE_G5", 784);
            pitches.Add("NOTE_GS5", 831);
            pitches.Add("NOTE_A5", 880);
            pitches.Add("NOTE_AS5", 932);
            pitches.Add("NOTE_B5", 988);
            pitches.Add("NOTE_C6", 1047);
            pitches.Add("NOTE_CS6", 1109);
            pitches.Add("NOTE_D6", 1175);
            pitches.Add("NOTE_DS6", 1245);
            pitches.Add("NOTE_E6", 1319);
            pitches.Add("NOTE_F6", 1397);
            pitches.Add("NOTE_FS6", 1480);
            pitches.Add("NOTE_G6", 1568);
            pitches.Add("NOTE_GS6", 1661);
            pitches.Add("NOTE_A6", 1760);
            pitches.Add("NOTE_AS6", 1865);
            pitches.Add("NOTE_B6", 1976);
            pitches.Add("NOTE_C7", 2093);
            pitches.Add("NOTE_CS7", 2217);
            pitches.Add("NOTE_D7", 2349);
            pitches.Add("NOTE_DS7", 2489);
            pitches.Add("NOTE_E7", 2637);
            pitches.Add("NOTE_F7", 2794);
            pitches.Add("NOTE_FS7", 2960);
            pitches.Add("NOTE_G7", 3136);
            pitches.Add("NOTE_GS7", 3322);
            pitches.Add("NOTE_A7", 3520);
            pitches.Add("NOTE_AS7", 3729);
            pitches.Add("NOTE_B7", 3951);
            pitches.Add("NOTE_C8", 4186);
            pitches.Add("NOTE_CS8", 4435);
            pitches.Add("NOTE_D8", 4699);
            pitches.Add("NOTE_DS8", 4978);
        }       

     
    }
}


