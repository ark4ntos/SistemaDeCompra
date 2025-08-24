using System;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Channels;

class Program
{
    public static void Main()
    {
        //Booleanos
        bool isRunning = true;
        //Variables usuario
        string[] userName = new string[3];
        int[] rTN = new int[3];
        int[] birthDate = new int[3];
        string[] userType = new string[3];
        //Variables producto
        int[] productId = new int[3];
        string[] productName = new string[3];
        int[] stockQuantity = new int[3];
        double[] productPrice = new double[3];

        while (isRunning)
        {
            Console.WriteLine("Menú principal");
            Console.WriteLine("1. Registrar usuario");
            Console.WriteLine("2. Buscar usuario");
            Console.WriteLine("3. Registrar producto");
            Console.WriteLine("4. Comprar producto");
            Console.WriteLine("0. Salir");
            int menu = Convert.ToInt32(Console.ReadLine());

            switch (menu)
            {
                case 1:
                    RegistrarUsuario(userName, rTN, birthDate, userType); break;
                case 2:
                    BuscarUsuario(userName, rTN, birthDate, userType); break;
                case 3:
                    RegistrarProducto(productId, productName, stockQuantity, productPrice); break;
                case 0:
                    isRunning = false; break;
                default:
                    Console.WriteLine("Opción inválida \n Por favor, ingrese una opción válida"); break;
            }
            Console.Clear();
        }
    }

    public static void RegistrarUsuario(string[] userName, int[] rTN, int[] birthDate, string[] userType)
    {
        Dictionary<int, string> userTypes = new Dictionary<int, string>()
        {
            {1, "Cliente natural"},
            {2, "Cliente fiscal" }
        };

        Console.WriteLine("Registrar usuario");
        for (int i= 0; i < 3; i++)
        {
            Console.WriteLine("Ingrese el nombre del usuario #"+ (i+1));
            userName[i] = Console.ReadLine() ?? "Sin nombre";
            Console.WriteLine("Ingrese el RTN del usuario #"+ (i+1));
            rTN[i] = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Ingrese la fecha de nacimiento");
            birthDate[i] = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Ingrese el tipo de cliente");
            Console.WriteLine("1. Cliente natural");
            Console.WriteLine("2. Cliente fiscal");
            int selectedOption = Convert.ToInt32(Console.ReadLine());
            userType[i] = userTypes[selectedOption];
        }
    }

    public static void BuscarUsuario(string[] userName, int[] rTN, int[] birthDate, string[] userType)
    {
        Console.WriteLine("Buscar usuario");
        Console.WriteLine("Ingrese el ID del usuario");
        int iD = Convert.ToInt32(Console.ReadLine());
        Console.WriteLine("Nombre del usuario #"+ (iD) +": "+ userName[iD-1]);
        Console.WriteLine("ID: "+ rTN[iD-1]);
        Console.WriteLine("Fecha de nacimiento: "+ birthDate[iD-1]);
        Console.WriteLine("Tipo de cliente: "+ userType[iD-1]);
        Console.ReadLine();
    }

    public static void RegistrarProducto(int[] productId, string[] productName, int[] stockQuantity, double[] productPrice)
    {
        Console.WriteLine("Registrar producto");

        for (int i= 0; i < 3; i++)
        {
            Console.WriteLine("Ingrese el código del producto #"+ (i+1));
            productId[i] = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Ingrese el nombre del producto #"+ (i+1));
            productName[i] = Console.ReadLine() ?? "Sin nombre";
            Console.WriteLine("Ingrese la cantidad disponible en stock");
            stockQuantity[i] = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Ingrese el precio del producto");
            productPrice[i] = Convert.ToDouble(Console.ReadLine());
        }
    }
}