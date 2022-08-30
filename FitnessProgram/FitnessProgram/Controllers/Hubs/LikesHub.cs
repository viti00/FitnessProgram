namespace FitnessProgram.Controllers.Hubs
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.SignalR;

    [Authorize]
    public class LikesHub : Hub
    {

        public async Task CountChanger(string likesCount)
        {
            await this.Clients.All.SendAsync("LikePost", likesCount);
        }
    }
}
