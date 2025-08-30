using System;
using System.Diagnostics.CodeAnalysis;
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
        //Variables compra
        int[,] quantityAdded = new int[3, 3];
        int[,] sumQuantityAdded = new int[3, 3];
        double[] cartSubtotal = new double[3];
        double[] cartTotal = new double[3];
        char continueShopping;
        

        while (isRunning)
        {
            Console.WriteLine("Menú principal");
            Console.WriteLine("1. Registrar usuario");
            Console.WriteLine("2. Buscar usuario");
            Console.WriteLine("3. Registrar producto");
            Console.WriteLine("4. Comprar producto");
            Console.WriteLine("5. Facturar");
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
                case 4:
                    ComprarProducto(stockQuantity, productName, productPrice, quantityAdded, sumQuantityAdded, cartSubtotal); break;
                case 5:
                    Facturar(userName, sumQuantityAdded, productName, productPrice, cartSubtotal, cartTotal);
                    Console.WriteLine("¿Desea agregar otro producto al carrito? [Y/N]");
                    continueShopping = char.ToLower(Convert.ToChar(Console.ReadLine()!));
                    if (continueShopping == 'y')
                    {
                        ComprarProducto(stockQuantity, productName, productPrice, quantityAdded, sumQuantityAdded, cartSubtotal);
                        Facturar(userName, sumQuantityAdded, productName, productPrice, cartSubtotal, cartTotal);
                    }
                    else if (continueShopping == 'n')
                    {
                        //ImprimirFactura(userName, rTN, birthDate, userType);
                    }
                    break;

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

    public static void ComprarProducto(int[] stockQuantity, string[] productName, double[] productPrice, int[,] quantityAdded, int[,] sumQuantityAdded, double[] cartSubtotal)
    {
        Console.WriteLine("Comprar producto");
        bool approved = false;
        Console.WriteLine("Ingrese el ID del usuario que desea comprar");
        int iD = Convert.ToInt32(Console.ReadLine());

        while (!approved)
        {
            Console.WriteLine("#\tDisp\tProducto\tPrecio");
            for (int i = 0; i < 3; i++)
            {
                Console.WriteLine($"{i + 1}.\t{stockQuantity[i]}\t{productName[i]}\t\t${productPrice[i]}");
            }
            Console.WriteLine("Ingrese el producto que desea agregar al carrito #"+ iD);
            int selectedProduct = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Ingrese la cantidad que desea agregar");
            quantityAdded[iD, selectedProduct-1] = Convert.ToInt32(Console.ReadLine());

            if (stockQuantity[selectedProduct-1] < quantityAdded[iD, selectedProduct-1])
            {
                Console.WriteLine("No hay suficiente stock");
                quantityAdded[iD,selectedProduct-1] = 0;
                Console.ReadKey();
                Console.WriteLine("Comprar producto");
            }
            else
            {
                stockQuantity[selectedProduct-1] -= quantityAdded[iD, selectedProduct-1];
                approved = true;
                sumQuantityAdded[iD, selectedProduct-1] += quantityAdded[iD, selectedProduct-1];
                Console.WriteLine($"Cantidad total del producto{productName[selectedProduct-1]} agregada: {sumQuantityAdded[iD, selectedProduct-1]}");

                for (int i = 0; i < 3; i++)
                {
                    cartSubtotal[iD] += productPrice[i] * sumQuantityAdded[iD, i];
                }
                Console.WriteLine($"Valor total del carrito: ${cartSubtotal[iD]}");
            }
        }
    }

    public static void Facturar(string[] userName, int[,] sumQuantityAdded, string[] productName, double[] productPrice, double[] cartSubtotal, double[] cartTotal)
    {
        Console.WriteLine("Facturar");
        Console.WriteLine("Ingrese el ID del cliente que desea facturar");
        int iD = (Convert.ToInt32(Console.ReadLine()) - 1);
        Console.WriteLine("Nombre del cliente: " + userName[iD] +"\n");
        Console.WriteLine("Producto\tCant.\tPrice");

        for (int selectedProduct = 0; selectedProduct < 3; selectedProduct++)
        {
            if (sumQuantityAdded[iD, selectedProduct] > 0)
            {
                Console.WriteLine($"{productName[selectedProduct]}\t{sumQuantityAdded[iD, selectedProduct]}\t${productPrice[selectedProduct]}");
            }
        }
        Console.WriteLine("Subtotal: $"+ cartSubtotal[iD]);
        double tax = cartSubtotal[iD] * 0.15;
        Console.WriteLine("ISV (15%): "+ tax);
        cartTotal[iD] = cartSubtotal[iD] + tax;
        Console.WriteLine("Total a pagar: $"+ cartTotal[iD]);
    }

    public static void ImprimirFactura(string[] userName, int[] rTN, int[] birthDate, string[] userType, double[] cartTotal)
    {
        bool isValidBill = false;
        while (!isValidBill)
        {
            Console.WriteLine("Ingrese el billete con el que desea facturar");
            double bill = Convert.ToDouble(Console.ReadLine());
            if (bill >= cartTotal[iD])
        }

            Console.WriteLine("Facturar");
        Console.WriteLine("Ingrese el ID del cliente que desea facturar");
        int iD = (Convert.ToInt32(Console.ReadLine())-1);
        Console.WriteLine("Nombre del cliente: "+ userName[iD]);
        Console.WriteLine("RTN: "+ rTN[iD]);
        Console.WriteLine("Fecha de nacimiento: "+ birthDate[iD]);
        Console.WriteLine("Tipo de usuario: "+ userType[iD] +"\n");
        Console.WriteLine("Producto \t Cantidad \t Precio");

        
    }
}