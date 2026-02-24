using System;

namespace CRCorreaInfo
{
    public class clsUsuarioInfo
    {
        public Int32 Id { get; set; }
        public Int32 idfolha { get; set; }
        public String cognome { get; set; }
        public Int32 filial { get; set; }
        public String Usuario { get; set; }
        public String Senha { get; set; }
        public DateTime Dataval { get; set; }
        public DateTime Datault { get; set; }
        public Boolean Ativo { get; set; }
        public DateTime Horaini { get; set; }
        public DateTime Horafim { get; set; }

        public Boolean Weekend { get; set; }
        public DateTime Trocar { get; set; }
        public String Trocasenha { get; set; }
        public String Email { get; set; }
        public String Nivelusuario { get; set; }
        public String Emailsenha { get; set; }
        public String Grupo { get; set; }
        public String Gruposystem { get; set; }
        public int idmaqct { get; set; }

    }
}
