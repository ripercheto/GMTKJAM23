using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : GameBehaviour
{
    public Transform target;
    public float duration = 0.1f;
    private bool shouldFaceRight = true;
    private bool isFacingRight = true;
    private bool isRotating;

    private Quaternion startRotation;

    private void Awake()
    {
        startRotation = target.localRotation;
    }

    private void Update()
    {
        shouldFaceRight = movement.desiredVelocity.x > 0;
        if (isFacingRight != shouldFaceRight && !isRotating)
        {
            StartCoroutine(Rotate());
        }
    }

    IEnumerator Rotate()
    {
        isRotating = true;
        var t = 0f;
        var start = Quaternion.Euler(0, isFacingRight ? 0 : 180, 0);
        var end = Quaternion.Euler(0, shouldFaceRight ? 0 : 180, 0);
        while (t < 1f)
        {
            t += Time.deltaTime / duration;
            var rot = Quaternion.Lerp(start, end, t);
            target.localRotation = startRotation * rot;
            yield return null;
        }
        isFacingRight = shouldFaceRight;
        isRotating = false;
    }
}