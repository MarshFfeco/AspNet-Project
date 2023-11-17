using System.Security.Cryptography;
using System.Text;

namespace ProfessorHelp.Services.Criptografia;

public class EncryptPassword
{
    private readonly string _keyEncryptInitial;
    private readonly string _keyEncryptFinal;

    public EncryptPassword(string keyEncryptInitial, string keyEncryptFinal)
    {
        _keyEncryptInitial = keyEncryptInitial;
        _keyEncryptFinal = keyEncryptFinal;
    }

    public string Encrypt(string password)
    {
        string passwordWithKey = $"{_keyEncryptInitial}{password}{_keyEncryptFinal}";

        var bytes = Encoding.UTF8.GetBytes(passwordWithKey);
        var sha512 = SHA512.Create();
        byte[] hashByte = sha512.ComputeHash(bytes);
        return StringBytes(hashByte);
    }

    private static string StringBytes(byte[] bytes)
    {
        var sb = new StringBuilder();
        foreach(byte b in bytes)
        {
            var hex = b.ToString("x2");
            sb.Append(hex);
        }

        return sb.ToString();
    }
}
