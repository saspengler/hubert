using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightWallHangingCheck : MonoBehaviour
{

  private HubertBehave player;

  // Use this for initialization
  void Start()
  {
    player = gameObject.GetComponentInParent<HubertBehave>();
  }

  private void OnTriggerEnter2D(Collider2D col)
  {
    if (col.gameObject.tag == "Ground" && !player.grounded)
    {
      player.wallHang = true;
    }

    if (col.gameObject.tag == "Ground" && player.grounded)
    {
      player.wallHang = false;
    }
  }
  private void OnTriggerStay2D(Collider2D col)
  {
    if (col.gameObject.tag == "Ground" && !player.grounded)
    {
      player.wallHang = true;
    }
    if (col.gameObject.tag == "Ground" && player.grounded)
    {
      player.wallHang = false;
    }
  }
  private void OnTriggerExit2D(Collider2D col)
  {
    if (col.gameObject.tag == "Ground")
    {
      player.wallHang = false;
    }   
  }
}
