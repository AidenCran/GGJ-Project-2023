using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TimerController : MonoBehaviour
{
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
    const float FirstPlaceTime = 90f;
    const float SecondPlaceTime = 150f;
    const float ThirdPlaceTime = 180f;
    
    float _timerValue;
    
    void Start()
    {
        // Setting Max Val = Time to beat
        firstPlaceSlider.DOValue(firstPlaceSlider.maxValue, FirstPlaceTime).SetEase(Ease.InQuad);
        firstColour.DOColor(endColor, FirstPlaceTime);
        
        secondPlaceSlider.DOValue(secondPlaceSlider.maxValue, SecondPlaceTime).SetEase(Ease.InQuad);;
        secondColour.DOColor(endColor, SecondPlaceTime);

        thirdPlaceSlider.DOValue(thirdPlaceSlider.maxValue, ThirdPlaceTime).SetEase(Ease.InQuad);;
        thirdColour.DOColor(endColor, ThirdPlaceTime);

        firstPlaceText.text = $"FIRST\n<size=15>{FirstPlaceTime} Seconds</size>";
        secondPlaceText.text = $"SECOND\n<size=15>{SecondPlaceTime} Seconds</size>";
        thirdPlaceText.text = $"THIRD\n<size=15>{ThirdPlaceTime} Seconds</size>";
    }

    void Update()
    {
        _timerValue += 1 * Time.deltaTime;
        var time = TimeSpan.FromSeconds(_timerValue);
        totalTimerText.text = $"{time.Minutes}:{time.Seconds}";
    }
}
