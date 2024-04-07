using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIInfo : MonoBehaviour
{
    [SerializeField] Image healthImageSlider;
    [SerializeField] Image manaImageSlider;
    [SerializeField] GameObject keyObjPrefab;

    private void OnEnable()
    {
        PlayerController.OnPlayerManaChange += OnManaChange;
        PlayerController.OnPlayerHealthChange += OnHealthChange;
    }
    private void OnDisable()
    {
        PlayerController.OnPlayerManaChange -= OnManaChange;
        PlayerController.OnPlayerHealthChange -= OnHealthChange;
    }
    void OnManaChange(float percent)
    {
        manaImageSlider.fillAmount = percent;

    }
    void OnHealthChange(float percent)
    {
        healthImageSlider.fillAmount = percent;
    }
}
