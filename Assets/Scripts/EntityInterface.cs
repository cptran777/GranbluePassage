public interface IEntity {
    // Expected to be on an entity so that if it can die the entity handler can trigger
    // a death sequence on it
    void OnStartDeathSequence();
}
