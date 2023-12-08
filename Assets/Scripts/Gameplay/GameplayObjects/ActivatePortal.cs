#region

using Gameplay.Config;
using Gameplay.GameplayObjects.Interactables._derivatives;
using UnityEngine;

#endregion

public class ActivatePortal : MonoBehaviour
{
    public void OnRoundFinished(GameManager.RoundTypes roundType)
    {
        //TODO: SFX Here
    }

    public void EnablePortal(bool enable)
    {
        gameObject.SetActive(enable);
    }

    /// <summary>
    /// Invoked when the player uses the portal.
    /// </summary>
    /// <param name="portalInteractable"></param>
    public void UsePortal(PortalInteractable portalInteractable)
    {
        SceneTransitionHandler.Instance.LoadScene(SceneTransitionHandler.SceneStates.Hell);
    }
}
