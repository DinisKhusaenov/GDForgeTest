using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInputController : MonoBehaviour
{
    [SerializeField] private Button _quitButton;
    [SerializeField] private Button _boostBtn;

    public event Action OnRollTheDiced;
    public event Action OnBoosted;

    private void Start()
    {
        _quitButton?.onClick.AddListener(OnRollTheDiceBtnClicked);
        _boostBtn?.onClick.AddListener(OnBoostBtnClicked);
    }

    private void OnRollTheDiceBtnClicked()
    {
        OnRollTheDiced?.Invoke();
    }

    private void OnBoostBtnClicked()
    {
        OnBoosted?.Invoke();
    }
}
