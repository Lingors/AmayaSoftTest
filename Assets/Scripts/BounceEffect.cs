using DG.Tweening;
using UnityEngine;

public class BounceEffect : MonoBehaviour
{
    private void Start()
    {
        transform.DOScale(new Vector3(0.5f, 0.5f, 1f), 0.5f);
    }
}