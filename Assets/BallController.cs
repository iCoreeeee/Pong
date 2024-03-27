using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using Random = UnityEngine.Random;

public class BallController : MonoBehaviour
{

    [SerializeField] private Rigidbody2D _rigidbody2D;
    [SerializeField] private float _speed;
    [SerializeField] private float reflectionRandomization;
    

    private Vector2 lastRigidBodyVelocity;
    private bool isGameStarted = false;
    
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        //StartCoroutine(SendBallRandomDir());
    }

    private IEnumerator SendBallRandomDir()
    {
        var randomX = Random.Range(-1f, 1f);
        while (randomX is -1 or 0 or 1)
        {
            randomX = Random.Range(-1f, 1f);
            yield return null;
        }
        
        var randomY = Random.Range(-1f, 1f);
        while (randomY is -1 or 0 or 1)
        {
            randomY = Random.Range(-1f, 1f);
            yield return null;
        }
        
        _rigidbody2D.velocity = new Vector2(randomX, randomY).normalized * _speed;
        lastRigidBodyVelocity = _rigidbody2D.velocity;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            isGameStarted = true;
            ResetBall();
            
        }

        if (isGameStarted && _rigidbody2D.velocity.magnitude < .1f)
        {
            isGameStarted = false;
            ResetBall();
        }
    }

    private void ResetBall()
    {
        _rigidbody2D.velocity = Vector3.zero;
        _rigidbody2D.simulated = false;
        _rigidbody2D.transform.position = Vector3.zero;
        _rigidbody2D.simulated = true;
        if (isGameStarted)
            StartCoroutine(SendBallRandomDir());
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        var randomOffset = new Vector2(Random.Range(-reflectionRandomization, 0), Random.Range(-reflectionRandomization, 0));
        var reflectedVector = Vector2.Reflect(lastRigidBodyVelocity, (other.contacts[0].normal + randomOffset).normalized);
        
        _rigidbody2D.velocity = reflectedVector;
        lastRigidBodyVelocity = _rigidbody2D.velocity;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (transform.position.x > 0)
            GameManager.P1Score++;
        if (transform.position.x < 0)
            GameManager.P2Score++;
        GameManager.ScoreUpdated?.Invoke(null, EventArgs.Empty);
        isGameStarted = false;
        ResetBall();
    }
}