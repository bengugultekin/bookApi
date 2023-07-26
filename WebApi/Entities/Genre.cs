﻿using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi;

public class Genre
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string Name { get; set; }
    public bool isActive { get; set; } = true;
}
