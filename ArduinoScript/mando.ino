#include <LiquidCrystal.h>


#include "Wire.h" 
const int MPU_ADDR = 0x68;
int16_t accelerometer_x, accelerometer_y, accelerometer_z;
int16_t gyro_x, gyro_y, gyro_z;
int16_t temperature;


const int button_shoot = 13;

const int button_joy = 12;
const int X_joy = 0;
const int Y_joy = 1;


int incomingByte = 0; 

String inputString = "";
bool stringComplete = false;

LiquidCrystal lcd(7, 6, 5, 4, 3, 2);

byte batery_1[8] = {
  B01110,
  B11011,
  B10001,
  B10001,
  B10001,
  B10001,
  B10001,
  B11111,
};
byte batery_2[8] = {
  B01110,
  B11011,
  B10001,
  B10001,
  B10001,
  B11111,
  B11111,
  B11111,
};
byte batery_3[8] = {
  B01110,
  B11011,
  B10001,
  B10001,
  B11111,
  B11111,
  B11111,
  B11111,
};
byte batery_4[8] = {
  B01110,
  B11011,
  B10001,
  B11111,
  B11111,
  B11111,
  B11111,
  B11111,
};
byte batery_5[8] = {
  B01110,
  B11111,
  B11111,
  B11111,
  B11111,
  B11111,
  B11111,
  B11111,
};


void setup() {
  Serial.begin(9600);
  inputString.reserve(200); 
  
  StartCommunicationWithGyroscope();

  pinMode(X_joy, INPUT);
  pinMode(Y_joy, INPUT);
  pinMode(button_shoot, INPUT);
  pinMode(button_joy, INPUT);
  digitalWrite(button_joy, HIGH);

    
   
  CreateSpecialCharacters();
   
  DrawMunitionInDisplay("Complete");

}


void loop() {
  
  
  if (stringComplete) {
    inputString.trim();
    if(inputString.equals("Nothing")){
      PaintBateryOnAlDisplay();    
    }else{
      DrawMunitionInDisplay(inputString);
      }
    inputString = "";
    stringComplete = false;
  }
  
  ReadValuesFromGyroscope();
 
  
  Serial.print(digitalRead(button_shoot));
  Serial.print(","); 
  Serial.print(analogRead(Y_joy));
  Serial.print(","); 
  Serial.print(gyro_y); 
  Serial.print(","); 
  Serial.print(gyro_z);
  Serial.print(",");
  Serial.print(digitalRead(button_joy));


  Serial.println();
  delay(50);
}



void StartCommunicationWithGyroscope(){
  Wire.begin();
  Wire.beginTransmission(MPU_ADDR); 
  Wire.write(0x6B);
  Wire.write(0); 
  Wire.endTransmission(true);
}
void ReadValuesFromGyroscope(){
    
   Wire.beginTransmission(MPU_ADDR);
  Wire.write(0x3B); 
  Wire.endTransmission(false); 
  Wire.requestFrom(MPU_ADDR, 7*2, true);
  
 
  accelerometer_x = Wire.read()<<8 | Wire.read();
  accelerometer_y = Wire.read()<<8 | Wire.read();
  accelerometer_z = Wire.read()<<8 | Wire.read();
  temperature = Wire.read()<<8 | Wire.read();
  gyro_x = Wire.read()<<8 | Wire.read();
  gyro_y = Wire.read()<<8 | Wire.read();
  gyro_z = Wire.read()<<8 | Wire.read();
  
  }

void CreateSpecialCharacters(){
  lcd.createChar(0, batery_1);
  lcd.createChar(1, batery_2);
  lcd.createChar(2, batery_3);
  lcd.createChar(3, batery_4);
  lcd.createChar(4, batery_5);
  lcd.begin(16, 2); 
}

void DrawMunitionInDisplay(String stateMunition){
  lcd.setCursor(0, 0);
    if(stateMunition.equals("VeryLow")){
      lcd.write(byte(0));
    }
    else if(stateMunition.equals("Low")){
      lcd.write(byte(1));
    }
    else if(stateMunition.equals("Medium")){
      lcd.write(byte(2));
    }
    else if(stateMunition.equals("High")){
      lcd.write(byte(3));
    }
    else if(stateMunition.equals("Complete")){      
      ClearDisplay();
      lcd.write(byte(4));
    }
 
    
}

void serialEvent() {
  while (Serial.available()) {
    char inChar = (char)Serial.read();
    if (inChar == '\n') {
      stringComplete = true;
    }
    else {
      inputString += inChar;
    }
  }
} 


void ClearDisplay(){
  for(int i = 0; i<=1;i++){
      for(int j = 0; j<=15;j++){
        lcd.setCursor(j, i);
        lcd.print(' ');
      }
    }
  lcd.setCursor(0, 0);
}

void PaintBateryOnAlDisplay(){
    
    for(int i = 0; i<=1;i++){
      for(int j = 0; j<=15;j++){
        lcd.setCursor(j, i);
        lcd.write(byte(4));
      }
    }

}
