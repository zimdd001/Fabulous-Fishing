using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SimpleTimer : MonoBehaviour
{
    [SerializeField] private float time;
    [SerializeField] private Image timerImage;
    [SerializeField] private Text timerText;

    public float _timeLeft = 0f;

    public static event System.Action OnTimerEnd; // 🆕 событие

    private void Start()
    {
        ManagerScene.StopGame += DisableTimer;
        _timeLeft = time;
        StartCoroutine(StartTimer());
    }

    private IEnumerator StartTimer()
    {
        while (_timeLeft > 0)
        {
            _timeLeft -= Time.deltaTime;
            var normalizedValue = Mathf.Clamp01(_timeLeft / time);
            timerImage.fillAmount = normalizedValue;

            if (timerText != null)
                timerText.text = Mathf.CeilToInt(_timeLeft).ToString();

            yield return null;
        }

        // Когда таймер закончился:
        if (timerText != null)
            timerText.text = "0";

        OnTimerEnd?.Invoke(); // 🟡 вызов события
    }

    public void DisableTimer()
    {
        ManagerScene.StopGame -= DisableTimer;
        Destroy(gameObject);
    }
}
