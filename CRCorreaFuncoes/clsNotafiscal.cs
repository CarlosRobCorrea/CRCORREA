using System;


namespace CRCorreaFuncoes
{
    public struct clsNotafiscal
    {
        // Dados da Origem e do Destino
        private Int32 _iduforigem;
        public Int32 iduforigem
        {
            get { return _iduforigem; }
            set { _iduforigem = value; }
        }

        private Int32 _idufdestino;
        public Int32 idufdestino
        {
            get { return _idufdestino; }
            set { _idufdestino = value; }
        }

        private Int32 _idcliente;
        public Int32 idcliente
        {
            get { return _idcliente; }
            set { _idcliente = value; }
        }

        private Boolean _isentoipi;
        public Boolean isentoipi
        {
            get { return _isentoipi; }
            set { _isentoipi = value; }
        }

        private Boolean _revendedor;
        public Boolean revendedor
        {
            get { return _revendedor; }
            set { _revendedor = value; }
        }

        private Boolean _consumo;
        public Boolean consumo
        {
            get { return _consumo; }
            set { _consumo = value; }
        }

        private Boolean _contribuinte;
        public Boolean contribuinte
        {
            get { return _contribuinte; }
            set { _contribuinte = value; }
        }

        private Boolean _arealivrecomercio;
        public Boolean arealivrecomercio
        {
            get { return _arealivrecomercio; }
            set { _arealivrecomercio = value; }
        }

        // Qtdes
        private Decimal _qtde;
        public Decimal qtde
        {
            get { return _qtde; }
            set { _qtde = value; }
        }

        private Decimal _qtdesaldo;
        public Decimal qtdesaldo
        {
            get { return _qtdesaldo; }
            set { _qtdesaldo = value; }
        }

        private Decimal _pesounit;
        public Decimal pesounit
        {
            get { return _pesounit; }
            set { _pesounit = value; }
        }

        private Decimal _pesototal;
        public Decimal pesototal
        {
            get { return _pesototal; }
            set { _pesototal = value; }
        }

        
        private Decimal _preco;
        public Decimal preco
        {
            get { return _preco; }
            set { _preco = value; }
        }

        private Decimal _totalmercadoria;
        public Decimal totalmercadoria
        {
            get { return _totalmercadoria; }
        }

        private Decimal _totalprevisto;
        public Decimal totalprevisto
        {
            get { return _totalprevisto; }
        }

        private Decimal _totalnota;
        public Decimal totalnota
        {
            get { return _totalnota; }
        }

        // Icms
        private String _origem;
        public String origem
        {
            get { return _origem; }
            set { _origem = value; }
        }

        private String _icmscst;
        public String icmscst
        {
            get { return _icmscst; }
            set { _icmscst = value; }
        }

        private Decimal _icmsbc;
        public Decimal icmsbc
        {
            get { return _icmsbc; }
        }

        private Decimal _icms;
        public Decimal icms
        {
            get { return _icms; }
            set { _icms = value; }
        }

        private Decimal _icmsreducao;
        public Decimal icmsreducao
        {
            get { return _icmsreducao; }
            set { _icmsreducao = value; }
        }

        private Decimal _basemp;
        public Decimal basemp
        {
            get { return _basemp; }
            set { _basemp = value; }
        }


        private Decimal _icmstotal;
        public Decimal icmstotal
        {
            get { return _icmstotal; }
        }

        // Icms - ST
        private Decimal _icmsstbc;
        public Decimal icmsstbc
        {
            get { return _icmsstbc; }
        }

        private Decimal _icmsst;
        public Decimal icmsst
        {
            get { return _icmsst; }
            set { _icmsst = value; }
        }

        private Decimal _icmsstreducao;
        public Decimal icmsstreducao
        {
            get { return _icmsstreducao; }
            set { _icmsstreducao = value; }
        }

        private Decimal _icmsstmva;
        public Decimal icmsstmva
        {
            get { return _icmsstmva; }
            set { _icmsstmva = value; }
        }

        private Decimal _icmssttotal;
        public Decimal icmssttotal
        {
            get { return _icmssttotal; }
        }

        // Ipi
        private String _ipicst;
        public String ipicst
        {
            get { return _ipicst; }
            set { _ipicst = value; }
        }

