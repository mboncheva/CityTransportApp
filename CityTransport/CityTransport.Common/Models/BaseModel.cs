namespace CityTransport.Common.Models
{
    public class BaseModel
    {
        public string Message { get; set; }

        public bool HasError { get; set; }

        public void SetError(string message)
        {
            this.HasError = true;
            this.Message = message;
        }
    }
}
