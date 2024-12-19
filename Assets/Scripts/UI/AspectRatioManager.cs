using UnityEngine;
using System.Collections;
using UnityEngine.UI;


/// <summary>
/// カメラとキャンバスのアスペクト比を管理するクラス。
/// 指定されたアスペクト比に基づいて、カメラのビューポートとキャンバスのスケーリングを調整
/// </summary>
public class AspectRatioManager : MonoBehaviour {

    [SerializeField] private float x_aspect = 1920f;
    [SerializeField] private float y_aspect = 1080f;
    [SerializeField] private CanvasScaler[] canvasScaler = new CanvasScaler[1];

	void Awake() {
		//Cameraのアスペクト比を設定する
		Camera camera = GetComponent<Camera>();
		Rect rect = calcAspect(x_aspect, y_aspect);
		camera.rect = rect;

		//Canvasのアスペクト比を設定する
		for (int i = 0; i < canvasScaler.Length; i++) {
			canvasScaler[i].matchWidthOrHeight = CheckScreenRatio(i);
		}

        // ウィンドウのアスペクト比を固定する
        SetFixedAspectRatio();
    }

	private Rect calcAspect(float width, float height) {
		float target_aspect = width / height;
		float window_aspect = (float)Screen.width / (float)Screen.height;
		float scale_height = window_aspect / target_aspect;
		Rect rect = new Rect(0.0f, 0.0f, 1.0f, 1.0f);

		if (1.0f > scale_height) {
			rect.x = 0;
			rect.y = (1.0f - scale_height) / 2.0f;
			rect.width = 1.0f;
			rect.height = scale_height;
		}
		else {
			float scale_width = 1.0f / scale_height;
			rect.x = (1.0f - scale_width) / 2.0f;
			rect.y = 0.0f;
			rect.width = scale_width;
			rect.height = 1.0f;
		}
		return rect;
	}


    /// <summary>
    /// 画面の縦横比をチェックして、縦横比が大きい方を返す
    /// </summary>
    private int CheckScreenRatio(int i) {
		if (Screen.width * canvasScaler[i].referenceResolution.y / canvasScaler[i].referenceResolution.x < Screen.height) {
			return 0;
		}
		else {
			return 1;
		}
	}

    /// <summary>
    /// ウィンドウのアスペクト比を固定する
    /// </summary>
    private void SetFixedAspectRatio()
    {
        float target_aspect = x_aspect / y_aspect;
        int width = Screen.width;
        int height = Mathf.RoundToInt(width / target_aspect);
        Screen.SetResolution(width, height, false);
    }

    private void OnRectTransformDimensionsChange()
    {
        Debug.Log("OnRectTransformDimensionsChange called");
        SetFixedAspectRatio();
    }
}