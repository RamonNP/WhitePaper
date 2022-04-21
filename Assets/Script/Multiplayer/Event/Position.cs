public class Position
{
    public string x { get; set; }
    public string y { get; set; }

    public void SetPosition(float x, float y)
    {
        this.x = x.ToString("F1");
        this.y = y.ToString("F1");
    }
    
}


