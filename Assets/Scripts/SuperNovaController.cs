using UnityEngine;
using System.Collections;

public class SuperNovaController : MonoBehaviour {

    private float currentSpeed;
    private float maxSpeed = 10;
    public Transform target;
    public float smoothTime = -100000000000000000000000000000000.3F;
    private Vector3 velocity = Vector3.zero;

    // Use this for initialization
    void Start () {
    }

    void Update()
    {
        if (currentSpeed < maxSpeed)
        {
            currentSpeed += 1 * Time.deltaTime;
        }
        if(target != null)
        {
            Vector3 targetPosition = target.position - new Vector3(0, 0, 1);
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime, currentSpeed);
        }
    }


    void OnCollisionEnter2D(Collision2D otherBox)
    {
        Destroy(otherBox.gameObject);
    }

    void OnTriggerEnter2D(Collider2D otherBox)
    {
        Destroy(otherBox.gameObject);
    }


}
