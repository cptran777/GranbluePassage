public interface IEntity {
    // Expected to be on an entity so that if it can die the entity handler can trigger
    // a death sequence on it
    void OnStartDeathSequence();
    // Expected to be on an entity so that if they are hit there can be a visual or audio
    // effect associated with that
    void OnStartHitSequence();
}
