namespace GCLab;

class Program
{
    // IMPORTANTE: ESTE CÓDIGO CONTÉM PROBLEMAS PROPOSITAIS.
    // Os alunos devem implementar as correções para chegar ao final com "GC limpo".
    static bool RunScenario()
    {
        var tracker = new IssueTracker();

        var publisher = new Publisher();
        var subscriber = new LeakySubscriber(publisher);
        tracker.Track("subscriber", subscriber);

        var lohBuffer = BigBufferHolder.Run();
        tracker.Track("lohBuffer", lohBuffer);

        var pinner = new Pinner();
        var pinned = pinner.PinShortTime();
        tracker.Track("pinnedBuffer", pinned);

        var logger = new Logger("log.txt");
        logger.WriteLines(10);
        tracker.Track("logger", logger);

        publisher.Raise();

        // cleanup
        subscriber.Dispose();
        pinner.Dispose();
        logger.Dispose();
        GlobalCache.Clear();

        subscriber = null;
        publisher = null;
        lohBuffer = null;
        pinned = null;
        logger = null;
        pinner = null;

        GCHelpers.FullCollect();
        tracker.Report();

        return tracker.HasSurvivors;
    }
    static void Main()
    {

        Console.WriteLine("=== GCLab - Versão com Problemas ===");
        Console.WriteLine($"GC Server Mode: {System.Runtime.GCSettings.IsServerGC}\n");

        var hasSurvivors = RunScenario();
        GCHelpers.FullCollect();
        GC.WaitForPendingFinalizers();
        GCHelpers.FullCollect();

        Console.WriteLine(hasSurvivors
            ? "\n❌ Existem sobreviventes indesejados. Sua missão: corrigir o código e rodar novamente."
            : "\n✅ GC limpo: nenhuma referência indesejada permaneceu viva.");
        Console.ReadLine();
    }
}