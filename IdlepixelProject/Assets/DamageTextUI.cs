using System;
using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(Text))]
public class DamageTextUI : PoolObject
{
    public Text damageText;
    public float speed = 5f;
    public float angle = 45f;
    public float lifetime = 2f;

    private float startTime;
    public bool isRed;
    private void Awake()
    {
        if (damageText == null) damageText = GetComponent<Text>();
        transform.parent = BattlePaticleUICanvas.instance.transform;
        transform.localScale = Vector3.one;
    }
    private void Update()
    {
        // 텍스트가 일정 시간 후에 사라지도록 함
        if (Time.time >= startTime + lifetime)
        {
            ObjectPooler.instance.InsertPoolObject(this);
        }
    }
    private float CalculateYOffset(float deltaTime)
    {
        float yOffset = speed * Mathf.Sin(angle * Mathf.Deg2Rad) * deltaTime - (Physics2D.gravity.magnitude * 0.5f * deltaTime * deltaTime);
        return yOffset;
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