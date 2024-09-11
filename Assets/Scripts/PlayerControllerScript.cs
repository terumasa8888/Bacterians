using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class PlayerControllerScript : MonoBehaviour
{
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

    void Start()
    {
        buttonManager = GameObject.Find("ButtonManager");
        buttonManagerScript = buttonManager.GetComponent<ButtonManagerScript>();
        //Debug.Log(count);//0
    }

    void LateUpdate() {
        if (rot) {
            circle.transform.rotation *= Quaternion.AngleAxis(angle, Vector3.back);
        }
    }

    void Update()
    {
        ChooseArea();
    }

    void ChooseArea() {

        int num = buttonManagerScript.GetPlayerNumber();
        if (num == 6) {
            if (EventSystem.current.IsPointerOverGameObject() != true) {//�{�^���̏�ɃJ�[�\�����Ȃ����
                if (Input.GetMouseButtonDown(0)) {
                    //Debug.Log("��������");
                    mousePosition1 = Input.mousePosition;//screen
                    mousePosition1.z = -1.0f;
                    objPos1 = Camera.main.ScreenToWorldPoint(mousePosition1);//world
                    objPos1.z = -1.0f;
                    

                    if (count >= 1) {//������
                                     //�ړ�
                        foreach (GameObject obj in list) {
                            if (obj != null) {
                                PlayerScript script = obj.GetComponent<PlayerScript>();
                                script.mousePosition = objPos1;
                                script.distinationFlag = false;

                                obj.GetComponent<NavMeshAgent>().speed = PlayerPrefs.GetFloat(obj.name);
                            }
                        }

                        list.Clear();//���X�g����
                        count = 0;
                        //Debug.Log("���������Bcount:" + count);
                        circle.SetActive(false);
                    }
                    else {//�~�߂�
                        count = 1;
                        //Debug.Log("�~�߂��Bcount:" + count);//1

                        ///////
                        Vector3 point = Vector3.zero;
                        RectTransform rc = circle.GetComponent<RectTransform>();
                        RectTransformUtility.ScreenPointToWorldPointInRectangle(rc, mousePosition1, null, out point);

                        circle.GetComponent<RectTransform>().position = point;
                        circle.SetActive(true);
                        //////


                        GameObject[] objects = GameObject.FindGameObjectsWithTag("NavMesh");

                        foreach (GameObject obj in objects) {
                            if (Vector3.Distance(obj.transform.position, objPos1) <= 2) {

                                float speed = obj.GetComponent<NavMeshAgent>().speed;
                                PlayerPrefs.SetFloat(obj.name, speed);
                                list.Add(obj);

                                obj.GetComponent<NavMeshAgent>().speed = 0;

                                /*PlayerScript script = obj.GetComponent<PlayerScript>();
                                script.mousePosition = mousePosition1;
                                script.distinationFlag = false;*/

                            }
                        }
                        if (list.Count == 0) {//���X�g�̗v�f0
                            //Debug.Log("���X�g�̗v�f0");
                            count = 0;
                            circle.SetActive(false);
                        }
                    }
                }
            }
        }
        

        
    }
}
