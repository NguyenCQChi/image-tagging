using authentication.Data;
using authentication.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace authentication.Middleware
{
    public class UpdateNumRequests
    {
        private readonly RequestDelegate _next;
        private readonly IServiceProvider _serviceProvider;

        public UpdateNumRequests(RequestDelegate next, IServiceProvider serviceProvider)
        {
            _next = next;
            _serviceProvider = serviceProvider;
        }

        public async Task Invoke(HttpContext context)
        {
            await _next(context);
            
            if (ShouldExecuteCustomLogic(context))
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var scopedServices = scope.ServiceProvider;
                    var db = scopedServices.GetRequiredService<ApplicationDbContext>();

                    if (context.Items.TryGetValue("endpointType", out var endpointTypeObject) && endpointTypeObject is EndpointType endpointType &&
                        context.Items.TryGetValue("user", out var userObject) && userObject is ApplicationUser user)
                    {
                        var userEndpointRequest = new UserEndpointRequests
                        {
                            UserId = user.Id,
                            EndpointTypeId = endpointType.Id
                        };

                        var existingRequest = await db.UserEndpointRequests.FindAsync(userEndpointRequest.UserId, userEndpointRequest.EndpointTypeId);
                        if (existingRequest != null)
                        {
                            existingRequest.NumRequests++;
                        }
                        else
                        {
                            userEndpointRequest.NumRequests = 1;
                            await db.UserEndpointRequests.AddAsync(userEndpointRequest);
                        }
                        await db.SaveChangesAsync();
                    }
                }
            }
        }

        private bool ShouldExecuteCustomLogic(HttpContext context)
        {
            return context.Request.Path.StartsWithSegments("/api/v1/auth");
        }
    }
}
