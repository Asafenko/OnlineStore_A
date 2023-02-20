using System.ComponentModel.DataAnnotations;

namespace OnlineStore.HttpModels.Requests;

public class RegisterRequest
{
    [Required] public string Name { get; set; }

    [Required(ErrorMessage = "Email is Required")]
    
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; }

    [Required(ErrorMessage = "Password is Required")]
    [DataType(DataType.Password)]
    [MinLength(6)]
    public string Password { get; set; }
}