﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediaPortal.Common;
using MediaPortal.Common.Settings;

namespace AtmoLight
{
  public class Settings
  {
    [Setting(SettingScope.User, "C:\\ProgramData\\Team MediaPortal\\MediaPortal\\AtmoWin\\AtmoWinA.exe")]
    public string AtmoWinExe { get; set; }

    [Setting(SettingScope.User, ContentEffect.MediaPortalLiveMode)]
    public ContentEffect VideoEffect { get; set; }

    [Setting(SettingScope.User, ContentEffect.LEDsDisabled)]
    public ContentEffect AudioEffect { get; set; }

    [Setting(SettingScope.User, ContentEffect.LEDsDisabled)]
    public ContentEffect MenuEffect { get; set; }

    [Setting(SettingScope.User, ContentEffect.LEDsDisabled)]
    public ContentEffect MPExitEffect { get; set; }

    [Setting(SettingScope.User, 1)]
    public int OnOffButton { get; set; }

    [Setting(SettingScope.User, 2)]
    public int ProfileButton { get; set; }

    [Setting(SettingScope.User, "00:00")]
    public string ExcludeTimeStart { get; set; }

    [Setting(SettingScope.User, "00:00")]
    public string ExcludeTimeEnd { get; set; }

    [Setting(SettingScope.User, false)]
    public bool ManualMode { get; set; }

    [Setting(SettingScope.User, false)]
    public bool SBS3D { get; set; }

    [Setting(SettingScope.User, false)]
    public bool LowCPU { get; set; }

    [Setting(SettingScope.User, 0)]
    public int LowCPUTime { get; set; }
    
    [Setting(SettingScope.User, false)]
    public bool Delay { get; set; }
    
    [Setting(SettingScope.User, 0)]
    public int DelayTime { get; set; }
    
    [Setting(SettingScope.User, 50)]
    public int DelayRefreshRate { get; set; }
    
    [Setting(SettingScope.User, true)]
    public bool StopAtmoWinOnExit { get; set; }
    
    [Setting(SettingScope.User, true)]
    public bool StartAtmoWinOnStart { get; set; }
    
    [Setting(SettingScope.User, true)]
    public bool RestartAtmoWinOnError { get; set; }
    
    [Setting(SettingScope.User, 0)]
    public int StaticColorRed { get; set; }
        
    [Setting(SettingScope.User, 0)]
    public int StaticColorGreen { get; set; }
        
    [Setting(SettingScope.User, 0)]
    public int StaticColorBlue { get; set; }

    [Setting(SettingScope.User, "")]
    public string GIFFile { get; set; }

    [Setting(SettingScope.User, true)]
    public bool AtmoWinTarget { get; set; }

    [Setting(SettingScope.User, false)]
    public bool HyperionTarget { get; set; }

    [Setting(SettingScope.User, false)]
    public bool HueTarget { get; set; }

    [Setting(SettingScope.User, 64)]
    public int CaptureWidth { get; set; }

    [Setting(SettingScope.User, 64)]
    public int CaptureHeight { get; set; }

    [Setting(SettingScope.User, "127.0.0.1")]
    public string HyperionIP { get; set; }

    [Setting(SettingScope.User, 19445)]
    public int HyperionPort { get; set; }

    [Setting(SettingScope.User, 1)]
    public int HyperionPriority { get; set; }

    [Setting(SettingScope.User, 1)]
    public int HyperionPriorityStaticColor { get; set; }

    [Setting(SettingScope.User, 10000)]
    public int HyperionReconnectDelay { get; set; }

    [Setting(SettingScope.User, 5)]
    public int HyperionReconnectAttempts { get; set; }

    [Setting(SettingScope.User, false)]
    public bool HyperionLiveReconnect { get; set; }

    [Setting(SettingScope.User, "")]
    public string hueExe { get; set; }

    [Setting(SettingScope.User, true)]
    public bool hueStart { get; set; }

    [Setting(SettingScope.User, false)]
    public bool hueIsRemoteMachine { get; set; }

    [Setting(SettingScope.User, "127.0.0.1")]
    public string HueIP { get; set; }

    [Setting(SettingScope.User, 20123)]
    public int HuePort { get; set; }

    [Setting(SettingScope.User, 25)]
    public int HueMinimalColorDifference { get; set; }


    ISettingsManager settingsManager = ServiceRegistration.Get<ISettingsManager>();
    Settings settings;

