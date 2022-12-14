using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleShipController : MonoBehaviour {

    [field: SerializeField] public float _speed { get; private set; } = 10f;
    [field: SerializeField] private Rigidbody2D _rigidbody2D { get; set; }

    private Vector2 _delta = Vector2.zero;
    private Vector2 _force = Vector2.zero;
    private float _torque { get; set; }

    // Multiplicadores
    [field: SerializeField] private float thrustMultiplier { get; set; } = 1f;
    [field: SerializeField] private float steerMultiplier { get; set; } = 1f;

    // Update is called once per frame
    void FixedUpdate() {

#if (UNITY_IOS || UNITY_ANDROID  && !UNITY_EDITOR || REMOTE)
        if (Input.touchCount > 0) {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Moved) {
                _delta.x = touch.deltaPosition.x * _speed * Time.deltaTime;
                _delta.y = touch.deltaPosition.y * _speed * Time.deltaTime;
            }
        }
#else
        _delta.x = Input.GetAxis("Horizontal");
        _delta.y = Input.GetAxis("Vertical");
#endif
        /*transform.Translate(new Vector3(0, _delta.y, 0) * _speed * Time.deltaTime);
        transform.Rotate(new Vector3(0, 0, -_delta.x) * _speed * 5 * Time.deltaTime);*/

        _force.y = _delta.y/* * _speed*/ * thrustMultiplier;
        _torque = -_delta.x/* * _speed*/ * steerMultiplier;

        _rigidbody2D.AddRelativeForce(_force);
        _rigidbody2D.AddTorque(_torque);

#if UNITY_PLAYMODE
        if (Input.GetKeyDown(KeyCode.Space)) {
            Debug.Log("PlayMode executed.");
        }
#endif
    }
}
