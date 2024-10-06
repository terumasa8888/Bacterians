using UnityEngine;

public class DisableWhenStart : MonoBehaviour {
    void Start() {
        gameObject.SetActive(false);
    }
}