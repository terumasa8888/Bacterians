using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using UniRx;
using UniRx.Triggers;


/// <summary>
/// Player�̓���𐧌䂷��X�N���v�g
/// </summary>
public class PlayersMover : MonoBehaviour
{
    private GameObject buttonManager;
    private ButtonManagerScript buttonManagerScript;
    private List<NavMeshAgent2D> agents = new List<NavMeshAgent2D>();
    private bool isSelected = false;
    private bool isMoving = false;
    private Vector3 destination;

    [SerializeField] private GameObject circle;
    [SerializeField] private float SELECTION_RADIUS = 2f;

    private void Start()
    {
        buttonManager = GameObject.Find("ButtonManager");
        buttonManagerScript = buttonManager.GetComponent<ButtonManagerScript>();

        // �}�E�X���N���b�N�C�x���g���Ď�
        this.UpdateAsObservable()
            .Where(_ => Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
            .Where(_ => buttonManagerScript.SelectedButtonType.Value == ButtonType.None)
            .Subscribe(_ => HandleMouseClick())
            .AddTo(this);

        // �}�E�X�E�N���b�N�C�x���g���Ď�
        this.UpdateAsObservable()
            .Where(_ => Input.GetMouseButtonDown(1))
            .Subscribe(_ => buttonManagerScript.ResetOther())
            .AddTo(this);
    }

    private void Update()
    {
        if (isMoving)
        {
            MoveAgents(destination);
            CheckAgentsArrival();
        }
        Debug.Log(buttonManagerScript.SelectedButtonType.Value);
    }

    /// <summary>
    /// �}�E�X�N���b�N�C�x���g����������
    /// </summary>
    private void HandleMouseClick()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = Camera.main.nearClipPlane;
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        worldPosition.z = 0;

        if (isSelected)
        {
            Debug.Log("Setting new destination: " + worldPosition);
            destination = worldPosition;
            isMoving = true;
            // �ړI�n��ݒ肵����ɃT�[�N�����\���ɂ���
            circle.SetActive(false);
        }
        else
        {
            SelectAgents(mousePosition, worldPosition);
        }
    }

    /// <summary>
    /// Agent���w�肵���ʒu�Ɉړ�������
    /// </summary>
    /// <param name="destination"></param>
    private void MoveAgents(Vector3 destination)
    {
        foreach (NavMeshAgent2D agent in agents)
        {
            if (agent == null) continue;

            agent.Resume();
            //Debug.Log("Moving agent to: " + destination);
            agent.SetDestination(new Vector2(destination.x, destination.y));
        }
    }

    /// <summary>
    /// �G�[�W�F���g���ړI�n�ɓ����������ǂ������m�F����
    /// </summary>
    private void CheckAgentsArrival()
    {
        bool allArrived = true;
        foreach (NavMeshAgent2D agent in agents)
        {
            if (agent == null) continue;
            if (Vector2.Distance(agent.transform.position, agent.destination) > 2f)
            {
                allArrived = false;
                break;
            }
        }

        if (allArrived)
        {
            Debug.Log("All agents have arrived at the destination");
            isMoving = false;
            isSelected = false;
            agents.Clear();
        }
    }

    /// <summary>
    /// Agent��I������
    /// </summary>
    /// <param name="mousePosition"></param>
    /// <param name="worldPosition"></param>
    private void SelectAgents(Vector3 mousePosition, Vector3 worldPosition)
    {
        Debug.Log("Selecting agents around: " + worldPosition);

        Vector3 point = Vector3.zero;
        RectTransform rc = circle.GetComponent<RectTransform>();
        RectTransformUtility.ScreenPointToWorldPointInRectangle(rc, mousePosition, null, out point);

        circle.GetComponent<RectTransform>().position = point;
        circle.SetActive(true);

        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject player in players)
        {
            float distance = Vector3.Distance(player.transform.position, worldPosition);
            if (distance > SELECTION_RADIUS) continue;

            NavMeshAgent2D agent = player.GetComponent<NavMeshAgent2D>();
            if (agent == null) continue;

            agents.Add(agent);
            agent.Stop();
        }

        if (agents.Count > 0)
        {
            isSelected = true;
        }
        else
        {
            Debug.Log("No agents selected");
            isSelected = false;
            circle.SetActive(false);
        }
    }
}
