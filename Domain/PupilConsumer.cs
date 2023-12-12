using MassTransit;

namespace RabbitMq1.Domain
{
    public class PupilConsumer:IConsumer<Pupil>
    {
        public async Task Consume(ConsumeContext<Pupil> context)
        {
            var pupil = context.Message;
            await Console.Out.WriteLineAsync(($"Received Pupil: {pupil.Name} {pupil.Surname}"));
        }
    }
}
