using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiamController : CharacterController
{
    public SuperFire superFirePrefab;
    // Start is called before the first frame update
    void Start()
    {
        //inputs.AssignInputs();
        airAttackPerformed = false;
        SetAttackDataOrigin();
        audio = AudioManager.Instance;
        //ChangeState(new IdleState());
    }

    // Update is called once per frame
    void Update()
    {
        currentState.Execute();
    }

    public override void OnHit(AttackData theAtk)
    {
        rb.velocity = new Vector2(0, 0);
        audio.Play("Grunt2");
        if (theAtk.causeKnockdown) //If the attack causes a knockdown
        {
            anim.Play("Knockdown");
            if (theAtk.launchForce != new Vector2(0, 0))        //If the attack launches the target, apply the launch force
            {
                ChangeState(new LaunchState());
            }
            else
            {
                ChangeState(new KnockdownState());
            }
        }
        else if (!IsGrounded())   //They are hit in the air
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
                hitSpark.transform.position = new Vector2(blockSpark.transform.position.x, collider.bounds.center.y);
                //anim.Play("CrouchHit", 0, 0);
                anim.Play("CrouchHit", 0, 0);
            }
            else
            {
                Debug.Log("Play stand hit stun");
                hitSpark.transform.position = new Vector2(blockSpark.transform.position.x, collider.bounds.center.y);
                anim.Play("StandHit", 0, 0);
            }
        }

        hitSpark.Play();
        stats.TakeDamage(theAtk.damage);
        stats.GainMeter(theAtk.damage * 0.3f);
    }

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
            anim.Play("CrouchBlocking", 0, 0);
            blockSpark.transform.position = new Vector2(blockSpark.transform.position.x, collider.bounds.center.y);
            blockSpark.Play();
        }
        else
        {
            anim.Play("StandBlocking", 0, 0);
            blockSpark.transform.position = new Vector2(blockSpark.transform.position.x, collider.bounds.center.y);
            blockSpark.Play();
        }

        stats.TakeDamage(theAtk.damage * 0.2f);
        stats.GainMeter(theAtk.damage * 0.2f);
    }

    public override void OnThrown(AttackData atkData)
    {
        ChangeState(new ThrownState(atkData));
        anim.SetBool("IsThrown", true);
        anim.Play("GetThrown");
        stats.GainMeter(atkData.damage * 0.3f);
    }

    public override void OnVictory()
    {
        //Enter the round start/empty state and play victory animation
        ChangeState(new RoundStartState());
        //anim.Play("Victory");
    }

    public override void OnKO()
    {
        //Enter the round start/empty state and play KO animation
        ChangeState(new RoundStartState());
        anim.SetBool("IsKO", true);
        anim.Play("KO");
    }

    public void SpecialAttack()
    {
        rb.AddForce(new Vector2(direction * (moveSpeed * 3), 0f), ForceMode2D.Impulse);
    }

    public void SuperAttack()
    {
        SuperFire superInstance = Instantiate(superFirePrefab, transform.position + new Vector3(direction * 5, -collider.bounds.extents.y, 0), transform.rotation);
        superInstance.rb.velocity = new Vector2(direction * superInstance.moveSpeed * Time.fixedDeltaTime, 0);
        superInstance.GetComponent<AttackData>().origin = this;
        audio.Play("FireballSound");
        Debug.Log("Fire Super");
    }
}
