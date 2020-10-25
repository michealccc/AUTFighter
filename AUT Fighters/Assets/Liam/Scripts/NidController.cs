using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

//Script that handles character controls
public class NidController : CharacterController
{
    public Collider2D myHitbox;
    public Collider2D enemyGroundCol;
    public ChairScript chairPrefab;
    public NidSuper chairSuperPrefab;   

    // Start is called before the first frame update
    void Start()
    {
        //inputs.AssignInputs();
        airAttackPerformed = false;
        SetAttackDataOrigin();
        //ChangeState(new IdleState());
        //Debug.Log(transform.Find("Hitbox").GetComponent<BoxCollider2D>());
        //Physics2D.IgnoreCollision(transform.Find("Hitbox").GetComponent<BoxCollider2D>(), opponent.transform.Find("GroundCollider").GetComponent<BoxCollider2D>());
        //Physics2D.IgnoreCollision(myHitbox, enemyGroundCol, true);
        audio = AudioManager.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        //DirectionToBeFacing();
        currentState.Execute();
    }

    public override void OnHit(AttackData theAtk)
    {
        //Apply the push back force of an attack
        //opponent.rb.AddForce(transform.right * -opponent.direction * opponent.currentAttackData.pushback, ForceMode2D.Impulse); //Maybe shift this into hitstun state, change the argument for the constructor
        //rb.AddForce(transform.right * -direction * opponent.currentAttackData.pushforward, ForceMode2D.Impulse);
        rb.velocity = new Vector2(0, 0);
        audio.Play("Grunt1");
        if(theAtk.causeKnockdown) //If the attack causes a knockdown
        {
            anim.Play("NidKnockdown");
            if (theAtk.launchForce != new Vector2(0, 0))        //If the attack launches the target, apply the launch force
            {
                //rb.AddForce(new Vector2(-direction * opponent.currentAttackData.launchForce.x, 1 * opponent.currentAttackData.launchForce.y), ForceMode2D.Impulse); //Maybe shift this into kncokdown state, change the argument for the constructor
                ChangeState(new LaunchState());
            }
            else
            {
                //Debug.Log(new Vector2(1 * -opponent.direction * opponent.currentAttackData.launchForce.x, 1 * opponent.currentAttackData.launchForce.y));
                ChangeState(new KnockdownState());
            }
        }
        else if(!IsGrounded())   //They are hit in the air
        {
            ChangeState(new AirResetState());
        }
        else //The attack does not cause a knockdown
        {
            anim.SetBool("InHitStun", true);
            ChangeState(new HitStunState(theAtk));
            if (inputs.crouch.ReadValue<float>() != 0)
            {
                Debug.Log("Play crouch hit stun");
                //anim.Play("NidCrouchHit", 0, 0);
                hitSpark.transform.position = new Vector2(blockSpark.transform.position.x, collider.bounds.center.y);
                hitSpark.Play();
                anim.Play("NidCrouchHit", 0, 0);
            }
            else
            {
                Debug.Log("Play stand hit stun");
                hitSpark.transform.position = new Vector2(blockSpark.transform.position.x, collider.bounds.center.y);
                hitSpark.Play();
                anim.Play("NidStandHit", 0, 0);
            }
        }

        stats.TakeDamage(theAtk.damage);
        stats.GainMeter(theAtk.damage * 0.3f);
    }

    //Hit by Special Attack
    //public override void OnHit(AttackData atkData)
    //{
    //    Debug.Log("Hit via special atk" + atkData);
    //    rb.velocity = new Vector2(0, 0);
    //    //rb.AddForce(transform.right * -direction * atkData.pushforward, ForceMode2D.Impulse);

