namespace EmailService.messaging
{
    public interface IAzureBus
    {
        Task start();
        Task stop();
    }
}
