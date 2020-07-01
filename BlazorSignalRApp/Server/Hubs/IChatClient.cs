using System.Threading.Tasks;

namespace BlazorSignalRApp.Server.Hubs
{
    public interface IChatClient
    {
        Task ReceiveMessage(string user, string message);
        Task ReceiveMessage(string message);
        Task SendMessage(string message);
    }
}
