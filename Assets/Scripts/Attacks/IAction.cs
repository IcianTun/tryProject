public interface IAction {
	void Perform(GameInstanceManager gameInstance);
    float GetTotalDelay();
    void MyAwake();
}
