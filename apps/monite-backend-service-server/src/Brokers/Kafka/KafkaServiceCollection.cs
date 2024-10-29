using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MoniteBackendService.Brokers.Infrastructure;

namespace MoniteBackendService.Brokers.Kafka;

public static class KafkaServiceCollection
{
    public static IServiceCollection AddKafka(this IHostApplicationBuilder app)
    {
        var kafkaOptions = app.Configuration.GetSection("kafka").Get<KafkaOptions>();
        if (kafkaOptions == null)
            throw new Exception("KafkaOptions not found in configuration section kafka");
        if (kafkaOptions.ConsumerGroupId == null)
            throw new Exception("ConsumerGroupId not found in configuration section kafka");
        if (kafkaOptions.BootstrapServers == null)
            throw new Exception("BootstrapServers not found in configuration section kafka");
        return app
            .Services.AddHostedService(x => new KafkaConsumerService(
                x.GetRequiredService<IServiceScopeFactory>(),
                kafkaOptions
            ))
            .AddSingleton(x => new KafkaProducerService(kafkaOptions.BootstrapServers))
            .AddScoped<KafkaMessageHandlersController>();
    }
}
