using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BaitKey : MonoBehaviour
{
    
    void Update()
    {
     if(GameObject.Find("SingleHook")!=null && Score.score!=0)
     {
       if(Input.GetKeyDown(KeyCode.Q))
      {
      Button MyButton = this.GetComponent<Button>();
      MyButton.GetComponent<Button>().onClick.Invoke();
      }
     }
    }
     
}