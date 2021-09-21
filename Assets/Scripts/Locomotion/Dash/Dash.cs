
using System.Collections;
using UnityEngine;

namespace Locomotion
{
    public class Dash : MonoBehaviour
    {
        private float startTime;
        [SerializeField] private float minDashRange = 1f;
        [SerializeField] private float maxDashRange = 10f;
        [SerializeField] private float dashTime = 0.2f;
        [SerializeField] private Animator fadeAnimator;
        [SerializeField] private Animator collisionAnimator;
        public GameObject target;
        public CursorMovement targetCursor;
        public CollisionRay collisionRay;
        public Transform cam;
        
        
        private float midDashRange;
        
        private void Start()
        {
            cam = Camera.main.transform;
            targetCursor.SetRanges(minDashRange, maxDashRange);
            collisionRay.maxdist = maxDashRange;
        }

        private void Update()
        {
            transform.rotation = new Quaternion(0.0f, cam.rotation.y, 0.0f, cam.rotation.w);
            if (Input.GetMouseButtonDown(0)) //Linker Mausbutton einmaliger return
            {
                targetCursor.moved = false;
                
            }
            if (Input.GetMouseButton(0))    //Linker Mausklick wird gehalten
            {
                CursorMove();
            }
            else if (Input.GetMouseButtonUp(0)) // Linker Mausbutton losgelassen
            {
                Vector3 endPoint = new Vector3(target.transform.position.x, 0, target.transform.position.z);
                if (collisionRay.collided)
                {
                    if (Vector3.Distance(collisionRay.collision, transform.position) < Vector3.Distance(endPoint, transform.position))
                    {
                        StartCoroutine(DoDash(collisionRay.collision, true));
                        //collisionAnimator.SetBool("Collision", true);
                        targetCursor.moved = true;
                    }
                    else
                    {
                        StartCoroutine(DoDash(endPoint, false));
                        targetCursor.moved = true;
                    }
                }
                else
                {
                    StartCoroutine(DoDash(endPoint, false));
                    targetCursor.moved = true;
                }
                
            }
        }
        
        private IEnumerator DoDash(Vector3 endPoint, bool collision)
        {
            fadeAnimator.SetBool("Mask", true);
            yield return new WaitForSeconds(0.1f);
            float elapsed = 0f;
            Vector3 startPoint = transform.position;
            
            if (collision)
            {
                endPoint = LerpByDistance(startPoint, endPoint, Vector3.Distance(startPoint, endPoint) - 1.0f);
            }
            
            while (elapsed < dashTime)
            {
                elapsed += Time.deltaTime;
                float elapsedPct = elapsed / dashTime;
                transform.position = Vector3.Lerp(startPoint, endPoint, elapsedPct);
                yield return null;
            }
            fadeAnimator.SetBool("Mask", false);
        }
        
        public Vector3 LerpByDistance(Vector3 start, Vector3 end, float x)
        {
            Vector3 P = x * Vector3.Normalize(end - start) + start;
            return P;
        }
        
        public void CursorMove()
        {
            targetCursor.MoveCursor();
        }
    }
}