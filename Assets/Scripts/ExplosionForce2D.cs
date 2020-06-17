// Developed by Ananda Gupta
// info@anandagupta.com
// http://anandagupta.com

using UnityEngine;
using System.Collections;

public class ExplosionForce2D : MonoBehaviour
{
	public float Power;
	public float Radius;
	
		// Use this for initialization
		void Start ()
		{
	
		}
	
		// Update is called once per frame
		void FixedUpdate ()
		{
# if (UNITY_ANDROID || UNITY_IPHONE)

		if (Input.touchCount > 0 && Input.GetTouch (0).phase == TouchPhase.Began) {
			Vector3 fingerPos = Input.GetTouch(0).position;
			fingerPos.z = 10;
			Vector3 objPos = Camera.main.ScreenToWorldPoint(fingerPos);
			AddExplosionForce(GetComponent<Rigidbody2D>(), Power * 100, objPos, Radius);
		}

# endif

# if (UNITY_EDITOR || UNITY_WEBPLAYER)

			if (Input.GetButtonDown("Fire1")){
				Vector3 mousePos = Input.mousePosition;
				mousePos.z = 10;
				Vector3 objPos1 = Camera.main.ScreenToWorldPoint(mousePos);
				AddExplosionForce(GetComponent<Rigidbody2D>(), Power * 100, objPos1, Radius);
			}
# endif	
	
		}

		public static void AddExplosionForce (Rigidbody2D body, float expForce, Vector3 expPosition, float expRadius)
		{
				var dir = (body.transform.position - expPosition);
				float calc = 1 - (dir.magnitude / expRadius);
				if (calc <= 0) {
						calc = 0;		
				}

				body.AddForce (dir.normalized * expForce * calc);
		}


}
