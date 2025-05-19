using EntityLayer.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Services
{
    public class InactiveUserService : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;

        public InactiveUserService(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using var scope = _scopeFactory.CreateScope();
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();

                var allUsers = await userManager.Users
                    .Where(u => u.IsOnline && u.LastActiveDate < DateTime.UtcNow.AddMinutes(-10))
                    .ToListAsync(stoppingToken);

                foreach (var user in allUsers)
                {
                    user.IsOnline = false;
                    await userManager.UpdateAsync(user);
                }

                await Task.Delay(TimeSpan.FromMinutes(5), stoppingToken);
            }
        }
    }

}
