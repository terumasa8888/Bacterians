using System;
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
    [SerializeField] private GameObject uiManager;
    private ButtonManagerScript buttonManagerScript;
    private ReactiveCollection<GameObject> selectedCharacters = new ReactiveCollection<GameObject>();
    private Dictionary<GameObject, IDisposable> characterSubscriptions = new Dictionary<GameObject, IDisposable>();
    private Vector3 destination;

    [SerializeField] private float selectionRadius = 2f;

    private void Start()
    {
        buttonManagerScript = uiManager.GetComponent<ButtonManagerScript>();

        // 左クリックでキャラクター選択または移動先の設定
        this.UpdateAsObservable()
            .Where(_ => Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
            .Where(_ => buttonManagerScript.SelectedButtonType.Value == ButtonType.None)
            .Subscribe(_ => ProcessMouseClick())
            .AddTo(this);

        // 右クリックで他のキャラクターの選択を解除
        this.UpdateAsObservable()
            .Where(_ => Input.GetMouseButtonDown(1))
            .Subscribe(_ => buttonManagerScript.ResetOther())
            .AddTo(this);
    }

    /// <summary>
    /// マウスクリック時の処理
    /// キャラクター選択または移動先の設定を行うメソッド
    /// </summary>
    private void ProcessMouseClick()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = Camera.main.nearClipPlane;
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        worldPosition.z = 0;

        // 選択されたキャラクターがいない場合、選択範囲内のキャラクターを選択
        if (selectedCharacters.Count == 0)
        {
            SelectCharactersInRadius(mousePosition, worldPosition);
            return;
        }

        // 選択されたキャラクターがいる場合、移動先を設定
        destination = worldPosition;
        foreach (GameObject character in selectedCharacters)
        {
            if (character == null) continue;

            //PlayerMovement mover = character.GetComponent<PlayerMovement>();
            PlayerBase playerBase = character.GetComponent<PlayerBase>();

            if (playerBase != null)
            {
                // 移動先を設定
                playerBase.SetDestination(destination);
            }

            // 選択解除時にスプライトを非表示にする
            var indicator = character.GetComponent<SelectionIndicator>();
            if (indicator != null)
            {
                indicator.Hide();
            }
        }

        selectedCharacters.Clear();
    }

    /// <summary>
    /// クリック位置付近のキャラクターを選択して停止させるメソッド
    /// </summary>
    private void SelectCharactersInRadius(Vector3 mousePosition, Vector3 worldPosition)
    {
        //selectionIndicator.Show(mousePosition);

        // ここ、タグ使わなくても、サークルのコライダーを使って判定できるのでは？
        GameObject[] characters = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject character in characters)
        {
            float distance = Vector3.Distance(character.transform.position, worldPosition);
            if (distance > selectionRadius) continue;

            PlayerBase characterBase = character.GetComponent<PlayerBase>();
            IStatus status = characterBase.Status;

            if (status == null) continue;

            selectedCharacters.Add(character);

            // 選択されたキャラクターのインジケーターを表示
            var indicator = character.GetComponent<SelectionIndicator>();
            if (indicator != null)
            {
                indicator.Show();
            }

            // 選択範囲内のキャラクター数の変化を把握するために死亡イベントを購読
            if (!characterSubscriptions.ContainsKey(character))
            {
                var subscription = status.OnDie
                    .Subscribe(_ =>
                    {
                        selectedCharacters.Remove(character);
                        Debug.Log($"{character.name}が死んだため、selectedCharactersから削除しました。");
                        characterSubscriptions.Remove(character);
                    })
                    .AddTo(this);

                characterSubscriptions[character] = subscription;
            }

            //キャラクターを停止
            Rigidbody2D rigidBody = character.GetComponent<Rigidbody2D>();
            if (rigidBody == null) continue;
            rigidBody.velocity = Vector2.zero;

            status.SetState(CharacterState.Selected);
        }
    }
}
