using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharlieController : CharacterController
{
    public DroneScript dronePrefab;
    public FireballScript superFireballPrefab;
    // Start is called before the first frame update
    void Start()
    {
        rb.gravityScale *= 1.25f;
        airAttackPerformed = false;
        ChangeState(new IdleState());
    }

    // Update is called once per frame
    void Update()
    {
        currentState.Execute();
    }

    public override void OnHit(CharacterController opponent)
    {
        //Apply the push back force of an attack
        //opponent.rb.AddForce(transform.right * -opponent.direction * opponent.currentAttackData.pushback, ForceMode2D.Impulse); //Maybe shift this into hitstun state, change the argument for the constructor
        //rb.AddForce(transform.right * -direction * opponent.currentAttackData.pushforward, ForceMode2D.Impulse);
        rb.velocity = new Vector2(0, 0);
        if (opponent.currentAttackData.causeKnockdown) //If the attack causes a knockdown
        {
            anim.Play("Knockdown");
            if (opponent.currentAttackData.launchForce != new Vector2(0, 0))        //If the attack launches the target, apply the launch force
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
        else if (!IsGrounded())   //They are hit in the air
        {
            ChangeState(new AirResetState());
        }
        else //The attack does not cause a knockdown
        {
            anim.SetBool("InHitStun", true);
            ChangeState(new HitStunState(opponent.currentAttackData));
            if (inputs.crouch.ReadValue<float>() != 0)
            {
                Debug.Log("Play crouch hit stun");
                //anim.Play("CrouchHit", 0, 0);
                anim.Play("CrouchHit", 0, 0);
            }
            else
            {
                Debug.Log("Play stand hit stun");
                anim.Play("StandHit", 0, 0);
            }
        }

        stats.TakeDamage(opponent.currentAttackData.damage);
        stats.GainMeter(opponent.currentAttackData.damage * 0.3f);
    }

    public override void OnHit(AttackData atkData)
    {
        Debug.Log("Hit via special atk" + atkData);
        rb.velocity = new Vector2(0, 0);
        //rb.AddForce(transform.right * -direction * atkData.pushforward, ForceMode2D.Impulse);

        if (atkData.causeKnockdown) //If the attack causes a knockdown
        {
            anim.Play("Knockdown");
            if (atkData.launchForce != new Vector2(0, 0))        //If the attack launches the target, apply the launch force
            {
                //rb.AddForce(new Vector2(-direction * atkData.launchForce.x, 1 * atkData.launchForce.y), ForceMode2D.Impulse); //Maybe shift this into kncokdown state, change the argument for the constructor
                ChangeState(new LaunchState());
            }
            else
            {
                //Debug.Log(new Vector2(1 * -opponent.direction * opponent.currentAttackData.launchForce.x, 1 * opponent.currentAttackData.launchForce.y));
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
            ChangeState(new HitStunState(atkData));
            if (inputs.crouch.ReadValue<float>() != 0)
            {
                Debug.Log("Play crouch hit stun");
                //anim.Play("CrouchHit", 0, 0);
                anim.Play("CrouchHit", 0, 0);
            }
            else
            {
                Debug.Log("Play stand hit stun");
                anim.Play("StandHit", 0, 0);
            }
        }

        stats.TakeDamage(atkData.damage);
        stats.GainMeter(atkData.damage * 0.3f);
    }

    public override void OnBlock(CharacterController opponent)
    {
        anim.SetBool("InBlockStun", true);
        rb.velocity = new Vector2(0, 0);
        //opponent.rb.AddForce(transform.right * -opponent.direction * opponent.currentAttackData.pushback, ForceMode2D.Impulse); //Maybe shift this into blockstun state, change the argument for the constructor
        //rb.AddForce(transform.right * -direction * opponent.currentAttackData.pushforward, ForceMode2D.Impulse);
        ChangeState(new BlockStunState(opponent.currentAttackData));

        if (inputs.crouch.ReadValue<float>() != 0)
        {
            anim.Play("CrouchBlocking", 0, 0);
        }
        else
        {
            anim.Play("StandBlocking", 0, 0);
        }

        stats.TakeDamage(opponent.currentAttackData.damage * 0.2f);
        stats.GainMeter(opponent.currentAttackData.damage * 0.2f);
    }

    public override void OnBlock(AttackData atkData)
    {
        anim.SetBool("InBlockStun", true);
        rb.velocity = new Vector2(0, 0);
        //rb.AddForce(transform.right * -direction * atkData.pushforward, ForceMode2D.Impulse);
        ChangeState(new BlockStunState(atkData));

        if (inputs.crouch.ReadValue<float>() != 0)
        {
            anim.Play("CrouchBlocking", 0, 0);
        }
        else
        {
            anim.Play("StandBlocking", 0, 0);
        }

        stats.TakeDamage(atkData.damage * 0.2f);
        stats.GainMeter(atkData.damage * 0.2f);
    }

    public override void OnThrown(CharacterController opponent)
    {
        ChangeState(new ThrownState(opponent));
        anim.SetBool("IsThrown", true);
        anim.Play("GetThrown");
        stats.GainMeter(opponent.currentAttackData.damage * 0.3f);
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

    public void SpawnDrone()
    {
        if(FindObjectOfType<DroneScript>() == null)
        {
            DroneScript droneInstance = Instantiate(dronePrefab, transform.position, transform.rotation);
            droneInstance.SetDirection(direction);
            droneInstance.destination = transform.position + new Vector3(direction * 2f, 0, 0);
            //chairInstance.rb.velocity = new Vector2(direction * chairInstance.moveSpeed, 0);
            //chairInstance.transform.parent = transform;
            Debug.Log("Spawned a drone!");
        }
    }

    public void SuperAttack()
    {
        FireballScript superInstance = Instantiate(superFireballPrefab, transform.position + new Vector3(direction * 5, 0, 0), transform.rotation);
        superInstance.rb.velocity = new Vector2(direction * superInstance.moveSpeed * Time.deltaTime, 0);
        superInstance.SetDirection(direction);
        //superInstance.transform.parent = transform;
        Debug.Log("Fireball Super");
    }
}
