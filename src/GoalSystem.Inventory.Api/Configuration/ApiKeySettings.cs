namespace GoalSystem.Inventory.Api.Configuration
{
    /// <summary>
    /// Class that get information of ApiKey
    /// </summary>
    public class ApiKeySettings
    {
        /// <summary>
        /// Name of ApiKey
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Secret key of ApiKey
        /// </summary>
        public string Secret { get; set; }
    }
}
