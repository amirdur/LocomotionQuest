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
        [SerializeField] private bool moveOnColliion;
        public GameObject collisionMask;
        private Animation collisionAnimation;
        public GameObject target;
        public CursorMovement targetCursor;
        public CollisionRay collisionRay;
        public Transform cam;
        
        
        private float midDashRange;
        
        private void Start()
        {
            collisionAnimation = collisionMask.GetComponent<Animation>();
            collisionAnimation.wrapMode = WrapMode.Once;
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
                    if (Vector3.Distance(collisionRay.collision, transform.position) < Vector3.Distance(endPoint, transform.position)
                        || Vector3.Distance(collisionRay.collision, transform.position) < minDashRange)
                    {
                        
                        if (moveOnColliion)
                        {
                            StartCoroutine(DoDash(collisionRay.collision, true));
                        }
                        else
                        {
                            //StartCoroutine(Collision());
                            collisionAnimation.Play();
                            targetCursor.moved = true;
                        }
                    }
                    else
                    {
                        StartCoroutine(DoDash(endPoint, false));
                    }
                }
                else
                {
                    StartCoroutine(DoDash(endPoint, false));
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
                endPoint = LerpByDistance(startPoint, endPoint, Vector3.Distance(startPoint, endPoint) - minDashRange);
            }
            
            while (elapsed < dashTime)
            {
                elapsed += Time.deltaTime;
                float elapsedPct = elapsed / dashTime;
                transform.position = Vector3.Lerp(startPoint, endPoint, elapsedPct);
                yield return null;
            }
            fadeAnimator.SetBool("Mask", false);
            targetCursor.moved = true;
        }
        
        public Vector3 LerpByDistance(Vector3 start, Vector3 end, float x)
        {
            Vector3 P = x * Vector3.Normalize(end - start) + start;
            return P;
        }

        private IEnumerator Collision()
        {
            collisionAnimator.SetBool("collision", true);
            yield return new WaitForSeconds(0.1f);
            targetCursor.moved = true;
            collisionAnimator.SetBool("collision", false);
            yield return null;
        }
        
        public void CursorMove()
        {
            targetCursor.MoveCursor();
        }
    }
}