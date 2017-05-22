using UnityEngine;
using UnityEngine.Networking;

public class PlayerAttacking : NetworkBehaviour
{
    [SerializeField]
    float attackCooldown = .3f;
    [SerializeField]
    Transform attackPosition;
    [SerializeField]
    AttackEffectsManager attackEffects;
    [SerializeField]
    float attackDistance = 50f;
    [SerializeField]
    int attackPower = 1;

    float ellapsedTime;
    bool canAttack;

    void Start()
    {
        attackEffects.Initialize();

        if (isLocalPlayer)
            canAttack = true;
    }

    void Update()
    {
        if (!canAttack)
            return;

        ellapsedTime += Time.deltaTime;

        if (Input.GetButtonDown("Fire1") && ellapsedTime > attackCooldown)
        {
            ellapsedTime = 0f;
            CmdFireShot(attackPosition.position, attackPosition.forward);
        }
    }

    [Command]
    void CmdFireShot(Vector3 origin, Vector3 direction)
    {
        RaycastHit hit;

        Ray ray = new Ray(origin, direction);
        Debug.DrawRay(ray.origin, ray.direction * 3f, Color.red, 1f);

        bool hitTarget = Physics.Raycast(ray, out hit, attackDistance);

        if (hitTarget)
        {
            PlayerHealth enemy = hit.transform.GetComponent<PlayerHealth>();

            if (enemy != null)
                enemy.TakeDamage(attackPower);

        }

        RpcProcessShotEffects(hitTarget, hit.point);
    }

    [ClientRpc]
    void RpcProcessShotEffects(bool playImpact, Vector3 impactPosition)
    {
        attackEffects.PlayShotEffects();

        if (playImpact)
            attackEffects.PlayImpactEffect(impactPosition);
    }
}