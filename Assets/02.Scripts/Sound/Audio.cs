using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio : PoolableMono
{
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public override void Reset()
    {
        audioSource.Stop();
    }
}
