using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 各キャラクターの生成可能回数を表示するUIのテキストを更新するスクリプト
/// 正確にいえばTextというよりCount
/// </summary>
public class UIManager : MonoBehaviour {

    public Text saruButtonText;
    public Text houseDustButtonText;
    public Text clioneButtonText;
    public Text mijinkoButtonText;
    public Text piroriButtonText;

    private Dictionary<ButtonType, Text> buttonTexts;

    public void InitializeButtonTexts(Dictionary<ButtonType, int> createTimes) {
        buttonTexts = new Dictionary<ButtonType, Text> {
            { ButtonType.Saru, saruButtonText },
            { ButtonType.HouseDust, houseDustButtonText },
            { ButtonType.Clione, clioneButtonText },
            { ButtonType.Mijinko, mijinkoButtonText },
            { ButtonType.Pirori, piroriButtonText }
        };

        foreach (var type in buttonTexts.Keys) {
            buttonTexts[type].text = createTimes[type].ToString();
        }
    }

    public void UpdateButtonText(ButtonType type, int createTimes) {
        if (buttonTexts.ContainsKey(type)) {
            buttonTexts[type].text = createTimes.ToString();
        }
    }
}