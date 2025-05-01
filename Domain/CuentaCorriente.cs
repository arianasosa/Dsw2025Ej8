using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Dsw2025Ej8.Domain.Excepciones;

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
            if (monto <= 0)
                throw new MontoNoValido();
            if (Estado != Estado.Activa)
                throw new CuentaNoActiva(Estado.ToString());

            decimal montoNeto = monto - (monto * Comision);
            Saldo += montoNeto;
        }

        public override void Retirar(decimal monto)
        {
            if (monto <= 0)
                throw new MontoNoValido();
            if (Estado != Estado.Activa)
                throw new CuentaNoActiva(Estado.ToString());

            if (Saldo - monto >= -LimiteDeDescubierto)
            {
                Saldo -= monto;

                if (Saldo < 0)
                {
                    Estado = Estado.Suspendida;
                }
            }
            else
            {
                Estado = Estado.Suspendida;
                throw new SaldoInsuficiente();
            }
        }

        public override void AplicarInteres()
        {            
        }
    }
}