    //    if (atkData.causeKnockdown) //If the attack causes a knockdown
    //    {
    //        anim.Play("NidKnockdown");
    //        if (atkData.launchForce != new Vector2(0, 0))        //If the attack launches the target, apply the launch force
    //        {
    //            //rb.AddForce(new Vector2(-direction * atkData.launchForce.x, 1 * atkData.launchForce.y), ForceMode2D.Impulse); //Maybe shift this into kncokdown state, change the argument for the constructor
    //            ChangeState(new LaunchState());
    //        }
    //        else
    //        {
    //            //Debug.Log(new Vector2(1 * -opponent.direction * opponent.currentAttackData.launchForce.x, 1 * opponent.currentAttackData.launchForce.y));
    //            ChangeState(new KnockdownState());
    //        }
    //    }
    //    else if (!IsGrounded())   //They are hit in the air
    //    {
    //        ChangeState(new AirResetState());
    //    }
    //    else //The attack does not cause a knockdown
    //    {
    //        anim.SetBool("InHitStun", true);
    //        ChangeState(new HitStunState(atkData));
    //        if (inputs.crouch.ReadValue<float>() != 0)
    //        {
    //            Debug.Log("Play crouch hit stun");
    //            //anim.Play("NidCrouchHit", 0, 0);
    //            anim.Play("NidCrouchHit", 0, 0);
    //        }
    //        else
    //        {
    //            Debug.Log("Play stand hit stun");
    //            anim.Play("NidStandHit", 0, 0);
    //        }
    //    }

    //    stats.TakeDamage(atkData.damage);
    //    stats.GainMeter(atkData.damage * 0.3f);
    //}

    public override void OnBlock(AttackData theAtk)
    {
        anim.SetBool("InBlockStun", true);
        rb.velocity = new Vector2(0, 0);
        audio.Play("BlockSound");
        //opponent.rb.AddForce(transform.right * -opponent.direction * opponent.currentAttackData.pushback, ForceMode2D.Impulse); //Maybe shift this into blockstun state, change the argument for the constructor
        //rb.AddForce(transform.right * -direction * opponent.currentAttackData.pushforward, ForceMode2D.Impulse);
        ChangeState(new BlockStunState(theAtk));

        if (inputs.crouch.ReadValue<float>() != 0)
        {
            anim.Play("NidCrouchBlocking", 0, 0);
            blockSpark.transform.position = new Vector2(blockSpark.transform.position.x, collider.bounds.center.y);
        }
        else
        {
            blockSpark.transform.position = new Vector2(blockSpark.transform.position.x, collider.bounds.center.y);
            anim.Play("NidStandBlocking", 0, 0);
        }

        blockSpark.Play();
        stats.TakeDamage(theAtk.damage * 0.2f);
        stats.GainMeter(theAtk.damage * 0.2f);
    }

    //public override void OnBlock(AttackData atkData)
    //{
    //    anim.SetBool("InBlockStun", true);
    //    rb.velocity = new Vector2(0, 0);
    //    //rb.AddForce(transform.right * -direction * atkData.pushforward, ForceMode2D.Impulse);
    //    ChangeState(new BlockStunState(atkData));

    //    if (inputs.crouch.ReadValue<float>() != 0)
    //    {
    //        anim.Play("NidCrouchBlocking", 0, 0);
    //    }
    //    else
    //    {
    //        anim.Play("NidStandBlocking", 0, 0);
    //    }

    //    stats.TakeDamage(atkData.damage * 0.2f);
    //    stats.GainMeter(atkData.damage * 0.2f);
    //}

    public override void OnThrown(AttackData atkData)
    {
        ChangeState(new ThrownState(atkData));
        anim.SetBool("IsThrown", true);
        anim.Play("NidGetThrown");
        stats.GainMeter(atkData.damage * 0.3f);
    }

    public override void OnVictory()
    {
       //Enter the round start/empty state and play victory animation
        ChangeState(new RoundStartState());
        //anim.Play("NidVictory");
    }

    public override void OnKO()
    {
        //Enter the round start/empty state and play KO animation
        ChangeState(new RoundStartState());
        anim.SetBool("IsKO", true);
        anim.Play("NidKO");
    }

    public void ThrowChair()
    {
        ChairScript chairInstance = Instantiate(chairPrefab, transform.position + new Vector3(direction * 3, 0, 0), transform.rotation);
        chairInstance.rb.velocity = new Vector2(direction * chairInstance.moveSpeed, 0);
        //chairInstance.transform.parent = transform;
        chairInstance.GetComponent<AttackData>().origin = this;
        audio.Play("ChairThrowSound");
        Debug.Log("Throw a chair!");
    }

    public void SuperAttack()
    {
        NidSuper superInstance = Instantiate(chairSuperPrefab, transform.position, transform.rotation);
        superInstance.transform.parent = transform;
        superInstance.origin = this;
        audio.Play("ChairSuperSound");
        Debug.Log("Chair Super");
    }
}
