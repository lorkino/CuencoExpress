namespace Film.Controllers
{
    public class NotificationModel
    {
        public string Title { get; set; }
        public string Message { get; set; }
        public string Url { get; set; } = "https://localhost:44322/profile";
    }
}