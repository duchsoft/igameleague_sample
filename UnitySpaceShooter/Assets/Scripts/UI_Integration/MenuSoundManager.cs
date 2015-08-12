using UnityEngine;
using System.Collections;

public class MenuSoundManager : MonoBehaviour
{
    public AudioSource soundPlayer;
    public AudioSource musicPlayer;
    public AudioClip buttonClickClip;

    public bool soundEnabled;

    static MenuSoundManager instance;
    public static MenuSoundManager Instance { get { return instance; } }

    void Awake()
    {
        instance = this;
    }

    // Use this for initialization
    void Start()
    {
        Invoke("InitSound", 1);
    }

    void InitSound()
    {
        if (MenuPrefsSaveManager.soundEnabled == 1)
        {
            soundEnabled = true;
            if (musicPlayer)
            {
                musicPlayer.Play();
            }
            MenuPrefsSaveManager.soundEnabled = 1;
               
        }
        else
        {
            soundEnabled = false;
            if (soundPlayer)
            {
                soundPlayer.Stop();
            }
                
            if (musicPlayer)
            {
                musicPlayer.Stop();
            }
                
            MenuPrefsSaveManager.soundEnabled = 0;
        }
        MenuPrefsSaveManager.SaveSoundData();
    }

    public void ChangeSoundState()
    {
        if (soundEnabled)
        {
            soundEnabled = false;
            MenuPrefsSaveManager.soundEnabled = 0;

            if (soundPlayer)
                soundPlayer.Stop();
            if (musicPlayer)
                musicPlayer.Stop();
        }
        else
        {
            soundEnabled = true;
            MenuPrefsSaveManager.soundEnabled = 1;
            if (soundPlayer)
                soundPlayer.Play();
            if (musicPlayer)
                musicPlayer.Play();
        }
        MenuPrefsSaveManager.SaveSoundData();
    }

    public void PlayMenuClick()
    {
        if (buttonClickClip && soundEnabled)
        {
            soundPlayer.clip = buttonClickClip;
            soundPlayer.Play();
        }
    }

    public void OnDisable()
    {
        MenuPrefsSaveManager.SaveSoundData();
    }
}
