using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorMovement: MonoBehaviour
{
    [SerializeField] private float speed = 10.0f;
    private float _minDashRange;
    private float _maxDashRange;
    
    public Transform cam;
    public bool moved = true;

    // Update is called once per frame
    void Update()
    {
        if (!Input.GetMouseButton(0) && moved)
        {
            transform.position = cam.position + cam.forward * _minDashRange;
            //transform.rotation = new Quaternion(0.0f, cam.rotation.y, 0.0f, cam.rotation.w);
        }
    }

    public void MoveCursor()
    {   
        Debug.Log(Vector3.Distance(cam.position, transform.position));
        if (Vector3.Distance(cam.position, transform.position) <= _maxDashRange)
        {
            transform.position = cam.position + cam.forward * Vector3.Distance(cam.position, transform.position);
            transform.position += Time.deltaTime * speed * cam.forward;
            //transform.rotation = new Quaternion(0.0f, cam.rotation.y, 0.0f, cam.rotation.w);
        }
        else
        {
            transform.position = cam.position + cam.forward * _maxDashRange;
            //transform.rotation = new Quaternion(0.0f, cam.rotation.y, 0.0f, cam.rotation.w);
        }
    }

    public void SetRanges(float min, float max)
    {
        _minDashRange = min;
        _maxDashRange = max;
    }

    private void OnCollisionEnter(Collision other)
    {
        Debug.Log("KOLLISION!!!!");
     
    }
}