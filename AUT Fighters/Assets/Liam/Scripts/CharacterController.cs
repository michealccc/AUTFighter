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

    public AttackData[] groundAttacks;
    public AttackData[] airAttacks;

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

    //All these callbacks could be replaced by having a data strucutre with the InputActions as variables and then poll them for their value
    //A callback function - Check for when the player inputs a walk control and set player to moving
    public void OnWalk(InputValue value)
    {
        moveDir = value.Get<float>();
        if(moveDir != 0)
        {
            isMoving = true;
            anim.SetFloat("MoveX", moveDir);
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
