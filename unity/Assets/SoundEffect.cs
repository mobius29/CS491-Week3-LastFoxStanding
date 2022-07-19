using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffect : MonoBehaviour
{
    private static AudioSource star_source;

    void Start() {
        star_source = GetComponent<AudioSource>();
        star_source.Stop();
    }
    public static void playStarSound() {
        star_source.Play();
        star_source.loop = true;
    }
    public static void stopStarSound() {
        star_source.Stop();
    }
}
