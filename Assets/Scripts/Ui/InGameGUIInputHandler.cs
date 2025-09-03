

using UnityEngine;
using UnityEngine.InputSystem;

public class InGameGUIInputHandler : MonoBehaviour
{
	[SerializeField] private UiView PauseView;
	[SerializeField] private UiView InventoryView;

	private InputAction _menuAction;
	private InputAction _backpackAction;

	public void Awake()
	{
		_menuAction = InputSystem.actions.FindAction("OpenMenu");
		_backpackAction = InputSystem.actions.FindAction("OpenBackpack");
	}


	public virtual void OnEnable()
	{
		_menuAction.Enable();
		_backpackAction.Enable();

		_menuAction.performed += OpenMenu;
		_backpackAction.performed += OpenBackpack;
	}

	public virtual void OnDisable()
	{
		_menuAction.performed -= OpenMenu;
		_backpackAction.performed -= OpenBackpack;

		_menuAction.Disable();
		_backpackAction.Disable();
	}
	
	public void OpenPauseView()
	{
		GUIController.Instance.InGameGUIButton_OnClick(PauseView);
	}

	public void OpenBackpackView()
	{
		GUIController.Instance.InGameGUIButton_OnClick(InventoryView);
	}


	private void OpenMenu(InputAction.CallbackContext ctx)
	{
		OpenPauseView();
	}

	private void OpenBackpack(InputAction.CallbackContext ctx)
	{
		OpenBackpackView();
	}
}
