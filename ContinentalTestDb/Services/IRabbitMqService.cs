namespace ContinentalTestDb.Services
{


    public interface IRabbitMqService
    {
        Task PublishMessage(string message, string topic);
    }
}
