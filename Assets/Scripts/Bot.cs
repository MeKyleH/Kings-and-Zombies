using UnityEngine;
using UnityEngine.Networking;

public class Bot : NetworkBehaviour
{
    public bool botCanAttack = true;

    [SerializeField]
    float AttackCooldown = 1f;

    PlayerAttacking playerAttacking;
    NetworkAnimator anim;
    float ellapsedTime = 0f;


    void Awake()
    {
        playerAttacking = GetComponent<PlayerAttacking>();
        anim = GetComponent<NetworkAnimator>();

        GetComponent<Player>().playerName = "Bot";
    }

    [ServerCallback]
    void Update()
    {
        anim.animator.SetFloat("Speed", 0f);
        anim.animator.SetFloat("Strafe", 0f);

        //below if checks are for debugging and need to be removed before this goes live
        if (Input.GetKey(KeyCode.Keypad8))
            anim.animator.SetFloat("Speed", 1f);

        if (Input.GetKey(KeyCode.Keypad2))
            anim.animator.SetFloat("Speed", -1f);

        if (Input.GetKey(KeyCode.Keypad4))
            anim.animator.SetFloat("Strafe", -1f);

        if (Input.GetKey(KeyCode.Keypad6))
            anim.animator.SetFloat("Strafe", 1f);

        if (Input.GetKeyDown(KeyCode.Keypad7))
            anim.SetTrigger("Died");

        if (Input.GetKeyDown(KeyCode.Keypad9))
            anim.SetTrigger("Restart");


        BotAutoFire();
    }

    [Server]
    void BotAutoFire()
    {
        ellapsedTime += Time.deltaTime;

        if (ellapsedTime < AttackCooldown)
            return;

        ellapsedTime = 0f;
        if (playerAttacking.enabled)
            playerAttacking.FireAsBot();
    }
}