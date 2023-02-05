using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ApplyOnClickSoundToAllButtons : MonoBehaviour
{
    [SerializeField] AudioClip ChosenSound;
    SoundManager _soundManager;

    void Start()
    {
        _soundManager = SoundManager.instance;

        var x = FindObjectsOfType<Button>(true);
        foreach (var item in x)
        {
            item.onClick.AddListener(PlayOnclickSound);
        }
    }

    void PlayOnclickSound() => _soundManager.PlaySound(ChosenSound);
}
