using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using UniRx;


/// <summary>
/// Player(Stand�̉��ɂ�����)�̓���𐧌䂷��X�N���v�g
/// �l�X�g���[������
/// public�ϐ��̂����ŃK�o�K�o
/// �ϐ������悭�Ȃ�
/// �X�s�[�h�ۑ�����̂�PlayerPrefs�g���Ă��邪�A�����Ɨǂ����@�͂Ȃ��̂�
/// </summary>
public class PlayersMover : MonoBehaviour {
    private Vector3 mousePosition1;
    private Vector3 objPos1;
    GameObject obj;
    private GameObject buttonManager;
    ButtonManagerScript buttonManagerScript;

    int count = 0;
    List<GameObject> list = new List<GameObject>();

    public GameObject circle;
    public float angle = 1;
    public bool rot = true;

    void Start() {
        buttonManager = GameObject.Find("ButtonManager");
        buttonManagerScript = buttonManager.GetComponent<ButtonManagerScript>();

        buttonManagerScript.SelectedButtonType
            .Where(buttonType => buttonType == ButtonType.None)
            .Subscribe(_ => EnableAreaSelection())
            .AddTo(this);
    }

    void LateUpdate() {
        if (rot) {
            circle.transform.rotation *= Quaternion.AngleAxis(angle, Vector3.back);
        }
    }

    void Update() {
        ChooseArea();
    }

    void ChooseArea() {
        if (EventSystem.current.IsPointerOverGameObject()) return;
        if (!Input.GetMouseButtonDown(0)) return;

        mousePosition1 = Input.mousePosition;
        mousePosition1.z = 0f;
        objPos1 = Camera.main.ScreenToWorldPoint(mousePosition1);

        if (count >= 1) {
            // ������
            foreach (GameObject obj in list) {
                if (obj != null) {
                    PlayerScript script = obj.GetComponent<PlayerScript>();
                    script.mousePosition = objPos1;
                    obj.GetComponent<NavMeshAgent>().speed = PlayerPrefs.GetFloat(obj.name);
                }
            }

            list.Clear();
            count = 0;
            circle.SetActive(false);
        }
        else {
            // �~�߂�
            count = 1;

            Vector3 point = Vector3.zero;
            RectTransform rc = circle.GetComponent<RectTransform>();
            RectTransformUtility.ScreenPointToWorldPointInRectangle(rc, mousePosition1, null, out point);

            circle.GetComponent<RectTransform>().position = point;
            circle.SetActive(true);

            GameObject[] objects = GameObject.FindGameObjectsWithTag("NavMesh");

            foreach (GameObject obj in objects) {
                if (Vector3.Distance(obj.transform.position, objPos1) <= 2) {
                    float speed = obj.GetComponent<NavMeshAgent>().speed;
                    PlayerPrefs.SetFloat(obj.name, speed);
                    list.Add(obj);
                    obj.GetComponent<NavMeshAgent>().speed = 0;
                }
            }

            if (list.Count == 0) {
                count = 0;
                circle.SetActive(false);
            }
        }
    }

    void EnableAreaSelection() {
        // �I���G���A��L���ɂ��鏈��
    }
}
