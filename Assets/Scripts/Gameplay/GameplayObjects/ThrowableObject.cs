#region

using System;
using UnityEngine;
using UnityEngine.Animations;

#endregion

public class ThrowableObject : MonoBehaviour
{
    [SerializeField]
    private float m_ThrowForce = 10f;

    [SerializeField]
    private ParticleSystem m_ThrowEffect;

    [SerializeField]
    private float m_ThrowEffectDuration = 3f;

    [SerializeField]
    private LayerMask m_ThrowableLayerMask = Physics.DefaultRaycastLayers;

    private Rigidbody m_Rigidbody;
    private Collider m_Collider;
    private bool m_IsThrown = false;
    private ParentConstraint m_ParentConstraint;

    public Action<ThrowableObject> OnThrowableObjectImpact;

    #region Init Data

    private void Awake()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
        m_Collider = GetComponent<Collider>();
        m_ParentConstraint = GetComponent<ParentConstraint>();
    }

    #endregion

    #region Getter & Setter

    public ParticleSystem ThrowEffect => m_ThrowEffect;
    public Rigidbody Rigidbody => m_Rigidbody;
    public Collider Collider => m_Collider;
    public bool IsThrown => m_IsThrown;
    public ParentConstraint ParentConstraint => m_ParentConstraint;
    public float ThrowForce => m_ThrowForce;
    public float ThrowEffectDuration => m_ThrowEffectDuration;

    #endregion

    private void OnCollisionEnter(Collision other)
    {
        if ((1 << other.gameObject.layer) == m_ThrowableLayerMask)
        {
            OnThrowableObjectImpact?.Invoke(this);
        }
    }
}
