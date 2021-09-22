using UnityEngine;

public class CursorMovement: MonoBehaviour
{
    [SerializeField] private float speed = 10.0f;
    [SerializeField] private Transform _player;
    
    private float _minDashRange;
    private float _maxDashRange;
    private bool _moved = true;
    
    // Update is called once per frame
    void Update()
    {
        if (!Input.GetMouseButton(0) && _moved)
        {
            var campos = new Vector3(_player.position.x, 0.5f, _player.position.z);
            transform.position = campos + _player.transform.forward * _minDashRange;
        }
    }

    public void MoveCursor()
    {   
        Debug.Log(Vector3.Distance(_player.position, transform.position));
        if (Vector3.Distance(_player.position, transform.position) <= _maxDashRange)
        {
            var campos = new Vector3(_player.position.x, 0.5f, _player.position.z);
            transform.position += Time.deltaTime * speed * _player.transform.forward;
            
        }
        else
        {
            var campos = new Vector3(_player.position.x, 0.5f, _player.position.z);
        }
    }
    public void SetRanges(float min, float max)
    {
        _minDashRange = min;
        _maxDashRange = max;
    }

    public void SetMoved(bool b)
    {
        _moved = b;
    }
    
}