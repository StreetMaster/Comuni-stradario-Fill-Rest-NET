using System;
using RestSharp;
using System.Windows.Forms;


namespace FillRest_NET
{
    /// <summary>
    /// Esempio di utilizzo del servizio WS FILL per la verifica e la normalizzazione degli indirizzi italiani 
    /// 
    /// L'end point del servizio è 
    ///     http://ec2-46-137-97-173.eu-west-1.compute.amazonaws.com/smrest/fill
    ///     
    /// Per l'utilizzo registrarsi sul sito http://streetmaster.it e richiedere la chiave per il servizio FILL 
    /// Il protocollo di comunicazione e' in formato JSON
    /// Per le comunicazioni REST è utilizzata la libreria opensource RestSharp (http://restsharp.org/)
    /// 
    ///  2016 - Software by StreetMaster (c)
    /// </summary>
    public partial class frmFillRest : Form
    {
        int currCand = 0;
        FillResponse outFill;

        public frmFillRest()
        {
            InitializeComponent();
        }


        private void btnCallVerify_Click(object sender, EventArgs e)
        {
            
            if (txtKey.Text==String.Empty)
            {
                MessageBox.Show("E' necessario specificare una chiave valida per il servizio FILL");
                txtKey.Focus();
                return;
            }

            Cursor = Cursors.WaitCursor;
            Application.DoEvents();


            // inizializzazione client del servizio FILL
            var clientFill = new RestSharp.RestClient();
            clientFill.BaseUrl = new Uri("http://ec2-46-137-97-173.eu-west-1.compute.amazonaws.com");

            var request = new RestRequest("smrest/webresources/fill", Method.GET);
            request.RequestFormat = DataFormat.Json;

            // valorizzazione input
            // per l'esempio viene valorizzato un insieme minimo dei parametri
            request.AddParameter("Key", txtKey.Text);
            request.AddParameter("Localita", txtInComune.Text);
            request.AddParameter("Cap", txtInCap.Text);
            request.AddParameter("Provincia", txtInProvincia.Text);
            request.AddParameter("Indirizzo", txtInIndirizzo.Text);
            request.AddParameter("Localita2", String.Empty);
            request.AddParameter("Dug", String.Empty);
            request.AddParameter("Civico", String.Empty);

            var response = clientFill.Execute<FillResponse>(request);
            outFill = response.Data;


            //  output generale
            txtOutEsito.Text = outFill.Norm.ToString();
            txtOutCodErr.Text = outFill.CodErr.ToString();
            txtOutNumCand.Text = outFill.NumCand.ToString();

            currCand = 0;

            // dettaglio del primo candidato se esiste
            // nella form di output e' riportato solo un sottoinsieme di tutti i valori restituiti
            if (outFill.Output.Count > 0)
            {
                txtOutCap.Text = outFill.Output[currCand].Cap;
                txtOutComune.Text = outFill.Output[currCand].Comune;
                txtOutFrazione.Text = outFill.Output[currCand].Frazione;
                txtOutIndirizzo.Text = outFill.Output[currCand].Indirizzo;
                txtOutProvincia.Text = outFill.Output[currCand].Prov;
                txtOutScoreComune.Text = outFill.Output[currCand].ScoreComune.ToString();
                txtOutScoreStrada.Text = outFill.Output[currCand].ScoreStrada.ToString();
                txtOutX.Text = outFill.Output[currCand].Geocod.X.ToString("0.#####");
                txtOutY.Text = outFill.Output[currCand].Geocod.Y.ToString("0.#####");
                txtOutRegione.Text = outFill.Output[currCand].Detail.Regione;
                txtOutIstatProv.Text = outFill.Output[currCand].Detail.IstatProv;
                txtOutIstatComune.Text = outFill.Output[currCand].Detail.IstatComune;
            }
            Cursor = Cursors.Default;
        }

        private void btnMovePrev_Click(object sender, EventArgs e)
        {
            // dettaglio del successivo candidato se esiste
            if (currCand > 0)
            {
                currCand-=1;
                txtOutCap.Text = outFill.Output[currCand].Cap;
                txtOutComune.Text = outFill.Output[currCand].Comune;
                txtOutFrazione.Text = outFill.Output[currCand].Frazione;
                txtOutIndirizzo.Text = outFill.Output[currCand].Indirizzo;
                txtOutProvincia.Text = outFill.Output[currCand].Prov;
                txtOutScoreComune.Text = outFill.Output[currCand].ScoreComune.ToString();
                txtOutScoreStrada.Text = outFill.Output[currCand].ScoreStrada.ToString();
                txtOutX.Text = outFill.Output[currCand].Geocod.X.ToString("0.#####");
                txtOutY.Text = outFill.Output[currCand].Geocod.Y.ToString("0.#####");
                txtOutRegione.Text = outFill.Output[currCand].Detail.Regione;
                txtOutIstatProv.Text = outFill.Output[currCand].Detail.IstatProv;
                txtOutIstatComune.Text = outFill.Output[currCand].Detail.IstatComune;
            }
        }

        private void btnMoveNext_Click(object sender, EventArgs e)
        {
            // dettagli del precedente candidato se esiste
            if(currCand + 1 < outFill.Output.Count)
            {
                currCand += 1;
                txtOutCap.Text = outFill.Output[currCand].Cap;
                txtOutComune.Text = outFill.Output[currCand].Comune;
                txtOutFrazione.Text = outFill.Output[currCand].Frazione;
                txtOutIndirizzo.Text = outFill.Output[currCand].Indirizzo;
                txtOutProvincia.Text = outFill.Output[currCand].Prov;
                txtOutScoreComune.Text = outFill.Output[currCand].ScoreComune.ToString();
                txtOutScoreStrada.Text = outFill.Output[currCand].ScoreStrada.ToString();
                txtOutX.Text = outFill.Output[currCand].Geocod.X.ToString("0.#####");
                txtOutY.Text = outFill.Output[currCand].Geocod.Y.ToString("0.#####");
                txtOutRegione.Text = outFill.Output[currCand].Detail.Regione;
                txtOutIstatProv.Text = outFill.Output[currCand].Detail.IstatProv;
                txtOutIstatComune.Text = outFill.Output[currCand].Detail.IstatComune;
            }
        }
    
    }
}
