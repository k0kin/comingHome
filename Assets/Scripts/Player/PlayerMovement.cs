using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using Unity.Mathematics;

public class PlayerMovement : MonoBehaviour
{
    private Collision col;
    [HideInInspector] public Rigidbody2D rb2D;

    [Space] 
    [Header("Stats")] 
    public float speed = 10f;
    public float jumpForce = 50f;
    public float slideSpeed = 5f;
    public float wallJumpLerp = 10f;
    public float dashSpeed = 20f;
    [Header("Jump")]
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;
    
    [Space]
    [Header("Booleans")]
    public bool canMove;
    public bool wallGrab;
    public bool wallJumped;
    public bool wallSlide;
    public bool isDashing;

    [Space] 
    [Header("Canon to Shoot")] 
    [SerializeField] private Transform canon;
    [Header("Sword Collider")] 
    [SerializeField] private Transform sword;

    [SerializeField] private float cooldownTimerSword = 0.5f;
    [Header("Gravity Gun")] 
    [SerializeField] private Transform gravityGun;

    [Space]
    [Header("VFX")]
    public ParticleSystem dashParticle;
    public ParticleSystem jumpParticle;
    public ParticleSystem wallJumpParticle;
    public ParticleSystem slideParticle;

    private bool CanJumpUpdate = true;
    private bool groundTouch;
    private bool hasDashed;

    private bool isSlashing;

    private Animator anim;

    public int side = 1;
    public int upDown = 0;
    
    
    void Start()
    {
        col = GetComponent<Collision>();
        rb2D = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

    }
    
    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        float xRaw = Input.GetAxisRaw("Horizontal");
        float yRaw = Input.GetAxisRaw("Vertical");
        Vector2 dir = new Vector2(x, y);
        
        //Run
        if (Input.GetKeyDown(KeyCode.F))
        {
            speed += 5f;
        }
        
        if (Input.GetKeyUp(KeyCode.F))
        {
            speed -= 5f;
        }
        
        if (Input.GetMouseButtonDown(1) && cooldownTimerSword <= 0f)
        {
            anim.SetTrigger("Attack");
            cooldownTimerSword = 0.3f;
        }

        cooldownTimerSword -= Time.deltaTime;

        Walk(dir);
        
        if(CanJumpUpdate)
            JumpUpdate();
        
        if (col.onWall && Input.GetButton("Fire3") && canMove)
        {
            wallGrab = true;
            wallSlide = false;
        }

        if (Input.GetButtonUp("Fire3") || !col.onWall || !canMove)
        {
            wallGrab = false;
            wallSlide = false;
        }

        if (col.onGround && !isDashing)
        {
            wallJumped = false;
            //CanJumpUpdate = false;
        }
        
        if (wallGrab && !isDashing)
        {
            rb2D.gravityScale = 0;
            if(x > .2f || x < -.2f)
                rb2D.velocity = new Vector2(rb2D.velocity.x, 0);

            float speedModifier = y > 0 ? .5f : 1;

            rb2D.velocity = new Vector2(rb2D.velocity.x, y * (speed * speedModifier));
        }
        else
        {
            rb2D.gravityScale = 3;
        }

        if(col.onWall && !col.onGround)
        {
            if (x != 0 && !wallGrab)
            {
                wallSlide = true;
                WallSlide();
            }
        }

        if (!col.onWall || col.onGround)
            wallSlide = false;
        
        if (Input.GetButtonDown("Jump"))
        {
            if (col.onGround)
                Jump(Vector2.up, false);
            if (col.onWall && !col.onGround)
                WallJump();
        }
        
        if (Input.GetKeyDown(KeyCode.LeftShift) && !hasDashed)
        {
            if(xRaw != 0 || yRaw != 0)
                Dash(xRaw, yRaw);
        }

        if (col.onGround && !groundTouch)
        {
            GroundTouch();
            groundTouch = true;
        }

        if(!col.onGround && groundTouch)
        {
            groundTouch = false;
        }

        if (wallGrab || wallSlide || !canMove)
            return;

        if(x > 0)
        {
            side = 1;
            canon.localPosition = new Vector3(0.5f, 0f, 0f);
            canon.localRotation = Quaternion.identity;
            
            gravityGun.localRotation = Quaternion.identity;
            
            if (!isSlashing)
            {
                sword.localPosition = new Vector3(0.8f, 0.61f, 0f);
                sword.localRotation = Quaternion.identity;
            }
        }
        if (x < 0)
        {
            side = -1;
            canon.localPosition = new Vector3(-0.5f, 0f, 0f);
            canon.localRotation = Quaternion.Euler(0f, 180f, 0f);
            
            gravityGun.localRotation = Quaternion.Euler(0f, 180f, 0f);
            
            if(!isSlashing)
            {
                sword.localPosition = new Vector3(-0.8f, 0.61f, 0f);
                sword.localRotation = Quaternion.Euler(0f, 180f, 0f);
            }
        }

