﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities;

public class User
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    public string Login { get; set; }
    
    [Required]
    public string? Password { get; set; }
    
    [Required]
    public string? FIO { get; set; }
    
    [Required]
    public string? Phone { get; set; }
    
    [Required]
    public string TelegramID { get; set; }
    
    [NotMapped]
    public IEnumerable<Order> Orders { get; set; }
}