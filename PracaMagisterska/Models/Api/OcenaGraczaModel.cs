using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PracaMagisterska.Models.Api
{
    public class OcenaGraczaModel
    {
        public long Id { get; set; }
        public byte Ocena { get; set; }
        public byte NumerZadania { get; set; }
        public int NumerRundy { get; set; }
        public Nullable<byte> DecyzjaGracza { get; set; }
        public Nullable<byte> DecyzjaTrenera { get; set; }
    }
}