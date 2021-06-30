using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class FadeEffect : MonoBehaviour
{
    public Text text;

    private void Start()
    {
        text.DOFade(1, 2);
    }
}