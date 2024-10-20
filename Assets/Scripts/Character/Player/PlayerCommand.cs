using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UniRx;
using UniRx.Triggers;

/// <summary>
/// �L�����N�^�[�̑I���ƃR�}���h�̔��s���Ǘ�����X�N���v�g
/// </summary>
public class PlayerCommand : MonoBehaviour
{
    private GameObject buttonManager;
    private ButtonManagerScript buttonManagerScript;
    private ReactiveCollection<GameObject> selectedCharacters = new ReactiveCollection<GameObject>();
    private Dictionary<GameObject, IDisposable> characterSubscriptions = new Dictionary<GameObject, IDisposable>();
    private Vector3 destination;

    //[SerializeField] private CharacterSelectionIndicator selectionIndicator;
    [SerializeField] private float selectionRadius = 2f;

    private void Start()
    {
        buttonManager = GameObject.Find("ButtonManager");
        buttonManagerScript = buttonManager.GetComponent<ButtonManagerScript>();

        // ���N���b�N�ŃL�����N�^�[�I���܂��͈ړ���̐ݒ�
        this.UpdateAsObservable()
            .Where(_ => Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
            .Where(_ => buttonManagerScript.SelectedButtonType.Value == ButtonType.None)
            .Subscribe(_ => ProcessMouseClick())
            .AddTo(this);

        // �E�N���b�N�ő��̃L�����N�^�[�̑I��������
        this.UpdateAsObservable()
            .Where(_ => Input.GetMouseButtonDown(1))
            .Subscribe(_ => buttonManagerScript.ResetOther())
            .AddTo(this);

        // selectedCharacters�̕ύX���Ď����A���X�g����ɂȂ����Ƃ���Indicator���\���ɂ���
        /*selectedCharacters.ObserveCountChanged()
            .Where(count => count == 0)
            .Subscribe(_ =>
            {
                selectionIndicator.Hide();
            })
            .AddTo(this);*/
    }

    /// <summary>
    /// �}�E�X�N���b�N���̏���
    /// �L�����N�^�[�I���܂��͈ړ���̐ݒ���s�����\�b�h
    /// </summary>
    private void ProcessMouseClick()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = Camera.main.nearClipPlane;
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        worldPosition.z = 0;

        // �I�����ꂽ�L�����N�^�[�����Ȃ��ꍇ�A�I��͈͓��̃L�����N�^�[��I��
        if (selectedCharacters.Count == 0)
        {
            SelectCharactersInRadius(mousePosition, worldPosition);
            return;
        }

        // �I�����ꂽ�L�����N�^�[������ꍇ�A�ړ����ݒ�
        destination = worldPosition;
        foreach (GameObject character in selectedCharacters)
        {
            if (character == null || !character) continue;

            PlayerMovement mover = character.GetComponent<PlayerMovement>();
            if (mover != null)
            {
                mover.SetDestination(destination);
            }

            // �I���������ɃX�v���C�g���\���ɂ���
            var indicator = character.GetComponent<SelectionIndicator>();
            if (indicator != null)
            {
                indicator.Hide();
            }
        }

        selectedCharacters.Clear();
    }

    /// <summary>
    /// �N���b�N�ʒu�t�߂̃L�����N�^�[��I�����Ē�~�����郁�\�b�h
    /// </summary>
    private void SelectCharactersInRadius(Vector3 mousePosition, Vector3 worldPosition)
    {
        //selectionIndicator.Show(mousePosition);

        GameObject[] characters = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject character in characters)
        {
            float distance = Vector3.Distance(character.transform.position, worldPosition);
            if (distance > selectionRadius) continue;

            Status status = character.GetComponent<Status>();
            if (status == null) continue;

            selectedCharacters.Add(character);

            // �I�����ꂽ�L�����N�^�[�̃X�v���C�g��\��
            var indicator = character.GetComponent<SelectionIndicator>();
            if (indicator != null)
            {
                indicator.Show();
            }

            // �I��͈͓��̃L�����N�^�[���̕ω���c�����邽�߂Ɏ��S�C�x���g���w��
            if (!characterSubscriptions.ContainsKey(character))
            {
                var subscription = status.OnDie
                    .Subscribe(_ =>
                    {
                        selectedCharacters.Remove(character);
                        Debug.Log($"{character.name}�����񂾂��߁AselectedCharacters����폜���܂����B");
                        characterSubscriptions.Remove(character);
                    })
                    .AddTo(this);

                characterSubscriptions[character] = subscription;
            }

            //�L�����N�^�[���~
            Rigidbody2D rb = character.GetComponent<Rigidbody2D>();
            if (rb == null) continue;
            rb.velocity = Vector2.zero;

            status.SetState(PlayerState.Selected);
        }
    }
}
