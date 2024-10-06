using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UniRx;

/// <summary>
/// �v���C���[�̃L�����N�^�[�𐶐�����X�N���v�g
/// �}�E�X�C���v�b�g�̓N���X�𕪗�������
/// <summary>
public class PlayerSpawnerScript : MonoBehaviour {

    //�Ȃ��int��float
    //�Ȃ��public
    public int X_Max, X_Min, Y_Max, Y_Min;
    public float exclusiveX_Max, exclusiveX_Min, exclusiveY_Max, exclusiveY_Min;//�N���b�N�͈͐���

    private Vector3 mousePosition;
    private Vector3 objPos;

    // �e�L�����N�^�[�̐����\�񐔂��C���X�y�N�^�[����ݒ�
    [SerializeField] private int saruCreatableTimes = 3;
    [SerializeField] private int houseDustCreatableTimes = 3;
    [SerializeField] private int clioneCreatableTimes = 3;
    [SerializeField] private int mijinkoCreatableTimes = 3;
    [SerializeField] private int piroriCreatableTimes = 3;

    // �e�L�����N�^�[�A�X�^���h�̃v���n�u
    [SerializeField] private GameObject saru;
    [SerializeField] private GameObject houseDust;
    [SerializeField] private GameObject clione;
    [SerializeField] private GameObject mijinko;
    [SerializeField] private GameObject pirori;
    [SerializeField] private GameObject playerStand;

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

        // ButtonManagerScript��SelectedButtonType���Ď�
        buttonManagerScript.SelectedButtonType
            .Where(buttonType => buttonType != ButtonType.None)
            .Subscribe(buttonType => CreatePlayer(buttonType))
            .AddTo(this);
    }

    void CreatePlayer(ButtonType buttonType) {
        if (EventSystem.current.IsPointerOverGameObject()) return;
        if (!Input.GetMouseButtonDown(0)) return;

        mousePosition = Input.mousePosition; // ���̍��W���擾
        mousePosition.z = 0f;
        objPos = Camera.main.ScreenToWorldPoint(mousePosition); // ���[���h���W�ɕϊ�

        if (objPos.x <= X_Min || objPos.x >= X_Max || objPos.y <= Y_Min || objPos.y >= Y_Max) return;
        if ((objPos.x >= exclusiveX_Min && objPos.x <= exclusiveX_Max) && (objPos.y >= exclusiveY_Min && objPos.y <= exclusiveY_Max)) return;
        if (creatableTimes[buttonType] <= 0) return;

        GameObject characterPrefab = characterPrefabs[buttonType];
        for (int i = 0; i < 5; i++) {
            GameObject character = Instantiate(characterPrefab) as GameObject;
            character.transform.position = objPos;
            character.transform.rotation = Quaternion.identity;

            // �v���C���[�̃X�N���v�g�ɖړI�n�Ƃ��ă}�E�X�̍��W��n��
            PlayerScript playerScript = character.GetComponent<PlayerScript>();
            playerScript.mousePosition = objPos;
        }
        DecreaseCreatableTimes(buttonType); // �����\�񐔂����炷
    }

    private void DecreaseCreatableTimes(ButtonType buttonType) {
        creatableTimes[buttonType]--;
        uiManager.UpdateButtonText(buttonType, creatableTimes[buttonType]); // UI���X�V
    }

    // �V�������\�b�h��ǉ�
    public int GetTotalCreatableTimes() {
        int total = 0;
        foreach (var times in creatableTimes.Values) {
            total += times;
        }
        return total;
    }
}