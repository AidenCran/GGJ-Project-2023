using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class TimerController : MonoBehaviour
{
    #region Singleton

    public static TimerController Instance { get; private set; }

    private void Singleton()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }

    #endregion

    
    [SerializeField] TMP_Text totalTimerText;
    
    [Space]
    [SerializeField] TMP_Text firstPlaceText;
    [SerializeField] TMP_Text secondPlaceText;
    [SerializeField] TMP_Text thirdPlaceText;

    [Space]
    [SerializeField] Slider firstPlaceSlider;
    [SerializeField] Slider secondPlaceSlider;
    [SerializeField] Slider thirdPlaceSlider;

    [Space] 
    [SerializeField] Image firstColour;
    [SerializeField] Image secondColour; 
    [SerializeField] Image thirdColour;
    
    [SerializeField] Color endColor;
    
    // Time in seconds
    public float FirstPlaceTime = 90f;
    public float SecondPlaceTime = 150f;
    public float ThirdPlaceTime = 180f;
    
    float _timerValue;

    [HideInInspector] public UnityEvent OnFirstPlaceTimeout;
    [HideInInspector] public UnityEvent OnSecondPlaceTimeout;
    [HideInInspector] public UnityEvent OnThirdPlaceTimeout;
    
    void Start()
    {
        // Setting Max Val = Time to beat
        firstPlaceSlider.DOValue(0f, FirstPlaceTime).SetEase(Ease.InQuad);
        firstColour.DOColor(endColor, FirstPlaceTime).onComplete += () => OnFirstPlaceTimeout?.Invoke();
        
        secondPlaceSlider.DOValue(0f, SecondPlaceTime).SetEase(Ease.InQuad);;
        secondColour.DOColor(endColor, SecondPlaceTime).onComplete += () => OnSecondPlaceTimeout?.Invoke();

        thirdPlaceSlider.DOValue(0f, ThirdPlaceTime).SetEase(Ease.InQuad);;
        thirdColour.DOColor(endColor, ThirdPlaceTime).onComplete += () => OnThirdPlaceTimeout?.Invoke();

        firstPlaceText.text = $"FIRST\n<size=15>{FirstPlaceTime} Seconds</size>";
        secondPlaceText.text = $"SECOND\n<size=15>{SecondPlaceTime} Seconds</size>";
        thirdPlaceText.text = $"THIRD\n<size=15>{ThirdPlaceTime} Seconds</size>";
    }

    void Update()
    {
        _timerValue += 1 * Time.deltaTime;
        var time = TimeSpan.FromSeconds(_timerValue);
        totalTimerText.text = $"{time.Minutes}:{time.Seconds.ToString().PadLeft(2,'0')}.{time.Milliseconds.ToString().PadLeft(3,'0')}";
    }
}
