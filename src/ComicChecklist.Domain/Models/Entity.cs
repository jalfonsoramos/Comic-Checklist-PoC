namespace ComicChecklist.Domain.Models
{
    public abstract class Entity
    {
        public int Id { get; init; }

        public DateTime Createad { get; init; }

        public bool Clocked { get; init; }
    }
}
