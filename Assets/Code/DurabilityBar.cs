using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;

public class DurabilityBar : MonoBehaviour
{
    public Attack attack;
    public Image image;
    
        private void Awake()
    {
        attack.onDurabilityChanged += SetFill;
    }

    void SetFill(float a)
    {
        image.fillAmount= a;
    }
}
