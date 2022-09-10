namespace FitnessProgram.Controllers.Hubs
{
    using Microsoft.AspNetCore.SignalR;

    public class LikesHub : Hub
    {

        public async Task CountChanger(string likesCount)
        {
            await this.Clients.All.SendAsync("LikePost", likesCount);
        }
    }
}
