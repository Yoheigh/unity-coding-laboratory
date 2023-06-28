using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class UITest : MonoBehaviour
{
    public static Action<float, float> UI_actionOnLoad;

    public Text text;
    public Image image;

    public void Init()
    {
        UI_actionOnLoad += UpdateAssetLoad;
    }

    public void UpdateAssetLoad(float count, float total)
    {
        if (count == total)
            text.text = $"������ �ҷ����� ���Դϴ� ... {count}/{total} �Ϸ�!";
        else
            text.text = $"������ �ҷ����� ���Դϴ� ... {count}/{total}";

        image.fillAmount = (count / total);
    }

    public void UpdateLoadingUI(float count, float total)
    {
        UI_actionOnLoad?.Invoke(count, total);
    }
}
