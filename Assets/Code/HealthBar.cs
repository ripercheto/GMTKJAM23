using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;

public class HealthBar : MonoBehaviour
{
    public Health health;
    public Image image;
    
        private void Awake()
    {
        health.onHealthChaned += SetFill;
    }
[Button]
    void SetFill(float a)
    {
        image.fillAmount= a;
    }
}
