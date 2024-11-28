namespace Domain.Common
{
    public class IdentifiableEntity
    {
         /// <summary>
        /// Get or set id
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="IdentifiableEntity"/> class.
        /// </summary>
        public IdentifiableEntity()
        {
        }
    }
}