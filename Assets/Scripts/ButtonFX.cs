using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonFX : MonoBehaviour
{
    public AudioSource buttonFX;
    public AudioClip   clickFX;
    
    public void ClickSound()
    {
      buttonFX.PlayOneShot(clickFX);
    }
}
