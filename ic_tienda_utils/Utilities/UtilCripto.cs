using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace ic_tienda_utils.Utilities
{
    public class UtilCripto
    {
        // Método para encriptar un texto claro usando AES (Advanced Encryption Standard).
        public static string encriptar_AES(string clearText)
        {
            // Convierte el texto claro a bytes.
            byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
            // Crea un objeto AES para encriptar.
            using (Aes encryptor = Aes.Create())
            {
                // Deriva una clave secreta y un vector de inicialización (IV) usando Rfc2898.
                //Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(ObtnerSecretKey(), new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                // Obtiene la clave de 32 bytes.
                //encryptor.Key = pdb.GetBytes(32);
                // Obtiene el IV de 16 bytes.
                //encryptor.IV = pdb.GetBytes(16);
                // Deriva una clave secreta y un vector de inicialización (IV) usando el constructor seguro de Rfc2898DeriveBytes.
                string secretKey = ObtnerSecretKey();
                byte[] salt = new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 };

                using (var pdb = new Rfc2898DeriveBytes(secretKey, salt, 100_000, HashAlgorithmName.SHA256))
                {
                    // Obtiene la clave de 32 bytes.
                    encryptor.Key = pdb.GetBytes(32);
                    // Obtiene el IV de 16 bytes.
                    encryptor.IV = pdb.GetBytes(16);
                }

                // Crea un stream en memoria para almacenar los datos encriptados.
                using (MemoryStream ms = new MemoryStream())
                {
                    // Crea un CryptoStream para encriptar los datos.
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        // Escribe los datos a encriptar en el CryptoStream.
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        // Cierra el CryptoStream después de escribir.
                        cs.Close();
                    }
                    // Convierte el resultado a una cadena Base64.
                    clearText = Convert.ToBase64String(ms.ToArray());
                }
            }
            // Retorna el texto encriptado en Base64.
            return clearText;
        }

        // Método para desencriptar un texto cifrado usando AES.
        public static string desencriptar_AES(string cipherText)
        {
            // Convierte el texto cifrado de Base64 a bytes.
            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            // Crea un objeto AES para desencriptar.
            using (Aes encryptor = Aes.Create())
            {
                // Deriva una clave secreta y un IV usando Rfc2898.
                //Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(ObtnerSecretKey(), new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                // Obtiene la clave de 32 bytes.
                //encryptor.Key = pdb.GetBytes(32);
                // Obtiene el IV de 16 bytes.
                //encryptor.IV = pdb.GetBytes(16);

                // Deriva una clave secreta y un vector de inicialización (IV) usando el constructor seguro de Rfc2898DeriveBytes.
                string secretKey = ObtnerSecretKey();
                byte[] salt = new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 };

                using (var pdb = new Rfc2898DeriveBytes(secretKey, salt, 100_000, HashAlgorithmName.SHA256))
                {
                    // Obtiene la clave de 32 bytes.
                    encryptor.Key = pdb.GetBytes(32);
                    // Obtiene el IV de 16 bytes.
                    encryptor.IV = pdb.GetBytes(16);
                }

                // Crea un stream en memoria para almacenar los datos desencriptados.
                using (MemoryStream ms = new MemoryStream())
                {
                    // Crea un CryptoStream para desencriptar los datos.
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        // Escribe los datos a desencriptar en el CryptoStream.
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        // Cierra el CryptoStream después de escribir.
                        cs.Close();
                    }
                    // Convierte el resultado de vuelta a un string.
                    cipherText = Encoding.Unicode.GetString(ms.ToArray());
                }
            }
            // Retorna el texto desencriptado.
            return cipherText;
        }

        // Método privado para obtener la clave secreta desde un archivo de configuración (appsettings.json).
        private static string ObtnerSecretKey()
        {
            // Crea un builder de configuración.
            IConfigurationBuilder configurationBuild = new ConfigurationBuilder();
            // Agrega el archivo appsettings.json.
            configurationBuild = configurationBuild.AddJsonFile("appsettings.json");
            // Construye la configuración.
            IConfiguration configurationFile = configurationBuild.Build();

            // Lee el valor de la clave "SecretKey" desde el archivo de configuración.
#pragma warning disable CS8600
            string str = configurationFile.GetSection("SecretKey").Value;
            // Retorna la clave secreta.
#pragma warning disable CS8603
            return str;
        }
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
#pragma warning restore CS8603 // Possible null reference return.
    }
}