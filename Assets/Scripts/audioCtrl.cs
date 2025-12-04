using System.Collections.Generic;
using UnityEngine;

public class audioCtrl : MonoBehaviour
{
    public static audioCtrl I;

    public AudioSource music_source;
    public AudioSource[] sound_sources;

    private Queue<AudioSource> queue_sources;

    public AudioClip music;
    public AudioClip click;
    public AudioClip move;
    public AudioClip win;
    public AudioClip lose;
    public AudioClip dynamic;
    public AudioClip shark;

    private void Awake()
    {
        I = this;
        queue_sources = new Queue<AudioSource>(sound_sources);
    }
    private void Start()
    {
        PlayMusic();
    }

    public virtual void PlaySoundByType(AudioType type)
    {
        switch (type)
        {
            case AudioType.CLICK:
                PlayAudio(click);
                break;
            case AudioType.WIN:
                PlayAudio(win); 
                break;
            case AudioType.MOVE:
                PlayAudio(move);
                break;
            case AudioType.LOSE:
                PlayAudio(lose);
                break;
            case AudioType.DYNAMIC:
                PlayAudio(dynamic);
                break;
            case AudioType.SHARK:
                PlayAudio(shark);
                break;
        }
    }

    public void PlayAudio(AudioClip clip)
    {
        var source = queue_sources.Dequeue();
        if (source == null)
            return;
        //source.volume = Data.Sound;
        source.PlayOneShot(clip);
        queue_sources.Enqueue(source);
    }

    public void PlayMusic()
    {
        music_source.clip = music;
        music_source.Play();
    }
}

public enum AudioType
{
    CLICK,
    MOVE,
    WIN,
    LOSE,
    DYNAMIC,
    SHARK
}