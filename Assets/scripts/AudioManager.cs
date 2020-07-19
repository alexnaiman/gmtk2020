using UnityEngine.Audio;
using System;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{

    public static AudioManager instance;

    public AudioMixerGroup mixerGroup;
    public float themeVolume;
    public Sound[] sounds;





    void Awake()
    {
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.loop = s.loop;

            s.source.outputAudioMixerGroup = mixerGroup;
            if (s.name.Contains("Theme")) s.source.volume = themeVolume;

        }
        Play("SlowThemeSong");
    }

    public void Play(string sound)
    {
        Sound s = Array.Find(sounds, item => item.name == sound);
        if (s == null)
        {
            return;
        }

        s.source.volume = s.volume * (1f + UnityEngine.Random.Range(-s.volumeVariance / 2f, s.volumeVariance / 2f));
        s.source.pitch = s.pitch * (1f + UnityEngine.Random.Range(-s.pitchVariance / 2f, s.pitchVariance / 2f));
        if (!s.source.isPlaying)
        {
            s.source.Play();
        }
    }

    public void Stop(string sound)
    {
        Sound s = Array.Find(sounds, item => item.name == sound);
        if (s == null)
        {
            return;
        }

        s.source.volume = s.volume * (1f + UnityEngine.Random.Range(-s.volumeVariance / 2f, s.volumeVariance / 2f));
        s.source.pitch = s.pitch * (1f + UnityEngine.Random.Range(-s.pitchVariance / 2f, s.pitchVariance / 2f));
        s.source.Stop();

    }


    public void OnFxVolumeChange()
    {

        AudioListener.volume = GameObject.Find("SoundFxSlider").GetComponent<Slider>().value;
    }
    public void OnMusicVolumeChange()
    {

        foreach (Sound s in sounds)
        {
            if (s.name.Contains("Theme")) s.source.volume = themeVolume * GameObject.Find("MusicSlider").GetComponent<Slider>().value;
        }
    }
}
