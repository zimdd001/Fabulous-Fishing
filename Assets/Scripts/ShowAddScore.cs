using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowAddScore : MonoBehaviour
{
    Text ShowScoreText;
    public int Showaddscore;
    Animator anim;
    private int countfish = 0;
    Text childText;
   
    void Start()
    {
     ShowScoreText = GetComponent<Text>();
     ShowScoreText.text = Showaddscore.ToString(); 
     anim = GetComponent<Animator>();  
    }
    public void EnableShow()
    {
       anim.SetTrigger("Show");
       Invoke("DisabledShow",0.2f);
    }
    public void ShowScore(int showScore)
    {
     countfish ++;
     if(countfish <= 1)
     {
         Showaddscore = showScore;
     }
       if(countfish >= 2)
     {
      Showaddscore = Showaddscore + showScore;
     }
     if(Showaddscore <= -1)
     {
      transform.GetChild(0).gameObject.SetActive(false);
      Invoke("ChildActive",1f);
     }
     ShowScoreText.text = Showaddscore.ToString(); 
     print(countfish);
    }
    private void DisabledShow()
    {
    anim.ResetTrigger("Show");
    Showaddscore = 0;
    countfish = 0;
    }
    private void ChildActive()
    {
    transform.GetChild(0).gameObject.SetActive(true); 
    }
}
