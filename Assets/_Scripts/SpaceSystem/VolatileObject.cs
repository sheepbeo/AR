using UnityEngine;
using System.Collections;

public class VolatileObject : MonoBehaviour {
	private bool _isDead = false;
	public Transform deadEffectPrefab;

	public string[] KillLayers = new string[] { "Sun", "SpaceObject", "SpaceObjectVolatile" };
	
	void OnTriggerEnter(Collider other) {
		foreach (string layerName in KillLayers) {
			if (LayerMask.NameToLayer(layerName) == other.gameObject.layer && other.gameObject != transform.gameObject) {
				if (!_isDead) {
					_isDead = true;
				}
				
				break;
			}
		}
	}

	void OnCollisionEnter(Collision collision) {
		foreach (string layerName in KillLayers) {
			if (LayerMask.NameToLayer(layerName) == collision.gameObject.layer && collision.gameObject != transform.gameObject) {
				if (!_isDead) {
					_isDead = true;
				}
				
				break;
			}
		}
	}

	void LateUpdate() {
		if (_isDead) {
			foreach(Transform child in transform) {
				child.parent = null;
			}

			if (deadEffectPrefab != null)
				Instantiate(deadEffectPrefab, transform.position, Quaternion.identity);

			Destroy(this.gameObject);
		}
	}

}
