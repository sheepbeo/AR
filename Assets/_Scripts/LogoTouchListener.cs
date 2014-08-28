using UnityEngine;
using System.Collections;

public class LogoTouchListener : MonoBehaviour {

	public float moveSpeed = 0.1f;
	public float pinchSpeed = 1.0f;

	TKPinchRecognizer pinchRecognizer = new TKPinchRecognizer();
	TKPanRecognizer panRecognizer = new TKPanRecognizer();


	void Start() {
		pinchRecognizer.zIndex = 1;
		pinchRecognizer.gestureRecognizedEvent += HandlePinchRecognized;
		pinchRecognizer.gestureCompleteEvent += HandlePinchComplete;
		TouchKit.addGestureRecognizer(pinchRecognizer);

		panRecognizer.zIndex = 0;
		panRecognizer.gestureRecognizedEvent += HandlePanRecognized;
		TouchKit.addGestureRecognizer(panRecognizer);

		//print(panRecognizer.zIndex);
		//print(pinchRecognizer.zIndex);
	}
	
	void HandlePinchRecognized (TKPinchRecognizer recognizer)
	{
		transform.localScale += Vector3.one * recognizer.deltaScale * pinchSpeed;
		panRecognizer.enabled = false;
		//Debugger.Debug("pinch touch count " + recognizer.trackingTouches.Count);
	}
	
	void HandlePinchComplete (TKPinchRecognizer recognizer)
	{
		//Debugger.Debug(3);
		panRecognizer.enabled = true;
	}
	
	void HandlePanRecognized (TKPanRecognizer recognizer)
	{
		transform.position += new Vector3(recognizer.deltaTranslation.x, recognizer.deltaTranslation.y, 0f) * moveSpeed;
		//Debugger.Debug(recognizer.GetType().ToString() + " " + recognizer.state);
		//Debugger.Debug(1);
	}
}
