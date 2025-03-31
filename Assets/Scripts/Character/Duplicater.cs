using System.Collections;
using UnityEngine;
using UniRx;

/// <summary>
/// 増殖する機能を提供するクラス
/// </summary>
public class Duplicater : IDuplicater
{
    private IStatus status;
    private CharacterBase character;
    private float timer = 0f;

    public Duplicater(IStatus status, CharacterBase character)
    {
        this.status = status;
        this.character = character;
    }

    public IEnumerator StartDuplicate()
    {
        while (true)
        {
            yield return null;

            if (status.CurrentState == CharacterState.Idle)
            {
                timer += Time.deltaTime;

                if (timer >= status.DuplicateInterval && status.DuplicatableNumber.Value > 0)
                {
                    Duplicate();
                    status.ReduceDuplicatableNumber();
                    timer = 0f;
                }
            }
            else
            {
                timer = 0f;
            }
        }
    }

    private void Duplicate()
    {
        GameObject clone = Object.Instantiate(character.gameObject);
        IStatus cloneStatus = clone.GetComponent<CharacterBase>().Status;
        if (cloneStatus != null)
        {
            cloneStatus.SetDuplicatableNumber(status.DuplicatableNumber.Value - 1);
        }
        else
        {
            Debug.LogError("Clone does not have an IStatus component.");
        }
    }
}

