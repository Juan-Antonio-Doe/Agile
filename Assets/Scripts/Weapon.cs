using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Deal with weapon behaviour.
/// </summary>

[AddComponentMenu("Scripts/ESI/Weapon")]
public class Weapon : MonoBehaviour {

    [field: SerializeField] private GameObject _projectile { get; set; }
    [field: SerializeField] private Collider2D _shipCollider2D { get; set; }

    private void Awake() {
        _shipCollider2D = transform.parent.GetComponent<Collider2D>();
    }

    /*private void Start() {
        Fire();
    }*/

    private void Fire() {
        GameObject projectileInstance = Instantiate(_projectile, transform.position, transform.rotation);
        Physics2D.IgnoreCollision(projectileInstance.GetComponent<Collider2D>(), _shipCollider2D);
    }
}
