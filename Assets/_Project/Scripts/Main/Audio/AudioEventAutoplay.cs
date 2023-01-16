using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Triggers;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioEventAutoplay : MonoBehaviour
{
    [SerializeField] private Behaviours _behaviour;
    [SerializeField] private AudioEvent _audioEvent;
    
    private AudioSource _audioSource;
    private Collider _collider;
    
    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _collider = GetComponent<Collider>();
    }

    private void Start()
    {
        switch (_behaviour)
        {
            case Behaviours.OnStart:
                Play();
                break;
            case Behaviours.OnCollideOnce:
                if (_collider == null) Debug.LogError("Collider not found. (Click for info)", gameObject);
                _ = CheckColliding();
                break;
        }
    }

    private async UniTask CheckColliding()
    {
        var trigger = this.GetAsyncCollisionEnterTrigger();
        var handler = trigger.GetOnCollisionEnterAsyncHandler();
        await handler.OnCollisionEnterAsync();
        Play();
    }

    private void Play()
    {
        _audioEvent.Play(_audioSource);
    }

    private enum Behaviours
    {
        OnStart,
        OnCollideOnce,
    }
}

