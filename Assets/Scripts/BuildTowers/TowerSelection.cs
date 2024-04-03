// Copyright (c) 2012-2023 FuryLion Group. All Rights Reserved.

using UnityEngine;

public class TowerSelection : MonoBehaviour
{
    [SerializeField] private TowerBuildController _buildController;

    public static TowerSelection Instance;
    private bool _placingTower;

    private int _towerCost = 20;

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;
    }

    private bool CanBuy()
    {
        return MoneyManager.Instance.CanAfford(_towerCost);
    }

    void Update()
    {
        //для дебага
        if (!_placingTower && Input.GetKeyDown(KeyCode.Y))
            StartPlacingTower();
    }

    public void StartPlacingTower()
    {
        if (!CanBuy())
            return;

        MoneyManager.Instance.SpendMoney(_towerCost);
        UIController.Instance.SetMoneyNumber(MoneyManager.Instance.GetMoney());

        _buildController.CreateTowerPreview(GetMouseWorldPosition());
        _placingTower = true;
    }

    public static void FinishPlacingTower()
    {
        Instance._placingTower = false;
    }

    private Vector3 GetMouseWorldPosition()
    {
        var mousePosition = Input.mousePosition;
        mousePosition.z = -Camera.main.transform.position.z;

        return Camera.main.ScreenToWorldPoint(mousePosition);
    }
}