using UnityEngine;

public abstract class InteractiveObject : MonoBehaviour
{
    public float Speed { set => _speed = value; }

    protected float _speed;
    protected Vector3 _pointDead;    

    public void Init(Vector3 pointSpawn, Vector3 pointDead, float speed)
    {
        transform.position = pointSpawn;
        _pointDead = pointDead;
        _speed = speed;
    }


    protected virtual void OnTriggerEnter(Collider other) { }    

    protected void Update()
    {
        if (_speed != 0)
        { 
            float step =  _speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, _pointDead, step);
        
            if (Vector3.Distance(transform.position, _pointDead) < 0.001f)
            {
                gameObject.SetActive(false);
                _speed = 0;
            }
        }    
    }
}
