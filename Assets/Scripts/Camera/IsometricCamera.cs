using UnityEngine;
using System.Collections;

public class IsometricCamera : MonoBehaviour
{
	public Player player;
	public Transform CameraParent;
	public float size = 10;
	public float scrollSpeed = 30;

	[SerializeField]
	float cameraDistanceY, cameraDistanceXZ = 100;

	Vector3 pos;

	void Start() {
		
		Camera.main.orthographic = true;
		// transform.rotation = Quaternion.Euler(30f, 45f, 0);
		transform.eulerAngles = new Vector3 (30f, 315f, 0f);

	}

	void LateUpdate() {

		if (GameController.current.Player == null) {

			return;

		}

		CameraParent.transform.position = GameController.current.Player.transform.position;

		Camera.main.orthographicSize -= Input.GetAxis("Mouse ScrollWheel") * scrollSpeed * Time.deltaTime;

		if (Input.GetKeyDown (KeyCode.Alpha1)) {

			transform.eulerAngles = new Vector3 (30f, 45f, 0f);

		}

		if (Input.GetKeyDown (KeyCode.Alpha2)) {

			transform.eulerAngles = new Vector3 (30f, 135f, 0f);

		}

		if (Input.GetKeyDown (KeyCode.Alpha3)) {

			transform.eulerAngles = new Vector3 (30f, 225f, 0f);

		}

		if (Input.GetKeyDown (KeyCode.Alpha4)) {

			transform.eulerAngles = new Vector3 (30f, 315f, 0f);

		}

		GameController.current.Player.GetComponent<CharController> ().ResetForwardDirection ();

		Vector3 addition = Vector3.zero;
		// cameraDistanceY = cameraDistanceXZ * .8f;

		if (transform.eulerAngles.y % 315f == 0) {

			addition = new Vector3 (cameraDistanceXZ, cameraDistanceY, -cameraDistanceXZ);

		} else if (transform.eulerAngles.y % 225f == 0) {

			addition = new Vector3 (cameraDistanceXZ, cameraDistanceY, cameraDistanceXZ);

		} else if (transform.eulerAngles.y % 135f == 0) {

			addition = new Vector3 (-cameraDistanceXZ, cameraDistanceY, cameraDistanceXZ);

		} else if (transform.eulerAngles.y % 45f == 0) {
	
			addition = new Vector3 (-cameraDistanceXZ, cameraDistanceY, -cameraDistanceXZ);

		} else {

			addition = new Vector3 (-cameraDistanceXZ, cameraDistanceY, -cameraDistanceXZ);

		}

		transform.position = GameController.current.Player.transform.position + addition;

	}

//	bool FindPlayer() {
//
//		if (GameController.current.Player != null) {
//
//			return false;
//
//		}
//
//		if (player != null) {
//
//			return true;
//
//		} else {
//
//			player = FindObjectOfType<Player> ();
//
//			if (player != null) {
//
//				return true;
//
//			}
//
//		}
//
//		return false;
//
//	}

}