using System.ComponentModel.DataAnnotations;

namespace SupCountBE.Application.Common.Models;

public class TokenRequestModel
{
    [EmailAddress]
    public required string  Email { get; set; }
    public required string Password { get; set; }
    public bool GoogleAuth { get; set; } = false;
}
