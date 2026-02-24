using System;

namespace CRCorreaInfo
{
    public class clsEmpresaFolhaInfo
    {
        Int32 _id;
        Int32 _empresa;
        Decimal _qtdehorasmes;
        Decimal _adiantamento;
        Int32 _competenciade;
        Int32 _competenciaate;
        String _bancohoras;
        Int32 _atraso;
        Int32 _atrasodesconta;
        Int32 _extraminutos;
        Int32 _tipoextra;
        String _metodoextra;
        Int32 _idempresamedica;
        String _pontoeletronico;
        String _adiantamentomes;

        public Int32 id
        {
            get { return _id; }
            set { _id = value; }
        }

        public Int32 empresa
        {
            get { return _empresa; }
            set { _empresa = value; }
        }

        public Decimal qtdehorasmes
        {
            get { return _qtdehorasmes; }
            set { _qtdehorasmes = value; }
        }

        public Decimal adiantamento
        {
            get { return _adiantamento; }
            set { _adiantamento = value; }
        }

        public Int32 competenciade
        {
            get { return _competenciade; }
            set { _competenciade = value; }
        }

        public Int32 competenciaate
        {
            get { return _competenciaate; }
            set { _competenciaate = value; }
        }

        public String bancohoras
        {
            get { return _bancohoras; }
            set { _bancohoras = value; }
        }

        public Int32 atraso
        {
            get { return _atraso; }
            set { _atraso = value; }
        }

        public Int32 atrasodesconta
        {
            get { return _atrasodesconta; }
            set { _atrasodesconta = value; }
        }

        public Int32 extraminutos
        {
            get { return _extraminutos; }
            set { _extraminutos = value; }
        }

        public Int32 tipoextra
        {
            get { return _tipoextra; }
            set { _tipoextra = value; }
        }

        public String metodoextra
        {
            get { return _metodoextra; }
            set { _metodoextra = value; }
        }

        public Int32 idempresamedica
        {
            get { return _idempresamedica; }
            set { _idempresamedica = value; }
        }

        public String pontoeletronico
        {
            get { return _pontoeletronico; }
            set { _pontoeletronico = value; }
        }

        public String adiantamentomes
        {
            get { return _adiantamentomes; }
            set { _adiantamentomes = value; }
        }
    }
}
