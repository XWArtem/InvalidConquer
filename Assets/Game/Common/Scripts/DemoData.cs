using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoData {
    private static DemoData _instance;

    public static DemoData Instance {
        get {
            _instance ??= new DemoData();
            return _instance;
        }
    }

    public bool Level1Opened
    {
        get => bool.Parse(PlayerPrefs.GetString("Level1Opened", "true"));
        set => PlayerPrefs.SetString("Level1Opened", value.ToString());
    }
    public bool Level2Opened
    {
        get => bool.Parse(PlayerPrefs.GetString("Level2Opened", "false"));
        set => PlayerPrefs.SetString("Level2Opened", value.ToString());
    }
    public bool Level3Opened
    {
        get => bool.Parse(PlayerPrefs.GetString("Level3Opened", "false"));
        set => PlayerPrefs.SetString("Level3Opened", value.ToString());
    }
    public bool Level4Opened
    {
        get => bool.Parse(PlayerPrefs.GetString("Level4Opened", "false"));
        set => PlayerPrefs.SetString("Level4Opened", value.ToString());
    }
    public bool Level5Opened
    {
        get => bool.Parse(PlayerPrefs.GetString("Level5Opened", "false"));
        set => PlayerPrefs.SetString("Level5Opened", value.ToString());
    }

    public int SubIndex {
        get => PlayerPrefs.GetInt($"Index", 0);
        set => PlayerPrefs.SetInt($"Index", value);
    }

    public bool DemoSound {
        get => bool.Parse(PlayerPrefs.GetString("demoSound", "true"));
        set => PlayerPrefs.SetString("demoSound", value.ToString());
    }

    public bool DemoMusic {
        get => bool.Parse(PlayerPrefs.GetString("demoMusic", "true"));
        set => PlayerPrefs.SetString("demoMusic", value.ToString());
    }
    public float DemoMusicVolume
    {
        get => PlayerPrefs.GetFloat("demoMusicVolume", 0.5f);
        set => PlayerPrefs.SetFloat("demoMusicVolume", value);
    }
    public float DemoSoundVolume
    {
        get => PlayerPrefs.GetFloat("demoSoundVolume", 0.7f);
        set => PlayerPrefs.SetFloat("demoSoundVolume", value);
    }
}