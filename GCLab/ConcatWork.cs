namespace GCLab;

// ===================================
// 4) Concatenação de string ineficiente
// ===================================
using System.Text;

static class ConcatWork
{
    public static string Good()
    {
        var sb = new StringBuilder(300_000);

        for (int i = 0; i < 50_000; i++)
            sb.Append(i);
        Console.WriteLine(sb.Length);

        return sb.ToString();
    }
}
