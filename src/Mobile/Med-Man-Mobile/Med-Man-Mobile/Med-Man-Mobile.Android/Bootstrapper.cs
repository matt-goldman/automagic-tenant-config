namespace Med_Man_Mobile.Droid
{
    public class Bootstrapper : MedManMobile.Bootstrapper
    {
        public static void Init()
        {
            var instance = new Bootstrapper();
        }
    }
}
