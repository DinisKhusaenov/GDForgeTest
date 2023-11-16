using System;
using System.Collections;
using UnityEngine;

public class Booster : MonoBehaviour
{
    [SerializeField] private Dice _dice;
    [SerializeField] private PlayerInputController _playerInput;

    public static event Action<string, int> OnBoosted;
    public static event Action OnBoostEffectActivated;

    private bool _isBoosted = false;

    private void OnEnable()
    {
        _playerInput.OnBoosted += Boost;
        _playerInput.OnRollTheDiced += ActivateBoostButton;
    }

    private void OnDisable()
    {
        _playerInput.OnBoosted += Boost;
        _playerInput.OnRollTheDiced -= ActivateBoostButton;
    }

    private void Boost()
    {
        if (BoostBtnCanBePressed())
        {
            int count = int.Parse(_dice.DiceRollResult) + 1;
            OnBoosted?.Invoke(count.ToString(), count);
            OnBoostEffectActivated?.Invoke();
            _isBoosted = true;
        }
    }

    private bool BoostBtnCanBePressed()
    {
        return (!_isBoosted && !_dice.DieIsThrown && _dice.DiceRollResult != "20" && _dice.DiceRollResult != null);
    }

    private void ActivateBoostButton()
    {
        _isBoosted = false;
    }
}
