using UnityEngine;
using System.Collections;

public static class MenuPrefsSaveManager : object
{

    public static int soundEnabled = 1;

    public static void LoadSoundData()
    {
        soundEnabled = PlayerPrefs.GetInt("SoundEnabled");
        PlayerPrefs.Save();
    }

    public static void SaveSoundData()
    {
        PlayerPrefs.SetInt("SoundEnabled", soundEnabled);
        PlayerPrefs.Save();
    }
}
