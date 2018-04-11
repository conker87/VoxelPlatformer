using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour {

    [SerializeField]
    private float speed = 5f, damage = 1f, destroyAfter = 5f;

    public float Speed {
        get {
            return speed;
        }

        set {
            speed = value;
        }
    }
    public float Damage {
        get {
            return damage;
        }

        set {
            damage = value;
        }
    }
    public float DestroyAfter {
        get {
            return destroyAfter;
        }

        set {
            destroyAfter = value;
        }
    }

    void Start () {

        if (DestroyAfter > 0f) {
            Destroy(gameObject, DestroyAfter);
        }
        
	}
	
	void Update () {

        transform.position += transform.forward * Speed * Time.deltaTime;

    }

    private void OnTriggerEnter(Collider other) {

        Player player;

        if ((player = other.GetComponentInParent<Player>()) != null) {

            Debug.Log("Ammo::OnTriggerEnter::Player");

            player.DamagePlayer(1);

            Speed = 0f;

            Destroy(GetComponentInChildren<MeshCollider>());

            transform.parent = other.transform;

        }

    }

}