// Copyright (c) 2012-2023 FuryLion Group. All Rights Reserved.

using UnityEngine;

public class MoneyManager : MonoBehaviour
{
    [SerializeField] private int _money = 20;

    public static MoneyManager Instance;

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;
    }

    private void Start()
    {
        UIController.Instance.SetMoneyNumber(_money);
    }

    public void AddMoney(int money)
    {
        _money += money;
        UIController.Instance.SetMoneyNumber(_money);
    }

    public bool CanAfford(int amount)
    {
        return _money >= amount;
    }

    public void SpendMoney(int amount)
    {
        if (CanAfford(amount))
            _money -= amount;
        else
            Debug.Log("Нет денег!");
    }

    public int GetMoney()
    {
        return _money;
    }
}