using UnityEngine;
using System.Collections;

public class Debugger : MonoBehaviour {
	public static Debugger instance;

	public string debugString {get; set;}

	public bool isEnabled;
	public GUIStyle debugStyle;
	public Vector2 dimensionPercentage = new Vector2(0.5f, 0.2f);

	void Awake() {
		instance = this;
		debugString = "Debug:";
	}

	void OnGUI() {
		if (isEnabled) {
			GUI.Label(new Rect(0,0,dimensionPercentage.x * Screen.width,dimensionPercentage.y * Screen.height)
			          , debugString, debugStyle);
		}
	}

	public static void Debug(System.Object obj) {
		instance.debugString += "|" + obj.ToString();
	}
}
