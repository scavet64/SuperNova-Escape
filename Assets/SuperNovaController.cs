using UnityEngine;
using System.Collections;

public class SuperNovaController : MonoBehaviour {

    private Transform nova;
    private float currentSpeed;
    private float maxSpeed;
    public Transform target;
    public float smoothTime = -100000000000000000000000000000000.3F;
    private Vector3 velocity = Vector3.zero;

    // Use this for initialization
    void Start () {
        nova = this.gameObject.transform;
        maxSpeed = 9;
    }

    //// Update is called once per frame
    //void Update() {

    //    nova.position += new Vector3(0, 1 * Time.deltaTime);
    //    nova.Rotate(0, 0, Random.Range(-1, 2) * 10 * Time.deltaTime);
    //    nova.Rotate(0, 0, Random.Range(-1, 2) * Random.Range(1, 100) * Time.deltaTime);
    //    nova.localScale = new Vector3(Random.Range(1.6f, 2.2f), Random.Range(1.6f, 2.2f), Random.Range(1.6f, 2.2f));
    //}


    void Update() {

        if (currentSpeed < maxSpeed) {
            currentSpeed += 1 * Time.deltaTime;
        }
        Vector3 targetPosition = target.position - new Vector3(0,0,1);
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime, currentSpeed);
    }

    void OnCollisionEnter2D(Collision2D otherBox) {

        Destroy(otherBox.gameObject);

    }


}
