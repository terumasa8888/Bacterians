using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UniRx;

/// <summary>
/// �v���C���[�̃L�����N�^�[�𐶐�����X�N���v�g
/// �}�E�X�C���v�b�g�̓N���X����������
/// <summary>
public class PlayerSpawnerScript : MonoBehaviour {

    [SerializeField] private float X_Max, X_Min, Y_Max, Y_Min;
    [SerializeField] private float exclusiveX_Max, exclusiveX_Min, exclusiveY_Max, exclusiveY_Min;//�N���b�N�͈͐���

    // �e�L�����N�^�[�̐����\��
    [SerializeField] private int saruCreatableTimes = 3;
    [SerializeField] private int houseDustCreatableTimes = 3;
    [SerializeField] private int clioneCreatableTimes = 3;
    [SerializeField] private int mijinkoCreatableTimes = 3;
    [SerializeField] private int piroriCreatableTimes = 3;

    // �e�L�����N�^�[�̃v���n�u
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

        // Type��Key�ɂ��āA��������L�����N�^�[�̃v���n�u��Value�ɐݒ�
        characterPrefabs = new Dictionary<ButtonType, GameObject>
        {
            { ButtonType.Saru, saru },
            { ButtonType.HouseDust, houseDust },
            { ButtonType.Clione, clione },
            { ButtonType.Mijinko, mijinko },
            { ButtonType.Pirori, pirori }
        };

        // �����\�񐔂̏�����
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
    /// �v���C���[�̃L�����N�^�[�𐶐�����
    /// </summary>
    /// <param name="buttonType">��������L�����N�^�[�̎��</param>
    void CreatePlayer(ButtonType buttonType)
    {
        if (EventSystem.current.IsPointerOverGameObject()) return;
        if (!Input.GetMouseButtonDown(0)) return;
        if (buttonManagerScript.SelectedButtonType.Value == ButtonType.None) return; // �ǉ�

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
    /// �L�����N�^�[�̐����ʒu���U�炷
    /// EnemySpawnerScript�ɂ��߂�����������
    /// </summary>
    private void ScatterPosition(GameObject character) {
        float x = Random.Range(-0.5f, 0.5f);
        float y = Random.Range(-0.5f, 0.5f);
        character.transform.Translate(x, y, 0);
    }

    /// <summary>
    /// �����\�񐔂����炷
    /// <summary>
    private void DecreaseCreatableTimes(ButtonType buttonType) {
        creatableTimes[buttonType]--;
        uiManager.UpdateButtonText(buttonType, creatableTimes[buttonType]); // UI���X�V
    }

    /// <summary>
    /// �S�L�����N�^�[�̐����\�񐔂̍��v���擾
    /// ��������ɕK�v
    /// </summary>
    public int GetTotalCreatableTimes() {
        int total = 0;
        foreach (var times in creatableTimes.Values) {
            total += times;
        }
        return total;
    }
}
