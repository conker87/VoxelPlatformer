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
		
		// Camera.main.orthographic = true;
        // transform.rotation = Quaternion.Euler(30f, 45f, 0);

        Camera.main.transform.eulerAngles = new Vector3 (30f, 315f, 0f);

        if (MainGameController.current.player == null) {

            return;

        }

        MainGameController.current.player.GetComponent<CharController>().ResetForwardDirection();

    }

	void LateUpdate() {

		if (MainGameController.current.player == null) {

			return;

		}

		transform.position = MainGameController.current.player.transform.position;

		Vector3 addition = Vector3.zero;

        cameraDistanceXZ = cameraDistanceY * (100f / 82.5f);

        addition = new Vector3 (cameraDistanceXZ, cameraDistanceY, -cameraDistanceXZ);

        Camera.main.transform.position = MainGameController.current.player.transform.position + addition;

	}
}