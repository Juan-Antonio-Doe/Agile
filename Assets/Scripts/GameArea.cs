using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

/// <summary>
/// Define a Rectangular Area.
/// </summary>

[AddComponentMenu("Scripts/ESI/Game Area")]
public class GameArea : MonoBehaviour {

    [field: Header("Debug")]
    [field: SerializeField] public Rect _area { get; private set; }

    [Header("Variables")]
    [SerializeField, HideInInspector] private Vector2 _size;

    [field: SerializeField] private Color gizmoColor { get; set; } = Color.green;

    // Accessor or Getter and Setter.
    /*public Rect Area {
        get { return _area; }
        set { _area = value; }
    }*/

    public Vector2 Size {
        get { return _area.size; }
        set {
            _size = value;
            _area = new Rect(value.x * -0.5f, value.y * -0.5f, value.x, value.y);
        }
    }

    private void OnValidate() {
        //SetArea(size);
        Size = _size;
    }

    /*public void SetArea(Vector2 size) {
        //_area = new Rect(0, 0, size.x, size.y);
        //_area = new Rect(Vector2.zero, size);
        _area = new Rect(size.x * -0.5f, size.y * -0.5f, size.x, size.y);

    }*/

    private void OnDrawGizmos() {
        if (Selection.activeGameObject != transform.gameObject)
            return;

        //Gizmos.matrix = transform.localToWorldMatrix; // Asignamos el gizmo a la posición del objeto.
        Gizmos.color = gizmoColor;
        Gizmos.DrawWireCube(transform.position, _area.size);
    }

    /*private void Update() {
        Debug.Log(size);
        Debug.Log(_area.size);
    }*/

    public Vector3 GetRandomPosition() {
        Vector3 randomPosition = Vector3.zero;
        randomPosition.x = Random.Range(-_size.x / 2, _size.x / 2);
        randomPosition.y = Random.Range(-_size.x / 2, _size.x / 2);
        randomPosition = transform.TransformPoint(randomPosition);
        return randomPosition;
    }
}
