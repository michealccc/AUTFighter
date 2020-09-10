using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

//Script that handles character controls
public class NidController : CharacterController
{
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

    public override void OnHit(CharacterController opponent)
    {
        anim.SetBool("InHitStun", true);

        opponent.rb.AddForce(transform.right * -opponent.direction * opponent.currentAttackData.pushback, ForceMode2D.Impulse); //Maybe shift this into hitstun state, change the argument for the constructor
        rb.AddForce(transform.right * -direction * opponent.currentAttackData.pushforward, ForceMode2D.Impulse);
        ChangeState(new HitStunState(opponent));

        if (isCrouching)
        {
            anim.Play("NidCrouchHit", 0, 0);
        }
        else
        {
            Debug.Log("Play stand hit stun");
            anim.Play("NidStandHit", 0, 0);
        }

    }

    public override void OnBlock(CharacterController opponent)
    {
        anim.SetBool("InBlockStun", true);

        opponent.rb.AddForce(transform.right * -opponent.direction * opponent.currentAttackData.pushback, ForceMode2D.Impulse); //Maybe shift this into blockstun state, change the argument for the constructor
        rb.AddForce(transform.right * -direction * opponent.currentAttackData.pushforward, ForceMode2D.Impulse);
        ChangeState(new BlockStunState(opponent));

        if (isCrouching)
        {
            anim.Play("NidCrouchBlocking");
        }
        else
        {
            anim.Play("NidStandBlocking");
        }
    }

}
