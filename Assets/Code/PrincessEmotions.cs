using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrincessEmotions : MonoBehaviour
{
    public float loveDuration = 2;
    public SpriteRenderer rend;
    public Sprite wantsWeapon, sendsLove, needsHealth;

    private bool isNeedsHealth;
    private bool isNeedsWeapon;

    public void SetNeedsHealth(bool active)
    {
        isNeedsHealth = active;
        HandleEmotions();
    }

    public void SetWantsWeapon(bool active)
    {
        isNeedsWeapon = active;
        HandleEmotions();
    }

    private void HandleEmotions()
    {
        if (isNeedsHealth)
        {
            rend.enabled = true;
            rend.sprite = needsHealth;
        }
        else if (isNeedsWeapon)
        {
            rend.enabled = true;
            rend.sprite = wantsWeapon;
        }
        else
        {
            rend.sprite = sendsLove;
            StartCoroutine(delaeydDisable());

            IEnumerator delaeydDisable()
            {
                yield return new WaitForSeconds(loveDuration);
                rend.enabled = false;
                HandleEmotions();
            }
        }
    }
}