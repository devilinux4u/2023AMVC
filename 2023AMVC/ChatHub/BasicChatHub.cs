﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;

namespace _2023AMVC.ChatHub
{
    public class BasicChatHub : Hub
    {

        private readonly UserManager<IdentityUser> _userManager;
        public BasicChatHub(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        public string GetUserId()
        {
            var httpContext = Context.GetHttpContext();
            var userId = httpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return userId;
        }

        public async Task<IList<string>> GetUserRoles(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            var roles = await _userManager.GetRolesAsync(user);
            return roles;
        }

        public static List<string> GroupsJoined { get; set; } =new List<string>();

        [Authorize]
        public async Task JoinGroup(string sender)
        {
            var user = GetUserId();
            var role = (await GetUserRoles(user)).FirstOrDefault();
            if(!GroupsJoined.Contains(Context.ConnectionId + ":" + role))
            {
                GroupsJoined.Add(Context.ConnectionId + ":" + role);

                await Groups.AddToGroupAsync(Context.ConnectionId, role);

            }
        }

    }
}
