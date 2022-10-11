
int STmelody[] = { 
131, 165, 196, 247, 262, 247, 196, 165, 131, 165, 196, 247, 262, 247, 196, 165, 131, 165, 196, 247, 262, 247, 196, 165, 131, 165, 196, 247, 262, 247, 196, 165, 131, 165, 196, 247, 262, 247, 196, 165, 131, 165, 196, 247, 262, 247, 196, 165 
};
int STnoteDurations[] = { 
188, 188, 188, 188, 188, 188, 188, 188, 188, 188, 188, 188, 188, 188, 188, 188, 188, 188, 188, 188, 188, 188, 188, 188, 188, 188, 188, 188, 188, 188, 188, 188, 188, 188, 188, 188, 188, 188, 188, 188, 188, 188, 188, 188, 188, 188, 188, 188 
};

int ATmelody[] = { 
196, 233, 220, 0, 175, 196, 196, 196, 233, 220, 0, 175, 147, 116, 131, 0, 110, 116, 116, 98, 110, 0, 87, 147, 116, 131, 0, 110, 116, 116, 98, 110, 0, 87, 175, 139, 156, 0, 131, 139, 139, 116, 131, 0, 104, 175, 139, 156, 0, 131 
};
int ATnoteDurations[] = { 
328, 164, 164, 164, 328, 164, 164, 164, 164, 164, 164, 492, 328, 164, 164, 164, 328, 164, 164, 328, 164, 164, 492, 328, 164, 164, 164, 328, 164, 164, 328, 164, 164, 492, 328, 164, 164, 164, 328, 164, 164, 328, 164, 164, 492, 328, 164, 164, 164, 328
};

int Mariomelody[] = { 
659, 659, 659, 0, 523, 659, 784, 392, 523, 0, 392, 0, 330, 0, 440, 0, 494, 0, 466, 440, 392, 659, 784, 880, 698, 784, 0, 659, 0, 523, 587, 494, 0, 523, 0, 392, 0, 330, 0, 440, 0, 494, 0, 466, 440, 392, 659, 784, 880, 698, 784, 0
};
int MarionoteDurations[] = { 
150, 300, 150, 150, 150, 300, 600, 600, 300, 150, 150, 300, 300, 150, 150, 150, 150, 150, 150, 300, 200, 200, 200, 300, 150, 150, 150, 150, 150, 150, 150, 150, 300, 300, 150, 150, 300, 300, 150, 150, 150, 150, 150, 150, 300, 200, 200, 200, 300, 150, 150, 150
};

const int buttonPin = 2;
int buttonState = 0;

int melodyNum = 0;

void setup() {

  // initialize the pushbutton pin as an input:
  pinMode(buttonPin, INPUT);
  
  //strangerThings();
  //AttackOnTitan();
  //Mario();
}

void loop() {

  // read the state of the pushbutton value:
  buttonState = digitalRead(buttonPin);

  if (buttonState == HIGH) {
    
    // turn on:
    if(melodyNum == 0)
    {
      strangerThings();
    }
    if(melodyNum == 1)
    {
      AttackOnTitan();
    }
    if(melodyNum == 2)
    {
      Mario();
    }

    if(melodyNum == 2)
    {
      melodyNum = 0;
    }
    else
    {
      melodyNum++;
    }
  }
}


void strangerThings()
{
  for (int thisNote = 0; thisNote < sizeof(STmelody) / sizeof(int); thisNote++)
  {    
    tone(8, STmelody[thisNote], STnoteDurations[thisNote] * .7);    
    delay(STnoteDurations[thisNote]);    
    noTone(8);
  }
}

void AttackOnTitan()
{
  for (int thisNote = 0; thisNote < sizeof(ATmelody) / sizeof(int); thisNote++)
  {    
    tone(8, ATmelody[thisNote], ATnoteDurations[thisNote] * .7);    
    delay(ATnoteDurations[thisNote]);    
    noTone(8);
  }
}

void Mario()
{
  for (int thisNote = 0; thisNote < sizeof(Mariomelody) / sizeof(int); thisNote++)
  {    
    tone(8, Mariomelody[thisNote], MarionoteDurations[thisNote] * .7);    
    delay(MarionoteDurations[thisNote]);    
    noTone(8);
  }
}
