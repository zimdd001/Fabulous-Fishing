using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeaBait : MonoBehaviour
{
Score baitboard;
[SerializeField] int AddBaitScore = 1;
  

  void Start()
  {
     baitboard = FindObjectOfType<Score>(); 
     baitboard.BaitScore(AddBaitScore);
     print("Малая рыба");
  }
  
    
     
    
    
   
              

 
 
}
