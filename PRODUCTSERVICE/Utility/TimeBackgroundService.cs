
using PRODUCTSERVICE.Services.IServices;

namespace PRODUCTSERVICE.Utility
{
    public class TimeBackgroundService : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        public TimeBackgroundService(IServiceScopeFactory serviceScope)
        {
            _scopeFactory = serviceScope;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while(!stoppingToken.IsCancellationRequested)
            {
                using(var scope = _scopeFactory.CreateScope())
                {
                    var prodservice = scope.ServiceProvider.GetRequiredService<IProducts>();

                    await prodservice.UpdateStatus();
                }
                await Task.Delay(1000*3*60, stoppingToken);
            }
        }
    }
}
