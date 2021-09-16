using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionRay : MonoBehaviour
{
    public Vector3 collision = Vector3.zero;
    public bool collided;
    public float maxdist;

    // Update is called once per frame
    void Update()
    {
        var pos = new Vector3(this.transform.position.x, 0, this.transform.position.z);
        var ray = new Ray(pos, this.transform.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, maxdist))
        {
            //Debug.Log(hit.distance);
            collision = new Vector3(hit.point.x, 0, hit.point.z);
            collided = true;
        }
        else 
        {
            ray = new Ray(this.transform.position, this.transform.forward);
            if (Physics.Raycast(ray, out hit, maxdist))
            {
                //Debug.Log(hit.distance);
                collision = new Vector3(hit.point.x, 0, hit.point.z);
                collided = true;
            }
            else
            {
                collided = false;
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(collision, 0.2f);
    }
}