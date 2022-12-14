using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Deal with asteroid collisions
 */

[AddComponentMenu("Scripts/ESI/Collision Damage")]
public class CollisionDamage : MonoBehaviour {

    [field: SerializeField] private float _damageAmount { get; set; } = 1f;

    private void OnCollisionEnter(Collision collision) {
        if (!collision.gameObject.CompareTag("Player")) {
            return;
        }

        //collision.gameObject.GetComponent<ShipDamage>().Damage += _damageAmount;
        collision.gameObject.SendMessage("ApplyDamage", _damageAmount);
    }
}
