public class Ranking
{
    public string name;

    public int points;

    public Ranking(string name, int points)
    {
        this.name = name;
        this.points = points;
    }    

    public void SetName(string name) 
    {
        this.name = name;
    }

    public void SetPoints(int points) 
    {
        this.points = points;
    }
}
