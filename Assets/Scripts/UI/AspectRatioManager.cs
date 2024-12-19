using UnityEngine;
using System.Collections;
using UnityEngine.UI;


/// <summary>
/// �J�����ƃL�����o�X�̃A�X�y�N�g����Ǘ�����N���X�B
/// �w�肳�ꂽ�A�X�y�N�g��Ɋ�Â��āA�J�����̃r���[�|�[�g�ƃL�����o�X�̃X�P�[�����O�𒲐�
/// </summary>
public class AspectRatioManager : MonoBehaviour {

    [SerializeField] private float x_aspect = 1920f;
    [SerializeField] private float y_aspect = 1080f;
    [SerializeField] private CanvasScaler[] canvasScaler = new CanvasScaler[1];

	void Awake() {
		//Camera�̃A�X�y�N�g���ݒ肷��
		Camera camera = GetComponent<Camera>();
		Rect rect = calcAspect(x_aspect, y_aspect);
		camera.rect = rect;

		//Canvas�̃A�X�y�N�g���ݒ肷��
		for (int i = 0; i < canvasScaler.Length; i++) {
			canvasScaler[i].matchWidthOrHeight = CheckScreenRatio(i);
		}

        // �E�B���h�E�̃A�X�y�N�g����Œ肷��
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
    /// ��ʂ̏c������`�F�b�N���āA�c���䂪�傫������Ԃ�
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
    /// �E�B���h�E�̃A�X�y�N�g����Œ肷��
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