        private Int32 _idipi;
        public Int32 idipi
        {
            get { return _idipi; }
            set { _idipi = value; }
        }

        private Decimal _ipibc;
        public Decimal ipibc
        {
            get { return _ipibc; }
        }

        private Decimal _ipi;
        public Decimal ipi
        {
            get { return _ipi; }
            set { _ipi = value; }
        }

        private Decimal _ipitotal;
        public Decimal ipitotal
        {
            get { return _ipitotal; }
        }

        // Pis/Pasep
        private String _piscst;
        public String piscst
        {
            get { return _piscst; }
            set { _piscst = value; }
        }

        private Decimal _pisbc;
        public Decimal pisbc
        {
            get { return _pisbc; }
        }

        private Decimal _pis;
        public Decimal pis
        {
            get { return _pis; }
            set { _pis = value; }
        }

        private Decimal _pistotal;
        public Decimal pistotal
        {
            get { return _pistotal; }
        }

        // Cofins
        private String _cofinscst;
        public String cofinscst
        {
            get { return _cofinscst; }
            set { _cofinscst = value; }
        }

        private Decimal _cofinsbc;
        public Decimal cofinsbc
        {
            get { return _cofinsbc; }
        }

        private Decimal _cofins;
        public Decimal cofins
        {
            get { return _cofins; }
            set { _cofins = value; }
        }

        private Decimal _cofinstotal;
        public Decimal cofinstotal
        {
            get 
            { 
                _cofinstotal = clsVisual.Truncar(_cofinstotal, 2);
                
                return _cofinstotal;
            }
        }

        public Boolean valoroutrassomabcicms;
        private Decimal _valoroutras;
        public Decimal valoroutras
        {
            get 
            { 
                _valoroutras = clsVisual.Truncar(_valoroutras, 2);
                return _valoroutras;
            }
            set { _valoroutras = clsVisual.Truncar(value, 2); }
        }

        public Boolean valorfretesomabcicms;
        private Decimal _valorfrete;
        public Decimal valorfrete
        {
            get 
            { 
                _valorfrete = clsVisual.Truncar(_valorfrete, 2);
                return _valorfrete;
            }
            set { _valorfrete = clsVisual.Truncar(value, 2); }
        }

        public Boolean valorsegurosomabcicms;
        private Decimal _valorseguro;
        public Decimal valorseguro
        {
            get 
            { 
                _valorseguro = clsVisual.Truncar(_valorseguro, 2);
                return _valorseguro;
            }
            set { _valorseguro = clsVisual.Truncar(value, 2); }
        }

