using UnityEngine;
using System.Collections;

public class IsometricCamera : MonoBehaviour
{
	public Player player;
	public float size = 10;
	public float scrollSpeed = 30;

	[SerializeField]
	float cameraDistanceY, cameraDistanceXZ = 100;

	Vector3 pos;

	void Start() {
		
		Camera.main.orthographic = true;
        // transform.rotation = Quaternion.Euler(30f, 45f, 0);
        Camera.main.transform.eulerAngles = new Vector3 (30f, 315f, 0f);

	}

	void LateUpdate() {

		if (GameController.current.Player == null) {

			return;

		}

		transform.position = GameController.current.Player.transform.position;

		GameController.current.Player.GetComponent<CharController> ().ResetForwardDirection ();

		Vector3 addition = Vector3.zero;

        cameraDistanceXZ = cameraDistanceY * (100f / 82.5f);

        addition = new Vector3 (cameraDistanceXZ, cameraDistanceY, -cameraDistanceXZ);

        Camera.main.transform.position = GameController.current.Player.transform.position + addition;

	}
}