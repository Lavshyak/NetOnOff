using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace NetOnOff
{
    internal class ConfigManager
    {
        public static string? name, login, password;
        private static readonly string configPath = Environment.CurrentDirectory + "//Config.NetOnOff";
        public static void Initialize()
        {
            if (!File.Exists(configPath))
            {
                //File.Create(configPath);
                Console.Write("Name of Internet connection (название соединения): ");
                name = Console.ReadLine();

                Console.Write("Login from provider (логин соединения): ");
                login = Console.ReadLine();

                Console.Write("Password from provider (пароль соединеня): ");
                password = Console.ReadLine();

                if (String.IsNullOrEmpty(name))
                {
                    Console.WriteLine("You entered an empty network name. is this part of your plan?  (вы указали пустое название сети. это часть вашего плана?) \n y/n ?");
                    if (!(Console.ReadKey().Key == ConsoleKey.Y))
                    {
                        return;
                    }
                    SomethingWentWrong();
                }

                using (StreamWriter sw = new StreamWriter(configPath))
                {
                    sw.WriteLine(name);
                    sw.WriteLine(login);
                    sw.WriteLine(password);

                    sw.Close();
                }
            }
            else
            {
                using (StreamReader sr = new StreamReader(configPath))
                {
                    name = sr.ReadLine();
                    login = sr.ReadLine();
                    password = sr.ReadLine();
                }
            }

        }


        public static void SomethingWentWrong()
        {
            Console.WriteLine("Something Went Wrong  (Что-то пошло не так)");
            Console.WriteLine("we will delete the config. let's create a new one (Мы удалим конфиг. давайте создадим новый)");
            Console.WriteLine("y/n ?");
            if (!(Console.ReadKey().Key == ConsoleKey.Y))
            {
                return;
            }

            if (File.Exists(configPath + "old"))
            {
                File.Delete(configPath + "old");
            }
            File.Move(configPath, configPath + "old");
            Console.WriteLine("old config:");
            Console.WriteLine(File.ReadAllText(configPath));
            Console.WriteLine("------------- \n\n");

            Initialize();
        }
    }
}
