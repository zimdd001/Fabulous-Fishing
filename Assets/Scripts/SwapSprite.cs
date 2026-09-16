using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwapSprite : MonoBehaviour
{
    public Sprite On;
    public Sprite Off;
    private Image im;
    void Start()
    {
        im = GetComponent <Image>();
        im.sprite = On;
    }
    public void Swap()
    {
        if (im.sprite == On)
        {
            im.sprite = Off;
            return;
        }

        if (im.sprite == Off)
        {
            im.sprite = On;
            return;
        }
    }
}
