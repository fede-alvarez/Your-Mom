using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public enum AudioList
    {
        Abucheo,
        Aplauso,
        Llorar,
        Punio,
        Risa,
        Pastel,
        CajaSorpresa,
        CardHover,
        CardSelected
    }

    [SerializeField] private List<AudioScriptable> _audioList;

    [Header("Sources")]
    [SerializeField] private AudioSource _musicSource;
    [SerializeField] private AudioSource _soundSource;
    [SerializeField] private AudioSource _uiSoundSource;

    [Header("Mixer")]
    [SerializeField] private AudioMixer _mixer;
    private bool _isFading = false;
    private bool _isPaused = false;

    private static AudioManager _instance;

    private void Awake()
    {
        if (_instance != null && _instance != this)
            Destroy(this.gameObject);
        else
            _instance = this;
    }

    private void OnGamePaused(bool state, bool other)
    {
        _isPaused = state;
    }

    public void PlayUISound(AudioList audioItem, bool randomSound = false)
    {
        if (_isPaused) return;
        AudioScriptable audioScript = _audioList[(int)audioItem];
        if (!audioScript) return;

        _uiSoundSource.volume = Random.Range(audioScript.volume.x, audioScript.volume.y);
        _uiSoundSource.pitch = Random.Range(audioScript.pitch.x, audioScript.pitch.y);

        AudioClip clip = (randomSound) ? audioScript.GetRandom() : audioScript.Get(0);
        _uiSoundSource.PlayOneShot(clip);
    }

    public void PlaySound(AudioList audioItem, bool randomSound = false)
    {
        if (_isPaused) return;
        AudioScriptable audioScript = _audioList[(int)audioItem];
        if (!audioScript) return;

        _soundSource.volume = Random.Range(audioScript.volume.x, audioScript.volume.y);
        _soundSource.pitch = Random.Range(audioScript.pitch.x, audioScript.pitch.y);

        AudioClip clip = (randomSound) ? audioScript.GetRandom() : audioScript.Get(0);
        _soundSource.PlayOneShot(clip);
    }
    public void PlaySoundAt(AudioList audioItem, Vector3 position, bool randomSound = false)
    {
        if (_isPaused) return;
        AudioScriptable audioScript = _audioList[(int)audioItem];
        if (!audioScript) return;

        GameObject go = new GameObject();
        go.transform.position = position;

        AudioSource source = go.AddComponent<AudioSource>();
        source.spatialBlend = 1.0f;
        source.rolloffMode = AudioRolloffMode.Linear;
        source.minDistance = 2.0f;
        source.maxDistance = 5.0f;

        //source.clip = audioScript.Get(0);
        source.PlayOneShot(audioScript.Get(0));

        Destroy(go, source.clip.length / source.pitch);
    }

    public void CreateSoundAndPlay(AudioList audioItem)
    {
        if (_isPaused) return;
        AudioScriptable audioScript = _audioList[(int)audioItem];
        if (!audioScript) return;

        GameObject go = new GameObject();
        AudioSource source = go.AddComponent<AudioSource>();

        source.clip = audioScript.Get(0);
        source.Play();

        Destroy(go, source.clip.length / source.pitch);
    }

    public void PlayMusic(AudioList audioItem, bool randomSound = true, int soundIndex = -1)
    {
        if (_isFading) return;

        AudioScriptable audioScript = _audioList[(int)audioItem];
        if (!audioScript || !audioScript.isMusic) return;

        if (randomSound)
            _musicSource.clip = audioScript.GetRandom();
        else
            _musicSource.clip = audioScript.Get(soundIndex);

        _musicSource.volume = Random.Range(audioScript.volume.x, audioScript.volume.y);
        _musicSource.pitch = Random.Range(audioScript.pitch.x, audioScript.pitch.y);

        _musicSource.Play();
    }

    public void StopMusic()
    {
        //StartCoroutine("FadeOutOnly");
        _musicSource.Stop();
    }

    public void FadeBetweenMusic(AudioList musicClip)
    {
        _isFading = true;
        StartCoroutine("FadeOut", musicClip);
    }

    private IEnumerator FadeOut(AudioList musicClip)
    {
        _isFading = false;

        float musicVolume;
        _mixer.GetFloat("MusicVolume", out musicVolume);

        while (musicVolume > -80)
        {

            _mixer.SetFloat("MusicVolume", musicVolume -= 2f);
            yield return new WaitForSeconds(0.1f);
        }

        PlayMusic(musicClip);
        StartCoroutine("FadeIn");
    }

    private IEnumerator FadeOutOnly()
    {
        _isFading = false;

        float musicVolume;
        _mixer.GetFloat("MusicVolume", out musicVolume);

        while (musicVolume > -80)
        {

            _mixer.SetFloat("MusicVolume", musicVolume -= 2f);
            yield return new WaitForSeconds(0.1f);
        }
    }


    private IEnumerator FadeIn()
    {
        float musicVolume;
        _mixer.GetFloat("MusicVolume", out musicVolume);

        while (musicVolume < -20)
        {
            _mixer.SetFloat("MusicVolume", musicVolume += 2f);
            yield return new WaitForSeconds(0.1f);
        }
    }

    private void OnDestroy()
    {
        //EventManager.GamePaused -= OnGamePaused;
        if (_instance != null) _instance = null;
    }

    public static AudioManager GetInstance
    {
        get { return _instance; }
    }
}
