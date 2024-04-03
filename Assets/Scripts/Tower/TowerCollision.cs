// Copyright (c) 2012-2023 FuryLion Group. All Rights Reserved.

using UnityEngine;
using UI;

public class TowerCollision : MonoBehaviour
{
    [SerializeField] private Tower _tower;

    public static UpgradeWindow UpgradeWindow;

    private void OnMouseDown()
    {
        if (UpgradeWindow != null)
            return;

        var upgradeWindow = WindowManager.OpenWindow<UpgradeWindow>();
        UpgradeWindow = upgradeWindow;
        upgradeWindow.SetData(_tower);
    }
}