using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket:MonoBehaviour {
	public new Rigidbody rigidbody;
	public float thrustForce;
	public float thrustVectorForce;
	public ParticleSystem mainThrusterFlame;
	public Light mainThrusterFlameLight;
	public float thrusterLightBrightness = 3.0f;
	int mainThrusterParticleCountMax = 120000;
	int mainThrusterParticleCount = 0;

	public ParticleSystem rightThrusterFlame;
	public Light rightThrusterFlameLight;
	int rightThrusterParticleCount = 0;

	public ParticleSystem leftThrusterFlame;
	public Light leftThrusterFlameLight;
	int leftThrusterParticleCount = 0;

	void Start() {
		rigidbody.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY;
	}

	void Update() {
		//transform.position = new Vector3(transform.position.x, transform.position.y, 0);

		float vectorToMainRatio = thrustVectorForce/thrustForce;
		int vectorThrusterParticleCountMax = Mathf.RoundToInt(mainThrusterParticleCountMax*vectorToMainRatio);

		if(Input.GetKey("w")) {
			Vector3 forceVector = (transform.Find("Main Thruster").Find("Thrust Location").position - transform.Find("Main Thruster").Find("Thrust Main").position).normalized*thrustForce*Time.deltaTime;
			rigidbody.AddForceAtPosition(forceVector, transform.Find("Main Thruster").Find("Thrust Location").position);
			//mainThrusterFlame.Emit(Mathf.RoundToInt(10000*Time.deltaTime));

			mainThrusterParticleCount += Mathf.RoundToInt(1200000*Time.deltaTime);
			mainThrusterParticleCount = Mathf.Clamp(mainThrusterParticleCount, 0, mainThrusterParticleCountMax);
		} else {
			mainThrusterParticleCount -= Mathf.RoundToInt(1200000*Time.deltaTime);
			mainThrusterParticleCount = Mathf.Clamp(mainThrusterParticleCount, 0, mainThrusterParticleCountMax);
		}
		ParticleSystem.EmissionModule mainEmissionModule = mainThrusterFlame.emission;
		ParticleSystem.MinMaxCurve mainTempCurve = mainEmissionModule.rateOverTimeMultiplier;
		mainTempCurve.constant = mainThrusterParticleCount;
		mainEmissionModule.rateOverTime = mainTempCurve;
		mainThrusterFlameLight.intensity = Mathf.Lerp(0f, thrusterLightBrightness, (float)mainThrusterParticleCount/(float)mainThrusterParticleCountMax);

		if(Input.GetKey("d")) {
			Vector3 forceVector = (transform.Find("Vector Thrusters").Find("Thrust Vector Location").position - transform.Find("Vector Thrusters").Find("Thrust Vector Right").position).normalized*thrustVectorForce*Time.deltaTime;
			rigidbody.AddForceAtPosition(forceVector, transform.Find("Vector Thrusters").Find("Thrust Vector Location").position);
			rightThrusterParticleCount += Mathf.RoundToInt(1200000*vectorToMainRatio*Time.deltaTime);
			rightThrusterParticleCount = Mathf.Clamp(rightThrusterParticleCount, 0, vectorThrusterParticleCountMax);
		} else {
			rightThrusterParticleCount -= Mathf.RoundToInt(1200000*vectorToMainRatio*Time.deltaTime);
			rightThrusterParticleCount = Mathf.Clamp(rightThrusterParticleCount, 0, vectorThrusterParticleCountMax);
		}
		ParticleSystem.EmissionModule rightEmissionModule = rightThrusterFlame.emission;
		ParticleSystem.MinMaxCurve rightTempCurve = rightEmissionModule.rateOverTimeMultiplier;
		rightTempCurve.constant = rightThrusterParticleCount;
		rightEmissionModule.rateOverTime = rightTempCurve;
		rightThrusterFlameLight.intensity = Mathf.Lerp(0f, thrusterLightBrightness*vectorToMainRatio, (float)rightThrusterParticleCount/(float)vectorThrusterParticleCountMax);

		if(Input.GetKey("a")) {
			Vector3 forceVector = (transform.Find("Vector Thrusters").Find("Thrust Vector Location").position - transform.Find("Vector Thrusters").Find("Thrust Vector Left").position).normalized*thrustVectorForce*Time.deltaTime;
			rigidbody.AddForceAtPosition(forceVector, transform.Find("Vector Thrusters").Find("Thrust Vector Location").position);
			leftThrusterParticleCount += Mathf.RoundToInt(1200000*vectorToMainRatio*Time.deltaTime);
			leftThrusterParticleCount = Mathf.Clamp(leftThrusterParticleCount, 0, vectorThrusterParticleCountMax);
		} else {
			leftThrusterParticleCount -= Mathf.RoundToInt(1200000*vectorToMainRatio*Time.deltaTime);
			leftThrusterParticleCount = Mathf.Clamp(leftThrusterParticleCount, 0, vectorThrusterParticleCountMax);
		}
		ParticleSystem.EmissionModule leftEmissionModule = leftThrusterFlame.emission;
		ParticleSystem.MinMaxCurve leftTempCurve = leftEmissionModule.rateOverTimeMultiplier;
		leftTempCurve.constant = leftThrusterParticleCount;
		leftEmissionModule.rateOverTime = leftTempCurve;
		leftThrusterFlameLight.intensity = Mathf.Lerp(0f, thrusterLightBrightness*vectorToMainRatio, (float)leftThrusterParticleCount/(float)vectorThrusterParticleCountMax);

		Debug.Log(rigidbody.velocity.magnitude);
	}

	private void OnDrawGizmos() {
		Gizmos.DrawSphere(transform.Find("Main Thruster").Find("Thrust Location").position, 0.08f);
		Gizmos.DrawSphere(transform.Find("Main Thruster").Find("Thrust Main").position, 0.025f);
		Gizmos.DrawLine(transform.Find("Main Thruster").Find("Thrust Main").position, transform.Find("Main Thruster").Find("Thrust Location").position);
		Gizmos.DrawSphere(transform.Find("Vector Thrusters").Find("Thrust Vector Location").position, 0.08f);
		Gizmos.DrawSphere(transform.Find("Vector Thrusters").Find("Thrust Vector Right").position, 0.025f);
		Gizmos.DrawSphere(transform.Find("Vector Thrusters").Find("Thrust Vector Left").position, 0.025f);
		Gizmos.DrawLine(transform.Find("Vector Thrusters").Find("Thrust Vector Right").position, transform.Find("Vector Thrusters").Find("Thrust Vector Location").position);
		Gizmos.DrawLine(transform.Find("Vector Thrusters").Find("Thrust Vector Left").position, transform.Find("Vector Thrusters").Find("Thrust Vector Location").position);
	}
}
