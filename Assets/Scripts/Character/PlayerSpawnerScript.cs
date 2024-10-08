using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UniRx;

/// <summary>
/// プレイヤーのキャラクターを生成するスクリプト
/// マウスインプットはクラスを分離したい
/// <summary>
public class PlayerSpawnerScript : MonoBehaviour {

    //なんでintとfloat
    //なんでpublic
    public int X_Max, X_Min, Y_Max, Y_Min;
    public float exclusiveX_Max, exclusiveX_Min, exclusiveY_Max, exclusiveY_Min;//クリック範囲制限

    private Vector3 mousePosition;
    private Vector3 objPos;

    // 各キャラクターの生成可能回数をインスペクターから設定
    [SerializeField] private int saruCreatableTimes = 3;
    [SerializeField] private int houseDustCreatableTimes = 3;
    [SerializeField] private int clioneCreatableTimes = 3;
    [SerializeField] private int mijinkoCreatableTimes = 3;
    [SerializeField] private int piroriCreatableTimes = 3;

    // 各キャラクター、スタンドのプレハブ
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

        // ButtonManagerScriptのSelectedButtonTypeを監視
        /*buttonManagerScript.SelectedButtonType
            .Where(buttonType => buttonType != ButtonType.None)
            .Subscribe(buttonType => CreatePlayer(buttonType))
            .AddTo(this);*/
        //ここ理解できてない
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

    void CreatePlayer(ButtonType buttonType) {
        if (EventSystem.current.IsPointerOverGameObject()) return;
        if (!Input.GetMouseButtonDown(0)) return;

        mousePosition = Input.mousePosition; // その座標を取得
        objPos = Camera.main.ScreenToWorldPoint(mousePosition); // ワールド座標に変換
        objPos.z = 0f;

        if (objPos.x <= X_Min || objPos.x >= X_Max || objPos.y <= Y_Min || objPos.y >= Y_Max) return;
        if ((objPos.x >= exclusiveX_Min && objPos.x <= exclusiveX_Max) && (objPos.y >= exclusiveY_Min && objPos.y <= exclusiveY_Max)) return;
        if (creatableTimes[buttonType] <= 0) return;

        GameObject characterPrefab = characterPrefabs[buttonType];
        for (int i = 0; i < 5; i++) {
            GameObject character = Instantiate(characterPrefab);
            character.transform.position = objPos;
            character.transform.rotation = Quaternion.identity;
            ScatterPosition(character);

            NavMeshAgent2D nav = character.GetComponent<NavMeshAgent2D>();
            nav.destination = objPos;
        }
        DecreaseCreatableTimes(buttonType);
    }

    private void ScatterPosition(GameObject character) {
        float x = Random.Range(-0.5f, 0.5f);
        float y = Random.Range(-0.5f, 0.5f);
        character.transform.Translate(x, y, 0);
    }

    private void DecreaseCreatableTimes(ButtonType buttonType) {
        creatableTimes[buttonType]--;
        uiManager.UpdateButtonText(buttonType, creatableTimes[buttonType]); // UIを更新
    }

    public int GetTotalCreatableTimes() {
        int total = 0;
        foreach (var times in creatableTimes.Values) {
            total += times;
        }
        return total;
    }
}
