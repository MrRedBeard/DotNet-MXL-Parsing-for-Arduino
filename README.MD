# DotNet-MXL-Parsing-for-Arduino
Parses MXL to an output to be used on Arduino
<br /><br />
Thanks to <a href="https://github.com/shvelo">Shvelo</a> who's Ruby code got me pointed in the right direction https://github.com/shvelo/musicxml_to_arduino
<br /><br />
This is not a pretty or complete solution. It was just enough to get what I wanted done and was written in a very short amount of time.
<br /><br />
MXL is an XML format for sheet music. The code allows you to parse the notes, duration and tempo information from the MXL in a format that is easy to place in your Arduino code.
<br /><br />
MXL is sometimes PKZipped - app contains neccessary code to unpackage<br />
MXL files often contain multiple voices or sets of notes played together - app accounts for this<br />
Arduino Uno has a limit of file size - app allows you to control how many notes are generated<br />

<img src="https://raw.githubusercontent.com/MrRedBeard/DotNet-MXL-Parsing-for-Arduino/master/ScreenShot.PNG" />