using UnityEngine;
using UnityEngine.UI;
public class Monster : PoolObject , IDamaged
{
    public enum monsterState { Move,Attack}
    public enum MonsterType { Nomal, Boss}
    public float maxHp;
    public float hp;
    public float MoveSpeed;
    public float ATK;
    private float ATKSpeed = 3f;
    private float rewardCount;
    private float rewardId;
    private float cooltime;
    public Image hpBar;
    public monsterState state = monsterState.Move;
    public MonsterType monsterType = MonsterType.Nomal;
    private float TargetPosX = -0.5f;
    public Animator animator;
    private void Update()
    {
        switch (state)
        {
            case monsterState.Move:
                Move();
                break;
            case monsterState.Attack:
                if (PlayerFindManager.instance.Player.playerState != PlayerState.Die)
                    Attack();
                break;
        }
    }
    private void Move()
    {
        if (gameObject.transform.position.x >= TargetPosX)
        {
            animator?.SetBool("Idle",false);
            animator?.SetBool("Run",true);
            gameObject.transform.Translate(Vector2.left * Time.deltaTime);
        }
        else
        {
            //Debug.Log("공격변경");
            animator?.SetBool("Run", false);
            animator?.SetBool("Idle", true);
            state = monsterState.Attack;
        }
    }
    private void Attack()
    {
        cooltime += Time.deltaTime;
        if (cooltime >= ATKSpeed)
        {
            animator?.SetTrigger("Attack");
            PlayerFindManager.instance.Player.IDamaged(new DamageType() { Damage = ATK, isCritical = false });
            cooltime = 0;
        }
    }
    public void IDamaged(DamageType _damageType)
    {
        DamageTextUI TextUI = ObjectPooler.instance.GetPoolObject("DamageText",transform.position,Quaternion.identity).GetComponent<DamageTextUI>();
        animator?.SetTrigger("Hit");
        hp -= _damageType.Damage;
        TextUI.textInput((int)_damageType.Damage, _damageType.isCritical);
        TextUI.init();
        SetHp();
        if (hp <= 0)
        {
            GameController.instance.currentSpawnMonsters.Remove(this);
            ObjectPooler.instance.InsertPoolObject(this);
            ItemInfoManager.instance.InsertItem((int)rewardId, (int)rewardCount);
        }
    }
    public void Monsterinit(MonsterData monsterData)
    {
        init();
        state = monsterState.Move;
        maxHp = monsterData.hp;
        MoveSpeed = monsterData.speed;
        ATK = monsterData.atk;
        ATKSpeed = 3;
        rewardCount = monsterData.rewardCount;
        rewardId = monsterData.rewardId;
        cooltime = ATKSpeed;
        hp = maxHp;

        if (monsterData.id >= 1999) monsterType = MonsterType.Boss;
        else monsterType = MonsterType.Nomal;

        SetHp();
    }
    public void SetHp()
    {
        hpBar.fillAmount = hp / maxHp;
    }
}
