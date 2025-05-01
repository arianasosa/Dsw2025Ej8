using Dsw2025Ej8.Domain;
using static Dsw2025Ej8.Domain.Excepciones;

namespace Dsw2025Ej8
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var caja1 = new CajaDeAhorro("CA001", 1000m, new[] {"Priscila"} ) { TasaDeInteres = 0.05m };
            var caja2 = new CajaDeAhorro("CA002", 0m, new[] { "Ariana", "Agostina" }) { TasaDeInteres = 0.03m };

            var corriente1 = new CuentaCorriente("CC001", 500m, new [] { "Maximiliano" }, 0.02m) { LimiteDeDescubierto = 200m };
            var corriente2 = new CuentaCorriente("CC002", 100m, new[] { "Marcela" } , 0.05m) { LimiteDeDescubierto = 100m };

            var cuentas = new List<CuentaBancaria> { caja1, caja2, corriente1, corriente2 };

            Console.WriteLine("-----------------------");
            Console.WriteLine("------OPERACIONES------");
            Console.WriteLine("-----------------------");

            // INTENTAR DEPOSITAR UN MONTO INVALIDO
            try
            {
                caja1.Depositar(-50m);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[CAJA1] Excepcion: {ex.Message}");
            }

            // DEPOSITAR MONTO VALIDO
            try
            {
                caja1.Depositar(500m);
                Console.WriteLine($"[CAJA1] Saldo tras depósito: {caja1.Saldo}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[CAJA1] Excepción: {ex.Message}");
            }

            // RETIRAR MAS DEL SALDO (suspende la cuenta)
            try
            {
                caja1.Retirar(2000m);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[CAJA1] Excepción: {ex.Message}");
            }

            // INTENTAR OPERAR CON CUENTA SUSPENDIDA
            try
            {
                caja1.Depositar(100m);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[CAJA1] Excepción: {ex.Message}");
            }

            // APLICAR INTERES A CAJA ACTIVA
            try
            {
                caja2.Depositar(1000m);
                caja2.AplicarInteres();
                Console.WriteLine($"[CAJA2] Saldo tras interés: {caja2.Saldo}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[CAJA2] Excepción: {ex.Message}");
            }

            // CC CON COMISIONES Y RETIRO DENTRO DEL DESCUBIERTO
            try
            {
                corriente1.Depositar(1000m);
                corriente1.Retirar(1600m);  // Deja la cuenta con saldo negativo pero dentro del descubierto
                Console.WriteLine($"[CC1] Saldo final: {corriente1.Saldo}  -  Estado: {corriente1.Estado}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[CC1] Excepción: {ex.Message}");
            }

            // CC QUE EXCEDE EL DESCUBIERTO
            try
            {
                corriente2.Retirar(250m); 
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[CC2] Excepción: {ex.Message}");
            }

            caja1.AplicarInteres();
            caja2.AplicarInteres();

            Console.WriteLine("\n------------------------");
            Console.WriteLine("---RESUMEN DE CUENTAS---");
            Console.WriteLine("------------------------");

            foreach (var cuenta in cuentas)
            {
                var resumen = new
                {
                    cuenta.Numero,
                    Tipo = cuenta.GetTipo(),
                    cuenta.Saldo
                };

                Console.WriteLine($"Número : {resumen.Numero}, Tipo: {resumen.Tipo}, Saldo: {resumen.Saldo:C}");
            }
        }
    }
}
