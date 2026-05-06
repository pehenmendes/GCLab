using System.Runtime.InteropServices;

namespace GCLab;

class IssueTracker
{
    private readonly List<(string label, WeakReference<object> wr)> _list = new();
    public bool HasSurvivors { get; private set; }

    public void Track(string label, object obj)
    {
        if (obj == null) return;
        _list.Add((label, new WeakReference<object>(obj)));
    }

    public void Report()
    {
        Console.WriteLine("\n--- Verificação de sobreviventes (WeakReference) ---");

        int alive = 0;

        for (int i = _list.Count - 1; i >= 0; i--)
        {
            var (label, wr) = _list[i];

            if (wr.TryGetTarget(out var _))
            {
                alive++;
                Console.WriteLine($"{label}: vivo");
            }
            else
            {
                Console.WriteLine($"{label}: coletado");
                _list.RemoveAt(i); // limpa entrada morta
            }
        }

        HasSurvivors = alive > 0;

        Console.WriteLine("-----------------------------------------------");
        Console.WriteLine($"Gen0: {GC.CollectionCount(0)} | Gen1: {GC.CollectionCount(1)} | Gen2: {GC.CollectionCount(2)}");
    }
}
