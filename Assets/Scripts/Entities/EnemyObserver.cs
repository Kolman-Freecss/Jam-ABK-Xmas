#region

using System.Collections.Generic;

#endregion

namespace Entities
{
    public class EnemyObserver
    {
        private List<IEnemyObserver> observers = new();

        public void AddObserver(IEnemyObserver enemyObserver)
        {
            observers.Add(enemyObserver);
        }

        public void RemoveObserver(IEnemyObserver enemyObserver)
        {
            observers.Remove(enemyObserver);
        }

        public void Notify(EnemyState enemyState)
        {
            foreach (var observer in observers)
            {
                observer.OnEnemyStateNotify(enemyState);
            }
        }
    }
}
