using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Need a GameArea. Return the size of the game area based on camera view.
/// </summary>

[AddComponentMenu("Scripts/ESI/Fit Area To Camera"), RequireComponent(typeof(GameArea))]
public class FitAreaToCamera : MonoBehaviour {

    [field: SerializeField] private GameArea _gameArea { get; set; }

    private void OnValidate() {
        AssingGameArea();
        FitToMainCamera();
    }

    private void Reset() {
        AssingGameArea();
        FitToMainCamera();
    }

    private void Awake() {
        AssingGameArea();
        FitToMainCamera();
    }

    private void AssingGameArea() {
        if (_gameArea == null)
            _gameArea = GetComponent<GameArea>();
    }

    private void FitToCamera(Camera cam) {
        //cam.aspect    // Set the aspect ratio of the camera.

        //cam.orthographicSize       // Set the orthographic size of the camera.

        //_gameArea.SetArea(new Vector2(cam.aspect * cam.orthographicSize * 2, cam.orthographicSize * 2));
        _gameArea.Size = new Vector2(cam.aspect * cam.orthographicSize * 2, cam.orthographicSize * 2);

        transform.position = new Vector3(cam.transform.position.x, cam.transform.position.y, transform.position.z);
        transform.rotation = cam.transform.rotation;
    }

    private void FitToMainCamera() {
        //FitToCamera(mainCamera);
        FitToCamera(Camera.main);
    }
}
