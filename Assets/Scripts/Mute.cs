using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mute : MonoBehaviour
{
    private AudioListener Meinlist;
    private bool Switch;

    void Start()
    {
    //   Meinlist = GetComponent<AudioListener>();  
    }
    public void  SwitchSound()
    {
     if(!Switch)
     {
      AudioListener.volume = 0f;
      Switch = true;
     }
     else if(Switch)
     {
      AudioListener.volume = 1f;
      Switch = false;
     }
    }

}
