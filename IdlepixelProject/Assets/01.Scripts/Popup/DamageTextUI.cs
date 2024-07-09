using System;
using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(Text))]
public class DamageTextUI : PoolObject
{
    public Text damageText;
    public float lifetime = 2f;
    private float startTime;
    public bool isRed;

    private float speed = 0.5f; // 이동 속도
    public float randomRange = 1.0f; // 랜덤 방향의 범위
    private Vector3 direction;

    private void Awake()
    {
        if (damageText == null) damageText = GetComponent<Text>();
        transform.parent = BattlePaticleUICanvas.instance.transform;
        transform.localScale = Vector3.one;

        float randomX = UnityEngine.Random.Range(-randomRange, randomRange);
        float randomZ = UnityEngine.Random.Range(-randomRange, randomRange);
        direction = new Vector3(randomX, 1.0f, randomZ).normalized;
    }
    private void Update()
    {
        // 텍스트가 일정 시간 후에 사라지도록 함
        if (Time.time >= startTime + lifetime)
        {
            ObjectPooler.instance.InsertPoolObject(this);
        }
        transform.position += direction * speed * Time.deltaTime;
    }
    public void textInput(int _damage, bool isRed)
    {
        init();
        startTime = Time.time;
        damageText.text = _damage.FormatNumber();
        if(isRed)
            damageText.color = Color.red;
        else
            damageText.color = Color.white;

    }

}