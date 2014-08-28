using UnityEngine;
using System.Collections;

public class InputHandler : MonoBehaviour {

	public Transform Bullet;
	public Transform GunVisual;
	public Transform GunTip;

	private float _chargeTime;
	public float minForce = 10f;
	public float maxForce = 5000f;
	public float minForceTime = 0f;
	public float maxForceTime = 5f;

	void Awake() {
		InputController3D inputController = GetComponent<InputController3D>();
		if (inputController != null) {
			inputController.OnInputBegin += HandleOnInputBegin;
			inputController.OnInputMoving += HandleOnInputMoving;
			inputController.OnInputEnd += HandleOnInputEnd;
		}
	}
	
	void HandleOnInputBegin (Vector3 pos)
	{
		_chargeTime = 0f;
	}
	
	void HandleOnInputMoving (Vector3 pos)
	{
		_chargeTime += Time.deltaTime;
		PointGunToward(pos);
	}

	void HandleOnInputEnd (Vector3 pos)
	{
		_chargeTime += Time.deltaTime;
		ShootGunToward(pos, _chargeTime);
	}

	private void PointGunToward(Vector3 pos) {
		if (GunVisual != null) {
			GunVisual.LookAt(pos);
		}
	}

	private void ShootGunToward(Vector3 pos, float chargeTime) {
		PointGunToward(pos);

		float speed = (maxForce - minForce) * (chargeTime / (maxForceTime - minForceTime));
		speed = Mathf.Clamp(speed, minForce, maxForce);

		Transform bullet = (Transform) Instantiate(Bullet, GunTip.position, Quaternion.identity);
		bullet.LookAt(pos);
		bullet.GetComponent<VelocityInitializer>().InitVelocity(pos - bullet.transform.position, speed);
	}
}
