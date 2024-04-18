namespace ActorsWebApplication.Models
{
    /// <summary>
    /// Represents an actor.
    /// </summary>
    public class ActorBase
    {
        /// <summary>
        /// Gets or sets the actor ID.
        /// </summary>
        public string Id { get; set; } = Guid.NewGuid().ToString();
        /// <summary>
        /// Gets or sets the actor name.
        /// </summary>
        public string Name { get; set; }
    }
}
