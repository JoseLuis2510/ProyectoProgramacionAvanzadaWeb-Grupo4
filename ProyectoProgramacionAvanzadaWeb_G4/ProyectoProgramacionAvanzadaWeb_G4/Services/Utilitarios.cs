using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace ProyectoProgramacionAvanzadaWeb_G4.Services
{
    public class Utilitarios : IUtilitarios
    {
        private readonly IConfiguration _configuration;
        public Utilitarios(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string Encrypt(string texto)
        {
            byte[] iv = new byte[16];
            byte[] array;

            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(_configuration.GetSection("Start:LlaveCifrado").Value!);
                aes.IV = iv;

                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                using MemoryStream memoryStream = new();
                using CryptoStream cryptoStream = new(memoryStream, encryptor, CryptoStreamMode.Write);
                using (StreamWriter streamWriter = new(cryptoStream))
                {
                    streamWriter.Write(texto);
                }

                array = memoryStream.ToArray();
            }

            return Convert.ToBase64String(array);
        }

        public long ObtenerIdUsuario(IEnumerable<Claim> token)
        {
            if (token.Any())
            {
                var idUsuarioClaim = token.FirstOrDefault(c => c.Type == "IdUsuario");
                if (idUsuarioClaim != null && long.TryParse(idUsuarioClaim.Value, out long idUsuario))
                {
                    return idUsuario;
                }
            }

            return 0;
        }

    }
}
