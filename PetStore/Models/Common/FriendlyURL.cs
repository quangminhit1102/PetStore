using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace PetStore.Models.Common
{
    public class FriendlyURL
    {
        public static string RemapInternationalCharToAscii(char c)
        {
            string s = c.ToString().ToLowerInvariant();
            if ("àáảãạâấầẩẫậắằẳẵặ".Contains(s))
            {
                return "a";
            }
            else if ("èéẻẽẹêếềểễệ".Contains(s))
            {
                return "e";
            }
            else if ("ìíỉĩị".Contains(s))
            {
                return "i";
            }
            else if ("òóôõỏọốồổỗộ".Contains(s))
            {
                return "o";
            }
            else if ("ùúũụủ".Contains(s))
            {
                return "u";
            }
            else if ("ýỷỹỳỵ".Contains(s))
            {
                return "y";
            }
            else if (c == 'đ')
            {
                return "d";
            }
            else
            {
                return "";
            }
        }
        public static string URLFriendly(string title)
        {
            if (title == null) return "";

            const int maxlen = 80;
            int len = title.Length;
            bool prevdash = false;
            var sb = new StringBuilder(len);
            char c;

            for (int i = 0; i < len; i++)
            {
                c = title[i];
                if ((c >= 'a' && c <= 'z') || (c >= '0' && c <= '9'))
                {
                    sb.Append(c);
                    prevdash = false;
                }
                else if (c >= 'A' && c <= 'Z')
                {
                    // tricky way to convert to lowercase
                    sb.Append((char)(c | 32));
                    prevdash = false;
                }
                else if (c == ' ' || c == ',' || c == '.' || c == '/' ||
                    c == '\\' || c == '-' || c == '_' || c == '=')
                {
                    if (!prevdash && sb.Length > 0)
                    {
                        sb.Append('-');
                        prevdash = true;
                    }
                }
                else if ((int)c >= 128)
                {
                    int prevlen = sb.Length;
                    sb.Append(RemapInternationalCharToAscii(c));
                    if (prevlen != sb.Length) prevdash = false;
                }
                if (i == maxlen) break;
            }

            if (prevdash)
                return sb.ToString().Substring(0, sb.Length - 1);
            else
                return sb.ToString();
        }
    }
}