using System.Collections;
using UnityEngine;

namespace Locomotion.Dash
{
    public class Dash : MonoBehaviour
    {
        
        [SerializeField] private float minDashRange = 1.0f;
        [SerializeField] private float maxDashRange = 15.0f;
        [SerializeField] private float dashTime = 0.15f;
        [SerializeField] private bool moveOnColliion;
        [SerializeField] private GameObject collisionMask;
        [SerializeField] private GameObject target;
        [SerializeField] private CursorMovement targetCursorScript;
        [SerializeField] private CollisionRay collisionRay;
        [SerializeField] private Animator fadeAnimator;
        
        private float _startTime;
        private Animation _collisionAnimation;
        private Transform _cam;

        private void Start()
        {
            _cam = Camera.main.transform;
            _collisionAnimation = collisionMask.GetComponent<Animation>();
            _collisionAnimation.wrapMode = WrapMode.Once;
            targetCursorScript.SetRanges(minDashRange, maxDashRange);
            collisionRay.SetMaxditance(maxDashRange);
        }

        private void Update()
        {
            transform.rotation = new Quaternion(0.0f, _cam.rotation.y, 0.0f, _cam.rotation.w);
            if (Input.GetMouseButtonDown(0))        //Linker Mausbutton einmaliger check
            {
                targetCursorScript.SetMoved(false);
                
            }
            if (Input.GetMouseButton(0))            //Linker Mausklick wird gehalten
            {
                targetCursorScript.MoveCursor();
            }
            else if (Input.GetMouseButtonUp(0))     //Linker Mausbutton losgelassen
            {
                Vector3 endPoint = new Vector3(target.transform.position.x, 0, target.transform.position.z);
                if (collisionRay.Collided())
                {
                    if (Vector3.Distance(collisionRay.CollisionVector(), transform.position) < Vector3.Distance(endPoint, transform.position)
                        || Vector3.Distance(collisionRay.CollisionVector(), transform.position) < minDashRange)
                    {
                        if (moveOnColliion)         //Falls bei Kollisionen Movement erlaubt wird
                        {
                            StartCoroutine(DoDash(collisionRay.CollisionVector(), true));
                        }
                        else
                        {
                            _collisionAnimation.Play();
                            targetCursorScript.SetMoved(true);
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
            
            if (collision)      //Nur relevant falls trotz Kollision Movement erlaubt wird
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
            targetCursorScript.SetMoved(true);
        }
        
        public Vector3 LerpByDistance(Vector3 start, Vector3 end, float x)
        {
            Vector3 P = x * Vector3.Normalize(end - start) + start;
            return P;
        }
    }
}