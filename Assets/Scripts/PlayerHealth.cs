using UnityEngine;
using UnityEngine.Networking;

public class PlayerHealth : NetworkBehaviour
{
    [SerializeField]
    int maxHealth = 3;
    int XP = 1;

    [SyncVar(hook = "OnHealthChanged")]
    int health;

    Player player;


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
    public int TakeDamage(int damage)
    {
        bool died = false;

        if (health <= 0)
            return 0;

        health -= damage;
        died = health <= 0;

        RpcTakeDamage(died);

        return died ? XP : 0;
    }

    [ClientRpc]
    void RpcTakeDamage(bool died)
    {
        if (isLocalPlayer)
            PlayerCanvas.canvas.FlashDamageEffect();

        if (died)
            player.Die();
    }

    void OnHealthChanged(int value)
    {
        health = value;
        if (isLocalPlayer)
            PlayerCanvas.canvas.SetHealth(value);
    }
}