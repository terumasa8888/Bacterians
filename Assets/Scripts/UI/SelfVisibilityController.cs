using UnityEngine;

public class SelfVisibilityController : MonoBehaviour
{
    [SerializeField] private StageClearData stageClearData;

    private void Start()
    {
        UpdateVisibility();
    }

    private void UpdateVisibility()
    {
        if (stageClearData != null)
        {
            gameObject.SetActive(stageClearData.AllCleared);
        }
    }
}
