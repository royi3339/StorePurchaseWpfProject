using PL.Commands;

namespace PL.VM
{
    public class mainWindowVM :viewModel
    {
        private bool flag = false;

        public genericCommand modeSwapB { get; set; }

        public purchaseProductVM currentPurchase { get; set; }

        public statisticVM currentStatistic { get; set; }

        public mainWindowVM()
        {
            currentPurchase = new purchaseProductVM();
            currentStatistic = new statisticVM();

            modeSwapB = new genericCommand();
            modeSwapB.genericClickEvent += modeSwapFoo;
            currentPurchase.purchaseVisible = true;
        }

        private void modeSwapFoo()
        {
            if (flag)
            {
                currentPurchase.purchaseVisible = true;
                currentStatistic. statisticVisible = false;
            }
            else
            {
                currentPurchase.purchaseVisible = false;
                currentStatistic.statisticVisible = true;
            }
            flag = !flag;
        }
    }
}
