﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Drawing;
using System.IO;
using System.Windows.Media.Imaging;
using System.Drawing.Imaging;
using System.Net.Sockets;
using System.Linq;
using proto;

namespace AtmoLight.Targets
{
  class HyperionHandler : ITargets
  {
    #region Fields

    public Target Name { get { return Target.Hyperion; } }

    private static TcpClient Socket = new TcpClient();
    private Stream Stream;
    private Boolean Connected = false;
    int hyperionReconnectCounter = 0;
    public int hyperionReconnectAttempts = 0;

    public string hyperionIP = "";
    public int hyperionPort = 0;
    public int hyperionPriority = 0;
    public int hyperionPriorityStaticColor = 0;
    public bool hyperionLiveReconnect = false;
    private int[] staticColor = { 0, 0, 0 };
    public Boolean hyperionReconnectOnError = false;
    public int hyperionReconnectDelay = 0;
    private bool disableOnExit;

    #endregion
    #region Hyperion

    public void Initialise(bool force = false)
    {
      try
      {
        Connect();
        ClearPriority(hyperionPriority);
        ChangeEffect(Core.GetCurrentEffect());
      }
      catch (Exception e)
      {
        Log.Error("Hyperion: Error during initialise");
        Log.Error("Exception: {0}", e.Message);
      }
    }

    public void ReInitialise(bool force = false)
    {
      //Reset reconnect counter if needed
      hyperionReconnectCounter = 0;
      Connect();
    }

    public void Dispose()
    {
      if (Socket.Connected)
      {
        if (disableOnExit)
        {
          ChangeEffect(ContentEffect.LEDsDisabled);
        }
        else
        {
          ClearPriority(hyperionPriority);
          ClearPriority(hyperionPriorityStaticColor);
        }
        Socket.Close();
      }
    }
    public bool IsConnected()
    {
      if (hyperionLiveReconnect)
      {
        Connect();
      }
      return Connected;
    }

    private void Connect()
    {
      Thread t = new Thread(ConnectThread);
      t.Start();
    }
    private void ConnectThread()
    {
      while (hyperionReconnectCounter <= hyperionReconnectAttempts)
      {
        if (Connected == false)
        {
          try
          {
            if (hyperionLiveReconnect == false)
            {
              Log.Debug("Hyperion: Trying to connect");
            }

            //Close old socket and create new TCP client which allows it to reconnect when calling Connect()
            try
            {
              Socket.Close();
            }
            catch (Exception e)
            {
              if (hyperionLiveReconnect == false)
              {
                Log.Error("Hyperion: Error while closing socket");
                Log.Error("Exception: {0}", e.Message);
              }
            }
            Socket = new TcpClient();

            Socket.SendTimeout = 5000;
            Socket.ReceiveTimeout = 5000;
            Socket.Connect(hyperionIP, hyperionPort);
            Stream = Socket.GetStream();
            Connected = Socket.Connected;

            if (hyperionLiveReconnect == false)
            {
              Log.Debug("Hyperion: Connected");
            }
          }
          catch (Exception e)
          {
            if (hyperionLiveReconnect == false)
            {
              Log.Error("Hyperion: Error while connecting");
              Log.Error("Exception: {0}", e.Message);
            }
            Connected = false;

          }

          //if live connect enabled don't use this loop and let IsConnected fire up new connections
          if (hyperionLiveReconnect)
          {
            break;
          }
          else
          {
            //Increment times tried
            hyperionReconnectCounter++;
          }

          //Sleep for specified time
          Thread.Sleep(hyperionReconnectDelay);

          //Log.Error("Hyperion: retry attempt {0} of {1}",hyperionReconnectCounter,hyperionReconnectAttempts);
        }
        else
        {
          //Log.Debug("Hyperion: Connected after {0} attempts.", hyperionReconnectCounter);
          hyperionReconnectCounter = 0;
          break;
        }
      }
    }

