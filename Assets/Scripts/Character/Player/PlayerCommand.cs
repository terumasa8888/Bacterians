using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UniRx;
using UniRx.Triggers;

/// <summary>
/// キャラクターの選択とコマンドの発行を管理するスクリプト
/// </summary>
public class PlayerCommand : MonoBehaviour
{
    private GameObject buttonManager;
    private ButtonManagerScript buttonManagerScript;
    private List<GameObject> selectedCharacters = new List<GameObject>();
    private Vector3 destination;

    [SerializeField] private CharacterSelectionIndicator selectionIndicator;
    [SerializeField] private float selectionRadius = 2f;

    private void Start()
    {
        buttonManager = GameObject.Find("ButtonManager");
        buttonManagerScript = buttonManager.GetComponent<ButtonManagerScript>();

        this.UpdateAsObservable()
            .Where(_ => Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
            .Where(_ => buttonManagerScript.SelectedButtonType.Value == ButtonType.None)
            .Subscribe(_ => ProcessMouseClick())
            .AddTo(this);

        this.UpdateAsObservable()
            .Where(_ => Input.GetMouseButtonDown(1))
            .Subscribe(_ => buttonManagerScript.ResetOther())
            .AddTo(this);
    }

    private void ProcessMouseClick()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = Camera.main.nearClipPlane;
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        worldPosition.z = 0;

        if (selectedCharacters.Count == 0)
        {
            SelectCharactersInRadius(mousePosition, worldPosition);
            return;
        }

        destination = worldPosition;
        foreach (GameObject character in selectedCharacters)
        {
            PlayerMovement mover = character.GetComponent<PlayerMovement>();
            if (mover != null)
            {
                mover.SetDestination(destination);
            }
        }
        selectedCharacters.Clear();
        selectionIndicator.Hide();
    }

    private void SelectCharactersInRadius(Vector3 mousePosition, Vector3 worldPosition)
    {
        selectionIndicator.Show(mousePosition);

        GameObject[] characters = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject character in characters)
        {
            float distance = Vector3.Distance(character.transform.position, worldPosition);
            if (distance > selectionRadius) continue;

            Status status = character.GetComponent<Status>();
            if (status == null) continue;

            selectedCharacters.Add(character);

            Rigidbody2D rb = character.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.velocity = Vector2.zero;
            }

            status.SetState(PlayerState.Selected);
        }

        if (selectedCharacters.Count == 0)
        {
            selectionIndicator.Hide();
        }
    }
}
