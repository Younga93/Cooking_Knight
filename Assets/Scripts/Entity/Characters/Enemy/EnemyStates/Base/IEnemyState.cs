public interface IEnemyState    //refactoring: Player랑 같은데 합칠수 있나? 고민해보기
{
    void EnterState(Enemy enemy);
    void UpdateState(Enemy enemy);
    void ExitState(Enemy enemy);
}