    public void ChangeColor(int red, int green, int blue)
    {
      ColorRequest colorRequest = ColorRequest.CreateBuilder()
        .SetRgbColor((red * 256 * 256) + (green * 256) + blue)
        .SetPriority(hyperionPriorityStaticColor)
        .SetDuration(-1)
        .Build();

      HyperionRequest request = HyperionRequest.CreateBuilder()
        .SetCommand(HyperionRequest.Types.Command.COLOR)
        .SetExtension(ColorRequest.ColorRequest_, colorRequest)
        .Build();

      SendRequest(request);
    }
    public void ClearPriority(int priority)
    {
      ClearRequest clearRequest = ClearRequest.CreateBuilder()
      .SetPriority(priority)
      .Build();

      HyperionRequest request = HyperionRequest.CreateBuilder()
      .SetCommand(HyperionRequest.Types.Command.CLEAR)
      .SetExtension(ClearRequest.ClearRequest_, clearRequest)
      .Build();

      SendRequest(request);
    }
    public void ClearAll()
    {
      HyperionRequest request = HyperionRequest.CreateBuilder()
      .SetCommand(HyperionRequest.Types.Command.CLEARALL)
      .Build();

      SendRequest(request);
    }
    public bool ChangeEffect(ContentEffect effect)
    {
      switch (effect)
      {
        case ContentEffect.LEDsDisabled:
        case ContentEffect.Undefined:
          ClearPriority(hyperionPriority);
          ClearPriority(hyperionPriorityStaticColor);
          break;
        case ContentEffect.StaticColor:
          ChangeColor(staticColor[0], staticColor[1], staticColor[2]);
          break;
      }
      return true;
    }
    public void ChangeProfile()
    {
      return;
    }
    public void ChangeImage(byte[] pixeldata, byte[] dummy)
    {
      // Hyperion expects the bytestring to be the size of 3*width*height.
      // So 3 bytes per pixel, as in RGB.
      // Given pixeldata however is 4 bytes per pixel, as in RGBA.
      // So we need to remove the last byte per pixel.
      byte[] newpixeldata = new byte[Core.GetCaptureHeight() * Core.GetCaptureWidth() * 3];
      int x = 0;
      int i = 0;
      while (i <= (newpixeldata.GetLength(0) - 2))
      {
        newpixeldata[i] = pixeldata[i + x + 2];
        newpixeldata[i + 1] = pixeldata[i + x + 1];
        newpixeldata[i + 2] = pixeldata[i + x];
        i += 3;
        x++;
      }

      ImageRequest imageRequest = ImageRequest.CreateBuilder()
        .SetImagedata(Google.ProtocolBuffers.ByteString.CopyFrom(newpixeldata))
        .SetImageheight(Core.GetCaptureHeight())
        .SetImagewidth(Core.GetCaptureWidth())
        .SetPriority(hyperionPriority)
        .SetDuration(-1)
        .Build();

      HyperionRequest request = HyperionRequest.CreateBuilder()
        .SetCommand(HyperionRequest.Types.Command.IMAGE)
        .SetExtension(ImageRequest.ImageRequest_, imageRequest)
        .Build();

      SendRequest(request);
    }

    private void SendRequest(HyperionRequest request)
    {
      if (Socket.Connected)
      {
        int size = request.SerializedSize;

        Byte[] header = new byte[4];
        header[0] = (byte)((size >> 24) & 0xFF);
        header[1] = (byte)((size >> 16) & 0xFF);
        header[2] = (byte)((size >> 8) & 0xFF);
        header[3] = (byte)((size) & 0xFF);

        int headerSize = header.Count();
        Stream.Write(header, 0, headerSize);
        request.WriteTo(Stream);
        Stream.Flush();
        HyperionReply reply = receiveReply();
      }
      else
      {
        Connected = false;
        Connect();
      }
    }

    private HyperionReply receiveReply()
    {
      Stream input = Socket.GetStream();
      byte[] header = new byte[4];
      input.Read(header, 0, 4);
      int size = (header[0] << 24) | (header[1] << 16) | (header[2] << 8) | (header[3]);
      byte[] data = new byte[size];
      input.Read(data, 0, size);
      HyperionReply reply = HyperionReply.ParseFrom(data);
      return reply;
    }
    #endregion

    #region settings
    public void SetStaticColor(int red, int green, int blue)
    {
      staticColor[0] = red;
      staticColor[1] = green;
      staticColor[2] = blue;
    }

    public void setReconnectOnError(Boolean reconnectOnError)
    {
      hyperionReconnectOnError = reconnectOnError;
    }

    public void SetDisableOnExit(bool disable)
    {
      disableOnExit = disable;
    }
    #endregion
  }
}
