using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using SprinklingApp.DataAccess.ORM.EFCore;

namespace SprinklingApp.Master.API.Console.Middlewares {

    public class EnsureCreatedDatabaseMiddleware {
        public EnsureCreatedDatabaseMiddleware(RequestDelegate next) {
            Next = next;
        }

        public RequestDelegate Next { get; set; }

        public async Task Invoke(HttpContext context, SpringklingContext dbContext) {
            await dbContext.Database.EnsureCreatedAsync();
            await Next(context);
        }
    }

}