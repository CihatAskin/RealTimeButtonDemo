using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.Authorization;
using RealTimeButtonDemo.API.Data;
using RealTimeButtonDemo.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace RealTimeButtonDemo.API.Hubs;

[Authorize]
public class ButtonHub : Hub
{
    private readonly ApplicationDbContext _context;

    public ButtonHub(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task JoinRoom(string roomId)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, roomId);
        
        var currentState = await _context.ButtonStates
            .FirstOrDefaultAsync(b => b.RoomId == roomId);
            
        if (currentState != null)
        {
            await Clients.Caller.SendAsync("ButtonStateChanged", currentState);
        }
    }

    public async Task ChangeButtonColor(string roomId, string color)
    {
        var username = Context.User?.Identity?.Name ?? "Anonymous";
        
        var buttonState = await _context.ButtonStates
            .FirstOrDefaultAsync(b => b.RoomId == roomId);
            
        if (buttonState == null)
        {
            buttonState = new ButtonState
            {
                RoomId = roomId,
                Color = color,
                LastClickedBy = username,
                LastClickedAt = DateTime.UtcNow,
                IsActive = true
            };
            _context.ButtonStates.Add(buttonState);
        }
        else
        {
            buttonState.Color = color;
            buttonState.LastClickedBy = username;
            buttonState.LastClickedAt = DateTime.UtcNow;
        }

        await _context.SaveChangesAsync();
        
        await Clients.Group(roomId).SendAsync("ButtonStateChanged", buttonState);
    }

    public async Task NotifyButtonClick(string roomId, string userId)
    {
        var username = Context.User?.Identity?.Name ?? "Anonymous";
        
        await Clients.Group(roomId).SendAsync("ButtonClicked", new 
        { 
            RoomId = roomId, 
            UserId = userId, 
            Username = username,
            Timestamp = DateTime.UtcNow 
        });
    }
}