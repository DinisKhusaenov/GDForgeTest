using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInputController : MonoBehaviour
{
    [SerializeField] private Button _quitButton;

    public event Action OnRollTheDiced;

    private void Start()
    {
        _quitButton.onClick.AddListener(OnRollTheDiceBtnClicked);
    }

    private void OnRollTheDiceBtnClicked()
    {
        OnRollTheDiced?.Invoke();
    }
}
