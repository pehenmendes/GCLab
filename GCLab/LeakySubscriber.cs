namespace GCLab;

class LeakySubscriber : IDisposable
{
    private readonly Publisher _publisher;

    public LeakySubscriber(Publisher publisher)
    {
        _publisher = publisher;
        _publisher.OnSomething += Handle;
    }

    private void Handle() { }

    public void Dispose()
    {
        _publisher.OnSomething -= Handle;
    }
}