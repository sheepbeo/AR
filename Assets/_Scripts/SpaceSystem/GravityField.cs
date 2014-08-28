using UnityEngine;
using System.Collections;

public class GravityField : MonoBehaviour {

	public string[] AffectedLayers = new string[] { "SpaceObject" };
	public float GravityCoefficient = 1.0f;

	void OnTriggerStay(Collider other) {
		foreach (string layerName in AffectedLayers) {
			if (LayerMask.NameToLayer(layerName) == other.gameObject.layer && other.gameObject != transform.parent.gameObject) {
				Vector3 direction = transform.position - other.transform.position;
				float magnitude = GravityCoefficient 
						* other.transform.rigidbody.mass 
						* transform.parent.rigidbody.mass
						/ direction.sqrMagnitude;

				//print(direction.sqrMagnitude);

				other.rigidbody.AddForce(direction.normalized * magnitude, ForceMode.Force);

				break;
			}
		}
	}
}
