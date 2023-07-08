using System.Collections;
using UnityEngine;
public class FlashController : MonoBehaviour
{
    public Renderer renderer;
    public float delay = 0.1f;
    public float restoreDuration = 0.1f;
    private MaterialPropertyBlock propertyBlock;

    private void Awake()
    {
        propertyBlock = new MaterialPropertyBlock();
    }

    public void Flash()
    {
        StopAllCoroutines();
        StartCoroutine(FlashCoroutine());
    }

    private IEnumerator FlashCoroutine()
    {
        renderer.GetPropertyBlock(propertyBlock);
        propertyBlock.SetFloat("_FlashAmount", 1);
        renderer.SetPropertyBlock(propertyBlock);
        yield return new WaitForSeconds(delay);
        var t = 0f;
        while (t < 1)
        {
            t += Time.deltaTime / restoreDuration;
            propertyBlock.SetFloat("_FlashAmount", 1 - t);
            renderer.SetPropertyBlock(propertyBlock);
            yield return null;
        }

        propertyBlock.SetFloat("_FlashAmount", 0);
        renderer.SetPropertyBlock(propertyBlock);
    }
}