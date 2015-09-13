using System;
using System.Net.Sockets;
using System.Net;

namespace AtmoLight.Targets.Domotica
{
  class KlikAanKlikUitDomotica : IDomotica
  {
    private bool isConnected;
    private readonly Core coreObject = Core.GetInstance();

    public KlikAanKlikUitDomotica(string comport, string devicename)
    {
      COMport = comport;
      DeviceName = devicename;
    }

    public DomoticaProtocol Protocol
    {
      get { return DomoticaProtocol.CLI; }
    }

    public DomoticaCapabilities Capabilities
    {
      get { return DomoticaCapabilities.LightToggle; }
    }

    public DomoticaBrand Brand
    {
      get { return DomoticaBrand.KlikAanKlikUit; }
    }

    public DomoticaType Type
    {
      get { return DomoticaType.SERIAL; }
    }

    public string IP { get; private set; }
    public int Port { get; private set; }
    public string ID { get; private set; }
    public string DeviceName { get; private set; }
    public string COMport { get; private set; }

    public void ConnectSerial(string comPort)
    {
    }
    public void Connect(string ip, int port)
    {
      // Not used
    }

    public void Disconnect()
    {
      try
      {
        isConnected = false;
      }
      catch (Exception ex)
      {
      }
    }

    public bool IsConnected()
    {
      return isConnected;
    }

    public void ChangeColor(string color)
    {
    }

    public string ReceiveData(string command)
    {
      return "";
    }

    public bool Sendcommand(string command)
    {
      return false;
    }
  }
}
