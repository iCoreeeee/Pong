using UnityEngine;
using Random = UnityEngine.Random;

public class BallController : MonoBehaviour
{

    [SerializeField] private Rigidbody2D _rigidbody2D;
    [SerializeField] private float _speed;
    [SerializeField] private float reflectionRandomization;
    

    private Vector2 lastRigidBodyVelocity;
    
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        SendBallRandomDir();
    }

    private void SendBallRandomDir()
    {
        _rigidbody2D.velocity = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized * _speed;
        lastRigidBodyVelocity = _rigidbody2D.velocity;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            ResetBall();
        }

        if (_rigidbody2D.velocity.magnitude < .1f)
        {
            ResetBall();
        }
    }

    private void ResetBall()
    {
        _rigidbody2D.velocity = Vector3.zero;
        _rigidbody2D.simulated = false;
        _rigidbody2D.transform.position = Vector3.zero;
        _rigidbody2D.simulated = true;
        SendBallRandomDir();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        var randomOffset = new Vector2(Random.Range(-reflectionRandomization, reflectionRandomization), Random.Range(-reflectionRandomization, reflectionRandomization));
        var reflectedVector = Vector2.Reflect(lastRigidBodyVelocity, (other.contacts[0].normal + randomOffset).normalized);
        
        _rigidbody2D.velocity = reflectedVector;
        lastRigidBodyVelocity = _rigidbody2D.velocity;
    }
}