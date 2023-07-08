using System.Collections;
using UnityEngine;

public class FlashController : MonoBehaviour
{
    public Renderer rend;
    public float delay = 0.1f;
    public float restoreDuration = 0.1f;
    private MaterialPropertyBlock propertyBlock;

    private void Awake()
    {
        InitializePropertyBlock();
    }

    private void InitializePropertyBlock()
    {
        propertyBlock ??= new MaterialPropertyBlock();
    }

    public void ResetFlash()
    {
        InitializePropertyBlock();
        rend.GetPropertyBlock(propertyBlock);
        propertyBlock.SetFloat("_FlashAmount", 0);
        rend.SetPropertyBlock(propertyBlock);
    }

    public void Flash(Color color)
    {
        StopAllCoroutines();
        StartCoroutine(FlashCoroutine());

        IEnumerator FlashCoroutine()
        {
            rend.GetPropertyBlock(propertyBlock);
            propertyBlock.SetFloat("_FlashAmount", 1);
            propertyBlock.SetColor("_FlashColor", color);
            rend.SetPropertyBlock(propertyBlock);
            yield return new WaitForSeconds(delay);
            var t = 0f;
            while (t < 1)
            {
                t += Time.deltaTime / restoreDuration;
                propertyBlock.SetFloat("_FlashAmount", 1 - t);
                rend.SetPropertyBlock(propertyBlock);
                yield return null;
            }

            propertyBlock.SetFloat("_FlashAmount", 0);
            rend.SetPropertyBlock(propertyBlock);
        }
    }
}