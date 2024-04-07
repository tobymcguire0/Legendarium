using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] Image fillImage;
    [SerializeField] float defaultShowTime = 1f;
    [SerializeField] Animator animator;
    bool barVisible = false;
    float showTime = 0;

    public void Display(float fillPercent)
    {
        if(showTime <= 0)
        {
            ShowBar();
        }
        showTime = defaultShowTime;
        fillImage.fillAmount= fillPercent;
    }

    public void Update()
    {
        if (!barVisible) return;
        if(showTime>0)
        {
            showTime-=Time.deltaTime;
        }else
        {
            HideBar();
        }
    }
    void ShowBar()
    {
        barVisible = true;
        animator.SetBool("Show",true);
    }
    void HideBar()
    {
        barVisible = false;
        animator.SetBool("Show",false);
    }



}
