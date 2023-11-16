using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Image))]
public class Dice : MonoBehaviour
{
    [SerializeField] private Sprite[] _sides;
    [SerializeField] private PlayerInputController _playerInput;
    [SerializeField] private float _throwDuration;
    [SerializeField] private float _scrollFrequency = 0.05f;

    private Image _image;
    private string _diceRollResult;
    private bool _dieIsThrown = false;

    public event Action OnResultIsChanged;
    public event Action OnDiceThrown;

    public string DiceRollResult => _diceRollResult;
    public float ThrowDuration => _throwDuration;

    public bool DieIsThrown => _dieIsThrown;

    private void Awake()
    {
        _image = GetComponent<Image>();
    }

    private void OnEnable()
    {
        _playerInput.OnRollTheDiced += TryRollingTheDice;
        Booster.OnBoosted += ChangeResult;
    }

    private void OnDisable()
    {
        _playerInput.OnRollTheDiced -= TryRollingTheDice;
        Booster.OnBoosted -= ChangeResult;
    }

    private void TryRollingTheDice()
    {
        if (!_dieIsThrown)
            StartCoroutine(RollTheDice());
    }

    private IEnumerator RollTheDice()
    {
        OnDiceThrown?.Invoke();
        _dieIsThrown = true;
        int randomDiceSide = 0;

        for (int i = 0; i < _throwDuration/_scrollFrequency; i++)
        {
            randomDiceSide = Random.Range(0, _sides.Length);
            _image.sprite = _sides[randomDiceSide];
            yield return new WaitForSeconds(_scrollFrequency);
        }

        _diceRollResult = _sides[randomDiceSide].name;
        OnResultIsChanged?.Invoke();
        _dieIsThrown = false;
    }

    private void ChangeResult(string newResult, int diceSide)
    {
        _diceRollResult = newResult;
        _image.sprite = _sides[diceSide-1];
        OnResultIsChanged?.Invoke();
    }
}
