using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarController : MonoBehaviour
{
    public Slider slider; // 用於控制血條的Slider元素
    public Gradient gradient; // 用於控制血條顏色的漸變
    public Image fill; // 血條的填充部分

    public void SetMaxHealth(float maxHealth)
    {
        slider.maxValue = maxHealth;
        slider.value = maxHealth;

        // 更新血條顏色
        fill.color = gradient.Evaluate(0f);
    }

    public void SetHealth(float health)
    {
        slider.value = health;

        // 更新血條顏色
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }
}
