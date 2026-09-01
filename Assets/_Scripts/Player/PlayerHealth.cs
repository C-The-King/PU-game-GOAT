using System.Collections;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IHealth
{
    public int maxHP = 100;
    private int hp = 100;
    float damageDelay = 0.25f;
    bool canTakeDamage = true;
    bool useAnim;
    Animator anim;
    [SerializeField] float deathAmimDuration = 1.2f;
    private void Start()
    {
        TryGetComponent<Animator>(out anim);
        if (anim != null)
        {
            useAnim = true;
        }
        InitiatePlayerHealth();

    }

    public void InitiatePlayerHealth()
    {
        hp = maxHP;
        if (useAnim) {
            anim.SetBool("Died", false);
            GetComponent<PlatformControllerAdv>()?.SetPlayerDead(false);
        }
        canTakeDamage = true;
        GetComponent<PlayerRespawn>()?.RewpawnPlayer();
    }
    public void TakeDamage(int dmg)
    {
        if (!canTakeDamage)
        {
            return;
        }
        
        hp -= dmg;
        if (hp <= 0)
        {
            StartCoroutine(DamageCooldown(damageDelay));
            if (useAnim)
            {
                anim.SetBool("Died", true);
                GetComponent<PlatformControllerAdv>()?.SetPlayerDead(true);
            }
            if (HealthVisualizer.Instance.LoseLife() && canTakeDamage) {
                Invoke("InitiatePlayerHealth", deathAmimDuration);
            }
            canTakeDamage = false;
        }
    }
    IEnumerator DamageCooldown(float delay)
    {
        yield return new WaitForSeconds(delay);
        canTakeDamage = true;
    }
}