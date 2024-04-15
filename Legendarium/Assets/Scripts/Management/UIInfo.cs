using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIInfo : MonoBehaviour
{
    [SerializeField] Image healthImageSlider;
    [SerializeField] Image manaImageSlider;
    [SerializeField] Transform keyArea;
    [SerializeField] GameObject keyObjPrefab;

    private void OnEnable()
    {
        PlayerController.OnPlayerManaChange += OnManaChange;
        PlayerController.OnPlayerHealthChange += OnHealthChange;
        PlayerController.OnPlayerKeyCountChange += OnKeyCountChange;
    }
    private void OnDisable()
    {
        PlayerController.OnPlayerManaChange -= OnManaChange;
        PlayerController.OnPlayerHealthChange -= OnHealthChange;
        PlayerController.OnPlayerKeyCountChange -= OnKeyCountChange;
    }
    void OnManaChange(float percent)
    {
        manaImageSlider.fillAmount = percent;

    }
    void OnHealthChange(float percent)
    {
        healthImageSlider.fillAmount = percent;
    }
    void OnKeyCountChange(float numKeys)
    {
        int intNumKeys = Mathf.CeilToInt(numKeys);
        if (keyArea.childCount > intNumKeys)
        {
            for (int i = keyArea.childCount-1; i > intNumKeys-1; i--)
            {
                Destroy(keyArea.GetChild(i).gameObject);
            }
        } else
        {
            for(int i = keyArea.childCount; i< intNumKeys; i++)
            {
                Instantiate(keyObjPrefab, keyArea);
            }
        }
    }
}
