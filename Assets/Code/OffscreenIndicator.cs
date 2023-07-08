using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OffscreenIndicator : MonoBehaviour
{
    public Camera cam;
    public GameObject target;
    public RectTransform element;
    public RectTransform canvasRectTransform;

    private void Update()
    {
        //0,0 for the canvas is at the center of the screen, whereas WorldToViewPortPoint treats the lower left corner as 0,0. Because of this, you need to subtract the height / width of the canvas * 0.5 to get the correct position.
        var viewportPos = cam.WorldToViewportPoint(target.transform.position);
        if (viewportPos.x > 0 && viewportPos.x < 1 && viewportPos.y > 0 && viewportPos.y < 1)
        {
            element.gameObject.SetActive(false);
            return;
        }

        element.gameObject.SetActive(true);
        var sizeDelta = canvasRectTransform.sizeDelta;

        var center = new Vector2(0.5f * sizeDelta.x - sizeDelta.x * 0.5f, 0.5f * sizeDelta.y - sizeDelta.y * 0.5f);
        var max = new Vector2(sizeDelta.x - sizeDelta.x * 0.5f, sizeDelta.y - sizeDelta.y * 0.5f);
        var screenPos = new Vector2(viewportPos.x * sizeDelta.x - sizeDelta.x * 0.5f, viewportPos.y * sizeDelta.y - sizeDelta.y * 0.5f);

        var elementHalfSize = element.rect.size * 0.5f;

        screenPos.x = Mathf.Clamp(screenPos.x, canvasRectTransform.rect.position.x + elementHalfSize.x, max.x - elementHalfSize.x);
        screenPos.y = Mathf.Clamp(screenPos.y, canvasRectTransform.rect.position.y + elementHalfSize.y, max.y - elementHalfSize.y);

        var dir = screenPos - center.normalized;
        element.rotation = Quaternion.Euler(0, 0, Vector2.Angle(Vector2.up, dir));
        element.anchoredPosition = screenPos;
    }
}