using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Deal with items behaviour.
/// </summary>

[AddComponentMenu("Scripts/ESI/Item")]
public class Item : MonoBehaviour {

    public enum Type {
        None,
        RepairKit,
        ExtraLife
    }

    [field: SerializeField] public Type type { get; private set; } = Type.None;

    [field: SerializeField] private AudioSource _audioSource { get; set; }

    [field: SerializeField] private Renderer _renderer { get; set; }
    [field: SerializeField] private Collider2D _collider2D { get; set; }

    private void Awake() {
        if (_audioSource == null)
            _audioSource = GetComponent<AudioSource>();
        if (_renderer == null)
            _renderer = GetComponent<Renderer>();
        if (_collider2D == null)
            _collider2D = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (!collision.CompareTag("Player")) 
            return;

        switch (type) {
            case Type.RepairKit:
                GameManager.Damage = 0;
                break;
            case Type.ExtraLife:
                GameManager.Lives++;
                break;
            default:
                break;
        }

        StartCoroutine(PlaySoundAndRelease());
    }

    private IEnumerator PlaySoundAndRelease() {
        _renderer.enabled = _collider2D.enabled = false;

        _audioSource.Play();
        yield return new WaitForSeconds(_audioSource.clip.length);
        //ToDo: Use Object Pooling.
        Destroy(gameObject);
    }
}
