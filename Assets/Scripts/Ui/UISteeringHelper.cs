using System;
using UnityEngine;
using UnityEngine.UI;

public class UISteeringHelper : MonoBehaviour
{
    [SerializeField]
    private Selectable SourceComponent;


    [SerializeField]
    private Selectable SelectOnUpIfTrue;
    [SerializeField]
    private Selectable SelectOnUpIfFalse;


    [SerializeField]
    private Selectable SelectOnDownIfTrue;
    [SerializeField]
    private Selectable SelectOnDownIfFalse;

    private bool _condition;

    public void Awake()
    {
        SetCondition(false);
    }


    public void SetCondition(bool condition)
    {
        _condition = condition;

        SetNavigation();
    }

    private void SetNavigation()
    {
        if (_condition)
        {
            Navigation nav = SourceComponent.navigation;
            nav.mode = Navigation.Mode.Explicit;
            nav.selectOnUp = SelectOnUpIfTrue;
            nav.selectOnDown = SelectOnDownIfTrue;
            SourceComponent.navigation = nav;
        }
        else
        {
            Navigation nav = SourceComponent.navigation;
            nav.mode = Navigation.Mode.Explicit;
            nav.selectOnUp = SelectOnUpIfFalse;
            nav.selectOnDown = SelectOnDownIfFalse;
            SourceComponent.navigation = nav;
        }
    }
}
