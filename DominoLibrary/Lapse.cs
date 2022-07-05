using System.Diagnostics;
namespace DominoLibrary;

public class Lapse
{
    public Lapse(int secods)
    {
        Stopwatch crono = new Stopwatch();

        crono.Start();
        while (crono.ElapsedMilliseconds < secods * 1000){}
        crono.Stop();
    }
}