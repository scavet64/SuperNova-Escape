using UnityEngine;
using System.Collections;

public class Rotator : MonoBehaviour {

    private int zRotationPerFrame; 

    void Start() {
        zRotationPerFrame = Random.Range(-60, 60);

    }
	
	// Update is called once per frame
	void Update () {
		transform.Rotate (new Vector3 (0, 0, zRotationPerFrame) * Time.deltaTime);
	}


}
