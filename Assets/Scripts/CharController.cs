﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharController : MonoBehaviour {

	[SerializeField]
	float moveSpeed = 4f;
	public Transform playerGFX;

	[SerializeField]
	float jumpHeight = 8f;
	[SerializeField]
	int jumpIndex = 0, jumpMax = 1;

	[SerializeField]
	float baseButtStompForce = 10f;
	bool hasButtStomped = false;

	float originalYLevel;

	[SerializeField]
	float lowestYLevel;

	Rigidbody rb;

	[SerializeField]
	PlayerForceMode playerForceMode;

	Vector3 forward, right;

	[SerializeField]
	bool OnGround;

	Player player;

	void Start() {
		
		ResetForwardDirection ();

		rb = GetComponent<Rigidbody> ();

		player = GetComponent<Player> ();


		rb.AddForce (player.transform.forward * 10f);

	}

	void FixedUpdate() {

		OnGround = IsOnGround ();

		if (OnGround && jumpIndex != 0) {

			jumpIndex = 0;

		}

		if (Input.GetButtonDown ("Jump")) {

			if (OnGround) {

				originalYLevel = transform.position.y;

			}

			Jump ();

		} else {
			
			Move ();

		}

		if (Input.GetButtonDown ("ButtStomp") && !OnGround && !hasButtStomped) {

			ButtStomp ();

		}

		rb.velocity = new Vector3 (0f, rb.velocity.y, 0f);

		if (!(rb.velocity.y > -0.01f && rb.velocity.y < 0.01f)) {
			
			Debug.Log (rb.velocity.y.ToString ("##.00000000"));

		}

		if (OnGround) {

			hasButtStomped = false;

		}

		lowestYLevel = (lowestYLevel > transform.position.y) ? transform.position.y : lowestYLevel;

	}

	void Move() {
		
		Vector3 rightMovement = right * moveSpeed * Time.deltaTime * Input.GetAxis ("HorizontalKey"); 
		Vector3 upMovement = forward * moveSpeed * Time.deltaTime * Input.GetAxis ("VerticalKey");
		Vector3 heading = Vector3.Normalize (rightMovement + upMovement);

		if (heading.magnitude > 0.1f) {
			
			transform.forward = heading;

			transform.position += rightMovement;
			transform.position += upMovement;

		}
	}

	void Jump() {

		if (player.HasAcquiredAbility ("DoubleJump")) { 

			jumpMax = 2;

		}

		if (jumpIndex >= jumpMax) {

			return;

		}

		rb.velocity = new Vector3 (rb.velocity.x, 0f, rb.velocity.z);
		rb.AddForce (Vector3.up * jumpHeight, SetPlayerForceMode(playerForceMode));
		jumpIndex++;

	}

	void ButtStomp() {

		float currentYLevel = transform.position.y;

		float buttonStompForce = baseButtStompForce + (1.5f * (currentYLevel - originalYLevel));

		Debug.Log (string.Format("buttonStompForce: {0}, original/currentY: {1}/{2}", buttonStompForce, originalYLevel, currentYLevel));

		hasButtStomped = true;
		rb.AddForce (-Vector3.up * buttonStompForce, ForceMode.VelocityChange);

	}

	bool IsOnGround() {

		return (rb.velocity.y > -0.01f && rb.velocity.y < 0.01f);
		// return Mathf.Approximately(0f, rb.velocity.y);

	}

	public void ResetForwardDirection() {

		forward = Camera.main.transform.forward; 
		forward.y = 0; 
		forward = Vector3.Normalize(forward); 
		right = Quaternion.Euler(new Vector3(0, 90, 0)) * forward; 

	}

	ForceMode SetPlayerForceMode(PlayerForceMode _playerForceMode) {

		if (_playerForceMode == PlayerForceMode.Acceleration) {

			return ForceMode.Acceleration;

		}

		if (_playerForceMode == PlayerForceMode.Force) {

			return ForceMode.Force;

		}

		if (_playerForceMode == PlayerForceMode.Impulse) {

			return ForceMode.Impulse;

		}

		if (_playerForceMode == PlayerForceMode.VelocityChange) {

			return ForceMode.VelocityChange;

		}

		return ForceMode.Impulse;

	}

} enum PlayerForceMode { Acceleration, Force, Impulse, VelocityChange }