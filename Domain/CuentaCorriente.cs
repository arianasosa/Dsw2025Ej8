using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dsw2025Ej8.Domain
{
    public class CuentaCorriente : CuentaBancaria
    {
        public decimal LimiteDeDescubierto { get; set; }
        public decimal Comision { get; set; }
        public CuentaCorriente(string numero, decimal saldo, string[] titulares,decimal comision)
            : base(numero, saldo, titulares)
        {       
            Comision = comision;
        }

        public override TipoCuenta GetTipo() => TipoCuenta.CuentaCorriente;

        public override void Depositar(decimal monto)
        {
            decimal montoNeto = monto - (monto * Comision);
            Saldo += montoNeto;
        }

        public override void Retirar(decimal monto)
        {
            if (Saldo - monto >= -LimiteDeDescubierto)
            {
                Saldo -= monto;

                if (Saldo < 0)
                {
                    Estado = Estado.Suspendida;
                }
            }            
        }

        public override void AplicarInteres()
        {            
        }
    }
}