    public bool LoadAll()
    {
      settings = settingsManager.Load<Settings>();
      AtmoWinExe = settings.AtmoWinExe;
      VideoEffect = settings.VideoEffect;
      AudioEffect = settings.AudioEffect;
      MenuEffect = settings.MenuEffect;
      OnOffButton = settings.OnOffButton;
      ProfileButton = settings.ProfileButton;
      ExcludeTimeStart = settings.ExcludeTimeStart;
      ExcludeTimeEnd = settings.ExcludeTimeEnd;
      ManualMode = settings.ManualMode;
      SBS3D = settings.SBS3D;
      LowCPU = settings.LowCPU;
      LowCPUTime = settings.LowCPUTime;
      Delay = settings.Delay;
      DelayTime = settings.DelayTime;
      DelayRefreshRate = settings.DelayRefreshRate;
      StopAtmoWinOnExit = settings.StopAtmoWinOnExit;
      StartAtmoWinOnStart = settings.StartAtmoWinOnStart;
      RestartAtmoWinOnError = settings.RestartAtmoWinOnError;
      StaticColorBlue = settings.StaticColorBlue;
      StaticColorGreen = settings.StaticColorGreen;
      StaticColorRed = settings.StaticColorRed;
      GIFFile = settings.GIFFile;
      MPExitEffect = settings.MPExitEffect;
      AtmoWinTarget = settings.AtmoWinTarget;
      HyperionTarget = settings.HyperionTarget;
      HueTarget = settings.HueTarget;
      HyperionIP = settings.HyperionIP;
      HyperionLiveReconnect = settings.HyperionLiveReconnect;
      HyperionPort = settings.HyperionPort;
      HyperionPriority = settings.HyperionPriority;
      HyperionPriorityStaticColor = settings.HyperionPriorityStaticColor;
      HyperionReconnectAttempts = settings.HyperionReconnectAttempts;
      HyperionReconnectDelay = settings.HyperionReconnectDelay;
      hueExe = settings.hueExe;
      hueStart = settings.hueStart;
      hueIsRemoteMachine = settings.hueIsRemoteMachine;
      HueIP = settings.HueIP;
      HuePort = settings.HuePort;
      HueMinimalColorDifference = settings.HueMinimalColorDifference;
      CaptureHeight = settings.CaptureHeight;
      CaptureWidth = settings.CaptureWidth;
      return true;
    }

    public bool SaveAll()
    {
      settings.AtmoWinExe = AtmoWinExe;
      settings.VideoEffect = VideoEffect;
      settings.AudioEffect = AudioEffect;
      settings.MenuEffect = MenuEffect;
      settings.OnOffButton = OnOffButton;
      settings.ProfileButton = ProfileButton;
      settings.ExcludeTimeStart = ExcludeTimeStart;
      settings.ExcludeTimeEnd = ExcludeTimeEnd;
      settings.ManualMode = ManualMode;
      settings.SBS3D = SBS3D;
      settings.LowCPU = LowCPU;
      settings.LowCPUTime = LowCPUTime;
      settings.Delay = Delay;
      settings.DelayTime = DelayTime;
      settings.DelayRefreshRate = DelayRefreshRate;
      settings.StopAtmoWinOnExit = StopAtmoWinOnExit;
      settings.StartAtmoWinOnStart = StartAtmoWinOnStart;
      settings.RestartAtmoWinOnError = RestartAtmoWinOnError;
      settings.StaticColorBlue = StaticColorBlue;
      settings.StaticColorGreen = StaticColorGreen;
      settings.StaticColorRed = StaticColorRed;
      settings.GIFFile = GIFFile;
      settings.MPExitEffect = MPExitEffect;
      settings.AtmoWinTarget = AtmoWinTarget;
      settings.HyperionTarget = HyperionTarget;
      settings.HueTarget = HueTarget;
      settings.HyperionIP = HyperionIP;
      settings.HyperionLiveReconnect = HyperionLiveReconnect;
      settings.HyperionPort = HyperionPort;
      settings.HyperionPriority = HyperionPriority;
      settings.HyperionPriorityStaticColor = HyperionPriorityStaticColor;
      settings.HyperionReconnectAttempts = HyperionReconnectAttempts;
      settings.HyperionReconnectDelay = HyperionReconnectDelay;
      settings.hueExe = hueExe;
      settings.hueStart = hueStart;
      settings.hueIsRemoteMachine = hueIsRemoteMachine;
      settings.HueIP = HueIP;
      settings.HuePort = HuePort;
      settings.HueMinimalColorDifference = HueMinimalColorDifference;
      settings.CaptureWidth = CaptureWidth;
      settings.CaptureHeight = CaptureHeight;
      settingsManager.Save(settings);
      return true;
    }
  }
}
