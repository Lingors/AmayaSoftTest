using DG.Tweening;
using UnityEngine;

public class WrongAnswer : MonoBehaviour
{
    private void OnMouseDown()
    {
        var trans = gameObject.transform;
        trans.DOShakeRotation(0.5f);
        trans.rotation = Quaternion.identity;
    }
}