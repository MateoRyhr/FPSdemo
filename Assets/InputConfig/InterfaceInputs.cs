//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.2.0
//     from Assets/InputConfig/InterfaceInputs.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public partial class @InterfaceInputs : IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @InterfaceInputs()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""InterfaceInputs"",
    ""maps"": [
        {
            ""name"": ""InterfaceInputsMap"",
            ""id"": ""88e7cd19-8cb2-4d84-8765-35f366934eea"",
            ""actions"": [
                {
                    ""name"": ""PauseGame"",
                    ""type"": ""Button"",
                    ""id"": ""8b009d60-e28c-4e69-8210-59a83ce4db44"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""4a1e1956-ece8-4559-b5aa-babcda3146d6"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PauseGame"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""23bdd8e2-8089-481e-a24b-8aa1553137b5"",
                    ""path"": ""<Gamepad>/start"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PauseGame"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // InterfaceInputsMap
        m_InterfaceInputsMap = asset.FindActionMap("InterfaceInputsMap", throwIfNotFound: true);
        m_InterfaceInputsMap_PauseGame = m_InterfaceInputsMap.FindAction("PauseGame", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }
    public IEnumerable<InputBinding> bindings => asset.bindings;

    public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
    {
        return asset.FindAction(actionNameOrId, throwIfNotFound);
    }
    public int FindBinding(InputBinding bindingMask, out InputAction action)
    {
        return asset.FindBinding(bindingMask, out action);
    }

    // InterfaceInputsMap
    private readonly InputActionMap m_InterfaceInputsMap;
    private IInterfaceInputsMapActions m_InterfaceInputsMapActionsCallbackInterface;
    private readonly InputAction m_InterfaceInputsMap_PauseGame;
    public struct InterfaceInputsMapActions
    {
        private @InterfaceInputs m_Wrapper;
        public InterfaceInputsMapActions(@InterfaceInputs wrapper) { m_Wrapper = wrapper; }
        public InputAction @PauseGame => m_Wrapper.m_InterfaceInputsMap_PauseGame;
        public InputActionMap Get() { return m_Wrapper.m_InterfaceInputsMap; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(InterfaceInputsMapActions set) { return set.Get(); }
        public void SetCallbacks(IInterfaceInputsMapActions instance)
        {
            if (m_Wrapper.m_InterfaceInputsMapActionsCallbackInterface != null)
            {
                @PauseGame.started -= m_Wrapper.m_InterfaceInputsMapActionsCallbackInterface.OnPauseGame;
                @PauseGame.performed -= m_Wrapper.m_InterfaceInputsMapActionsCallbackInterface.OnPauseGame;
                @PauseGame.canceled -= m_Wrapper.m_InterfaceInputsMapActionsCallbackInterface.OnPauseGame;
            }
            m_Wrapper.m_InterfaceInputsMapActionsCallbackInterface = instance;
            if (instance != null)
            {
                @PauseGame.started += instance.OnPauseGame;
                @PauseGame.performed += instance.OnPauseGame;
                @PauseGame.canceled += instance.OnPauseGame;
            }
        }
    }
    public InterfaceInputsMapActions @InterfaceInputsMap => new InterfaceInputsMapActions(this);
    public interface IInterfaceInputsMapActions
    {
        void OnPauseGame(InputAction.CallbackContext context);
    }
}
