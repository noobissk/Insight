using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PlayAudioClip : MonoBehaviour
{
    AudioSource _source;
    bool _hasPlayed = false;
    void Start()
    {
        if (_source == null)
            _source = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (_source.isPlaying && !_hasPlayed)
            _hasPlayed = true;

        if (!_source.isPlaying && _hasPlayed)
            Destroy(gameObject);
    }

    public void PlayAudio(AudioClip i_clip)
    {
        if (_source == null)
            _source = GetComponent<AudioSource>();
        _source.clip = i_clip;
        _source.Play();
    }
}
