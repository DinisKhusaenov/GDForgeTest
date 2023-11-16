using TMPro;
using UnityEngine;

public class DiceRollResult : MonoBehaviour
{
    [SerializeField] private TMP_Text _resultText;
    [SerializeField] private Dice _dice;

    private string _result;

    private void OnEnable()
    {
        _dice.OnResultIsChanged += TryChangeResult;
    }

    private void OnDisable()
    {
        _dice.OnResultIsChanged -= TryChangeResult;
    }

    private void Start()
    {
        _resultText.text = "";
    }

    private void TryChangeResult()
    {
        if (_dice.DiceRollResult != null)
        {
            _result = _dice.DiceRollResult;
            ChangeResultText();
        }
    }

    private void ChangeResultText()
    {
        _resultText.text = "Result: " + _result;
    }
}
