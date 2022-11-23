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
    [SerializeField] private Vector2 size;

    [field: SerializeField] private Color gizmoColor { get; set; } = Color.green;

    // Accessor or Getter and Setter.
    /*public Rect Area {
        get { return _area; }
        set { _area = value; }
    }*/

    private void OnValidate() {
        SetArea(size);
    }

    private void Awake() {
        SetArea(size);
    }

    public void SetArea(Vector2 size) {
        //_area = new Rect(0, 0, size.x, size.y);
        //_area = new Rect(Vector2.zero, size);
        _area = new Rect(size.x * -0.5f, size.y * -0.5f, size.x, size.y);

    }

    private void OnDrawGizmos() {
        if (Selection.activeGameObject != transform.gameObject)
            return;

        //Gizmos.matrix = transform.localToWorldMatrix; // Asignamos el gizmo a la posición del objeto.
        Gizmos.color = gizmoColor;
        Gizmos.DrawWireCube(transform.position, size);
    }
}
