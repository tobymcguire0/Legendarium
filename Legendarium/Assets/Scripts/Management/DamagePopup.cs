using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamagePopup : MonoBehaviour
{
    [SerializeField] TMP_Text text;
    [SerializeField] Color damageColor;
    [SerializeField] Color healColor;
    [SerializeField] Color manaHealColor;
    public enum PopupType
    {
        Damage,
        Heal,
        ManaHeal,
    }
    public void Init(int amount,PopupType type)
    {
        text.text = amount.ToString();
        switch (type)
        {
            case PopupType.Damage:
                text.color = damageColor;
                break;
            case PopupType.Heal:
                text.color = healColor;
                break;
            case PopupType.ManaHeal:
                text.color = manaHealColor;
                break;
        }
    }
    public void EndAnimation()
    {
        Destroy(this.gameObject);
    }
}
