using UnityEngine;

public class EconomyManager : Singleton<EconomyManager>
{
    private int totalMoney = 10;

    public int TotalMoney
    {
        get => totalMoney;
        set
        {
            totalMoney = value;
            UIManager.Instance.UpdateMoneyDisplay(totalMoney);
        }
    }

    public void AddMoney(int amount)
    {
        TotalMoney += amount;
    }

    public void SubtractMoney(int amount)
    {
        TotalMoney -= amount;
    }
}
