using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerSpawnerScript : MonoBehaviour
{
    public GameObject player;//Saru�����ł͂Ȃ���ʉ��������̂ŁAFind�łƂ��Ă��邱�ƂɂȂ�B
    
    public int X_Max, X_Min, Y_Max, Y_Min;//�X�e�[�W�ɂ���ĕς���Ă���
    public float exclusiveX_Max, exclusiveX_Min, exclusiveY_Max, exclusiveY_Min;

    private Vector3 mousePosition;
    private Vector3 objPos;
    float hp, attack, speed, multiplySpeed, count;
    //SpriteRenderer spriteRenderer;
    Sprite sprite;
    int createTimes;

    ///////////
    int multiplyTimes = -1;
    int j = 1;

    public float saruHP, saruAttack, saruSpeed, saruMultiplySpeed, saruCount;
    public float houseDustHP, houseDustAttack, houseDustSpeed, houseDustMultiplySpeed, houseDustCount;
    public float clioneHP, clioneAttack, clioneSpeed, clioneMultiplySpeed, clioneCount;
    public float mijinkoHP, mijinkoAttack, mijinkoSpeed, mijinkoMultiplySpeed, mijinkoCount;
    public float piroriHP, piroriAttack, piroriSpeed, piroriMultiplySpeed, piroriCount;

    public Sprite saruSprite, houseDustSprite, clioneSprite, mijinkoSprite, piroriSprite;
    public int saruCreateTimes, houseDustCreateTimes, clioneCreateTimes, mijinkoCreateTimes, piroriCreateTimes;

    /////////
    //public int saruMultiplyTimes, houseDustMultiplyTimes, clioneMultiplyTimes, mijinkoMultiplyTimes, piroriMultiplyTimes;


    public GameObject saru;
    public GameObject houseDust;
    public GameObject clione;
    public GameObject mijinko;
    public GameObject pirori;
    public GameObject playerStand;

    public Text saruButtonText;
    public Text houseDustButtonText;
    public Text clioneButtonText;
    public Text mijinkoButtonText;
    public Text piroriButtonText;

    private GameObject buttonManager;
    ButtonManagerScript buttonManagerScript;



    void Start()
    {
        buttonManager = GameObject.Find("ButtonManager");
        buttonManagerScript = buttonManager.GetComponent<ButtonManagerScript>();

        saruButtonText.text = saruCreateTimes.ToString();
        houseDustButtonText.text = houseDustCreateTimes.ToString();
        clioneButtonText.text = clioneCreateTimes.ToString();
        mijinkoButtonText.text = mijinkoCreateTimes.ToString();
        piroriButtonText.text = piroriCreateTimes.ToString();
    }

    /*void FixedUpdate() {
        Debug.Log("a");
    }*/
    void Update()
    {
        CreatePlayer();
    }

    void CreatePlayer() {

        if (EventSystem.current.IsPointerOverGameObject() != true) {//�{�^���̏�ɃJ�[�\�����Ȃ����
            if (Input.GetMouseButtonDown(0)) {//�N���b�N�����
                mousePosition = Input.mousePosition;//���̍��W���擾
                mousePosition.z = -1.0f;
                objPos = Camera.main.ScreenToWorldPoint(mousePosition);//���[���h���W�ɕϊ�

                if (objPos.x > X_Min && objPos.x < X_Max) {
                    if (objPos.y > Y_Min && objPos.y < Y_Max) {
                        if ((objPos.x < exclusiveX_Min || objPos.x > exclusiveX_Max) || (objPos.y < exclusiveY_Min || objPos.y > exclusiveY_Max)) {//�N���b�N�͈͐���

                            int num = buttonManagerScript.GetPlayerNumber();

                            //switch
                            switch (num) {
                                case 0:
                                    break;
                                case 1:
                                    player = saru;
                                    hp = saruHP;
                                    attack = saruAttack;
                                    speed = saruSpeed;
                                    multiplySpeed = saruMultiplySpeed;
                                    sprite = saruSprite;
                                    count = saruCount;

                                    if (saruCreateTimes > 0) {
                                        saruCreateTimes--;
                                        saruButtonText.text = saruCreateTimes.ToString();
                                    }
                                    else {
                                        return;
                                    }
                                    //SetMultiplyTimes(saruMultiplyTimes);
                                    break;
                                case 2:
                                    player = houseDust;
                                    hp = houseDustHP;
                                    attack = houseDustAttack;
                                    speed = houseDustSpeed;
                                    multiplySpeed = houseDustMultiplySpeed;
                                    sprite = houseDustSprite;
                                    count = houseDustCount;

                                    if (houseDustCreateTimes > 0) {
                                        houseDustCreateTimes--;
                                        houseDustButtonText.text = houseDustCreateTimes.ToString();
                                    }
                                    else {
                                        return;
                                    }
                                    //SetMultiplyTimes(houseDustMultiplyTimes);
                                    break;//�ȉ�����
                                case 3:
                                    player = clione;
                                    hp = clioneHP;
                                    attack = clioneAttack;
                                    speed = clioneSpeed;
                                    multiplySpeed = clioneMultiplySpeed;
                                    sprite = clioneSprite;
                                    count = clioneCount;

                                    if (clioneCreateTimes > 0) {
                                        clioneCreateTimes--;
                                        clioneButtonText.text = clioneCreateTimes.ToString();
                                    }
                                    else {
                                        return;
                                    }
                                    // SetMultiplyTimes(clioneMultiplyTimes);
                                    break;
                                case 4:
                                    player = mijinko;
                                    hp = mijinkoHP;
                                    attack = mijinkoAttack;
                                    speed = mijinkoSpeed;
                                    multiplySpeed = mijinkoMultiplySpeed;
                                    sprite = mijinkoSprite;
                                    count = mijinkoCount;

                                    if (mijinkoCreateTimes > 0) {
                                        mijinkoCreateTimes--;
                                        mijinkoButtonText.text = mijinkoCreateTimes.ToString();
                                    }
                                    else {
                                        return;
                                    }
                                    //SetMultiplyTimes(mijinkoMultiplyTimes);
                                    break;
                                case 5:
                                    player = pirori;
                                    hp = piroriHP;
                                    attack = piroriAttack;
                                    speed = piroriSpeed;
                                    multiplySpeed = piroriMultiplySpeed;
                                    sprite = piroriSprite;
                                    count = piroriCount;

                                    if (piroriCreateTimes > 0) {
                                        piroriCreateTimes--;
                                        piroriButtonText.text = piroriCreateTimes.ToString();
                                    }
                                    else {
                                        return;
                                    }
                                    //SetMultiplyTimes(piroriMultiplyTimes);
                                    break;
                                case 6:
                                    break;
                            }



                            if (num != 0 && num != 6) {//�Ȃ�炩�̃{�^�����I�����Ă���Ƃ�
                                                       //if (multiplyTimes > 0) {
                                                       //Debug.Log(multiplyTimes);
                                for (int i = 0; i < 5; i++) {




                                    //Player(j)��PlayerStand(j)�𐶐�
                                    var o1 = Instantiate(player) as GameObject;
                                    o1.name = player.name + "(" + j + ")";

                                    var o2 = Instantiate(playerStand) as GameObject;
                                    o2.name = player.name + "Stand" + "(" + j + ")";

                                    SpriteRenderer spriteRenderer = o2.GetComponent<SpriteRenderer>();
                                    spriteRenderer.sprite = sprite;

                                    o1.GetComponent<NavMeshAgent>().speed = speed;//�X�s�[�h������NavMesh���炢����B
                                    o1.transform.position = objPos;
                                    o1.transform.rotation = Quaternion.identity;

                                    PlayerScript playerScript = o1.GetComponent<PlayerScript>();
                                    playerScript.mousePosition = objPos;
                                    StandScript script = o2.GetComponent<StandScript>();
                                    script.standuser = o1;

                                    script.SetStandStatus(hp, attack, speed, multiplySpeed);
                                    j++;
                                }




                                //multiplyTimes -= 1;
                                /*switch (num) {
                                    case 1:
                                        if (saruMultiplyTimes > 0) {
                                            saruMultiplyTimes -= 1;
                                        }
                                        break;
                                    case 2:
                                        houseDustMultiplyTimes -= 1;
                                        break;
                                    case 3:
                                        clioneMultiplyTimes -= 1;
                                        break;
                                    case 4:
                                        mijinkoMultiplyTimes -= 1;
                                        break;
                                    case 5:
                                        piroriMultiplyTimes -= 1;
                                        break;

                                }*/
                                //}
                            }
                            //Debug.Log(objPos);
                        }
                    }
                }
            }
        }
    }

    /*void SetMultiplyTimes(int times) {
        if (multiplyTimes == -1) {//���߂Ă̑���Ȃ��
            multiplyTimes = times;//���������ėǂ��B�����łȂ��Ȃ�X�V�͂��Ȃ�
        }
    }*/
}