        public void Calcular()
        {
            //Decimal ipialiquota;
            Boolean ipint;

            //Decimal cnaealiquota;
            //Int32 cnaeid = Parser.Int32Parse(clsProcedures.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select idcnae from cliente where id=" + _idcliente));
            //cnaealiquota = Parser.DecimalParse(clsProcedures.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select iva from tab_cnae_uf where idtabcnae=" + cnaeid + " and iduf=" + _idufdestino));

            String ufdestino;
            String uforigem;

            ufdestino = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select estado from estados where id=" + _idufdestino);
            uforigem = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select estado from estados where id=" + _iduforigem);

            //ipialiquota = Parser.DecimalParse(clsProcedures.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select aliquota from ipi where id = " + _idipi));
            String result = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select aliquota from ipi where id = " + _idipi);

            if (result != null)
            {
                result = result.ToUpper();
                ipint = (result == "NT");
            }
            else
            {
                ipint = false;
            }

            _qtde = clsVisual.Truncar(_qtde, 5);
            _preco = clsVisual.Truncar(_preco, 6);

            // Se for para Área de Livre Comércio e isento de imposto, realiza o desconto do preço
            //if (_icmscst == "40" && _arealivrecomercio == true)
            //{
            //    preco = preco - (0.07 * preco);
            //    preco = clsVisual.Truncar(preco, 5);
            //}

            _totalmercadoria = _qtde * _preco;
            _pesototal = _qtde * _pesounit;

            if (ufdestino == "EX")
            {
                _ipi = 0;
                _ipicst = "54";
            }
            else if (_isentoipi == true)
            {
                //else if (_isentoipi == true || (_revendedor == false && _consumo == false))
                _ipi = 0;
                if (_ipicst == "50" ||
                    _ipicst == "51" ||
                    _ipicst == "53")    // 54 e 55 não devem ser modificados
                {
                    _ipicst = "52";
                }
            }
            else if (_ipicst == "51" ||
                     _ipicst == "52" ||
                     _ipicst == "53" ||
                     _ipicst == "54" ||
                     _ipicst == "55" ||
                     _ipicst == "01" ||
                     _ipicst == "02" ||
                     _ipicst == "03" ||
                     _ipicst == "04" ||
                     _ipicst == "05")
            {
                _ipi = 0;
            }
            else
            {
                //_ipi = ipialiquota;

                if (ipint == true)
                {
                    _ipi = 0;
                    _ipicst = "53";
                }
                else if (_ipi == 0)
                {
                    _ipicst = "51";
                }
                else if (_ipicst == "51" && _ipi > 0)
                {
                    _ipicst = "50";
                }
            }

            if (_ipicst == "00" ||
                _ipicst == "49" ||
                _ipicst == "50" ||
                _ipicst == "99")
            {
                if (_origem == "0")
                {
                    _ipibc = _totalmercadoria;
                }
                else
                {
                    _ipibc = 0;
                }
            }
            else
            {
                _ipibc = 0;
            }

            _ipitotal = (_ipi / 100) * _ipibc;

            if (_icmscst == "30" ||
                _icmscst == "40" ||
                _icmscst == "41" ||
                _icmscst == "50" ||
                _icmscst == "51")
            {
                icms = 0;
            }
            else
            {
                //icms = Parser.DecimalParse(clsProcedures.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select aliquota from ipiestadosicms where idipi=" + idipi + " and idestado = " + _iduforigem + " and idestadodestino = " + _idufdestino));
            }

            if (_icmscst == "20" ||
                _icmscst == "70")
            {
                ///_icmsreducao = Parser.DecimalParse(clsProcedures.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select reducao from ipiestadosicms where idipi=" + idipi + " and idestado = " + _iduforigem + " and idestadodestino = " + _idufdestino));
            }

            if ((_icmscst == "30" || _icmscst == "70") && ufdestino != "EX" && ufdestino != uforigem && consumo == true)
            {
                if (ufdestino == "MT")
                {
                    //_icmsstmva = cnaealiquota;
                }
                else
                {
                    //_icmsstmva = Parser.DecimalParse(clsProcedures.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select iva from ipiestadosicms where idipi=" + idipi + " and idestado = " + _iduforigem + " and idestadodestino = " + _idufdestino));
                }

                //_icmsst = Parser.DecimalParse(clsProcedures.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select aliquota from ipiestadosicms where idipi=" + idipi + " and idestado = " + _idufdestino + " and idestadodestino = " + _idufdestino));
                //_icmsstreducao = Parser.DecimalParse(clsProcedures.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select reducaoiva from ipiestadosicms where idipi=" + idipi + " and idestado = " + _idufdestino + " and idestadodestino = " + _idufdestino));
            }

            if (_icms > 0)
            {
                // contribuinte == false
                if (consumo == true)
                {
                    _icmsbc = _totalmercadoria + _ipitotal;
                }
                else
                {
                    _icmsbc = _totalmercadoria;
                }

                if (valoroutrassomabcicms == true)  _icmsbc += valoroutras;
                if (valorfretesomabcicms == true)   _icmsbc += valorfrete;
                if (valorsegurosomabcicms == true)  _icmsbc += valorseguro;

                if (_icmsreducao > 0)
                {
                    _icmsbc = _icmsbc - (_icmsbc * (_icmsreducao / 100));
                }

                _icmstotal = _icmsbc * (_icms / 100);
            }
            else
            {
                _icmsbc = 0;
                _icmsreducao = 0;
                _icmstotal = 0;
            }

            if (_icmsst > 0 && ufdestino != "EX" && (_icmscst == "10" || _icmscst == "30" || _icmscst == "70" || _icmscst == "90"))
            {
                _icmsstbc = totalmercadoria + ipitotal;
                _icmsstbc = _icmsstbc + (_icmsstbc * (_icmsstmva / 100));

                if (_icmsstreducao > 0)
                {
                    _icmsstbc = _icmsstbc - (_icmsstbc * (_icmsstreducao / 100));
                }

                _icmssttotal = _icmsstbc * (icmsst / 100);
                _icmssttotal = _icmssttotal - _icmstotal;
            }
            else
            {
                _icmsst = 0;
                _icmsstbc = 0;
                _icmsstmva = 0;
                _icmsstreducao = 0;
                _icmssttotal = 0;
            }

            if (_piscst == "04" ||
                _piscst == "06" ||
                _piscst == "07" ||
                _piscst == "08" ||
                _piscst == "09")
            {
                _pis = 0;
                _pisbc = 0;
                _pistotal = 0;
            }
            else if (_pis > 0)
            {
                _pisbc = _totalmercadoria;
                _pistotal = _pisbc * (_pis / 100);
            }
            else
            {
                _pisbc = 0;
                _pistotal = 0;
            }

            if (_cofinscst == "04" ||
                _cofinscst == "06" ||
                _cofinscst == "07" ||
                _cofinscst == "08" ||
                _cofinscst == "09")
            {
                _cofins = 0;
                _cofinsbc = 0;
                _cofinstotal = 0;
            }
            else if (_cofins > 0)
            {
                _cofinsbc = _totalmercadoria;
                _cofinstotal = _cofinsbc * (_cofins / 100);
            }
            else
            {
                _cofinsbc = 0;
                _cofinstotal = 0;
            }

            //_totalnota = _totalmercadoria + _ipitotal + _icmssttotal;
            _totalnota = _totalmercadoria + _ipitotal + _icmssttotal + _valorfrete + _valoroutras + _valorseguro;

            if (icmscst == "40" && _arealivrecomercio == true)
            {
                _totalnota = Decimal.Parse((Double.Parse(totalnota.ToString()) - Double.Parse((Double.Parse(totalnota.ToString()) * 0.07).ToString())).ToString());
            }
        }

