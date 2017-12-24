using UnityEngine;
using System.Collections;

public class IsometricCamera : MonoBehaviour
{
	public GameObject target;
	public Transform CameraParent;
	public float size = 10;
	public float scrollSpeed = 30;

	[SerializeField]
	float cameraDistanceY, cameraDistanceXZ = 100;

	Vector3 pos;

	void Start() {
		
		Camera.main.orthographic = true;
		CameraParent.transform.rotation = Quaternion.Euler(30f, 45f, 0);

	}

	void LateUpdate() {
		
		Camera.main.orthographicSize -= Input.GetAxis("Mouse ScrollWheel") * scrollSpeed * Time.deltaTime;

		if (Input.GetKeyDown (KeyCode.Alpha1)) {

			CameraParent.transform.eulerAngles = new Vector3 (30f, 45f, 0f);

		}

		if (Input.GetKeyDown (KeyCode.Alpha2)) {

			CameraParent.transform.eulerAngles = new Vector3 (30f, 135f, 0f);

		}

		if (Input.GetKeyDown (KeyCode.Alpha3)) {

			CameraParent.transform.eulerAngles = new Vector3 (30f, 225f, 0f);

		}

		if (Input.GetKeyDown (KeyCode.Alpha4)) {

			CameraParent.transform.eulerAngles = new Vector3 (30f, 315f, 0f);

		}

		target.GetComponent<CharController> ().ResetForwardDirection ();

		Vector3 addition = Vector3.zero;
		// cameraDistanceY = cameraDistanceXZ * .8f;

		if (CameraParent.transform.eulerAngles.y % 315f == 0) {

			addition = new Vector3 (cameraDistanceXZ, cameraDistanceY, -cameraDistanceXZ);

		} else if (CameraParent.transform.eulerAngles.y % 225f == 0) {

			addition = new Vector3 (cameraDistanceXZ, cameraDistanceY, cameraDistanceXZ);

		} else if (CameraParent.transform.eulerAngles.y % 135f == 0) {

			addition = new Vector3 (-cameraDistanceXZ, cameraDistanceY, cameraDistanceXZ);

		} else if (CameraParent.transform.eulerAngles.y % 45f == 0) {
	
			addition = new Vector3 (-cameraDistanceXZ, cameraDistanceY, -cameraDistanceXZ);

		}

		CameraParent.transform.position = target.transform.position + addition;

	}
}