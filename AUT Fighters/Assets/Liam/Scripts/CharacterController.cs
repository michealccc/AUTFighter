using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterController : MonoBehaviour, IGettingAttacked
{
    public Animator anim;
    public Rigidbody2D rb;
    public BoxCollider2D collider;
    public BoxCollider2D groundCollider;
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
        rb.velocity = new Vector2(moveSpeed * moveDir * Time.deltaTime, rb.velocity.y);
    }

    public void Jump()
    {
        moveDir = inputs.walk.ReadValue<float>();
        rb.velocity = new Vector2(jumpForceX * moveDir * Time.deltaTime, jumpForceY);
    }

    public void DirectionToBeFacing()
    {
        float distDifference = opponent.transform.position.x - gameObject.transform.position.x;
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
        RaycastHit2D raycastHit = Physics2D.BoxCast(collider.bounds.center, collider.bounds.size, 0f, Vector2.down, extraHeightCheck, platformLayer);
        //Debug.Log("Raycast Grounded: " + raycastHit.collider);
        //Return the raycast collider if it isn't null
        return raycastHit.collider != null;
    }

    public bool IsBlocking()
    {
        //Moving in the opposite direciton they are facing
        if (inputs.walk.ReadValue<float>() == (direction * -1))
        {
            Debug.Log("Is Blocking");
            return true;
        }

        return false;
    }

    public void HandleAttackPress()
    {
        if (inputs.light.ReadValue<float>() != 0)
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

        if (anim.GetInteger("AttackStrength") != 0)
        {
            if (anim.GetBool("IsJumping"))
            {
                Debug.Log("Jump attcking");
                ChangeState(new JumpAtkState());
            }
            else if(anim.GetBool("IsCrouching"))
            {
                ChangeState(new CrouchAttackState());
            }
            else
            {
                ChangeState(new AttackState());
            }
        }
    }

    public void ToggleGroundCollider()
    {
        //groundCollider.enabled = !groundCollider.enabled;
    }

    public void SetAttackData(string atkData)
    {
        currentAttackData = FindAttackData(atkData);
    }

    //Detecting the hitbox that belongs to the opposing character
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponentInParent<CharacterController>().gameObject == opponent)
        {
            currentState.OnTriggerEnter(other);
        }
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

    //All these callbacks could be replaced by having a data strucutre with the InputActions as variables and then poll them for their value
    //A callback function - Check for when the player inputs a walk control and set player to moving
    //public void OnWalk(InputValue value)
    //{
    //    moveDir = value.Get<float>();
    //    if (moveDir != 0)
    //    {
    //        isMoving = true;
    //        anim.SetFloat("MoveX", moveDir * direction);
    //    }
    //    else
    //    {
    //        isMoving = false;
    //    }
    //}

    //public void OnJump(InputValue value)
    //{
    //    if (value.Get<float>() != 0 && IsGrounded())
    //    {
    //        isJumping = true;
    //    }
    //    else
    //    {
    //        isJumping = false;
    //    }
    //}

    //public void OnCrouch(InputValue value)
    //{
    //    if (value.Get<float>() != 0)
    //    {
    //        isCrouching = true;
    //    }
    //    else
    //    {
    //        isCrouching = false;
    //    }

    //    //anim.SetBool("IsCrouching", isCrouching);
    //}

    public virtual void OnHit(CharacterController opponent)
    {
        throw new System.NotImplementedException();
    }

    public virtual void OnBlock(CharacterController opponent)
    {
        throw new System.NotImplementedException();
    }
}
