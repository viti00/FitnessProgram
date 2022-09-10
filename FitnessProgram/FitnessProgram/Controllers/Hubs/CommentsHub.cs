namespace FitnessProgram.Controllers.Hubs
{
    using Microsoft.AspNetCore.SignalR;

    public class CommentsHub : Hub
    {
        public async Task CreateComment(string element, int count)
        {
            await this.Clients.Others.SendAsync("Comment", element, count);
        }

        public async Task EditComment(string message, string commentId)
        {
            await this.Clients.All.SendAsync("Edit", message, commentId);
        }

        public async Task DeleteComment(int commentId, int count)
        {
            await this.Clients.All.SendAsync("Delete", commentId, count);
        }
    }
}
