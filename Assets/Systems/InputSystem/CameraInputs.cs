//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.7.0
//     from Assets/Systems/InputSystem/CameraInputs.inputactions
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

public partial class @CameraInputs: IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @CameraInputs()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""CameraInputs"",
    ""maps"": [
        {
            ""name"": ""Camera"",
            ""id"": ""a736957f-ce1e-4cd6-a871-19c0c5225dac"",
            ""actions"": [
                {
                    ""name"": ""ResetZoom"",
                    ""type"": ""Button"",
                    ""id"": ""e1d868eb-eab7-4620-8562-994eaab78f30"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Zoom"",
                    ""type"": ""Value"",
                    ""id"": ""c28dcf57-649c-467d-a1ed-01226992e532"",
                    ""expectedControlType"": ""Delta"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""RotateCamera"",
                    ""type"": ""Value"",
                    ""id"": ""75c97ff4-2d26-4468-bfc8-77edea0c0616"",
                    ""expectedControlType"": ""Delta"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""EnableRotation"",
                    ""type"": ""Button"",
                    ""id"": ""5876da2e-a4e6-42e1-a383-2fd95b590dfe"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""54869da4-dbff-47aa-9ed5-62227459c501"",
                    ""path"": ""<Mouse>/middleButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ResetZoom"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""78e4ddbe-ecf7-492f-9d17-936169b905b3"",
                    ""path"": ""<Mouse>/scroll"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Zoom"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1e65c44b-47e1-4caf-9d40-c30505ee7625"",
                    ""path"": ""<Mouse>/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""RotateCamera"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""77144756-63b3-49a1-adaa-13a3c2aca6d5"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""EnableRotation"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Camera
        m_Camera = asset.FindActionMap("Camera", throwIfNotFound: true);
        m_Camera_ResetZoom = m_Camera.FindAction("ResetZoom", throwIfNotFound: true);
        m_Camera_Zoom = m_Camera.FindAction("Zoom", throwIfNotFound: true);
        m_Camera_RotateCamera = m_Camera.FindAction("RotateCamera", throwIfNotFound: true);
        m_Camera_EnableRotation = m_Camera.FindAction("EnableRotation", throwIfNotFound: true);
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

    // Camera
    private readonly InputActionMap m_Camera;
    private List<ICameraActions> m_CameraActionsCallbackInterfaces = new List<ICameraActions>();
    private readonly InputAction m_Camera_ResetZoom;
    private readonly InputAction m_Camera_Zoom;
    private readonly InputAction m_Camera_RotateCamera;
    private readonly InputAction m_Camera_EnableRotation;
    public struct CameraActions
    {
        private @CameraInputs m_Wrapper;
        public CameraActions(@CameraInputs wrapper) { m_Wrapper = wrapper; }
        public InputAction @ResetZoom => m_Wrapper.m_Camera_ResetZoom;
        public InputAction @Zoom => m_Wrapper.m_Camera_Zoom;
        public InputAction @RotateCamera => m_Wrapper.m_Camera_RotateCamera;
        public InputAction @EnableRotation => m_Wrapper.m_Camera_EnableRotation;
        public InputActionMap Get() { return m_Wrapper.m_Camera; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(CameraActions set) { return set.Get(); }
        public void AddCallbacks(ICameraActions instance)
        {
            if (instance == null || m_Wrapper.m_CameraActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_CameraActionsCallbackInterfaces.Add(instance);
            @ResetZoom.started += instance.OnResetZoom;
            @ResetZoom.performed += instance.OnResetZoom;
            @ResetZoom.canceled += instance.OnResetZoom;
            @Zoom.started += instance.OnZoom;
            @Zoom.performed += instance.OnZoom;
            @Zoom.canceled += instance.OnZoom;
            @RotateCamera.started += instance.OnRotateCamera;
            @RotateCamera.performed += instance.OnRotateCamera;
            @RotateCamera.canceled += instance.OnRotateCamera;
            @EnableRotation.started += instance.OnEnableRotation;
            @EnableRotation.performed += instance.OnEnableRotation;
            @EnableRotation.canceled += instance.OnEnableRotation;
        }

        private void UnregisterCallbacks(ICameraActions instance)
        {
            @ResetZoom.started -= instance.OnResetZoom;
            @ResetZoom.performed -= instance.OnResetZoom;
            @ResetZoom.canceled -= instance.OnResetZoom;
            @Zoom.started -= instance.OnZoom;
            @Zoom.performed -= instance.OnZoom;
            @Zoom.canceled -= instance.OnZoom;
            @RotateCamera.started -= instance.OnRotateCamera;
            @RotateCamera.performed -= instance.OnRotateCamera;
            @RotateCamera.canceled -= instance.OnRotateCamera;
            @EnableRotation.started -= instance.OnEnableRotation;
            @EnableRotation.performed -= instance.OnEnableRotation;
            @EnableRotation.canceled -= instance.OnEnableRotation;
        }

        public void RemoveCallbacks(ICameraActions instance)
        {
            if (m_Wrapper.m_CameraActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(ICameraActions instance)
        {
            foreach (var item in m_Wrapper.m_CameraActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_CameraActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public CameraActions @Camera => new CameraActions(this);
    public interface ICameraActions
    {
        void OnResetZoom(InputAction.CallbackContext context);
        void OnZoom(InputAction.CallbackContext context);
        void OnRotateCamera(InputAction.CallbackContext context);
        void OnEnableRotation(InputAction.CallbackContext context);
    }
}
