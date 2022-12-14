using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Deal with player damage.
/// </summary>

[AddComponentMenu("Scripts/ESI/Player Damage")]
public class ShipDamage : MonoBehaviour {

    [field: SerializeField] private float vulnerability { get; set; } = 1f;

    [field: SerializeField] private Rigidbody2D _rigidbody2D { get; set; }
    [field: SerializeField] private Collider2D _collider2D { get; set; }

    private float _damage;
    public float Damage {
        get { return GameManager.Damage; }
        set { GameManager.Damage = value; }
    }

    private void Awake() {
        if (_rigidbody2D == null)
            _rigidbody2D = GetComponent<Rigidbody2D>();
        if (_collider2D == null)
            _collider2D = GetComponent<Collider2D>();
    }

    /*private void ApplyDamage(float damage) {
        Damage += damage;
    }*/

    private void OnCollisionEnter2D(Collision2D collision) {

        float damage = collision.relativeVelocity.magnitude;
        
        if (collision.collider.sharedMaterial) {
            damage *= _collider2D.sharedMaterial.friction * collision.collider.sharedMaterial.friction *
                (1 / _collider2D.sharedMaterial.bounciness) * (1 / collision.collider.sharedMaterial.bounciness);
        }
        
        if (collision.rigidbody) {
            damage *= (collision.rigidbody.mass / _rigidbody2D.mass);
        }
        
        Damage += damage;
    }
}
