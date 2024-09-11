using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIScaleScript : MonoBehaviour {
    public float period;
    public float time, changeSpeed, destroyTime;
    public bool enlarge;

    void Start() {
        //period = Mathf.PI;
        enlarge = true;
    }

    void Update() {
        changeSpeed = Time.deltaTime * 0.1f;
        destroyTime += Time.deltaTime;

        if (time < 0) {
            enlarge = true;
        }
        if (time > period) {
            enlarge = false;
        }

        if (enlarge == true) {
            time += Time.deltaTime;
            transform.localScale += new Vector3(changeSpeed, changeSpeed, changeSpeed);
        }
        else {
            time -= Time.deltaTime;
            transform.localScale -= new Vector3(changeSpeed, changeSpeed, changeSpeed);
        }

        if(destroyTime > 15.0f) {
            Destroy(this.gameObject);
        }
    }
}
