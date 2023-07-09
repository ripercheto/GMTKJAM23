using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrincessEmotions : MonoBehaviour
{
    public float loveDuration = 2;
    public SpriteRenderer rend;
    public Sprite wantsWeapon, sendsLove;

    public void SetWantsWeapon(bool active)
    {
        if (active)
        {
            rend.gameObject.SetActive(true);
            rend.sprite = wantsWeapon;
        }
        else
        {
            rend.sprite = sendsLove;
            StartCoroutine(delaeydDisable());

            IEnumerator delaeydDisable()
            {
                yield return new WaitForSeconds(loveDuration);
                rend.gameObject.SetActive(false);
            }
        }
    }
}