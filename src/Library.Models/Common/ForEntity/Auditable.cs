namespace Library.Models.Common.ForEntity;

public class Auditable : Entity
{
    public bool IsDeleted { get; set; }
}
