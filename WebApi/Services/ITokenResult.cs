using System;

namespace WebApi.Services
{
    /// <summary>
    /// ITokenResult interface
    /// </summary>
    public interface ITokenResult
    {
        /// <summary>
        /// 
        /// </summary>
        DateTime Created { get; }
        /// <summary>
        /// 
        /// </summary>
        DateTime Expires { get; }
        /// <summary>
        /// 
        /// </summary>
        bool Status { get; }
        /// <summary>
        /// 
        /// </summary>
        string Token { get; }
    }
}