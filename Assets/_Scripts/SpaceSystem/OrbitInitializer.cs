using UnityEngine;
using System.Collections;

public class OrbitInitializer : MonoBehaviour {

	public Vector3 direction;
	public float magnitude;

	void OnEnable () {
		rigidbody.AddForce(direction.normalized * magnitude);
	}
}
