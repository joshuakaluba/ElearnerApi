/// <summary>
/// Author: Joshua Kaluba
/// </summary>

namespace ElearnerApi.Data.Models
{
    public class ErrorViewModel
    {
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty( RequestId );
    }
}