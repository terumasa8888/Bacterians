/*using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "StageCharacterSettings", menuName = "ScriptableObjects/StageCharacterSettings", order = 2)]
public class StageCharacterSettings : ScriptableObject {
    public List<CharacterSpawnInfo> characterSpawnInfos; // CharacterSpawnInfo のリスト

    private Dictionary<ButtonType, CharacterSpawnInfo> characterSpawnInfoDict;

    public void Initialize() {
        characterSpawnInfoDict = new Dictionary<ButtonType, CharacterSpawnInfo>();
        foreach (var spawnInfo in characterSpawnInfos) {
            characterSpawnInfoDict[spawnInfo.characterData.type] = spawnInfo;
        }
    }

    public bool CanCreateCharacter(CharacterData characterData) {
        return characterSpawnInfoDict.TryGetValue(characterData.type, out var spawnInfo) && spawnInfo.HasRemainingCreates();
    }

    public void ReduceCreateTimes(CharacterData characterData) {
        if (characterSpawnInfoDict.TryGetValue(characterData.type, out var spawnInfo)) {
            spawnInfo.ReduceCreateTimes();
        }
    }

    public int GetRemainingCreates(CharacterData characterData) {
        if (characterSpawnInfoDict.TryGetValue(characterData.type, out var spawnInfo)) {
            return spawnInfo.GetRemainingCreates();
        }
        return 0;
    }
}
*/