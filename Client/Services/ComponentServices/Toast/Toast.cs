namespace PannonBlazor.Client.Services.ComponentServices.Toast
{//https://www.youtube.com/watch?v=4heY5s3CIX8
    public class Toast
    {
        public Toast(Guid id, DateTime timeStamp, ToastSettings toastSettings)
        {
            Id = id;
            TimeStamp = timeStamp;
            ToastSettings = toastSettings;
        }
        public Guid Id { get; set; }
        public DateTime TimeStamp { get; set; }
        public ToastSettings ToastSettings { get; set; }
    }

    public class ToastSettings
    {
        public ToastSettings(string title, string message, ToastType type, string textCssClass, string backgroundCssClass)
        {
            Title = title;
            Message = message;
            Type = type;
            TextCssClass = textCssClass;
            BackgroundCssClass = backgroundCssClass;
        }

        public string Title { get; set; }
        public string Message { get; set; }
        public ToastType Type { get; set; }
        public string TextCssClass { get; set; }
        public string BackgroundCssClass { get; set; }
    }
    public enum ToastType
    {
        Primary,
        Danger,
        Warning,
        Success,
        Info
    }

}
