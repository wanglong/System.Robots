namespace System.Robots.Core
{
    /// <summary>
    /// Interface ILog
    /// </summary>
    public interface IJobLog
    {
        /// <summary>
        /// Informations the specified title.
        /// </summary>
        /// <param name="title">The title.</param>
        /// <param name="message">The message.</param>
        /// <param name="mail">if set to <c>true</c> [mail].</param>
        void Info(string title, string message, bool mail = false);

        /// <summary>
        /// Informations the specified title.
        /// </summary>
        /// <param name="title">The title.</param>
        /// <param name="mail">if set to <c>true</c> [mail].</param>
        void Info(string title, bool mail = false);

        /// <summary>
        /// Errors the specified title.
        /// </summary>
        /// <param name="title">The title.</param>
        /// <param name="e">The e.</param>
        /// <param name="message">The message.</param>
        /// <param name="mail">if set to <c>true</c> [mail].</param>
        void Error(string title, Exception e, string message, bool mail = false);

        /// <summary>
        /// Errors the specified title.
        /// </summary>
        /// <param name="title">The title.</param>
        /// <param name="e">The e.</param>
        /// <param name="mail">if set to <c>true</c> [mail].</param>
        void Error(string title, Exception e, bool mail = false);
    }
}