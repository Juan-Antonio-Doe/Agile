using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Spawner Class
 * Spawn asteroids in a position inside the game area.
 */

[AddComponentMenu("Scripts/ESI/Spawner"), /*RequireComponent(typeof(GameArea))*/]
public class Spawner : MonoBehaviour {

    [field: Header("Spawn")]
    [field: SerializeField] private GameObject reference { get; set; }

    [field: Header("Spawning")]
    [field: SerializeField, Range(0.001f, 100f)] private float _minRate { get; set; } = 1f;
    [field: SerializeField, Range(0.001f, 100f)] private float _maxRate { get; set; } = 100f;
    [field: SerializeField] private bool infinite { get; set; }
    [field: SerializeField] private int maxNumber { get; set; } = 5;
    private float _timeStamp { get; set; } = 0f;
    private int _remaingNumber { get; set; } = 0;

    [field: Header("Locations")]
    [field: SerializeField] private GameArea _gameArea { get; set; }
    [field: SerializeField] private Transform _player { get; set; }
    [field: SerializeField] private float _minDistanceFromPlayer { get; set; } = 5f;

    private void Start() {
        //Instantiate(reference, transform.position, Quaternion.identity);
        //_timeStamp = Time.time;
        _remaingNumber = maxNumber;
        StartCoroutine(Spawn());
    }

    /*private void Update() {
        if (Time.time <= _timeStamp) {
            return;
        }

        if (_remaingNumber > 0) {
            Instantiate(reference, GetRandomPosition(), Quaternion.identity);
            _remaingNumber--;
            if (_remaingNumber <= 0)
                enabled = false;
        }

        _timeStamp = Time.time;
    }*/

    IEnumerator Spawn() {
        while (infinite || _remaingNumber > 0) {
            Vector3 randomPos;
            /*if (_gameArea) {
                randomPos = _gameArea.GetRandomPosition();
                randomPos.y = transform.position.y;
            }
            else
                randomPos = transform.position;*/

            // var variable = condition ? true : false
            randomPos = _gameArea ? _gameArea.GetRandomPosition() : transform.position;
            //randomPos.y = transform.position.y;

            if (_player && Vector3.Distance(randomPos, _player.position) < _minDistanceFromPlayer) {
                //randomPos = _player.position + (_player.position - randomPos).normalized * _minDistanceFromPlayer;
                Debug.Log("Player is too close");
                Debug.DrawLine(_player.position, randomPos, Color.red, 1f);
            }

            Instantiate(reference, randomPos, Quaternion.identity);
            _remaingNumber--;

            yield return new WaitForSeconds(1 / UnityEngine.Random.Range(_minRate, _maxRate));
        }
    }
}
