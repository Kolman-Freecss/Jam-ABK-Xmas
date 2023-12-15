#region

using Gameplay.GameplayObjects.Interactables._common;

#endregion

namespace Gameplay.GameplayObjects.Interactables._derivatives
{
    public class StairsInteractable : BaseInteractableObject
    {
        public override void DoInteraction<TData>(TData obj)
        {
            // RoundManager.Instance.OnPlayerInteractsWithPuzzle();
            base.DoInteraction(obj);
        }
    }
}
