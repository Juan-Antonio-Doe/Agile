using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Deal with projectile behaviour.
/// </summary>

[AddComponentMenu("Scripts/ESI/Projectile")]
public class Projectile : MonoBehaviour {

    [field: SerializeField] private float _speed { get; set; } = 0.5f;

    private void FixedUpdate() {
        transform.Translate(_speed * Time.fixedDeltaTime * Vector3.up);
        //transform.Translate(0, _speed * Time.fixedDeltaTime, 0, Space.Self);
    }

}
