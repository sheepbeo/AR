using UnityEngine;
using System.Collections;

public class SplashManager : MonoBehaviour {

	public float timeOut = 3f;
	private float _timer = 0f;
	public string nextScene;
	
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (_timer < timeOut) {
			_timer += Time.deltaTime;
		} else {
			Application.LoadLevel(nextScene);
		}
	}
}
