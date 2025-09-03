using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class UiView : MonoBehaviour
{

    [Header("UI VIEW elements")]
    [SerializeField]
    private bool UnpauseOnClose = false;

    [SerializeField] private bool CloseOnNewView = true;
    [SerializeField] private Button BackButon;
    [SerializeField] public GameObject SelectedOnView;

    private UiView _parentView;
    private InputAction _cancelAction;
    private Action _onBackButtonAction;

    public virtual void Awake()
    {
        BackButon.onClick.AddListener(() => DisableView_OnClick(this));
        _cancelAction = InputSystem.actions.FindAction("Cancel");
    }

    public virtual void OnEnable()
    {
        if(_cancelAction != null) _cancelAction.performed += OnCancel;
    }

    public virtual void OnDisable()
    {
        if(_cancelAction != null) _cancelAction.performed -= OnCancel;
    }

    private void OnCancel(InputAction.CallbackContext ctx)
    {
        if (_onBackButtonAction != null) _onBackButtonAction();
        this.DisableView();
    }

    public void ActiveView_OnClick(UiView viewToActive)
    {
        viewToActive.SetParentView(this);
        viewToActive.ActiveView();
        this.ActiveView(!CloseOnNewView);
    }

    private void DisableView_OnClick(UiView viewToDisable)
    {
        viewToDisable.DisableView();
    }

    public void DestroyView_OnClick(UiView viewToDisable)
    {
        viewToDisable.DestroyView();
    }

    public void SetParentView(UiView parentView)
    {
        _parentView = parentView;
    }

    public void ActiveView(bool active)
    {
        this.gameObject.SetActive(active);
    }

    public void ActiveView(Action onBackButtonAction = null)
    {
        if (onBackButtonAction != null)
        {
            _onBackButtonAction = onBackButtonAction;
            BackButon.onClick.AddListener(() => onBackButtonAction());
        }

        if (!gameObject.activeSelf) this.ActiveView(true);

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(SelectedOnView);
    }

    public void DisableView()
    {
        EventSystem.current.SetSelectedGameObject(null);

        if (_parentView != null)
        {
            _parentView.ActiveView();
        }

        if (UnpauseOnClose) GameControlller.Instance.UnPause();

        this.ActiveView(false);
    }

    public void DestroyView()
    {
        EventSystem.current.SetSelectedGameObject(null);

        if (_parentView != null)
        {
            _parentView.ActiveView();
        }

        Destroy(this.gameObject);
    }

    public void DisableBackButton()
    {
        BackButon.gameObject.SetActive(false);
    }

    public Button GetBackButton()
    {
        return BackButon;
    }
}