        if (y > 0)
        {
            upDown = 1;
            sword.localPosition = new Vector3(0f, 1f, 0f);
            sword.localRotation = Quaternion.Euler(0f, 0f, 90f);
        }
        else if (y < 0 && !isSlashing)
        {
            upDown = -1;
        }
        else
        {
            upDown = 0;
        }
    }
    
    void GroundTouch()
    {
        hasDashed = false;
        isDashing = false;
    }
    
    private void Dash(float x, float y)
    {
        Camera.main.transform.DOComplete();
        Camera.main.transform.DOShakePosition(.2f, .5f, 14, 90, false, true);

        hasDashed = true;

        rb2D.velocity = Vector2.zero;
        Vector2 dir = new Vector2(x, y);

        rb2D.velocity += dir.normalized * dashSpeed;
        StartCoroutine(DashWait());
    }
    
    IEnumerator DashWait()
    {
        //FindObjectOfType<GhostTrail>().ShowGhost();
        StartCoroutine(GroundDash());
        DOVirtual.Float(14, 0, .8f, RigidbodyDrag);

        rb2D.gravityScale = 0;
        CanJumpUpdate = false;
        wallJumped = true;
        isDashing = true;

        yield return new WaitForSeconds(.3f);

        rb2D.gravityScale = 3;
        CanJumpUpdate = true;
        wallJumped = false;
        isDashing = false;
    }
    
    IEnumerator GroundDash()
    {
        FindObjectOfType<RippleEffect>().Emit(Camera.main.WorldToViewportPoint(transform.position));
        
        yield return new WaitForSeconds(.15f);
        if (col.onGround)
            hasDashed = false;
    }

    private void WallJump()
    {
        if ((side == 1 && col.onRightWall) || side == -1 && !col.onRightWall)
        {
            side *= -1;
        }

        StopCoroutine(DisableMovement(0));
        StartCoroutine(DisableMovement(.1f));

        Vector2 wallDir = col.onRightWall ? Vector2.left : Vector2.right;

        Jump((Vector2.up / 1.5f + wallDir / 1.5f), true);

        wallJumped = true;
    }

    private void WallSlide()
    {
        if(col.wallSide != side)
            //anim.Flip(side * -1);

        if (!canMove)
            return;

        bool pushingWall = false;
        if((rb2D.velocity.x > 0 && col.onRightWall) || (rb2D.velocity.x < 0 && col.onLeftWall))
        {
            pushingWall = true;
        }
        float push = pushingWall ? 0 : rb2D.velocity.x;

        rb2D.velocity = new Vector2(push, -slideSpeed);
    }
    
    IEnumerator DisableMovement(float time)
    {
        canMove = false;
        yield return new WaitForSeconds(time);
        canMove = true;
    }

    void RigidbodyDrag(float x)
    {
        rb2D.drag = x;
    }
    
    private void Walk(Vector2 dir)
    {
        
        if (!canMove)
            return;

        if (wallGrab)
            return;

        if (!wallJumped)
        {
            rb2D.velocity = new Vector2(dir.x * speed, rb2D.velocity.y);
        }
        else
        {
            rb2D.velocity = Vector2.Lerp(rb2D.velocity, (new Vector2(dir.x * speed, rb2D.velocity.y)), wallJumpLerp * Time.deltaTime);
        }
        
    }
    
    private void Jump(Vector2 dir, bool wall)
    {
        rb2D.velocity = new Vector2(rb2D.velocity.x, 0);
        rb2D.velocity += dir * jumpForce;
    }

    private void JumpUpdate()
    {
        if(rb2D.velocity.y < 0)
        {
            rb2D.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }else if(rb2D.velocity.y > 0 && !Input.GetButton("Jump"))
        {
            rb2D.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }
    }

    public void BeginSlash()
    {
        isSlashing = true;
    }

    public void EndSlash()
    {
        isSlashing = false;
    }

    /*
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            if(side == 1)
            {
                Vector2 impulseDir = new Vector2(-1f, .4f);
                rb2D.AddForce(impulseDir * 2500, ForceMode2D.Force);
            }
            else
            {
                Vector2 impulseDir = new Vector2(1f, .4f);
                rb2D.AddForce(impulseDir * 2500, ForceMode2D.Force);
            }
        }
    }
    */
}
