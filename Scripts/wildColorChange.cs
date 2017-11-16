using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wildColorChange : MonoBehaviour
{

    // Use this for initialization
    SpriteRenderer farbi;
    private void Start()
    {
        farbi = gameObject.GetComponent<SpriteRenderer>();
    }
    // Update is called once per frame
    void Update()
    {
        farbi.color = new Color((Random.Range(0, 50)), (Random.Range(0, 50)), (Random.Range(0, 50)));
    }
}
