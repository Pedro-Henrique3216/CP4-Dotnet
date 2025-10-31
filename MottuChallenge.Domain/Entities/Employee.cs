using System.Security.Cryptography;
using System.Text;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MottuChallenge.Domain.Validations;

namespace MottuChallenge.Domain.Entities;

public class Employee
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; private set; }

    [BsonElement("name")]
    public string Name { get; set; }

    [BsonElement("email")]
    public string Email { get; set; }

    [BsonRepresentation(BsonType.ObjectId)]
    [BsonElement("yardId")]
    [BsonIgnore]
    public string YardId { get; set; }

    [BsonIgnore]
    public Yard Yard { get; set; }

    [BsonElement("passwordHash")]
    public string PasswordHash { get; private set; }

    [BsonElement("passwordSalt")]
    public string PasswordSalt { get; private set; }

    protected Employee() { }

    public Employee(string name, string email, Guid yardId, string password)
    {
        Guard.AgainstNullOrWhitespace(name, nameof(name), nameof(Employee));
        Guard.AgainstNullOrWhitespace(email, nameof(email), nameof(Employee));
        Guard.AgainstNullOrWhitespace(password, nameof(password), nameof(Employee));

        Id = ObjectId.GenerateNewId().ToString();
        Name = name;
        Email = email;
        YardId = yardId.ToString();
        CreatePasswordHash(password);
    }

    private void CreatePasswordHash(string password)
    {
        using var hmac = new HMACSHA512();
        PasswordSalt = Convert.ToBase64String(hmac.Key);
        PasswordHash = Convert.ToBase64String(hmac.ComputeHash(Encoding.UTF8.GetBytes(password)));
    }

    public bool VerifyPassword(string password)
    {
        if (string.IsNullOrWhiteSpace(password))
            return false;

        using var hmac = new HMACSHA512(Convert.FromBase64String(PasswordSalt));
        var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        var computedHashString = Convert.ToBase64String(computedHash);

        return computedHashString == PasswordHash;
    }

    public void UpdatePassword(string newPassword)
    {
        Guard.AgainstNullOrWhitespace(newPassword, nameof(newPassword), nameof(Employee));
        CreatePasswordHash(newPassword);
    }
}
