using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Dsw2025Ej8.Domain.Excepciones;

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
            if (monto <= 0)
                throw new MontoNoValido();
            if (Estado != Estado.Activa)
                throw new CuentaNoActiva(Estado.ToString());
            
            Saldo += monto;
        }

        public override void Retirar(decimal monto)
        {
            if (monto <= 0)
                throw new MontoNoValido();
            if (Estado != Estado.Activa)
                throw new CuentaNoActiva(Estado.ToString());

            if (Saldo >= monto)
                Saldo -= monto;
            else
            {
                Estado = Estado.Suspendida;
                throw new SaldoInsuficiente();
            }

        }
        
        public override void AplicarInteres()
        {
            Saldo += Saldo * TasaDeInteres;
        }
    }
}
