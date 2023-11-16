using DG.Tweening;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Dice))]
public class DiceAnimation : MonoBehaviour
{
    [SerializeField] private float _size;
    [SerializeField] private float _durationScale;
    [SerializeField] private Transform[] _movePoints;
    [SerializeField] private PlayerInputController _playerInput;
    [SerializeField] private int _numberOfSpins;
    [SerializeField] private GameObject _spinEffect;
    [SerializeField] private GameObject _boostEffect;
    [SerializeField] private GameObject _clickImage;

    private Dice _dice;

    private void Awake()
    {
        _dice = GetComponent<Dice>();
    }

    private void OnEnable()
    {
        _playerInput.OnRollTheDiced += DiceMoveAnimation;
        Booster.OnBoostEffectActivated += ActivateBoostEffect;
    }

    private void OnDisable()
    {
        _playerInput.OnRollTheDiced -= DiceMoveAnimation;
        Booster.OnBoostEffectActivated -= ActivateBoostEffect;
    }

    private void Start()
    {
        _spinEffect.SetActive(false);
        _boostEffect.SetActive(false);
        _clickImage.SetActive(true);

        ScaleAnimation();
    }

    private void ScaleAnimation()
    {
        transform.DOScale(_size, _durationScale).SetLoops(2, LoopType.Yoyo);
    }

    private void DiceMoveAnimation()
    {
        if (!_dice.DieIsThrown)
        {
            transform.DOPath(ConvertTransformToVector3(_movePoints), _dice.ThrowDuration, PathType.CatmullRom);
            transform.DORotate(new Vector3(0, 0, 360 * _numberOfSpins), _dice.ThrowDuration, RotateMode.FastBeyond360);

            StartCoroutine(ActivateSpinEffect());
        }
    }

    private IEnumerator ActivateSpinEffect()
    {
        _spinEffect.SetActive(true);
        _clickImage.SetActive(false);
        DeactivateBoostEffect();

        yield return new WaitForSeconds(_dice.ThrowDuration);
        _spinEffect.SetActive(false);
        _clickImage.SetActive(true);

        yield return new WaitForSeconds(0.1f);
        ScaleAnimation();
    }

    private void ActivateBoostEffect()
    {
        _boostEffect.SetActive(true);
    }

    private void DeactivateBoostEffect()
    {
        _boostEffect.SetActive(false);
    }

    private static Vector3[] ConvertTransformToVector3(Transform[] points)
    {
        Vector3[] path = new Vector3[points.Length];

        for (int i = 0; i < points.Length; i++)
        {
            path[i] = points[i].position;
        }

        return path;
    }
}
