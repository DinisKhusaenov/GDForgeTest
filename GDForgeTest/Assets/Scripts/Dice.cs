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

    private Image _image;
    private string _diceRollResult;
    private bool _dieIsThrown = false;

    public event Action OnResultIsChanged;

    public string DiceRollResult => _diceRollResult;

    private void Awake()
    {
        _image = GetComponent<Image>();
    }

    private void OnEnable()
    {
        _playerInput.OnRollTheDiced += TryRollingTheDice;
    }

    private void OnDisable()
    {
        _playerInput.OnRollTheDiced -= TryRollingTheDice;
    }

    private void TryRollingTheDice()
    {
        if (!_dieIsThrown)
            StartCoroutine(RollTheDice());
    }

    private IEnumerator RollTheDice()
    {
        _dieIsThrown = true;
        int randomDiceSide = 0;

        for (int i = 0; i < 20; i++)
        {
            randomDiceSide = Random.Range(0, _sides.Length);
            _image.sprite = _sides[randomDiceSide];
            yield return new WaitForSeconds(0.05f);
        }

        _diceRollResult = _sides[randomDiceSide].name;
        OnResultIsChanged?.Invoke();
        _dieIsThrown = false;
    }
}
