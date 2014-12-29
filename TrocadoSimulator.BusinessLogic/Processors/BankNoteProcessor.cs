using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TrocadoSimulator.BusinessLogic.Processors {

    public class BankNoteProcessor : AbstractChangeProcessor {

        public override List<ChangeData> Calculate(int amountInCents) {

            if (amountInCents < 200) { return new List<ChangeData>(); }

            List<ChangeData> changeCollection = new List<ChangeData>();

            int[] availableValues = this.AvailableValues();

            //Lista ordenada do menor para o maior
            int[] orderedCoinCollection = availableValues.OrderByDescending(p => p).ToArray();

            //Iteração sobre todas as moedas
            foreach (int changeItem in orderedCoinCollection) {
                //Enquanto o valor do troco restante for igual ou menor ao valor da moeda, adiciona a lista
                while (changeItem <= amountInCents) {
                    //Se o troco restante for zero, retorna o valor
                    if (amountInCents < 200) {
                        return changeCollection;
                    }
                    //Recupera o item a ser adicionado na lista, caso já esteja apenas incrementa.
                    ChangeData changeInBankNotes = changeCollection.SingleOrDefault(o => o.AmountInCents == changeItem);
                    if (changeInBankNotes == null) {
                        changeCollection.Add(new ChangeData(changeItem, "cédula"));
                    }
                    else {
                        changeInBankNotes.Quantity++;
                    }
                    amountInCents = amountInCents - changeItem;
                }
            }
            return changeCollection;
        }

        /// <summary>
        /// Obtém os valores válidos para este processador.
        /// </summary>
        /// <returns>Retorna um array de int com os valores válidos.</returns>
        public override int[] AvailableValues() {

            return new int[] { 10000, 5000, 2000, 1000, 500, 200 };
        }

        public override string GetName()
        {
            return "Processador de cédulas";
        }
    }
}
