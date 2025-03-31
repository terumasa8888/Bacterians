using UnityEngine;

/// <summary>
/// ボスキャラクターのクラス
/// </summary>
public class Boss : CharacterBase
{
    protected override void Awake()
    {
        base.Awake();
        // ボスの特別な初期化処理があればここに記述
    }

    private void Update()
    {
        // ボスの特別な攻撃ロジックをここに記述
        PerformSpecialAttack();
    }

    private void PerformSpecialAttack()
    {
        // ボスの特別な攻撃ロジックを実装
        // 例: 一定時間ごとに弾を発射する
    }
}



