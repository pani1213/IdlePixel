using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public enum PlayerState { Idle, Die}
public class Player : MonoBehaviour, IDamaged
{
    public Image HpBar;

    public float maxHp;
    public float hp;
    public float hpRegen;
    public float ATK;
    public float ATKSpeed;
    public float critical;
    public float criticalPower;
    public float doubleShot;
    public float tripleShot;
    public float highclassATK;
    public float normalAnemyATK;
    public float attackRage = 2.5f;

    public PlayerState playerState = PlayerState.Idle;
    private bool isCriticalAttack;
    //CoolTime
    private float hpRegenCoolTime = 0f, hpRegenCoolTimeLImit = 0.5f;
    private float AttackCooltime;


    public Animator myAnimator;
    // 체력 리젠 
    // 공격 속도
    // 치명타
    // 치명타 피해량
    // 더블샷, 트리플샷(공격함수 중복실행)
    public void IDamaged(DamageType _damageType)
    {
        hp -= _damageType.Damage;

        if (hp <= 0)
        {
           
            Die();
        }
        HpBar.fillAmount = hp / maxHp;
    }
 
    void Start()
    {
        hp = maxHp;
    }
    private void Update()
    {
        if (playerState == PlayerState.Idle)
        {
            Attack();
            HpRegen();
        }
    }
    private void ContinuityShot(float _ShotType)
    {
        if (Random.value < _ShotType / 100.0f)
        {
            Monster monster = null;
            if (GameController.instance.currentSpawnMonsters.Count > 0)
            {
                monster = GameController.instance.currentSpawnMonsters[0];
                monster?.IDamaged(new DamageType { Damage = AttackPower(), isCritical = isCriticalAttack });
            }
            isCriticalAttack = false;
            Debug.Log("Continuity Shot!");
        }
    }
    private void Attack()
    {
        if (GameController.instance.currentSpawnMonsters.Count > 0 && GetDistance(this.gameObject, GameController.instance.currentSpawnMonsters[0].gameObject) <= attackRage)
        {
            AttackCooltime += Time.deltaTime * ATKSpeed;
            if (AttackCooltime > 1)
            {
                myAnimator.SetTrigger("Attack");
                Monster monster = null;
                if (GameController.instance.currentSpawnMonsters.Count > 0)
                    monster = GameController.instance.currentSpawnMonsters[0];
                monster?.IDamaged(new DamageType { Damage = AttackPower(), isCritical = isCriticalAttack });
                isCriticalAttack = false;
                ContinuityShot(doubleShot);
                ContinuityShot(tripleShot);
                AttackCooltime = 0;
            }
        }
    }
    private void HpRegen()
    {
        hpRegenCoolTime += Time.deltaTime;
        if (hpRegenCoolTime >= hpRegenCoolTimeLImit && hp < maxHp)
        {
            hp += hpRegen;
            hpRegenCoolTime = 0;
        }
    }
    private void Die()
    {
        playerState = PlayerState.Die;
        StartCoroutine(DieAction());
        hp = maxHp;
    }
    IEnumerator DieAction()
    {
        Debug.Log("died Action Play");
        myAnimator.SetBool("IsDie", true);
        yield return new WaitForSeconds(1);
        myAnimator.SetBool("IsDie", false);
        playerState = PlayerState.Idle;
        GameController.instance.ClearStage();
    }
    private float GetDistance(GameObject player, GameObject target)
    {
        return Vector2.Distance(player.transform.position, target.transform.position);
    }
    private float AttackPower()
    {
        float power = ATK;
        if (Random.value < critical / 100.0f)
        {
            power *= criticalPower;
            isCriticalAttack = true;
            Debug.Log("critical hit! " + power);
        }
        return power;
    }
}