        public void CalcularNfeSimplesNacional()
        {
            Boolean ipint;

            String ufdestino;
            String uforigem;

            ufdestino = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select estado from estados where id=" + _idufdestino);
            uforigem = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select estado from estados where id=" + _iduforigem);

            String result = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select aliquota from ipi where id = " + _idipi);

            if (result != null)
            {
                result = result.ToUpper();
                ipint = (result == "NT");
            }
            else
            {
                ipint = false;
            }

            _qtde = clsVisual.Truncar(_qtde, 5);
            _preco = clsVisual.Truncar(_preco, 5);

            _totalmercadoria = _qtde * _preco;
            _pesototal = _qtde * _pesounit;

            //if (ufdestino == "EX")
            //{
            //    _ipi = 0;
            //    _ipicst = "54";
            //}
            //else if (_isentoipi == true)
            //{              
            //    _ipi = 0;
            //    if (_ipicst == "50" ||
            //        _ipicst == "51" ||
            //        _ipicst == "53")    // 54 e 55 não devem ser modificados
            //    {
            //        _ipicst = "52";
            //    }
            //}
            //else if (_ipicst == "51" ||
            //         _ipicst == "52" ||
            //         _ipicst == "53" ||
            //         _ipicst == "54" ||
            //         _ipicst == "55" ||
            //         _ipicst == "01" ||
            //         _ipicst == "02" ||
            //         _ipicst == "03" ||
            //         _ipicst == "04" ||
            //         _ipicst == "05")
            //{
            //    _ipi = 0;
            //}
            //else
            //{
            //    if (ipint == true)
            //    {
            //        _ipi = 0;
            //        _ipicst = "53";
            //    }
            //    else if (_ipi == 0)
            //    {
            //        _ipicst = "51";
            //    }
            //    else if (_ipicst == "51" && _ipi > 0)
            //    {
            //        _ipicst = "50";
            //    }
            //}

            //if (_ipicst == "00" ||
            //    _ipicst == "49" ||
            //    _ipicst == "50" ||
            //    _ipicst == "99")
            //{
            //    if (_origem == "0")
            //    {
            //        _ipibc = _totalmercadoria;
            //    }
            //    else
            //    {
            //        _ipibc = 0;
            //    }
            //}
            //else
            //{
            //    _ipibc = 0;
            //}

            _ipitotal = (_ipi / 100) * _ipibc;

            if (_icmscst == "30" ||
                _icmscst == "40" ||
                _icmscst == "41" ||
                _icmscst == "50" ||
                _icmscst == "51")
            {
                icms = 0;
            }
            else
            {
                //icms = Parser.DecimalParse(clsProcedures.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select aliquota from ipiestadosicms where idipi=" + idipi + " and idestado = " + _iduforigem + " and idestadodestino = " + _idufdestino));
            }

            if (_icmscst == "20" ||
                _icmscst == "70")
            {
                ///_icmsreducao = Parser.DecimalParse(clsProcedures.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select reducao from ipiestadosicms where idipi=" + idipi + " and idestado = " + _iduforigem + " and idestadodestino = " + _idufdestino));
            }

            if ((_icmscst == "30" || _icmscst == "70") && ufdestino != "EX" && ufdestino != uforigem && consumo == true)
            {
                if (ufdestino == "MT")
                {
                    //_icmsstmva = cnaealiquota;
                }
                else
                {
                    //_icmsstmva = Parser.DecimalParse(clsProcedures.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select iva from ipiestadosicms where idipi=" + idipi + " and idestado = " + _iduforigem + " and idestadodestino = " + _idufdestino));
                }

                //_icmsst = Parser.DecimalParse(clsProcedures.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select aliquota from ipiestadosicms where idipi=" + idipi + " and idestado = " + _idufdestino + " and idestadodestino = " + _idufdestino));
                //_icmsstreducao = Parser.DecimalParse(clsProcedures.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select reducaoiva from ipiestadosicms where idipi=" + idipi + " and idestado = " + _idufdestino + " and idestadodestino = " + _idufdestino));
            }

            if (_icms > 0)
            {
                // contribuinte == false
                if (consumo == true)
                {
                    _icmsbc = _totalmercadoria + _ipitotal;
                }
                else
                {
                    _icmsbc = _totalmercadoria;
                }

                if (valoroutrassomabcicms == true) _icmsbc += valoroutras;
                if (valorfretesomabcicms == true) _icmsbc += valorfrete;
                if (valorsegurosomabcicms == true) _icmsbc += valorseguro;

                if (_icmsreducao > 0)
                {
                    _icmsbc = _icmsbc - (_icmsbc * (_icmsreducao / 100));
                }

                _icmstotal = _icmsbc * (_icms / 100);
            }
            else
            {
                _icmsbc = 0;
                _icmsreducao = 0;
                _icmstotal = 0;
            }

            if (_icmsst > 0 && ufdestino != "EX" && (_icmscst == "10" || _icmscst == "30" || _icmscst == "70" || _icmscst == "90"))
            {
                _icmsstbc = totalmercadoria + ipitotal;
                _icmsstbc = _icmsstbc + (_icmsstbc * (_icmsstmva / 100));

                if (_icmsstreducao > 0)
                {
                    _icmsstbc = _icmsstbc - (_icmsstbc * (_icmsstreducao / 100));
                }

                _icmssttotal = _icmsstbc * (icmsst / 100);
                _icmssttotal = _icmssttotal - _icmstotal;
            }
            else
            {
                _icmsst = 0;
                _icmsstbc = 0;
                _icmsstmva = 0;
                _icmsstreducao = 0;
                _icmssttotal = 0;
            }

            if (_piscst == "04" ||
                _piscst == "06" ||
                _piscst == "07" ||
                _piscst == "08" ||
                _piscst == "09")
            {
                _pis = 0;
                _pisbc = 0;
                _pistotal = 0;
            }
            else if (_pis > 0)
            {
                _pisbc = _totalmercadoria;
                _pistotal = _pisbc * (_pis / 100);
            }
            else
            {
                _pisbc = 0;
                _pistotal = 0;
            }

            if (_cofinscst == "04" ||
                _cofinscst == "06" ||
                _cofinscst == "07" ||
                _cofinscst == "08" ||
                _cofinscst == "09")
            {
                _cofins = 0;
                _cofinsbc = 0;
                _cofinstotal = 0;
            }
            else if (_cofins > 0)
            {
                _cofinsbc = _totalmercadoria;
                _cofinstotal = _cofinsbc * (_cofins / 100);
            }
            else
            {
                _cofinsbc = 0;
                _cofinstotal = 0;
            }

            //_totalnota = _totalmercadoria + _ipitotal + _icmssttotal;
            _totalnota = _totalmercadoria + _ipitotal + _icmssttotal + _valorfrete + _valoroutras + _valorseguro;

            if (icmscst == "40" && _arealivrecomercio == true)
            {
                _totalnota = Decimal.Parse((Double.Parse(totalnota.ToString()) - Double.Parse((Double.Parse(totalnota.ToString()) * 0.07).ToString())).ToString());
            }
        }

