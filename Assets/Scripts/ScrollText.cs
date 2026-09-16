using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollText : MonoBehaviour
{
    public bool repeat;
    public int scrollSpeed;
    public GameObject textToScroll;
    private Rect screen;

    public void Start()
    {
      
        Canvas menuCanvas = gameObject.GetComponentInParent<Canvas>();

        Vector3 canvasWorldPointZero = menuCanvas.worldCamera.ScreenToWorldPoint(Vector3.zero);
        Vector3 canvasWorldPointWH = menuCanvas.worldCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height));

        screen = new Rect(canvasWorldPointZero,
            new Vector2(canvasWorldPointWH.x - canvasWorldPointZero.x, canvasWorldPointWH.y - canvasWorldPointZero.y));

    }
    public void Update()
    {
        Vector3[] wc = new Vector3[4];

        textToScroll.GetComponent<RectTransform>().GetWorldCorners(wc);

        Rect rect = new Rect(wc[0].x, wc[0].y, wc[2].x - wc[0].x, wc[2].y - wc[0].y);


        
        if(rect.Overlaps(screen))
        {
            
            textToScroll.transform.Translate(Vector3.up * (scrollSpeed * Time.deltaTime));
        }
    }
}
