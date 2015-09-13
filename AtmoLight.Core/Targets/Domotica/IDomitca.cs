using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AtmoLight.Targets.Domotica;

namespace AtmoLight.Targets
{
  interface IDomotica
  {
    DomoticaBrand Brand { get; }
    DomoticaType Type { get; }
    DomoticaProtocol Protocol { get; }
    DomoticaCapabilities Capabilities { get; }
    string DeviceName { get; }
    string IP { get; }
    string COMport { get; }
    int Port { get; }
    void Connect(string ip, int port);
    void ConnectSerial(string comPort);
    void Disconnect();
    bool IsConnected();
    void ChangeColor(string color);
    string ReceiveData(string command);
    bool Sendcommand(string command);
  }
}
