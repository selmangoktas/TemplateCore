using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace TemplateCore.WebUI.Tools
{
    public class GeneralTool
    {
        public static string urlConvert(string text)
        {
            return text.ToLower().Replace(" ","-").Replace("&","-").Replace("ü","u").Replace("ı", "i").Replace("ö", "o").Replace("ş", "s").Replace("ç", "c").Replace("ğ", "g").Replace("%", "").Replace("'", "").Replace("_", "-");
        }
        public static string getMD5(string _text)
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] hash = md5.ComputeHash(Encoding.UTF8.GetBytes(_text));
                return BitConverter.ToString(hash).Replace("-", "");
            }
        }
    }
}
