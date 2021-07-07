
/// <summary>
/// class for all objects that have motion.
/// </summary>
public class mobileObject : objetMere
{
    /// <summary>
    ///  Add the state and the management.
    /// </summary>
    protected string _direction;
    protected string _state;


    public mobileObject() : base()
    {

        this.setDirection("");
        this.setState("alive");
    }
    public mobileObject(float x, float y, string direction) : base(x, y)
    {
        this.setDirection(direction);
        this.setState("alive");
    }

    /// <summary>
    ///  read accessor
    /// </summary>
    public string getDirection()
    {
        return _direction;
    }

    public string getEtat()
    {
        return _state;
    }

    /// <summary>
    ///  writing accessor.
    /// </summary>
    public void setDirection(string direction)
    {
        _direction = direction;
    }



    public void setState(string state)
    {
        _state = state;
    }

}

