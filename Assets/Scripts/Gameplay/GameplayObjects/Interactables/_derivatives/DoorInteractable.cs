#region

using Gameplay.GameplayObjects.Interactables._common;

#endregion

namespace Gameplay.GameplayObjects.Interactables._derivatives
{
    public class DoorInteractable : BaseInteractableObject
    {
        public override void DoInteraction<DoorInteractable>(DoorInteractable obj)
        {
            // Audio.PlaySound("door_open");
            base.DoInteraction(obj);
        }
    }
}
