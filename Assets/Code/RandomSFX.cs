using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class RandomSFX : MonoBehaviour
{
    public AudioClip[] Sounds;
    public AudioSource AudioSource;
    // Start is called before the first frame update
    void OnEnable()
    {
        AudioSource.clip = Sounds[Random.Range(0, Sounds.Length)];
        AudioSource.Play();
    }

}
