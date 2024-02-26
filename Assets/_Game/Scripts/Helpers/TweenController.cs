using System;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

public class TweenController : MonoBehaviour
{
    private Sequence _sequence;
    public SequenceType type;
    public float strength;
    public float tweenInterval;

    private void Start()
    {
        _sequence = DOTween.Sequence();
        DecideSequence();
        _sequence.AppendInterval(Random.Range(tweenInterval - .1f, tweenInterval + .1f)).SetLoops(int.MaxValue);
    }

    private void DecideSequence()
    {
        switch (type)
        {
            case SequenceType.Rotating:
                AddRotatingSequence();
                break;
            case SequenceType.Scaling:
                AddScalingSequence();
                break;
            case SequenceType.Bubble:
                AddBubbleSequence();
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }


    private void AddScalingSequence()
    {
        _sequence.Append(transform.DOScale(Vector3.one * strength / 4, .2f))
            .Append(transform.DOScale(Vector3.one, .2f));
    }

    private void AddRotatingSequence()
    {
        _sequence.Append(transform.DORotate(new Vector3(0, 0, strength), .2f))
            .Append(transform.DORotate(new Vector3(0, 0, -strength), .2f))
            .Append(transform.DORotate(new Vector3(0, 0, 0), .2f));
    }
		
    private void AddBubbleSequence()
    {
        _sequence.Append(transform.DOScale((strength / 4) * Vector3.one, .5f).SetEase(Ease.OutElastic))
            .Append(transform.DOScale(Vector3.one, .2f).SetEase(Ease.OutExpo));
    }

    public void PauseTween()
    {
        _sequence.Rewind();
    }

    public void ResumeTween()
    {
        _sequence.Restart();
    }
}
	
public enum SequenceType {
    Rotating,
    Scaling,
    Bubble
}