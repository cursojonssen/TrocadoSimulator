using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrocadoSimulator.BusinessLogic.Processors {

    public class CoinProcessor : AbstractChangeProcessor {

        public CoinProcessor() {
         
        }

        /// <summary>
        /// Calcula a quantidade de moedas para o troco
        /// </summary>
        /// <param name="amountInCents">Valor em centavos</param>
        /// <returns>Lista das moedas</returns>
        public override List<ChangeData> Calculate(int amountInCents) {

            if (amountInCents < 0) { throw new ArgumentOutOfRangeException("amountInCents", "Value must be equals  or greater than zero."); }

            List<ChangeData> changeInCoinsCollection = new List<ChangeData>();

            int[] availableValues = this.AvailableValues();

            //Lista ordenada do menor para o maior
            int[] orderedCoinCollection = availableValues.OrderByDescending(p => p).ToArray();

            //Iteração sobre todas as moedas
            foreach (int coin in orderedCoinCollection) {
                //Enquanto o valor do troco restante for igual ou menor ao valor da moeda, adiciona a lista
                while (coin <= amountInCents) {

                    //Se o troco restante for zero, retorna o valor
                    if (amountInCents == 0) {
                        return changeInCoinsCollection;
                    }

                    //Recupera o item a ser adicionado na lista, caso já esteja apenas incrementa.
                    ChangeData changeInCoins = changeInCoinsCollection.SingleOrDefault(o => o.AmountInCents == coin);

                    if (changeInCoins == null) {
                        changeInCoinsCollection.Add(new ChangeData(coin, "moeda"));
                    }
                    else {
                        changeInCoins.Quantity++;
                    }
                    amountInCents = amountInCents - coin;
                }
            }

            return changeInCoinsCollection;
        }

        /// <summary>
        /// Obtém os valores válidos para este processador.
        /// </summary>
        /// <returns>Retorna um array de int com os valores válidos.</returns>
        public override int[] AvailableValues() {

            return new int[] { 100, 50, 25, 10, 5, 1 };
        }

        public override string GetName()
        {
            return "Processador de moedas";
        }
    }
}
