using System.Text;

namespace OnlineStore.WebApi.Configurations;

// Шаг 4: Добавление класса с параметрами токена
public class JwtConfig
{
    public string SigningKey { get; set; } = "";
    public TimeSpan LifeTime { get; set; }
    public string Audience { get; set; } = "";
    public string Issuer { get; set; } = "";

    public byte[] SigningKeyBytes => Encoding.UTF8.GetBytes(SigningKey);
}