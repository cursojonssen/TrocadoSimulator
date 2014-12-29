using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrocadoSimulator.BusinessLogic.Processors
{
    public class CandyProcessor : AbstractChangeProcessor
    {
        public override List<ChangeData> Calculate(int amountInCents)
        {
            if (amountInCents < 75) { return new List<ChangeData>(); }

            List<ChangeData> changeDataCollection = new List<ChangeData>();

            int[] availableValues = this.AvailableValues();

            //Lista ordenada do menor para o maior
            int[] orderedCollection = availableValues.OrderByDescending(p => p).ToArray();

            //Iteração sobre todas as moedas
            foreach (int changeItem in orderedCollection)
            {
                //Enquanto o valor do troco restante for igual ou menor ao valor da moeda, adiciona a lista
                while (changeItem <= amountInCents)
                {
                    //Se o troco restante for zero, retorna o valor
                    if (amountInCents < 75)
                    {
                        return changeDataCollection;
                    }
                    //Recupera o item a ser adicionado na lista, caso já esteja apenas incrementa.
                    ChangeData changeData = changeDataCollection.SingleOrDefault(o => o.AmountInCents == changeItem);
                    if (changeData == null)
                    {
                        changeDataCollection.Add(new ChangeData(changeItem, "balinha"));
                    }
                    else
                    {
                        changeData.Quantity++;
                    }
                    amountInCents = amountInCents - changeItem;
                }
            }
            return changeDataCollection;
        }

        public override int[] AvailableValues()
        {
            return new int[] { 125, 120, 115, 110, 105, 100, 95, 90, 85, 80, 75 };
        }

        public override string GetName()
        {
            return "Processador de balinhas";
        }
    }
}
