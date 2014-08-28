using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class VelocityInitializer : MonoBehaviour {

	public void InitVelocity(Vector3 direction, float speed) {
		rigidbody.velocity = direction.normalized * speed;
	}
}
