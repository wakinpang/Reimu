using UnityEngine;
using System.Collections;

public class Zombie_Controller : MonoBehaviour {

	public float turnSpeed;
	public float moveSpeed;
	private Vector3 moveDirection;


	// Use this for initialization
	void Start () {
		moveDirection = Vector3.right;
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 currentPosition = transform.position;
		float targetAngle = Mathf.Atan2 (moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;

		if (Input.GetButton ("Fire1")) {
			Vector3 moveToward = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			moveDirection = moveToward - currentPosition;
			moveDirection.z = 0;
			moveDirection.Normalize();
		}


		Vector3 target = moveDirection * moveSpeed + currentPosition;
		transform.position = Vector3.Lerp (currentPosition, target, Time.deltaTime);


		transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.Euler (0, 0, targetAngle), turnSpeed * Time.deltaTime);
	}
}
