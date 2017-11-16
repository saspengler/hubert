using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class walkSound : MonoBehaviour
{
    private AudioSource Source;
    // Use this for initialization
    void Start()
    {
        Source = gameObject.GetComponent<AudioSource>();
        Source.volume = 0.3f;
        Source.playOnAwake = true;
    }

    // Update is called once per frame
    void Update()
    {
        Source.pitch = Random.Range(0.6f, .8f);

    }
}
