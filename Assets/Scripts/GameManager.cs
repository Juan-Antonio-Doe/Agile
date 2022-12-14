using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manage game state.
/// </summary>

[AddComponentMenu("Scripts/ESI/Game Manager")]
public class GameManager : MonoBehaviour {

    public const float maxDamage = 100f;

    private static float _damage;
    public static float Damage {
        get { return _damage; }
        set {
            if (_damage != value) {
                _damage = value;
                Debug.Log($"Current Damage: {_damage}");

                if (_damage >= maxDamage) {
                    _lives--;
                    _damage = 0;
                }
            }
        }
    }

    private static int _lives = 5;
    public static int Lives {
        get { return _lives; }
        set {
            if (value != _lives) {
                _lives = value;
                Debug.Log($"Current Lives: {_lives}");
                if (_lives <= 0) {
                    //ToDo: Handle Game Over State.
                    Debug.Log("Game Over");
                }
            }
        }
    }

}
