using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftWallHangingCheck : MonoBehaviour
{

    private HubertBehave player;

    // Use this for initialization
    void Start()
    {
        player = gameObject.GetComponentInParent<HubertBehave>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Ground")
        {
            player.leftWallHang = true;
        }
    }
    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.tag == "Ground")
        {
            player.leftWallHang = true;
        }
    }
    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Ground")
        {
            player.leftWallHang = false;
        }
    }
}

