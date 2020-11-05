using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EasingSample : MonoBehaviour
{
    public bool isDrawBox = true;
    public bool isDrawGridL = false;
    public bool isDrawGridM = false;
    public bool isDrawLoad = false;
    [SerializeField] EasingTest easingTest;


    private void OnDrawGizmos()
    {
        if (isDrawBox)
        {
            Gizmos.DrawLine(transform.position + new Vector3(1, 1, 0), transform.position + new Vector3(1, 0, 0));
            Gizmos.DrawLine(transform.position + new Vector3(1, 0, 0), transform.position + new Vector3(0, 0, 0));
            Gizmos.DrawLine(transform.position + new Vector3(0, 0, 0), transform.position + new Vector3(0, 1, 0));
            Gizmos.DrawLine(transform.position + new Vector3(0, 1, 0), transform.position + new Vector3(1, 1, 0));
        }
        if (isDrawGridL)
        {
            Gizmos.DrawLine(transform.position + new Vector3(0, 0.5f, 0), transform.position + new Vector3(1, 0.5f, 0));
            Gizmos.DrawLine(transform.position + new Vector3(0.5f, 0, 0), transform.position + new Vector3(0.5f, 1, 0));
        }
        if (isDrawGridM)
        {
            Gizmos.DrawLine(transform.position + new Vector3(0, 0.25f, 0), transform.position + new Vector3(1, 0.25f, 0));
            Gizmos.DrawLine(transform.position + new Vector3(0.25f, 0, 0), transform.position + new Vector3(0.25f, 1, 0));
            Gizmos.DrawLine(transform.position + new Vector3(0, 0.75f, 0), transform.position + new Vector3(1, 0.75f, 0));
            Gizmos.DrawLine(transform.position + new Vector3(0.75f, 0, 0), transform.position + new Vector3(0.75f, 1, 0));
        }
        if (isDrawLoad)
        {
            for (float x = 0; x <= 1; x += 0.01F)
            {
                float y = Easing.GetEasing(x, easingTest.type);
                Gizmos.DrawSphere(transform.position + new Vector3(x, y, 0), 0.01f);
            }
        }
    }
}