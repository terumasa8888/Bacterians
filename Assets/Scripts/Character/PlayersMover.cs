using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using UniRx;
using UniRx.Triggers;


/// <summary>
/// Player(Stand�̉��ɂ�����)�̓���𐧌䂷��X�N���v�g
/// </summary>
public class PlayersMover : MonoBehaviour
{
    private GameObject buttonManager;
    private ButtonManagerScript buttonManagerScript;
    private List<NavMeshAgent2D> agents = new List<NavMeshAgent2D>();
    private bool isSelected = false;

    [SerializeField] private GameObject circle;
    [SerializeField] private float SELECTION_RADIUS = 2f;

    private void Start()
    {
        buttonManager = GameObject.Find("ButtonManager");
        buttonManagerScript = buttonManager.GetComponent<ButtonManagerScript>();

        // �}�E�X�N���b�N�C�x���g���Ď�
        this.UpdateAsObservable()
            .Where(_ => Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
            .Where(_ => buttonManagerScript.SelectedButtonType.Value == ButtonType.None)
            .Subscribe(_ => HandleMouseClick())
            .AddTo(this);
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
            MoveAgents(worldPosition);
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
            if (agent == null) return;

            agent.Resume();
            agent.SetDestination(destination);
        }

        agents.Clear();
        isSelected = false;
        circle.SetActive(false);
    }

    /// <summary>
    /// Agent��I������
    /// </summary>
    /// <param name="mousePosition"></param>
    /// <param name="worldPosition"></param>
    private void SelectAgents(Vector3 mousePosition, Vector3 worldPosition)
    {
        isSelected = true;

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

        if (agents.Count != 0) return;

        isSelected = false;
        circle.SetActive(false);
    }
}
