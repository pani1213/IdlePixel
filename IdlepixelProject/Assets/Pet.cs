using UnityEngine;
using UnityEngine.U2D.Animation;
public enum PetState { Idle, Attack}
public class Pet : MonoBehaviour
{
    public float attackPower;
    public float attackSpeed;

    private float AttackCooltime;

    public Animator myAnimator;
    private bool isCriticalAttack;
    public PetState petState = PetState.Idle;

    public SpriteLibrary mySpriteLibrary;

    public void init(int id)
    {
        GachaItemData data = null;
        if (id != 0) 
            data = DataContainer.instance.allGachaData[id];
        else
        {
            gameObject.SetActive(false);
            return;
        } 
        attackPower = data.atkValue;
        attackSpeed = data.atkSpeedValue;
        Debug.Log($"{data.prefabName} 키가 잇는지 확인 {PetController.instance.petSpriteContainer.ContainsKey(data.prefabName)}");
        mySpriteLibrary.spriteLibraryAsset = PetController.instance.petSpriteContainer[data.prefabName];

        gameObject.SetActive(true);
    }
    public void Update()
    {
        Attack();
    }
        private void Attack()
    {
        if (GameController.instance.currentSpawnMonsters.Count > 0 && GetDistance(PlayerFindManager.instance.Player.gameObject, GameController.instance.currentSpawnMonsters[0].gameObject) <= PlayerFindManager.instance.Player.attackRage)
        {
            AttackCooltime += Time.deltaTime * attackSpeed;
            if (AttackCooltime > 1)
            {
                myAnimator.SetTrigger("Attack");
                Monster monster = null;
                if (GameController.instance.currentSpawnMonsters.Count > 0)
                    monster = GameController.instance.currentSpawnMonsters[0];
                monster?.IDamaged(new DamageType { Damage = AttackPower(), isCritical = isCriticalAttack });
                isCriticalAttack = false;
                AttackCooltime = 0;
            }
        }
    }
    private float AttackPower()
    {
        float power = attackPower;
        if (Random.value < PlayerFindManager.instance.Player.critical / 100.0f)
        {
            power *= PlayerFindManager.instance.Player.criticalPower;
            isCriticalAttack = true;
            Debug.Log("critical hit! " + power);
        }
        return power;
    }
    private float GetDistance(GameObject player, GameObject target)
    {
        return Vector2.Distance(player.transform.position, target.transform.position);
    }
    
}