using System.Threading;

namespace Everest.Common.Utils
{
    public static class ThreadPromotion
    {
        private static int IdUsuario;
        private static int IdPromotion;
        private static bool IsAlreadyAsigned;
        private static Thread thread;

        public static void ActivarPromocionParaUsuario(int idUsuario, int idPromotion)
        {
            if (idUsuario == default)
                IsAlreadyAsigned = false;
            if (!IsAlreadyAsigned)
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

        public static bool AgendarPromocionParaUsuario(int idUsuario, int idPromotion)
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
