using System;
using UnityEngine;

public class Test : MonoBehaviour
{
    public GameObject t1;
    public GameObject t2;
    public GameObject t3;
    public GameObject t4;

    private Vector2 relativeRight1;
    private Vector2 relativeRight2;
    private Vector2 relativeRight3;
    private Vector2 relativeRight4;


    private void Start()
    {
        relativeRight1 = transform.TransformPoint(new Vector2(0.33f,0));
        relativeRight2 = transform.TransformPoint(new Vector2(0.99f,0));
        relativeRight3 = transform.TransformPoint(new Vector2(-0.33f,0));
        relativeRight4 = transform.TransformPoint(new Vector2(-0.99f,0));
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            t1.transform.position = (Vector3)relativeRight1;
            t2.transform.position = (Vector3)relativeRight2;
            t3.transform.position = (Vector3)relativeRight3;
            t4.transform.position = (Vector3)relativeRight4;
        }
    }
}