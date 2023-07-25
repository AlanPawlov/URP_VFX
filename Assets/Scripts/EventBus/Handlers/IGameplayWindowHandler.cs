namespace EventBus.Handlers
{
    public interface IGameplayWindowHandler : IGlobalSubscriber
    {
        void HandleShowScoreBoard();
        void HandleHideScoreBoard();
        void HandleShowPause();
        void HandleHidePause();
    }
}