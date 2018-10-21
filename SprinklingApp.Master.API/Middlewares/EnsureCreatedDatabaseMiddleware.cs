using Microsoft.AspNetCore.Http;
using SprinklingApp.DataAccess.ORM.EFCore;
using System.Threading.Tasks;

namespace SprinklingApp.Master.API.Middlewares
{
    public class EnsureCreatedDatabaseMiddleware
    {
        public RequestDelegate Next { get; set; }

        public EnsureCreatedDatabaseMiddleware(RequestDelegate next)
        {
            this.Next = next;
        }

        public async Task Invoke(HttpContext context, SpringklingContext dbContext)
        {
            await dbContext.Database.EnsureCreatedAsync();
            await this.Next(context);
        }
    }
}
