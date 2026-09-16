using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hook : MonoBehaviour
{
    [SerializeField] public BoxCollider2D hookArea;
    [SerializeField] public Rigidbody2D _rigidbody;
    public float hookSpeed = 0.1f;
    public float returnSpeed = 0.5f;
    [SerializeField] int MaxFishOnHook;
    private GameObject DestroyOhHook;

    bool Spoil = false;
    int CountSpoil;
    bool Play = true;
    bool isHooking = false;
    Vector3 defaultPosition;

    Camera mainCamera;

    List<InteractFish> fishes;

    void Start()
    {
        ManagerScene.StopGame += StopPlay;
        mainCamera = Camera.main;
        defaultPosition = _rigidbody.position;
    }

    void Update()
    {
        if (Play && !Spoil && !isHooking && Touched())
        {
            Vector3 pos = GetTouchWorldPosition();
            if (OnHookArea(pos))
            {
                fishes = new List<InteractFish>();
                isHooking = true;
                CountSpoil = 0;
                StartCoroutine(Hooking(pos));
                Debug.Log("Hooking");
            }
        }
    }

    bool OnHookArea(Vector3 pos)
    {
        Vector3 hookAreaCenter = hookArea.transform.position;
        Vector3 hookAreaSize = hookArea.bounds.size;

        float yHalfSize = hookAreaSize.y / 2f;
        if (pos.y > (hookAreaCenter.y + yHalfSize) || pos.y < (hookAreaCenter.y - yHalfSize))
        {
            return false;
        }

        float xHalfSize = hookAreaSize.x / 2f;
        if (pos.x > (hookAreaCenter.x + xHalfSize) || pos.x < (hookAreaCenter.x - xHalfSize))
        {
            return false;
        }

        return true;
    }

    IEnumerator Hooking(Vector3 pos)
    {
        transform.rotation = GetLookRotation(transform.position, pos);

        while (!Spoil && Vector3.Distance(_rigidbody.position, pos) >= 0.01f)
        {
            MoveTowards(hookSpeed, pos);
            yield return new WaitForEndOfFrame();


        }

        while (Vector3.Distance(_rigidbody.position, defaultPosition) >= 0.01f)
        {
            MoveTowards(returnSpeed, defaultPosition);
            yield return new WaitForEndOfFrame();
        }

        for (int i = 0; i < fishes.Count; i++)
        {
            if (isHooking == true && fishes[i] != null)
            {
                fishes[i].Catching();
            }

        }
        fishes.Clear();

        transform.rotation = Quaternion.identity;
        isHooking = false;
        Spoil = false;
        CountSpoil = 0;
    }

    void MoveTowards(float speed, Vector3 target)
    {
        _rigidbody.position = Vector3.MoveTowards(_rigidbody.position, target, speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isHooking && !Spoil && collision.TryGetComponent(out fishmove fish) && collision.gameObject.tag == "Fish")
        {
            fish.Catched(transform);
            fishes.Add(collision.GetComponent<InteractFish>());
            CountSpoil++;
            if (CountSpoil >= MaxFishOnHook)
            {
                Spoil = true;
            }
        }
    }
    public void HookActivate()
    {
        Spoil = false;
        CountSpoil -= 1;
        isHooking = false;
    }

    public void StopPlay()
    {
        Play = false;
        ManagerScene.StopGame -= StopPlay;
    }

    bool Touched()
    {
        // Сначала проверяем мышь
        if (Input.GetMouseButtonUp(0))
        {
            return true;
        }

        // Если есть хотя бы одно касание и оно завершилось
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            return true;
        }

        return false;
    }

    Vector3 GetTouchWorldPosition()
    {
        Vector3 pos;

        // Приоритет мыши
        if (Input.GetMouseButtonUp(0))
        {
            pos = Input.mousePosition;
        }
        else if (Input.touchCount > 0)
        {
            pos = Input.GetTouch(0).position;
        }
        else
        {
            pos = Vector3.zero;
        }

        Vector3 worldPos = mainCamera.ScreenToWorldPoint(pos);
        worldPos.z = 0f;
        return worldPos;
    }

    Vector2 GetTouchPosition()
    {
        if (Input.touchSupported)
        {
            return Input.GetTouch(0).position;
        }
        else
        {
            return Input.mousePosition;
        }
    }

    Quaternion GetLookRotation(Vector3 position, Vector3 targetPosition)
    {
        Vector3 dir = targetPosition - position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        angle += 90f;
        return Quaternion.AngleAxis(angle, Vector3.forward);
    }

    public void ReturnToDefault()
    {
        float DefXpos = 0.027f;
        float DefYpos = 0.14f;
        this.transform.position = new Vector2(DefXpos, DefYpos);
        if (isHooking == true)
        {
            HookActivate();
            DestroyOhHook = GameObject.FindGameObjectWithTag("FishOnHook");
            if (DestroyOhHook != null)
            {
                Destroy(DestroyOhHook);
            }
            GameObject[] list = GameObject.FindGameObjectsWithTag("FishOnHook");
            int i = list.Length;
            while (--i >= 0)
            {
                GameObject obj = list[i];
                Destroy(obj);
            }
        }
    }

    //public void MoveHook()
    //{
    //    if (SystemInfo.deviceType == DeviceType.Desktop)
    //    {
    //        Vector2 screeenPosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
    //        Vector2 worldPosition = Camera.main.ScreenToWorldPoint(screeenPosition);
    //        float yOffset = Speed * Time.deltaTime;
    //        float newYpos = worldPosition.y + yOffset;
    //        if (newYpos < TopHook && newYpos > BotHook)
    //        {
    //            transform.position = new Vector2(transform.position.x, newYpos);
    //            //  transform.position = Vector2.Lerp(transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition), Speed);
    //        }
    //        // if (newYpos < TopHook && newYpos > BotHook)
    //        //   {
    //        //     this.transform.position = Vector2.Lerp(transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition), Speed);
    //        //   }
    //        if (worldPosition.y + yOffset > 1.27f && Spoil)
    //        {
    //            Spoil = false;
    //            CountSpoil = 0;
    //        }
    //    }
    //    else if (Input.touchCount > 0)
    //    {
    //        Touch touch = Input.GetTouch(0);
    //        Vector2 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
    //        float yOffset = Speed * Time.deltaTime;
    //        float newYpos = touchPosition.y + yOffset;
    //        if (touchPosition.y + newYpos < TopHook && touchPosition.y + newYpos > BotHook)
    //        {
    //            transform.position = new Vector2(transform.position.x, touchPosition.y + newYpos);

    //        }
    //        if (touchPosition.y + yOffset > 1.27f && Spoil)
    //        {
    //            Spoil = false;
    //            CountSpoil = 0;
    //        }
    //    }
    //}
    public void ReassignSpeed()
    {
        hookSpeed = 9f;
        returnSpeed = 7f;
    }
}
