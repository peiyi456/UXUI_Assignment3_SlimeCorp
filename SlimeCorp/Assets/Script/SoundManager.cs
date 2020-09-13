using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static AudioClip BGmusic, AttackBG, WinBG, LoseBG;
    static AudioSource audioSrc;

    // Start is called before the first frame update
    void Start()
    {
        audioSrc = GetComponent<AudioSource>();
        BGmusic = Resources.Load<AudioClip>("BGmusic");
        AttackBG = Resources.Load<AudioClip>("AttackMusic");
        WinBG = Resources.Load<AudioClip>("WinMusic");
        LoseBG = Resources.Load<AudioClip>("LossMusic");
    }

    //AttackBG
    public static void playBGmusic()
    {
        audioSrc.clip = BGmusic;
        audioSrc.Play();
    }

    public static void playAttackMusic()
    {
        audioSrc.clip = AttackBG;
        audioSrc.Play();
    }

    public static void playWinMusic()
    {
        audioSrc.clip = WinBG;
    }

    public static void playLossMusic()
    {
        audioSrc.clip = LoseBG;
    }
}
