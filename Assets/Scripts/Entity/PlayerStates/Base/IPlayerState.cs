public interface IPlayerState   //내가 왜 interface로 하려했더라...???
{
    void EnterState(Player player);
    void UpdateState(Player player);
    void ExitState(Player player);
}