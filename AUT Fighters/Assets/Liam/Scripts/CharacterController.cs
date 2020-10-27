using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class CharacterController : MonoBehaviour, IGettingAttacked, IWinOrLose
{
    public Animator anim;
    public Rigidbody2D rb;
    public BoxCollider2D collider;
    public BoxCollider2D throwHurtbox;
    public BoxCollider2D throwBox;
    public CharacterController opponent;
    public AttackData attack;
    public ParticleSystem blockSpark;
    public ParticleSystem hitSpark;

    public ICharacterState currentState;

    public Characters characterID;  //Maybe separate this stuff into its down class
    public Sprite charactePortrait;
    public string characterName;
    protected AudioManager audio;

    public PlayerStats stats;

    public AttackData[] attacks;
    public AttackData currentAttackData;

    public float moveSpeed;
    public float jumpForceY;
    public float jumpForceX;

    public bool isMoving;
    public bool isJumping;
    public bool isCrouching;

    public float direction;
    public float moveDir;

    public bool airAttackPerformed;

    public InputChecker inputs;

    [SerializeField]
    public LayerMask platformLayer;

    void Awake()
    {
        rb.gravityScale *= 3.75f;
    }

    void Start()
    {
        //audio = FindObjectOfType<AudioManager>();
    }

    public void ChangeState(ICharacterState newState)
    {
        if (currentState != null)
        {
            currentState.Exit();
        }

        currentState = newState;

        currentState.Enter(this);
    }

    public void Walk()
    {
        rb.velocity = new Vector2(moveSpeed * moveDir, rb.velocity.y);
    }

    public void Jump()
    {
        rb.velocity = new Vector2(0, 0);
        moveDir = inputs.walk.ReadValue<float>();
        //rb.velocity = new Vector2(jumpForceX * moveDir * Time.fixedDeltaTime, jumpForceY);
        rb.AddForce(new Vector2(jumpForceX * moveDir, jumpForceY), ForceMode2D.Impulse);
    }

    public void DirectionToBeFacing()
    {
        float distDifference = opponent.gameObject.transform.position.x - gameObject.transform.position.x;
        //Debug.Log("Distance difference: " + distDifference);
        float newDirection = 0;
        //Can probably replace this if statement using some calculation and normalize
        if (distDifference < 0)
        {
            newDirection = -1;
        }
        else if (distDifference > 0)
        {
            newDirection = 1;
        }

        if (newDirection != direction)
        {
            direction = newDirection;
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x) * direction, transform.localScale.y, 1);
            hitSpark.transform.localScale = new Vector3(Mathf.Abs(hitSpark.transform.localScale.x), Mathf.Abs(hitSpark.transform.localScale.y), Mathf.Abs(hitSpark.transform.localScale.z)) * direction;
            blockSpark.transform.localScale = new Vector3(Mathf.Abs(blockSpark.transform.localScale.x), Mathf.Abs(blockSpark.transform.localScale.y), Mathf.Abs(blockSpark.transform.localScale.z)) * direction;
        }
    }

    public bool IsGrounded()
    {
        float extraHeightCheck = 0.10f;
        RaycastHit2D raycastHit = Physics2D.BoxCast(collider.bounds.center, collider.bounds.size, 0f, Vector2.down, extraHeightCheck, platformLayer);
        //Debug.Log("Raycast Grounded: " + raycastHit.collider);
        //Return the raycast collider if it isn't null
        return raycastHit.collider != null;
    }

    public bool IsLanding()
    {
        float extraHeightCheck = 0.45f; 
        RaycastHit2D raycastHitGround = Physics2D.BoxCast(collider.bounds.center, collider.bounds.size, 0f, Vector2.down, extraHeightCheck, platformLayer);
        return raycastHitGround.collider != null;
    }

    public bool IsBlocking(AttackData atkData)  //Blocking for normals
    {
        bool isBlock = false;
        //Moving in the opposite direciton they are facing
        if (inputs.walk.ReadValue<float>() == (direction * -1) && IsGrounded())
        {
            if(atkData.attackType == AttackType.LOW && inputs.crouch.ReadValue<float>() != 0)   //Crouching for a low attack
            {
                isBlock = true;
            }
            else if(atkData.attackType == AttackType.OVERHEAD && inputs.crouch.ReadValue<float>() == 0)     //Standing for an overhead attack
            {
                isBlock = true;
            }
            else if(atkData.attackType == AttackType.HIGH)
            {
                isBlock = true;
            }
            Debug.Log("Is Blocking");
        }

        return isBlock;
    }

    //This handles the different attacks of a character - needs a lot of improvement
    public void HandleAttackPress()
    {
        if (inputs.light.ReadValue<float>() != 0 && inputs.med.ReadValue<float>() != 0)
        {
            anim.SetInteger("AttackStrength", (int)AttackStrength.THROW);
        }
        else if(inputs.heavy.ReadValue<float>() != 0 && inputs.special.ReadValue<float>() != 0 && stats.currentSuperMeter == stats.maxSuperMeter && IsGrounded())
        {
            anim.SetInteger("AttackStrength", (int)AttackStrength.SUPER);
            stats.ResetSuperMeter();
        }
        else if (inputs.light.ReadValue<float>() != 0)
        {
            anim.SetInteger("AttackStrength", (int)AttackStrength.LIGHT);
        }
        else if (inputs.med.ReadValue<float>() != 0)
        {
            anim.SetInteger("AttackStrength", (int)AttackStrength.MED);
        }
        else if (inputs.heavy.ReadValue<float>() != 0)
        {
            anim.SetInteger("AttackStrength", (int)AttackStrength.HEAVY);
        }
        else if (inputs.special.ReadValue<float>() != 0)
        {
            anim.SetInteger("AttackStrength", (int)AttackStrength.SPECIAL);
        }

        //If an attack input has been detected
        if (anim.GetInteger("AttackStrength") != 0)
        {
            //Throw attack
            if (anim.GetInteger("AttackStrength") == (int)AttackStrength.THROW && anim.GetBool("IsJumping") == false)
            {
                ChangeState(new ThrowState());
            }
            else if (anim.GetBool("IsJumping") && anim.GetInteger("AttackStrength") != (int)AttackStrength.THROW && anim.GetBool("IsJumping") && anim.GetInteger("AttackStrength") != (int)AttackStrength.SPECIAL && anim.GetInteger("AttackStrength") != (int)AttackStrength.SUPER)
            {
                Debug.Log("Jump attcking");
                ChangeState(new JumpAtkState());
            }
            else if(anim.GetBool("IsCrouching"))
            {
                ChangeState(new CrouchAttackState());
            }
            else if (anim.GetBool("IsJumping") && anim.GetInteger("AttackStrength") == (int)AttackStrength.THROW || anim.GetBool("IsJumping") && anim.GetInteger("AttackStrength") == (int)AttackStrength.SPECIAL || anim.GetBool("IsJumping") && anim.GetInteger("AttackStrength") == (int)AttackStrength.SUPER)
            {
                //Do nothing
            }
            else
            {
                ChangeState(new AttackState());
            }
        }
    }

    public void CheckThrowCollider()
    {
        if(throwBox.IsTouching(opponent.throwHurtbox))
        {
            Debug.Log("Got throw");
            anim.SetBool("IsThrowing", true);
        }
    }

    public void ThrowFinish()
    {
        opponent.GetComponent<CharacterController>().anim.SetBool("IsThrown", false);
        
    }

    public void JumpLandCheck(GameObject opponent)
    {
        if(opponent.GetComponent<Rigidbody2D>().velocity.y < 0)
        {
            Debug.Log("Checking jump land!");
            //Vector2 futurePos = (Vector2)opponent.transform.position + opponent.GetComponent<Rigidbody2D>().velocity.normalized;        //Get future position of the falling character
            Vector2 oppPos = opponent.transform.position;
            float xPosDiff = oppPos.x - transform.position.x;                                                                //Difference on the x-axis between this character and the opponent - to get if they're landing on the left or right side
            if (xPosDiff <= 0)   //Left side landing
            {
                //if(Mathf.Abs(xPosDiff) <= 1)
                //{

                //}
                opponent.transform.position = new Vector2(opponent.transform.position.x - opponent.GetComponent<BoxCollider2D>().bounds.extents.x, opponent.transform.position.y - 1);
            }
            else                //Right side landing
            {
                //if (Mathf.Abs(xPosDiff) <= 1)
                //{

                //}
                opponent.transform.position = new Vector2(opponent.transform.position.x + opponent.GetComponent<BoxCollider2D>().bounds.extents.x, opponent.transform.position.y - 1);
            }

            //rb.AddForce(new Vector2(opponent.GetComponent<Rigidbody2D>().velocity.x * 2000, 0), ForceMode2D.Force);

            //Move the position of the falling opponent into the future position (this should force the collider of the falling opponent into the collider of other character causing them to push each other away)
            //opponent.transform.position = futurePos;
            //opponent.GetComponent<Rigidbody2D>().velocity = new Vector2(0, opponent.GetComponent<Rigidbody2D>().velocity.y);
        }
    }

    private bool CheckNextToWall(Vector2 direction, Vector2 origin, float dist)
    {
        RaycastHit2D raycastWallHit = Physics2D.Raycast(origin, direction, dist, platformLayer);
        Debug.DrawRay(origin, direction * dist);
        Debug.Log("CheckWall: " + raycastWallHit.collider);
        if(raycastWallHit == null)
        {
            Debug.Log("Not next to wall!");
            return false;
        }
        Debug.Log("next to wall!");
        return true;
    }

    public void SetAttackData(string atkData)
    {
        //attack = FindAttackData(atkData);
        transform.Find("Hitbox").GetComponent<AttackData>().SetAttackData(FindAttackData(atkData));
        transform.Find("Hitbox").GetComponent<AttackData>().origin = this;
        //GetComponentInChildren<AttackData>() = FindAttackData(atkData);
    }

    //Detecting the hitbox that belongs to the opposing character
    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(other.tag);
        if (other.CompareTag("Attack") || other.CompareTag("Throwbox"))
        {
            Debug.Log(other.GetComponent<AttackData>().origin);
            if (other.GetComponent<AttackData>().origin == opponent)
            {
                Debug.Log("Took an attack");
                currentState.OnTriggerEnter(other);
            }
        }
        else if (other.CompareTag("Ground") || other.CompareTag("Wall"))
        {
            //Ignore if its the ground
        }
        else if(other.CompareTag("Landing"))
        {
            JumpLandCheck(other.GetComponentInParent<CharacterController>().gameObject);
        }
        //else if (other.GetComponentInParent<CharacterController>().gameObject == opponent.gameObject)
        //{
        //    Debug.Log("Trigger Collision");
        //    currentState.OnTriggerEnter(other);
        //}
    }

    private AttackData FindAttackData(string atkName)
    {
        AttackData theData = null;

        foreach (AttackData atk in attacks)
        {
            if (atk.attackName.CompareTo(atkName) == 0)
            {
                theData = atk;
            }
        }

        if (theData == null)
        {
            Debug.LogError("ATTACK DATA NOT FOUND!");
        }
        return theData;
    }

    protected void SetAttackDataOrigin()
    {
        foreach(AttackData atk in attacks)
        {
            atk.origin = this;
        }
    }

    public virtual void OnHit(AttackData theAtk)
    {
        throw new System.NotImplementedException();
    }

    //public virtual void OnHit(AttackData atkData)
    //{
    //    throw new System.NotImplementedException();
    //}

    public virtual void OnBlock(AttackData theAtk)
    {
        throw new System.NotImplementedException();
    }

    //public virtual void OnBlock(AttackData atkData)
    //{
    //    throw new System.NotImplementedException();
    //}

    public virtual void OnThrown(AttackData atkData)
    {
        throw new System.NotImplementedException();
    }

    public virtual void OnVictory()
    {
        throw new System.NotImplementedException();
    }

    public virtual void OnKO()
    {
        throw new System.NotImplementedException();
    }
}
