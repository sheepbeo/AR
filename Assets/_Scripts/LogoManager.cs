using UnityEngine;
using System.Collections;

public class LogoManager : MonoBehaviour {

	public Transform destination;
	private Vector3 _origin;

	public float deathZone;
	public float moveSpeed = 1f;

	void Awake() {
		_origin = transform.position;
		//StartMoving();
	}

	public void StartMoving() {
		transform.position = _origin;
		StartCoroutine(Move());
	}

	IEnumerator Move() {
		while ((destination.position - transform.position).magnitude > deathZone) {
			transform.position += (destination.position - transform.position).normalized * moveSpeed * Time.deltaTime;
			print((destination.position - transform.position).magnitude);
			yield return null;
		}


	}
}
