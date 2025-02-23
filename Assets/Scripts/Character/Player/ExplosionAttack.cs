using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

/// <summary>
/// ぶつかった敵とともに爆発
/// </summary>
public class ExplosionAttack : IAttackBehaviour
{
    private GameObject explosionEffect;
    private Status attackerStatus;
    private const string explosionEffectAddress = "Assets/JMO Assets-TABLET-3K4KBDLP/Cartoon FX/CFX Prefabs/Explosions/CFX_Explosion_B_Smoke+Text.prefab"; // Addressablesのアドレス

    public ExplosionAttack(Status attackerStatus)
    {
        this.attackerStatus = attackerStatus;
        LoadExplosionEffect();
    }

    private void LoadExplosionEffect()
    {
        Addressables.LoadAssetAsync<GameObject>(explosionEffectAddress).Completed += OnExplosionEffectLoaded;
    }

    private void OnExplosionEffectLoaded(AsyncOperationHandle<GameObject> obj)
    {
        if (obj.Status == AsyncOperationStatus.Succeeded)
        {
            explosionEffect = obj.Result;
        }
        else
        {
            Debug.LogError("Failed to load explosion effect.");
        }
    }

    public void Attack(Status targetStatus)
    {
        if (targetStatus != null && explosionEffect != null)
        {
            GameObject.Instantiate(explosionEffect, targetStatus.transform.position, Quaternion.identity);
            GameObject.Destroy(targetStatus.gameObject); // 敵を破壊
            GameObject.Destroy(attackerStatus.gameObject); // 自分自身も破壊
        }
    }
}
