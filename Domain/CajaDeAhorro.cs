using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dsw2025Ej8.Domain
{
    public class CajaDeAhorro : CuentaBancaria
    {
        public decimal TasaDeInteres { get; set; }
        public CajaDeAhorro(string numero, decimal saldo, string[] titulares)
            : base(numero, saldo, titulares) 
        {
        }
        public override TipoCuenta GetTipo() => TipoCuenta.CajaDeAhorro;
        public override void Depositar(decimal monto)
        {
            Saldo += monto;
        }

        public override void Retirar(decimal monto)
        {
            Saldo -= monto;
        }
        
        public override void AplicarInteres()
        {
            Saldo += Saldo * TasaDeInteres;
        }
    }
}
