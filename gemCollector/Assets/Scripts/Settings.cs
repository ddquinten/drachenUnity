using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Settings : MonoBehaviour
{

    public AudioMixer am;
    public void setMusic(float volume)
    {
        am.SetFloat("Music", volume);
    }
    public void setMaster(float volume)
    {
        am.SetFloat("Master", volume);
    }
}
