using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Timers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WinState : MonoBehaviour
{
    [SerializeField] FadeUI fadeUI;
    [SerializeField] TMP_Text completionTimeText;

    [SerializeField] Button mainMenuButton;

    void Start()
    {
        mainMenuButton.onClick.AddListener(() =>
        {
            // In case timescale persists through levels
            Time.timeScale = 1;
        });
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<player>(out var player))
        {
            // Level Complete
            LevelComplete();
        }
    }

    void LevelComplete()
    {
        var timeValue = TimerController.Instance.TimerValue;
        var time = TimeSpan.FromSeconds(timeValue);
        var stringBuilder = new StringBuilder();
        stringBuilder.AppendLine(
            $"You're Time Was | {time.Minutes}:{time.Seconds.ToString().PadLeft(2, '0')}.{time.Milliseconds.ToString().PadLeft(3, '0')}\n");
        
        if (timeValue <= TimerController.Instance.FirstPlaceTime) stringBuilder.AppendLine("FIRST PLACE!");
        else if (timeValue <= TimerController.Instance.SecondPlaceTime) stringBuilder.AppendLine("SECOND PLACE!");
        else if (timeValue <= TimerController.Instance.ThirdPlaceTime) stringBuilder.AppendLine("THIRD PLACE!");
        else stringBuilder.AppendLine("Unfortunately You're Equivalent to a snail :(");

        completionTimeText.text = stringBuilder.ToString();
        
        fadeUI.ShowUI(.5f);

        IEnumerator QuickPause()
        {
            yield return Helper.GetWait(1);
            Time.timeScale = 0;
        }
    }
}
