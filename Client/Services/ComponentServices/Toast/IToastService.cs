namespace PannonBlazor.Client.Services.ComponentServices.Toast
{
    public interface IToastService
    {
        event Action<string, string, ToastType>? OnShow;
        void ShowToast(string title, string message, ToastType type);
    }
    public class ToastService : IToastService
    {
        public ToastService()
        {
        }

        public event Action<string, string, ToastType>? OnShow;
        public void ShowToast(string title, string message, ToastType type)
            => OnShow?.Invoke(title, message, type);
    }
}