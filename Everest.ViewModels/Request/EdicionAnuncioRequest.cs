namespace Everest.ViewModels.Request
{
    public class EdicionAnuncioRequest : CreacionAnuncioRequest
    {
        public int IdAnuncio { get; set; }
        public bool Activo { get; set; }
    }
}
