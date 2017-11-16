using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HubertBlock : MonoBehaviour
{
  public HubertBehave Hubert;

  Animator anim;

  public bool isBlocking;
  public bool isShooting;

  bool canBlock;

  Collider2D blockTrigger;

  SpriteRenderer myRenderer;

  public GameObject Projectile;
  public GameObject shootTarget;

  // Use this for initialization
  void Start()
  {
    myRenderer = gameObject.GetComponent<SpriteRenderer>();
    myRenderer.enabled = false;

    blockTrigger = gameObject.GetComponent<Collider2D>();
    blockTrigger.enabled = false;

    anim = gameObject.GetComponentInParent<Animator>();

    isShooting = false;
    isBlocking = false;
    canBlock = true;
  }

  // Update is called once per frame
  void Update()
  {

    //Blocken
    if (Input.GetKey(KeyCode.K) && canBlock && Hubert.grounded && !Hubert.isDuck)
    {
      isBlocking = true;
    }

    if (Input.GetKeyUp(KeyCode.K) || !canBlock || !Hubert.grounded)
    {
      isBlocking = false;
    }

    if (isBlocking)
    {
      blockTrigger.enabled = true;
      myRenderer.enabled = true;
    }

    if (!isBlocking)
    {
      blockTrigger.enabled = false;
      myRenderer.enabled = false;
    }

    //Schießen
    if (Input.GetKey(KeyCode.L))
    {
      isShooting = true;
    }

    if (Input.GetKeyDown(KeyCode.L))
    {
      Instantiate(Projectile,
      new Vector3(shootTarget.transform.position.x,
      shootTarget.transform.position.y,
      shootTarget.transform.position.z),
      Quaternion.identity);
    }

    if (Input.GetKeyUp(KeyCode.L))
    {
      isShooting = false;
    }
    anim.SetBool("isBlocking", isBlocking);
    anim.SetBool("isShooting", isShooting);
  }
}
