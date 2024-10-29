using Microsoft.Extensions.DependencyInjection;
using MoniteBackendService.Brokers.Infrastructure;
using MoniteBackendService.Brokers.Kafka;

namespace MoniteBackendService.Brokers.Kafka;

public class KafkaConsumerService : KafkaConsumerService<KafkaMessageHandlersController>
{
    public KafkaConsumerService(IServiceScopeFactory serviceScopeFactory, KafkaOptions kafkaOptions)
        : base(serviceScopeFactory, kafkaOptions) { }
}
