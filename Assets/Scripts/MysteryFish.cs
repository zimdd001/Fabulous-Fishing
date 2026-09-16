using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MysteryFish : MonoBehaviour
{
 
   public int MysteryScoreAdd;
   public List<int> rarest;
   private int totalChance;
   public int randomScore;
   private  int [] table =
   {
    45,
    35,
    20,
   };
   
   void Start()
   {
    randomAddScore();
   }
   private void randomAddScore()
   {
      foreach(var item in table)
      {
        totalChance += item;
      }
      randomScore = Random.Range(0,totalChance);
      foreach(var weight in table)

    for(int i = 0; i < table.Length; i++)
   {
    if(randomScore<= table[i])
    {
     MysteryScoreAdd = rarest[i];
     Debug.Log(rarest[i]);
     return;
    }
    else
    {
      randomScore-=table[i];
    }
   }
   }
}