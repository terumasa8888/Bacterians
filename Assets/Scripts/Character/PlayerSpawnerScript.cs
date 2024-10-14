using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UniRx;

/// <summary>
/// プレイヤーのキャラクターを生成するスクリプト
/// マウスインプットはクラス分離を検討
/// <summary>
public class PlayerSpawnerScript : MonoBehaviour {

    [SerializeField] private float X_Max, X_Min, Y_Max, Y_Min;
    [SerializeField] private float exclusiveX_Max, exclusiveX_Min, exclusiveY_Max, exclusiveY_Min;//クリック範囲制限

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

    private GameObject buttonManager;
    private ButtonManagerScript buttonManagerScript;
    private UIManager uiManager;

    private Dictionary<ButtonType, GameObject> characterPrefabs;
    private Dictionary<ButtonType, int> creatableTimes;

    void Start() {
        buttonManager = GameObject.Find("ButtonManager");
        buttonManagerScript = buttonManager.GetComponent<ButtonManagerScript>();
        uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();

        // TypeをKeyにして、生成するキャラクターのプレハブをValueに設定
        characterPrefabs = new Dictionary<ButtonType, GameObject>
        {
            { ButtonType.Saru, saru },
            { ButtonType.HouseDust, houseDust },
            { ButtonType.Clione, clione },
            { ButtonType.Mijinko, mijinko },
            { ButtonType.Pirori, pirori }
        };

        // 生成可能回数の初期化
        creatableTimes = new Dictionary<ButtonType, int>
        {
            { ButtonType.Saru, saruCreatableTimes },
            { ButtonType.HouseDust, houseDustCreatableTimes },
            { ButtonType.Clione, clioneCreatableTimes },
            { ButtonType.Mijinko, mijinkoCreatableTimes },
            { ButtonType.Pirori, piroriCreatableTimes }
        };

        uiManager.InitializeButtonTexts(creatableTimes);

        buttonManagerScript.SelectedButtonType
            .Where(buttonType => buttonType != ButtonType.None)
            .Subscribe(buttonType => {
                Observable.EveryUpdate()
                    .Where(_ => Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
                    .Subscribe(_ => CreatePlayer(buttonType))
                    .AddTo(this);
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
        if (buttonManagerScript.SelectedButtonType.Value == ButtonType.None) return; // 追加

        Vector3 mousePosition = Input.mousePosition;
        Vector3 objPos = Camera.main.ScreenToWorldPoint(mousePosition);
        objPos.z = 0f;

        if (creatableTimes[buttonType] <= 0) return;

        GameObject characterPrefab = characterPrefabs[buttonType];
        for (int i = 0; i < 5; i++)
        {
            GameObject character = Instantiate(characterPrefab);
            character.transform.position = objPos;
            character.transform.rotation = Quaternion.identity;
            ScatterPosition(character);

            NavMeshAgent2D nav = character.GetComponent<NavMeshAgent2D>();
            nav.destination = objPos;
        }
        DecreaseCreatableTimes(buttonType);
    }

    /// <summary>
    /// キャラクターの生成位置を散らす
    /// EnemySpawnerScriptにも近い処理がある
    /// </summary>
    private void ScatterPosition(GameObject character) {
        float x = Random.Range(-0.5f, 0.5f);
        float y = Random.Range(-0.5f, 0.5f);
        character.transform.Translate(x, y, 0);
    }

    /// <summary>
    /// 生成可能回数を減らす
    /// <summary>
    private void DecreaseCreatableTimes(ButtonType buttonType) {
        creatableTimes[buttonType]--;
        uiManager.UpdateButtonText(buttonType, creatableTimes[buttonType]); // UIを更新
    }

    /// <summary>
    /// 全キャラクターの生成可能回数の合計を取得
    /// 勝利判定に必要
    /// </summary>
    public int GetTotalCreatableTimes() {
        int total = 0;
        foreach (var times in creatableTimes.Values) {
            total += times;
        }
        return total;
    }
}
