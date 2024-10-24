using UnityEngine;

[CreateAssetMenu(fileName = "StageClearData", menuName = "ScriptableObjects/StageClearData", order = 1)]
public class StageClearData : ScriptableObject
{
    [SerializeField] private bool stage1Cleared;
    [SerializeField] private bool stage2Cleared;
    [SerializeField] private bool stage3Cleared;

    private bool allCleared;

    public bool Stage1Cleared
    {
        get { return stage1Cleared; }
        set { stage1Cleared = value; }
    }

    public bool Stage2Cleared
    {
        get { return stage2Cleared; }
        set { stage2Cleared = value; }
    }

    public bool Stage3Cleared
    {
        get { return stage3Cleared; }
        set { stage3Cleared = value; }
    }

    public bool AllCleared
    {
        get { return allCleared; }
        set
        {
            if (!allCleared && value)
            {
                allCleared = value;
            }
        }
    }
}
