internal class Program
{
    public enum StudentState { TERDAFTAR, CUTI, AKTIF, LULUS};
    public enum Trigger { MENGAJUKAN_CUTI, MENYELESAIKAN_CUTI, CETAK_KSM, LULUS};

    class Mahasiswa
    {
        public StudentState curentState = StudentState.TERDAFTAR;
        public int biayaPembayaran;
        public class Transition
        {
            public StudentState stateAwal;
            public StudentState stateAkhir;
            public Trigger trigger;

            public Transition(StudentState stateAwal, StudentState stateAkhir, Trigger trigger)
            {
                this.stateAwal = stateAwal;
                this.stateAkhir = stateAkhir;
                this.trigger = trigger;
            }
        }
        Transition[] transisi = 
        {
            new Transition(StudentState.TERDAFTAR, StudentState.CUTI, Trigger.MENGAJUKAN_CUTI),
            new Transition(StudentState.CUTI, StudentState.TERDAFTAR, Trigger.MENYELESAIKAN_CUTI),
            new Transition(StudentState.TERDAFTAR, StudentState.AKTIF, Trigger.CETAK_KSM),
            new Transition(StudentState.AKTIF, StudentState.LULUS, Trigger.LULUS)
        };

        private StudentState GetStateBerikutnya(StudentState stateAwal, Trigger trigger) 
        {
            StudentState stateAkhir = stateAwal;

            for(int i = 0; i < transisi.Length; i++)
            {
                Transition perubahan = transisi[i];

                if (stateAwal == perubahan.stateAwal && trigger == perubahan.trigger)
                {
                    stateAkhir = perubahan.stateAkhir;
                }
            }

            return stateAkhir;
        }

        public void ActivateTrigger(Trigger trigger)
        {
            curentState = GetStateBerikutnya(curentState, trigger);

            Console.WriteLine("State sekarang : " + curentState);

            if (curentState == StudentState.LULUS)
            {
                Console.WriteLine("Mahasiswa telah lulus, data dihapus");
            }
            else if (curentState == StudentState.AKTIF)
            {
                Console.WriteLine("Mahasiswa sedang mengambil kuliah");
            }
        }
    }
    private static void Main(string[] args)
    {
        Mahasiswa objMhs = new Mahasiswa();
        objMhs.ActivateTrigger(Trigger.MENGAJUKAN_CUTI);
        objMhs.ActivateTrigger(Trigger.MENYELESAIKAN_CUTI);
        objMhs.ActivateTrigger(Trigger.CETAK_KSM);
        objMhs.ActivateTrigger(Trigger.LULUS);
    }
}