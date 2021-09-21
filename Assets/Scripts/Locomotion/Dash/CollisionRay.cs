using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionRay : MonoBehaviour
{
    public Vector3 collision = Vector3.zero;
    public Vector3 giz = Vector3.zero;
    public bool collided;
    public float maxdist;

    private void FixedUpdate()
    {
        Vector3 boxPos = new Vector3(transform.position.x, 0.95f, transform.position.z);
        Vector3 boxSize = new Vector3(0.5f, 1.81f, 0.5f);
        RaycastHit hit;
        if (Physics.BoxCast(boxPos, boxSize*0.5f, transform.forward, out hit, transform.rotation))
        {
            collision = new Vector3(hit.point.x, 0, hit.point.z);
            giz = hit.point;
            collided = true;
        }
        else
        {
            collided = false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position,transform.forward * maxdist);
        
        Vector3 boxPos = new Vector3(transform.position.x, 0.95f, transform.position.z);
        Vector3 boxSize = new Vector3(0.5f, 1.81f, 0.5f);
        RaycastHit hit;
        if (Physics.BoxCast(boxPos, boxSize*0.5f, transform.forward, out hit, transform.rotation))
        {
            Gizmos.DrawWireCube(boxPos + transform.forward * hit.distance, boxSize);
        }
    }
}