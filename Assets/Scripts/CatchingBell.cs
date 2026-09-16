using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatchingBell : MonoBehaviour
{
    public AudioSource Bells;
    public AudioClip CatchBell;
    void Start()
    {
     Bells.PlayOneShot(CatchBell);
    }
}
