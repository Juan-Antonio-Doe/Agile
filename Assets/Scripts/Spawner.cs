using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

/* Spawner Class
 * Spawn asteroids in a position inside the game area.
 */

[AddComponentMenu("Scripts/ESI/Spawner")]
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

    [field: Header("Velocity")]
    [field: SerializeField, Range(-180f, 180f)] private float angle { get; set; }
    [field: SerializeField, Range(0f, 360f)] private float spread { get; set; } = 30f;
    [field: SerializeField, Range(0f, 10f)] private float minStrength { get; set; } = 1f;
    [field: SerializeField, Range(0f, 10f)] private float maxStrength { get; set; } = 10f;

    [field: Header("Animation")]
    [field: SerializeField] private Animator _animator { get; set; }
    [field: SerializeField] private string _animatorSpawningParameterName { get; set; } = "Spawning";
    private int _spawningHashID;
    [field: SerializeField] private float _animatorDelayIn { get; set; } = 1f;
    [field: SerializeField] private float _animatorDelayOut { get; set; } = 1f;


    /*    private void Start() {
            //Instantiate(reference, transform.position, Quaternion.identity);
            //_timeStamp = Time.time;
            StartCoroutine(Spawn());
        }*/

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

    private void Awake() {
        if (_animator)
            _spawningHashID = Animator.StringToHash(_animatorSpawningParameterName);
    }

    IEnumerator Start() {
        if (!_gameArea)
            _gameArea = GameArea.Main;
        
        _remaingNumber = maxNumber;
        
        if (_minDistanceFromPlayer > 0) {
            if (_player == null)
                _player = GameObject.FindGameObjectWithTag("Player").transform;

            if (_player == null)
                Debug.LogWarning("Player not found. Please, assing Player tag to the player object.");
        }

        if (_animator) {
            _animator.SetBool(_spawningHashID, true);
            yield return new WaitForSeconds(_animatorDelayIn);
        }

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
                //Debug.Log("Player is too close");
                Vector2 debugPos = randomPos;
                Debug.DrawLine(transform.position, randomPos, Color.green, 3f);
                Debug.DrawLine(_player.position, debugPos, Color.red, 1f);
                randomPos = (randomPos - _player.position).normalized * _minDistanceFromPlayer;
                Debug.DrawLine(debugPos, randomPos, Color.yellow, 1f);
                //Debug.Break();
            }

            // ToDo: Use Object Pooling

            GameObject go = Instantiate(reference, randomPos, Quaternion.identity);
            Rigidbody2D rb2D = go.GetComponent<Rigidbody2D>();

            if (rb2D) {
                float angleDelta = UnityEngine.Random.Range(-spread * 0.5f, spread * 0.5f);
                float angle_ = angle + angleDelta;
                Vector2 direction = new Vector2(Mathf.Sin(angle_ * Mathf.Deg2Rad), Mathf.Cos(angle_ * Mathf.Deg2Rad));
                direction *= UnityEngine.Random.Range(minStrength, maxStrength);
                rb2D.velocity = direction;
                //rb2D.AddForce(direction, ForceMode2D.Impulse);

            }
            
            _remaingNumber--;

            yield return new WaitForSeconds(1 / UnityEngine.Random.Range(_minRate, _maxRate));
        }
        
        if (_animator) {
            _animator.SetBool(_spawningHashID, false);
            yield return new WaitForSeconds(_animatorDelayOut);
        }

        gameObject.SetActive(false);
    }
}
