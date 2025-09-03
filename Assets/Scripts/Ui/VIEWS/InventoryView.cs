using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryView : UiView
{
    [Header("Inventory Elements")]
    [SerializeField]
    private SoulInformation SoulItemPlaceHolder;

    [SerializeField] private Text Description;
    [SerializeField] private Text Name;
    [SerializeField] private Image Avatar;
    [SerializeField] private Button UseButton;
    [SerializeField] private Button DestroyButton;

    private RectTransform _contentParent;
    private GameObject _currentSelectedGameObject;
    private SoulInformation _currentSoulInformation;
    private List<SoulInformation> _availableSouls;

    public override void Awake()
    {
        base.Awake();
        _contentParent = (RectTransform)SoulItemPlaceHolder.transform.parent;
        InitializeInventoryItems();
    }

    private void InitializeInventoryItems()
    {
        _availableSouls = new();
        for (int i = 0, j = SoulController.Instance.Souls.Count; i < j; i++)
        {
            SoulInformation newSoul = Instantiate(SoulItemPlaceHolder.gameObject, _contentParent).GetComponent<SoulInformation>();
            newSoul.SetSoulItem(SoulController.Instance.Souls[i], () => SoulItem_OnClick(newSoul));
            _availableSouls.Add(newSoul);
        }

        SoulItemPlaceHolder.gameObject.SetActive(false);

        SelectElement(0);
        ClearSoulInformation();
    }

    public override void OnEnable()
    {
        base.OnEnable();
    }

    private void ClearSoulInformation()
    {
        Description.text = "";
        Name.text = "";
        Avatar.sprite = null;
        SetupUseButton(false);
        SetupDestroyButton(false);
        _currentSelectedGameObject = null;
        _currentSoulInformation = null;
    }

    public void SoulItem_OnClick(SoulInformation soulInformation)
    {
        _currentSoulInformation = soulInformation;
        _currentSelectedGameObject = soulInformation.gameObject;
        SetupSoulInformation(soulInformation.soulItem);

        if(UseButton.IsActive() && UseButton.interactable)
        {
            EventSystem.current.SetSelectedGameObject(UseButton.gameObject);
        }
        else if(DestroyButton.IsActive() && DestroyButton.interactable)
        {
            EventSystem.current.SetSelectedGameObject(DestroyButton.gameObject);
        }
    }

    private void SetupSoulInformation(SoulItem soulItem)
    {
        Description.text = soulItem.Description;
        Name.text = soulItem.Name;
        Avatar.sprite = soulItem.Avatar;
        SetupUseButton(soulItem.CanBeUsed);
        SetupDestroyButton(soulItem.CanBeDestroyed);
    }

    private void SelectElement(int index)
    {
        if (index >= _availableSouls.Count)
            return;
        _currentSelectedGameObject = _availableSouls[index].gameObject;
        _currentSoulInformation = _availableSouls[index];
        SelectedOnView = _currentSelectedGameObject;
        EventSystem.current.SetSelectedGameObject(_availableSouls[index].gameObject);
    }

    private void CantUseCurrentSoul()
    {
        PopUpInformation popUpInfo = new PopUpInformation { DisableOnConfirm = true, UseOneButton = true, Header = "CAN'T USE", Message = "THIS SOUL CANNOT BE USED IN THIS LOCALIZATION" };
        GUIController.Instance.ShowPopUpMessage(popUpInfo);
    }

    private void UseCurrentSoul(bool canUse)
    {
        if (!canUse)
        {
            CantUseCurrentSoul();
        }
        else
        {
            //USE SOUL
            Debug.Log(GameEvents.SoulUsed);
            Debug.Log(_currentSoulInformation);
            Debug.Log(_currentSoulInformation.soulItem);
            GameEvents.SoulUsed.Invoke(_currentSoulInformation.soulItem);
            RemoveSoul();
        }
    }

    private void DestroyCurrentSoul()
    {
        RemoveSoul();
    }

    private void RemoveSoul()
    {
        _availableSouls.Remove(_currentSoulInformation);
        Destroy(_currentSelectedGameObject);
        ClearSoulInformation();
        SelectElement(0);
    }


    private void SetupUseButton(bool active)
    {
        UseButton.onClick.RemoveAllListeners();
        if (active)
        {
            bool isInCorrectLocalization = GameControlller.Instance.IsCurrentLocalization(_currentSoulInformation.soulItem.UsableInLocalization);
            PopUpInformation popUpInfo = new PopUpInformation
            {
                DisableOnConfirm = isInCorrectLocalization,
                UseOneButton = false,
                Header = "USE ITEM",
                Message = "Are you sure you want to USE: " + _currentSoulInformation.soulItem.Name + " ?",
                Confirm_OnClick = () => UseCurrentSoul(isInCorrectLocalization),
                Parent = this
            };
            UseButton.onClick.AddListener(() => GUIController.Instance.ShowPopUpMessage(popUpInfo));

            if (!isInCorrectLocalization)
            {
                UseButton.interactable = false;
            }
            else
            {
                UseButton.interactable = true;
            }
        }
        UseButton.gameObject.SetActive(active);
    }

    private void SetupDestroyButton(bool active)
    {
        DestroyButton.onClick.RemoveAllListeners();
        if (active)
        {
            PopUpInformation popUpInfo = new PopUpInformation
            {
                DisableOnConfirm = true,
                UseOneButton = false,
                Header = "DESTROY ITEM",
                Message = "Are you sure you want to DESTROY: " + Name.text + " ?",
                Confirm_OnClick = () => DestroyCurrentSoul(),
                Parent = this
            };
            DestroyButton.onClick.AddListener(() => GUIController.Instance.ShowPopUpMessage(popUpInfo));
        }

        DestroyButton.gameObject.SetActive(active);
    }
}