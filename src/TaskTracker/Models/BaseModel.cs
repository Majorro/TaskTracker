using System;
using System.ComponentModel.DataAnnotations;

namespace TaskTracker.Models
{
    public class BaseModel
    {
        [Key]
        public Guid Id { get; init; }
    }
}
