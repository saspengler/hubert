using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HubertBehave : MonoBehaviour
{
  //Ints
  public int curHealth;
  public int maxHealth = 100;
  //floats
  //private float maxWallTimer = .5f;
  //private float wallTimer;
  private float lowPitch = 0.75f;
  private float highPitch = 1.5f;
  public float maxSpeed = 3;
  public float speed = 50f;
  public float jumpPower = 150f;
  public float _volume = 0.2f;
  public float cooldown = 0.5f;
  public float shadowCd = 0.3f;
  public float dbljump = 2f;
  //bools
  public bool isStanding;
  public bool leftWallHang;
  public bool wallHang;
  public bool isDuck;
  public bool isDblJump;
  public bool isSprint;
  public bool grounded;
  public bool canDoubleJump;
  public bool isShadow = false;
  public bool isActive = false;
  //Referenzen
  HubertAttack Attack;
  HubertBlock Block;
  public GameObject walljumptarget;
  public BoxCollider2D standCol;
  public BoxCollider2D duckCol;
  private Rigidbody2D rb2d;
  private Animator anim;
  //private BoxCollider2D bc2d;
  private AudioSource source;
  public AudioClip jump, dblJump;

  // Use this for initialization
  void Start()
  {
    Block = gameObject.GetComponentInChildren<HubertBlock>();
    Attack = gameObject.GetComponentInChildren<HubertAttack>();
    duckCol.enabled = false;
    isDblJump = false;
    transform.localScale = new Vector3(0.05f, 0.05f, 1);
    source = gameObject.GetComponent<AudioSource>();
    rb2d = gameObject.GetComponent<Rigidbody2D>();
    anim = gameObject.GetComponent<Animator>();
    curHealth = maxHealth;
  }

  // Update is called once per frame
  void Update()
  {
    //Animationsparameter für State-Machine
    anim.SetBool("isStand", isStanding);
    anim.SetBool("isWallHang", wallHang);
    anim.SetBool("grounded", grounded);
    anim.SetBool("isDblJump", isDblJump);
    anim.SetFloat("speed", Mathf.Abs(rb2d.velocity.x));
    anim.SetBool("isSprint", isSprint);
    anim.SetBool("isDuck", isDuck);

    //nach links drehen
    if (Input.GetKey(KeyCode.A) && !Block.isBlocking)
    {
      transform.localScale = new Vector3(-0.05f, 0.05f, 1);
    }
    //nach rechts drehen
    if (Input.GetKey(KeyCode.D) && !Block.isBlocking)
    {
      transform.localScale = new Vector3(0.05f, 0.05f, 1);
    }
    //Sprinten
    if (Input.GetKey(KeyCode.LeftShift) && grounded)
    {
      isSprint = true;
      speed = 75;
      maxSpeed = 4.5f;
    }
    //Wieder nur laufen
    if (Input.GetKeyUp(KeyCode.LeftShift) /*&& grounded*/)
    {
      isSprint = false;
      speed = 50;
      maxSpeed = 3f;
    }
    //Luftspeed
    if (!grounded)
    {
      speed = 10;
    }
    //bodenspeed ohne kriechen
    if (grounded && !isSprint && !isDuck)
    {
      speed = 50;
    }
    //bodenspeed mit kriechen
    if (isDuck)
    {
      speed = 35;
    }
    //Springen
    if (Input.GetKeyDown(KeyCode.Space) && grounded && !isDuck)
    {
      rb2d.velocity = new Vector2(0, jumpPower);
      canDoubleJump = true;
      source.pitch = Random.Range(lowPitch, highPitch);
      source.PlayOneShot(jump);
    }
    //Doppelsprung
    if (Input.GetKey(KeyCode.Space) && canDoubleJump && !grounded && rb2d.velocity.y <= 0 && !wallHang)
    {
      rb2d.AddForce(Vector2.up * jumpPower * dbljump);
      canDoubleJump = false;
      source.PlayOneShot(dblJump);
      source.pitch = Random.Range(lowPitch, highPitch);
      //Für Gleiten
      isDblJump = true;
    }
    //Gleiten
    if (Input.GetKey(KeyCode.Space) && !wallHang && rb2d.velocity.y <= 0)
    {
      rb2d.gravityScale = 0.3f;
    }
    //Doppelsprung Animation
    if (!grounded && !canDoubleJump && Input.GetKeyDown(KeyCode.Space))
    {
      isDblJump = true;
    }
    //Nicht mehr gleiten
    if (Input.GetKeyUp(KeyCode.Space))
    {
      rb2d.gravityScale = 1.5f;
    }
    //Ducken
    if ((Input.GetKey(KeyCode.S)) && grounded)
    {
      isDuck = true;
    }
    //Nicht mehr ducken
    if (Input.GetKeyUp(KeyCode.S) && !isStanding)
    {
      isDuck = false;
    }
    //Ducken Bool
    if (isDuck)
    {
      standCol.enabled = false;
      duckCol.enabled = true;
    }
    //Nicht mehr ducken Bool
    if (!isDuck)
    {
      standCol.enabled = true;
      duckCol.enabled = false;
    }
    //Wandhängen
    if (wallHang)
    {
      isDblJump = false;

      if (Input.GetKeyDown(KeyCode.Space))
      {
        canDoubleJump = true;

        rb2d.velocity = new Vector3((walljumptarget.transform.position.x - transform.position.x) * 4000,
        (walljumptarget.transform.position.y - transform.position.y) * 8, 0);

        gameObject.transform.localScale = new Vector3(transform.localScale.x * -1,
        transform.localScale.y,
        transform.localScale.z);
      }
    }
    //Nicht mehr wandhängen
    if (!wallHang)
    {
    }
  }

  private void FixedUpdate()
  {
    Vector3 easeVelocity = rb2d.velocity;
    easeVelocity.y = rb2d.velocity.y;
    easeVelocity.z = 0.0f;
    easeVelocity.x *= 0.8f;

    float m = Input.GetAxis("Horizontal");
    //Bewegung
    if (!Attack.isAttacking && !Block.isBlocking && (!Block.isShooting))
    {
      rb2d.AddForce((Vector2.right * speed) * m);
    }

    //Doppelsprung wegmachen
    if (grounded)
    {
      isDblJump = false;
    }
    //Reibung
    if (grounded) { rb2d.velocity = easeVelocity; }

    //SpeedLimit rechts
    if (rb2d.velocity.x > maxSpeed)
    {
      rb2d.velocity = new Vector2(maxSpeed, rb2d.velocity.y);
    }
    //SpeedLimit links
    if (rb2d.velocity.x < -maxSpeed)
    {
      rb2d.velocity = new Vector2(-maxSpeed, rb2d.velocity.y);
    }
    //SpeedLimit links Luft
    if (rb2d.velocity.x < -maxSpeed && !grounded)
    {
      rb2d.velocity = new Vector2(-3 / 5, rb2d.velocity.y);
    }
    //SpeedLimit rechts Luft
    if (rb2d.velocity.x > maxSpeed && !grounded)
    {
      rb2d.velocity = new Vector2(3 / 5, rb2d.velocity.y);
    }

  }
}
