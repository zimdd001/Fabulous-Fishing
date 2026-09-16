using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwapHooks : MonoBehaviour
{
    public GameObject SingleH,DoubleH;
    EnableBaitButton EnabledBait;
    EnableBaitButton DisabledBait;
    private int Baitbuttint;
    Score Baitscore;
    Hook Dh;
    ButtonFX PlayFX;
    
    private void Start()
    {
      PlayFX = this.GetComponent<ButtonFX>();
    }
    
    void Update()
    {
      SwapKey();
    }
    public void SwapKey()
    {
      if(GameObject.FindGameObjectWithTag("FishOnHook")==null)
      {
         if(Input.GetKeyDown(KeyCode.E))
       {
        SwitchHook();
        SwitchBaitButt();
        Dh = GameObject.FindWithTag("Player").GetComponent<Hook>();
        Dh.ReturnToDefault();
        PlayFX.ClickSound();
       }
      }
    }
    
    public void SwitchHook()
     {
        DoubleH.SetActive(!DoubleH.activeSelf);
        SingleH.SetActive(!SingleH.activeSelf);
     }
    
    public void SwitchBaitButt()
    {
       if(SingleH.activeSelf)
       {
        EnabledBait = GameObject.Find("Player").GetComponent<EnableBaitButton>();
        EnabledBait.BlockBait = false;
        if(GameObject.Find("Score") != null)
         {
          Baitscore = GameObject.Find("Score").GetComponent<Score>();
          Baitbuttint = Score.score;
         }
         if(Baitbuttint>=1)
         {
          EnabledBait.EnabledButton();
         }
       }
       if(DoubleH.activeSelf)
       {
        DisabledBait = GameObject.Find("Player").GetComponent<EnableBaitButton>();
        DisabledBait.DisBait();
        DisabledBait.BlockBait = true;
       }
    }
}
