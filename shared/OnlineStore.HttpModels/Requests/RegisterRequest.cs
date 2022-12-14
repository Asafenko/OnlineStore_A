using System.ComponentModel.DataAnnotations;

namespace OnlineStore.HttpModels.Requests;

public class RegisterRequest
{
    [Required] public string Name { get; set; }

    [Required(ErrorMessage = "Email is Required")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Password is Required")]
    [MinLength(8)]
    public string Password { get; set; }
}