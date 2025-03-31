using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UniRx;
using System;

/// <summary>
/// プレイヤーのキャラクターを生成するスクリプト
/// マウスインプットはクラス分離を検討
/// <summary>
public class PlayerSpawner : MonoBehaviour {

    //[SerializeField] private float X_Max, X_Min, Y_Max, Y_Min;
    //[SerializeField] private float exclusiveX_Max, exclusiveX_Min, exclusiveY_Max, exclusiveY_Min;//クリック範囲制限

    // 各キャラクターの生成可能回数
    [SerializeField] private int saruCreatableTimes = 3;
    [SerializeField] private int houseDustCreatableTimes = 3;
    [SerializeField] private int clioneCreatableTimes = 3;
    [SerializeField] private int mijinkoCreatableTimes = 3;
    [SerializeField] private int piroriCreatableTimes = 3;

    // 各キャラクターのプレハブ
    [SerializeField] private GameObject saru;
    [SerializeField] private GameObject houseDust;
    [SerializeField] private GameObject clione;
    [SerializeField] private GameObject mijinko;
    [SerializeField] private GameObject pirori;

    [SerializeField] private GameObject uiManager;
    private ButtonManagerScript buttonManagerScript;
    private UIManager uiManagerScript;

    private Dictionary<ButtonType, GameObject> characterPrefabs;
    private Dictionary<ButtonType, int> creatableTimes;

    private CompositeDisposable disposables = new CompositeDisposable();

    void Start() {
        buttonManagerScript = uiManager.GetComponent<ButtonManagerScript>();
        uiManagerScript = uiManager.GetComponent<UIManager>();

        characterPrefabs = new Dictionary<ButtonType, GameObject>
        {
            { ButtonType.Saru, saru },
            { ButtonType.HouseDust, houseDust },
            { ButtonType.Clione, clione },
            { ButtonType.Mijinko, mijinko },
            { ButtonType.Pirori, pirori }
        };

        creatableTimes = new Dictionary<ButtonType, int>
        {
            { ButtonType.Saru, saruCreatableTimes },
            { ButtonType.HouseDust, houseDustCreatableTimes },
            { ButtonType.Clione, clioneCreatableTimes },
            { ButtonType.Mijinko, mijinkoCreatableTimes },
            { ButtonType.Pirori, piroriCreatableTimes }
        };

        Debug.Log("creatableTimes initialized.");

        uiManagerScript.InitializeButtonTexts(creatableTimes);

        buttonManagerScript.SelectedButtonType
            .Where(buttonType => buttonType != ButtonType.None)
            .Subscribe(buttonType => {
                disposables.Clear();
                Observable.EveryUpdate()
                    .Where(_ => Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
                    .Subscribe(_ => CreatePlayer(buttonType))
                    .AddTo(disposables);
            })
            .AddTo(this);
    }

    /// <summary>
    /// プレイヤーのキャラクターを生成する
    /// </summary>
    /// <param name="buttonType">生成するキャラクターの種類</param>
    void CreatePlayer(ButtonType buttonType)
    {
        if (EventSystem.current.IsPointerOverGameObject()) return;
        if (!Input.GetMouseButtonDown(0)) return;
        if (buttonManagerScript.SelectedButtonType.Value == ButtonType.None) return;

        Vector3 mousePosition = Input.mousePosition;
        Vector3 objPos = Camera.main.ScreenToWorldPoint(mousePosition);
        objPos.z = 0f;

        // クリックした位置にEnemyがいたら生成しない
        Collider2D hitCollider = Physics2D.OverlapPoint(objPos);
        if (hitCollider != null && hitCollider.CompareTag("Enemy")) return;

        if (creatableTimes[buttonType] <= 0) return;

        GameObject characterPrefab = characterPrefabs[buttonType];
        for (int i = 0; i < 5; i++)
        {
            GameObject character = Instantiate(characterPrefab);
            character.transform.position = objPos;
            character.transform.rotation = Quaternion.identity;
            ScatterPosition(character);
        }
        DecreaseCreatableTimes(buttonType);
    }

    /// <summary>
    /// キャラクターの生成位置を散らす
    /// </summary>
    private void ScatterPosition(GameObject character)
    {
        float x = UnityEngine.Random.Range(-0.5f, 0.5f);
        float y = UnityEngine.Random.Range(-0.5f, 0.5f);
        character.transform.Translate(x, y, 0);
    }

    /// <summary>
    /// 生成可能回数を減らす
    /// <summary>
    private void DecreaseCreatableTimes(ButtonType buttonType) {
        creatableTimes[buttonType]--;
        uiManagerScript.UpdateButtonText(buttonType, creatableTimes[buttonType]);
    }

    /// <summary>
    /// 全キャラクターの生成可能回数の合計を取得
    /// 勝利判定に必要
    /// </summary>
    public int GetTotalCreatableTimes()
    {
        int total = 0;
        foreach (var times in creatableTimes.Values)
        {
            total += times;
        }
        return total;
    }
}
