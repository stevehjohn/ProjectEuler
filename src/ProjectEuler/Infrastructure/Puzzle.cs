namespace ProjectEuler.Infrastructure;

public abstract class Puzzle
{
    protected string[] Input;
    
    public abstract string GetAnswer();

    protected void LoadInput()
    {
        Input = File.ReadAllLines($"{AppDomain.CurrentDomain.BaseDirectory}/Inputs/{GetType().Name[6..]}.clear");
    }
}