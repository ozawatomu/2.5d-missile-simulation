using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera:MonoBehaviour {
	public GameObject target;
	public float smoothSpeed = 0.125f;
	public Vector3 offset;
	void Start() {

	}

	// Update is called once per frame
	void FixedUpdate() {
		transform.LookAt(target.transform.position);
		Vector3 desiredPosition = target.transform.position + offset;
		transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
	}
}
