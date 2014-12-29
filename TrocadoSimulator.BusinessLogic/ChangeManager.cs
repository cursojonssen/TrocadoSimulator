using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrocadoSimulator.BusinessLogic.DataContracts;
using TrocadoSimulator.BusinessLogic.Processors;

namespace TrocadoSimulator.BusinessLogic
{
    public delegate void ProcessorDoneEventHandler(object sender, ProcessorDoneEventArgs e);

    public class ChangeManager
    {
        /// <summary>
        /// Evento disparado quando um processador finalizar o cálculo.
        /// </summary>
        public event ProcessorDoneEventHandler OnProcessorDone;

        public ChangeResponse CalculateChange(ChangeRequest changeRequest) {

            ChangeResponse changeResponse = new ChangeResponse();

            try {
                // Valida os dados recebidos no request.
                if (changeRequest.IsValid == false) {
                    changeResponse.ErrorReport = changeRequest.Errors;
                    changeResponse.Success = false;
                    return changeResponse;
                }

                // Calcula o troco.
                int originalChangeAmount = changeRequest.PaidAmount - changeRequest.ProductAmount;

                // Instancia coleção de troco
                List<ChangeData> changeDataCollection = new List<ChangeData>();

                // enquanto houver troco
                int changeAmount = originalChangeAmount;
                while (changeAmount > 0) {

                    // seleciona o processador
                    AbstractChangeProcessor processor = ProcessorFactory.Create(changeAmount);

                    // calcula o troco
                    List<ChangeData> processorChangeDataCollection = processor.Calculate(changeAmount);

                    // adiciona o resultado do troco do processador a coleção da resposta
                    changeDataCollection.AddRange(processorChangeDataCollection);

                    // Soma o valor de troco processado
                    int processorChangeAmount = processorChangeDataCollection.Sum(i => i.Quantity * i.AmountInCents);

                    // Dispara um evento informando que o processador terminou o cálculo.
                    if (this.OnProcessorDone != null) { this.OnProcessorDone(this, new ProcessorDoneEventArgs(processor.GetName(), processorChangeAmount)); }

                    // Diminui do troco o valor processado
                    changeAmount -= processorChangeAmount;
                }            

                // Atribui a coleção de trocos a resposta
                changeResponse.ChangeDataCollection = changeDataCollection;
                changeResponse.ChangeAmount = originalChangeAmount;

                // Atribui true ao success
                changeResponse.Success = true;
            }
            catch (Exception ex) {

                // TODO: Escrever log.

                ErrorReport errorReport = new ErrorReport();
                errorReport.Field = "Internal Error";
                errorReport.Message = "Ocorreu um erro interno.";

#if DEBUG
                errorReport.Message += " DEBUG:" + ex.ToString();
#endif
                changeResponse.ErrorReport.Add(errorReport);

                // Retorna sucesso falso
                changeResponse.Success = false;
            }

            return changeResponse;
        }
    }
}
