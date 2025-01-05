using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class FadeUI : MonoBehaviour
{

    void Start()
    { 
        {
            var sequence = DOTween.Sequence()
            .Append(transform.GetComponent<Image>().DOFade(1, 1).SetRelative())
           .Join(transform.DOScale(new Vector3(1.5f, 1.5f, 1.5f), 1));
            sequence.SetLoops(-1, LoopType.Yoyo);
        }

    }

}
