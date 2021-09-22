using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionRay : MonoBehaviour
{
    private Vector3 _collision = Vector3.zero;
    private bool _collided;
    private float _maxdist;

    private void FixedUpdate()
    {
        Vector3 boxPos = new Vector3(transform.position.x, 0.95f, transform.position.z);
        Vector3 boxSize = new Vector3(0.5f, 1.81f, 0.5f);
        RaycastHit hit;
        if (Physics.BoxCast(boxPos, boxSize*0.5f, transform.forward, out hit, transform.rotation))
        {
            _collision = new Vector3(hit.point.x, 0, hit.point.z);
            _collided = true;
        }
        else
        {
            _collided = false;
        }
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position,transform.forward * _maxdist);
        
        Vector3 boxPos = new Vector3(transform.position.x, 0.95f, transform.position.z);
        Vector3 boxSize = new Vector3(0.5f, 1.81f, 0.5f);
        RaycastHit hit;
        if (Physics.BoxCast(boxPos, boxSize*0.5f, transform.forward, out hit, transform.rotation))
        {
            Gizmos.DrawWireCube(boxPos + transform.forward * hit.distance, boxSize);
        }
    }

    public bool Collided()
    {
        return _collided;
    }

    public Vector3 CollisionVector()
    {
        return _collision;
    }

    public void SetMaxditance(float f)
    {
        _maxdist = f;
    }
    
}