using System.Configuration;

namespace Pinata
{
    public static class Globalization
    {
        public static string GetDefaultCulture()
        {
            return "pt-BR";
        }

        public static string GetValidCulture(string name)
        {
            if (string.IsNullOrEmpty(name))
                return GetDefaultCulture();

            if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["globalization"]))
            {
                string[] cultures = ConfigurationManager.AppSettings["globalization"].Split(';');

                //locate exact match
                foreach (string c in cultures)
                {
                    if (c.Equals(name))
                        return c;
                }

                //locate language match
                foreach (string c in cultures)
                {
                    if (c.StartsWith(name.Substring(0, 2)))
                    {
                        return c;
                    }
                }
            }

            return GetDefaultCulture();
        }

    }
}
