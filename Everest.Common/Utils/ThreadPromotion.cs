using System.Threading;

namespace Everest.Common.Utils
{
    public static class ThreadPromotion
    {
        private static string IdUsuario;
        private static int IdPromotion;
        private static bool IsAlreadyAsigned;
        private static Thread thread;

        public static void GenerarPromocion(int idPromotion)
        {
            IdUsuario = default;
            IdPromotion = idPromotion;
            IsAlreadyAsigned = false;
        }

        public static void ActivarPromocionParaUsuario(string idUsuario, int idPromotion)
        {
            if (!IsAlreadyAsigned && IdUsuario == default)
            {
                IdUsuario = idUsuario;
                IdPromotion = idPromotion;
                thread = new Thread(() => EsperarTreMinutos());
                thread.Start();
            }           
        }

        private static void EsperarTreMinutos()
        {
            Thread.Sleep(180000);
            IdUsuario = default;
            if (!IsAlreadyAsigned)
            {
                thread = new Thread(() => EsperarTreMinutos());
                thread.Start();
            }   
        }

        public static bool AgendarPromocionParaUsuario(string idUsuario, int idPromotion)
        {
            if (IsAlreadyAsigned)
                return false;
            if (IdUsuario == idUsuario && IdPromotion == idPromotion)
            {
                IsAlreadyAsigned = true;
                return IsAlreadyAsigned;
            }
            return false;
        }
                
    }
}
