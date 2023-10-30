﻿namespace ComicChecklist.Domain.Models
{
    public class Issue : Entity
    {
        public int Order { get; set; }

        public string Title { get; set; }

        public int ChecklistId { get; set; }
    }
}
