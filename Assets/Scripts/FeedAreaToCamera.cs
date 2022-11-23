using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Feed the area to the camera.
/// </summary>

[AddComponentMenu("Scripts/ESI/Feed Area To Camera"), RequireComponent(typeof(GameArea))]
public class FeedAreaToCamera : MonoBehaviour {

    [field: SerializeField] private GameArea _gameArea { get; set; }


    private void FitToCamera(Camera cam) {
        //cam.aspect    // Set the aspect ratio of the camera.

        //cam.orthographicSize       // Set the orthographic size of the camera.

        _gameArea.SetArea(new Vector2(cam.aspect * cam.orthographicSize * 2, cam.orthographicSize * 2));
        transform.position = cam.transform.position;
        transform.rotation = cam.transform.rotation;
    }
}
