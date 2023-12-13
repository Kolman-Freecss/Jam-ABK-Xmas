namespace Entities
{
    public interface IEnemyObserver
    {
        void OnEnemyStateNotify(EnemyState enemyState);
    }
}
