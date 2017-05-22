using UnityEngine;
using UnityEngine.Networking;

public class PlayerHealth : NetworkBehaviour
{
    [SerializeField]
    int maxHealth = 3;

    Player player;
    int health;

    void Awake()
    {
        player = GetComponent<Player>();
    }

    [ServerCallback]
    void OnEnable()
    {
        health = maxHealth;
    }

    [Server]
    public bool TakeDamage(int damage)
    {
        bool dead = false;

        if (health <= 0)
            return dead;

        health -= damage;
        dead = health <= 0;

        RpcTakeDamage(dead);

        return dead;
    }

    [ClientRpc]
    void RpcTakeDamage(bool dead)
    {
        if (dead)
            player.Die();
    }
}