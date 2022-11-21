using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleShipController : MonoBehaviour {

    [field: SerializeField] public float _speed { get; private set; } = 10f;

    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        transform.Translate(new Vector3(0, y, 0) * _speed * Time.deltaTime);
        transform.Rotate(new Vector3(0, 0, -x) * _speed * 5 * Time.deltaTime);
    }
}
