using ErrorOr;

public class Breakfast 
{
    public const int MinNameLength = 5;
    public const int MaxNameLength = 50;
    
    public const int MinDescriptionLength = 50;
    public const int MaxDescriptionLength = 150;
    public Guid Id { get; }
    public string Name { get; }
    public string Description { get; }
    public DateTime StartDateTime { get; }
    public DateTime EndDateTime { get; }
    public DateTime LastModifiedDateTime { get; }
    public List<string> Savory { get; }
    public List<string> Sweet { get; }

    private Breakfast(
        Guid id,
        string name,
        string description,
        DateTime startDateTime,
        DateTime endDateTime,
        DateTime lastModifiedDateTime,
        List<string> savory,
        List<string> sweet)
    {
        Id = id;
        Name = name;
        Description = description; 
        StartDateTime =  startDateTime;
        EndDateTime = endDateTime;
        LastModifiedDateTime = lastModifiedDateTime;
        Savory = savory;
        Sweet = sweet;
    }

    public static ErrorOr<Breakfast> Create(
        string name,
        string description,
        DateTime startDateTime,
        DateTime endDateTime,
        List<string> savory,
        List<string> sweet,
        Guid? id = null)
    {

        //enforce invariants
        List<Error> errors = new();
        
        if(name.Length is < MinNameLength or > MaxNameLength)
        {
            errors.Add(Errors.BreakfastError.InvalidName);
        }

        if(description.Length is < MinDescriptionLength or > MaxDescriptionLength)
        {
            errors.Add(Errors.BreakfastError.InvalidDescription);
        }
        
        if(errors.Count > 0 )
        {
            return errors;
        }

        return new Breakfast(
            id ?? Guid.NewGuid(),// set new guid
            name,
            description,
            startDateTime,
            endDateTime,
            DateTime.UtcNow,  //set current Time func
            savory,
            sweet);
    }
}

