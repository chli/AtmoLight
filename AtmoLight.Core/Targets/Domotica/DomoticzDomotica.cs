using System;
using System.Net;
using System.Net.Sockets;
using Newtonsoft.Json;

namespace AtmoLight.Targets.Domotica
{
  class DomoticzDomotica : IDomotica
  {
    private bool isConnected;
    private TcpClient client;
    private readonly Core coreObject = Core.GetInstance();

    public DomoticzDomotica(string ip, int port, string devicename)
    {
      IP = ip;
      Port = port;
      DeviceName = devicename;
    }

    public DomoticaProtocol Protocol
    {
      get { return DomoticaProtocol.JSON; }
    }

    public DomoticaCapabilities Capabilities
    {
      get { return DomoticaCapabilities.LightToggle; }
    }

    public DomoticaBrand Brand
    {
      get { return DomoticaBrand.Domoticz; }
    }

    public DomoticaType Type
    {
      get { return DomoticaType.TCP; }
    }

    public string DeviceName { get; private set; }
    public string ID { get; private set; }

    public string IP { get; private set; }

    public string COMport { get; private set; }

    public int Port { get; private set; }

    public void Connect(string ip, int port)
    {
      IP = ip;
      Port = port;
    }
    public void ConnectSerial(string comPort)
    {
      // Not used
    }

    public void Disconnect()
    {
      try
      {
        if (client != null)
        {
          client.Close();
          client = null;
        }
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

    public void ChangeColor(byte red, byte green, byte blue, bool forceLightsOff, string orbId)
    {
      if (!IsConnected())
      {
        return;
      }

      try
      {
        // Send message
      }
      catch (Exception)
      {
        Log.Error("Error during send message..");
      }
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
