using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

//Script that handles character controls
public class CharacterController : MonoBehaviour
{
    public Animator anim;
    public Rigidbody2D rb;
    public BoxCollider2D collider;
    public ICharacterState currentState;
    public GameObject opponent;

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
    // Start is called before the first frame update
    void Start()
    {
        ChangeState(new IdleState());
        rb.gravityScale *= 1.25f;
        Time.timeScale = 1f;
        airAttackPerformed = false;
    }

    // Update is called once per frame
    void Update()
    {
        //DirectionToBeFacing();
        currentState.Execute();
    }

    public void ChangeState(ICharacterState newState)
    {
        if(currentState != null)
        {
            currentState.Exit();
        }

        currentState = newState;

        currentState.Enter(this);
    }

    public void Walk()
    {
        rb.velocity = new Vector2(moveSpeed * moveDir * Time.deltaTime, rb.velocity.y);
    }

    public void Jump()
    {
        rb.velocity = new Vector2(jumpForceX * moveDir * Time.deltaTime, jumpForceY);
    }

    public void DirectionToBeFacing()
    {
        float distDifference = opponent.transform.position.x - gameObject.transform.position.x;
        Debug.Log("Distance difference: " + distDifference);
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

        if(newDirection != direction)
        {
            direction = newDirection;
            Debug.Log(this.gameObject.name + " The new direction is: " + direction);
            //anim.Play("NidTurn");
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x) * direction, transform.localScale.y, 1);
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
        float extraHeightCheck = 0.55f;
        RaycastHit2D raycastHit = Physics2D.BoxCast(collider.bounds.center, collider.bounds.size, 0f, Vector2.down, extraHeightCheck, platformLayer);
        //Debug.Log("Raycast Grounded: " + raycastHit.collider);
        //Return the raycast collider if it isn't null
        return raycastHit.collider != null;
    }

    public bool IsBlocking()
    {
        return false;
    }

    public void HandleAttackPress()
    {
        if(inputs.light.ReadValue<float>() != 0)
        {
            anim.SetInteger("AttackStrength", (int)AttackStrength.LIGHT);
        }
        else if(inputs.med.ReadValue<float>() != 0)
        {
            anim.SetInteger("AttackStrength", (int)AttackStrength.MED);
        }
        else if(inputs.heavy.ReadValue<float>() != 0)
        {
            anim.SetInteger("AttackStrength", (int)AttackStrength.HEAVY);
        }

        if(anim.GetInteger("AttackStrength") != 0)
        {
            if(anim.GetBool("IsJumping"))
            {
                Debug.Log("Jump attcking");
                ChangeState(new JumpAtkState());
            }
            else
            {
                ChangeState(new AttackState());
            }
        }
    }

    public void OnHit()
    {

    }

    public void OnBlock()
    {

    }

    public void SetAttackData(string atkData)
    {
        currentAttackData = FindAttackData(atkData);
    }

    private AttackData FindAttackData(string atkName)
    {
        AttackData theData = null;

        foreach(AttackData atk in attacks)
        {
            if(atk.attackName.CompareTo(atkName) == 0)
            {
                theData = atk;
            }
        }

        if(theData == null)
        {
            Debug.LogError("ATTACK DATA NOT FOUND!");
        }
        return theData;
    }

    //All these callbacks could be replaced by having a data strucutre with the InputActions as variables and then poll them for their value
    //A callback function - Check for when the player inputs a walk control and set player to moving
    public void OnWalk(InputValue value)
    {
        moveDir = value.Get<float>();
        if(moveDir != 0)
        {
            isMoving = true;
            anim.SetFloat("MoveX", moveDir * direction);
        }
        else
        {
            isMoving = false;
        }
    }

    public void OnJump(InputValue value)
    {
        if(value.Get<float>() != 0 && IsGrounded())
        {
            isJumping = true;
        }
        else
        {
            isJumping = false;
        }
    }

    public void OnCrouch(InputValue value)
    {
        if(value.Get<float>() != 0)
        {
            isCrouching = true;
        }
        else
        {
            isCrouching = false;
        }

        anim.SetBool("IsCrouching", isCrouching);
    }
}
