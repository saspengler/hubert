using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HubertAttack : MonoBehaviour
{
  public HubertBehave Hubert;

  Animator anim;

  bool rightPunch;
  public bool isAttacking;

  public bool isAirAttacking;

  float attackTimer;
  float attackCd = 0.3f;

  float airAttackTimer;
  float airAttackCd = 0.6f;

  Collider2D attackTrigger;

  public Collider2D airAttackTrigger;


  // Use this for initialization
  void Start()
  {
    attackTrigger = gameObject.GetComponent<Collider2D>();
    attackTrigger.enabled = false;
    airAttackTrigger.enabled = false;

    anim = gameObject.GetComponentInParent<Animator>();

    rightPunch = true;
    isAttacking = false;

    attackTimer = attackCd;
  }

  // Update is called once per frame
  void Update()
  {
    if (Input.GetKeyDown(KeyCode.J) && !isAttacking &&
        !Hubert.isDuck)
    {
      //Boden-Angriff
      if (Hubert.grounded && !Hubert.isSprint)
      {
        isAttacking = true;
        rightPunch = !rightPunch;
        attackTrigger.enabled = true;
      }
      //Luft-Angriff
      if (!Hubert.grounded)
      {
        isAirAttacking = true;
        airAttackTrigger.enabled = true;
      }
    }
    //Boden-Timer
    if (isAttacking)
    {
      if (attackTimer > 0)
      {
        attackTimer -= Time.deltaTime;
      }
      else
      {
        isAttacking = false;
        attackTrigger.enabled = false;
        attackTimer = attackCd;
      }
    }
    //Luft-Timer
    if (isAirAttacking)
    {
      if (airAttackTimer > 0)
      {
        airAttackTimer -= Time.deltaTime;
      }
      else
      {
        isAirAttacking = false;
        airAttackTrigger.enabled = false;
        airAttackTimer = airAttackCd;
      }
    }
    //Luft-Angriff zurücksetzen
    if (Hubert.grounded)
    {
      isAirAttacking = false;
      airAttackTrigger.enabled = false;
      airAttackTimer = airAttackCd;
    }
    anim.SetBool("rightPunch", rightPunch);
    anim.SetBool("isAttacking", isAttacking);
    anim.SetBool("isAirAttacking", isAirAttacking);
  }
}