        public void CalcularNota()
        {
            //Decimal ipialiquota;
            Boolean ipint;

            //Decimal cnaealiquota;
            //Int32 cnaeid = Parser.Int32Parse(clsProcedures.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select idcnae from cliente where id=" + _idcliente));
            //cnaealiquota = Parser.DecimalParse(clsProcedures.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select iva from tab_cnae_uf where idtabcnae=" + cnaeid + " and iduf=" + _idufdestino));

            String ufdestino;
            String uforigem;

            ufdestino = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select estado from estados where id=" + _idufdestino);
            uforigem = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select estado from estados where id=" + _iduforigem);

            //ipialiquota = Parser.DecimalParse(clsProcedures.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select aliquota from ipi where id = " + _idipi));
            String result = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select aliquota from ipi where id = " + _idipi);

            if (result != null)
            {
                result = result.ToUpper();
                ipint = (result == "NT");
            }
            else
            {
                ipint = false;
            }

            _qtde = clsVisual.Truncar(_qtde, 5);
            _preco = clsVisual.Truncar(_preco, 6);


            // Se for para Área de Livre Comércio e isento de imposto, realiza o desconto do preço
            //if (_icmscst == "40" && _arealivrecomercio == true)
            //{
            //    preco = preco - (0.07 * preco);
            //    preco = clsVisual.Truncar(preco, 5);
            //}

            _totalmercadoria = _qtde * _preco;
            _pesototal = _qtde * _pesounit;
            _totalprevisto = _qtdesaldo * _preco;

            if (ufdestino == "EX")
            {
                _ipi = 0;
                _ipicst = "54";
            }
            else if (_isentoipi == true)
            {
                //else if (_isentoipi == true || (_revendedor == false && _consumo == false))
                _ipi = 0;
                if (_ipicst == "50" ||
                    _ipicst == "51" ||
                    _ipicst == "53")    // 54 e 55 não devem ser modificados
                {
                    _ipicst = "52";
                }
            }
            else if (_ipicst == "51" ||
                     _ipicst == "52" ||
                     _ipicst == "53" ||
                     _ipicst == "54" ||
                     _ipicst == "55" ||
                     _ipicst == "01" ||
                     _ipicst == "02" ||
                     _ipicst == "03" ||
                     _ipicst == "04" ||
                     _ipicst == "05")
            {
                _ipi = 0;
            }
            else
            {
                //_ipi = ipialiquota;

                if (ipint == true)
                {
                    _ipi = 0;
                    _ipicst = "53";
                }
                else if (_ipi == 0)
                {
                    _ipicst = "51";
                }
                else if (_ipicst == "51" && _ipi > 0)
                {
                    _ipicst = "50";
                }
            }

            if (_ipicst == "00" ||
                _ipicst == "49" ||
                _ipicst == "50" ||
                _ipicst == "99")
            {
                if (_origem == "0")
                {
                    _ipibc = _totalmercadoria;
                }
                else
                {
                    _ipibc = 0;
                }
            }
            else
            {
                _ipibc = 0;
            }

            _ipitotal = (_ipi / 100) * _ipibc;

            if (_icmscst == "30" ||
                _icmscst == "40" ||
                _icmscst == "41" ||
                _icmscst == "50" ||
                _icmscst == "51")
            {
                icms = 0;
            }
            else
            {
                //icms = Parser.DecimalParse(clsProcedures.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select aliquota from ipiestadosicms where idipi=" + idipi + " and idestado = " + _iduforigem + " and idestadodestino = " + _idufdestino));
            }

            if (_icmscst == "20" ||
                _icmscst == "70")
            {
                ///_icmsreducao = Parser.DecimalParse(clsProcedures.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select reducao from ipiestadosicms where idipi=" + idipi + " and idestado = " + _iduforigem + " and idestadodestino = " + _idufdestino));
            }

            if ((_icmscst == "30" || _icmscst == "70") && ufdestino != "EX" && ufdestino != uforigem && consumo == true)
            {
                if (ufdestino == "MT")
                {
                    //_icmsstmva = cnaealiquota;
                }
                else
                {
                    //_icmsstmva = Parser.DecimalParse(clsProcedures.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select iva from ipiestadosicms where idipi=" + idipi + " and idestado = " + _iduforigem + " and idestadodestino = " + _idufdestino));
                }

                //_icmsst = Parser.DecimalParse(clsProcedures.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select aliquota from ipiestadosicms where idipi=" + idipi + " and idestado = " + _idufdestino + " and idestadodestino = " + _idufdestino));
                //_icmsstreducao = Parser.DecimalParse(clsProcedures.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select reducaoiva from ipiestadosicms where idipi=" + idipi + " and idestado = " + _idufdestino + " and idestadodestino = " + _idufdestino));
            }

            if (_icms > 0)
            {
                // contribuinte == false
                if (consumo == true)
                {
                    _icmsbc = _totalmercadoria + _ipitotal;
                }
                else
                {
                    _icmsbc = _totalmercadoria;
                }
                if (_basemp > 0)  // se base de mat.prima 
                {
                    _icmsbc = (_icmsbc * (_basemp / 100));
                }

                if (valoroutrassomabcicms == true) _icmsbc += valoroutras;
                if (valorfretesomabcicms == true) _icmsbc += valorfrete;
                if (valorsegurosomabcicms == true) _icmsbc += valorseguro;

                if (_icmsreducao > 0)
                {
                    _icmsbc = _icmsbc - (_icmsbc * (_icmsreducao / 100));
                }

                _icmstotal = _icmsbc * (_icms / 100);
            }
            else
            {
                _icmsbc = 0;
                _icmsreducao = 0;
                _icmstotal = 0;
            }

            if (_icmsst > 0 && ufdestino != "EX" && (_icmscst == "10" || _icmscst == "30" || _icmscst == "70" || _icmscst == "90"))
            {
                _icmsstbc = totalmercadoria + ipitotal;
                _icmsstbc = _icmsstbc + (_icmsstbc * (_icmsstmva / 100));

                if (_icmsstreducao > 0)
                {
                    _icmsstbc = _icmsstbc - (_icmsstbc * (_icmsstreducao / 100));
                }

                _icmssttotal = _icmsstbc * (icmsst / 100);
                _icmssttotal = _icmssttotal - _icmstotal;
            }
            else
            {
                _icmsst = 0;
                _icmsstbc = 0;
                _icmsstmva = 0;
                _icmsstreducao = 0;
                _icmssttotal = 0;
            }

            if (_piscst == "04" ||
                _piscst == "06" ||
                _piscst == "07" ||
                _piscst == "08" ||
                _piscst == "09")
            {
                _pis = 0;
                _pisbc = 0;
                _pistotal = 0;
            }
            else if (_pis > 0)
            {
                _pisbc = _totalmercadoria;
                _pistotal = _pisbc * (_pis / 100);
            }
            else
            {
                _pisbc = 0;
                _pistotal = 0;
            }

            if (_cofinscst == "04" ||
                _cofinscst == "06" ||
                _cofinscst == "07" ||
                _cofinscst == "08" ||
                _cofinscst == "09")
            {
                _cofins = 0;
                _cofinsbc = 0;
                _cofinstotal = 0;
            }
            else if (_cofins > 0)
            {
                _cofinsbc = _totalmercadoria;
                _cofinstotal = _cofinsbc * (_cofins / 100);
            }
            else
            {
                _cofinsbc = 0;
                _cofinstotal = 0;
            }

            //_totalnota = _totalmercadoria + _ipitotal + _icmssttotal;
            _totalnota = _totalmercadoria + _ipitotal + _icmssttotal + _valorfrete + _valoroutras + _valorseguro;

            if (icmscst == "40" && _arealivrecomercio == true)
            {
                _totalnota = Decimal.Parse((Double.Parse(totalnota.ToString()) - Double.Parse((Double.Parse(totalnota.ToString()) * 0.07).ToString())).ToString());
            }
        }

    }
}
