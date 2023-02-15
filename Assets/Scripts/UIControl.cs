using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIControl : MonoBehaviour
{
    private StatusCharacter statusCharacter;
    private Slider slider;

    private void Start()
    {
        slider = GetComponentInChildren<Slider>();
        statusCharacter = GetComponentInParent<StatusCharacter>();
        slider.maxValue = statusCharacter.Life;
    }
    private void Update()
    {
        slider.value = statusCharacter.Life;
    }
}
