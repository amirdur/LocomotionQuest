using UnityEngine;

public class CursorMovement: MonoBehaviour
{
    [SerializeField] private float speed = 10.0f;
    private float _minDashRange;
    private float _maxDashRange;
    
    public Transform player;
    public bool moved = true;
    
    // Update is called once per frame
    void Update()
    {
        if (!Input.GetMouseButton(0) && moved)
        {
            //transform.rotation = new Quaternion(transform.rotation.x, cam.rotation.y, 0.0f, cam.rotation.w);
            var campos = new Vector3(player.position.x, 0.5f, player.position.z);
            transform.position = campos + player.transform.forward * _minDashRange;
        }
    }

    public void MoveCursor()
    {   
        Debug.Log(Vector3.Distance(player.position, transform.position));
        if (Vector3.Distance(player.position, transform.position) <= _maxDashRange)
        {
            //transform.rotation = new Quaternion(0.0f, cam.rotation.y, 0.0f, cam.rotation.w);
            var campos = new Vector3(player.position.x, 0.5f, player.position.z);
            //transform.position = campos + transform.forward * Vector3.Distance(cam.position, transform.position);
             transform.position += Time.deltaTime * speed * player.transform.forward;
            
        }
        else
        {
           // transform.rotation = new Quaternion(0.0f, cam.rotation.y, 0.0f, cam.rotation.w);
            var campos = new Vector3(player.position.x, 0.5f, player.position.z);
           // transform.position = campos + transform.forward * _maxDashRange;
            
        }
    }

    public void SetRanges(float min, float max)
    {
        _minDashRange = min;
        _maxDashRange = max;
    }
    
}