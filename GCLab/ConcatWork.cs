namespace GCLab;

// ===================================
// 4) Concatenação de string ineficiente
// ===================================
using System.Text;

static class ConcatWork
{
    public static string Good()
    {
        var sb = new StringBuilder();

        for (int i = 0; i < 10; i++)
            sb.Append(i);

        return sb.ToString();
    }
}
