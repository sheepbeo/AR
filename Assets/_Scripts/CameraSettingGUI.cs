using UnityEngine;
using System.Collections;

public class CameraSettingGUI : MonoBehaviour {

	public Vector2 size;

	// Use this for initialization
	void Start () {
		if (GameObject.FindObjectOfType<QCARBehaviour>() != null) {
			CameraDevice.Instance.SetFocusMode(CameraDevice.FocusMode.FOCUS_MODE_CONTINUOUSAUTO);
		}

	}

	void OnGUI() {
		if (GUI.Button(new Rect(0,0,size.x,size.y), "focus")) {
			if (GameObject.FindObjectOfType<QCARBehaviour>() != null) {
				CameraDevice.Instance.SetFocusMode(CameraDevice.FocusMode.FOCUS_MODE_TRIGGERAUTO);
			}
		}
	}
}
