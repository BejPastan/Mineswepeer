using UnityEngine;
using UnityEngine.UI;

public class SoldierHP : MonoBehaviour
{
    int maxHP;
    int currentHP;

    [SerializeField]
    Slider hpBar;

    public void SetHP(int newMaxHP)
    {
        maxHP = newMaxHP;
        currentHP = maxHP;
        hpBar.maxValue = maxHP;
        hpBar.value = currentHP;
    }

    public bool TakeDamage(int damage)
    {
        currentHP -= damage;
        hpBar.value = currentHP;
        if(currentHP <= 0)
        {
            return false;
        }
        return true;
    }
}
