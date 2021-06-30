using DG.Tweening;
using UnityEngine;

public class RightAnswer : MonoBehaviour
{
    private void OnMouseDown()
    {
        // запускаем партиклы
        var component = GetComponent<ParticleSystem>();
        var emissionModule = component.emission;
        emissionModule.enabled = true;
        component.Play();
        transform.localScale = Vector3.zero;
        transform.DOScale(new Vector3(0.5f, 0.5f, 1f), 0.5f);
        Invoke(nameof(NewLevel), 1f);
    }

    public void NewLevel()
    {
        if (Camera.main is null) return;
        var generateScript = Camera.main.GetComponent<Generate>();
        generateScript.AllGenerate();
    }
}
