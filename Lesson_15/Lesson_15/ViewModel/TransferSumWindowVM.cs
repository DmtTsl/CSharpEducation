﻿namespace Lesson_15
{
    public class TransferSumWindowVM
    {
        public decimal SumToTransfer { get; set; }
        public int AccountNumberToTransfer { get; set; }
        public TransferSumWindow TransferSumWindow { get; set; }
        public TransferSumWindowVM(TransferSumWindow window)
        {
            TransferSumWindow = window;
        }
        private RelayCommand _transfer;
        public RelayCommand Transfer
        {
            get
            {
                return _transfer ??
                    (_transfer = new RelayCommand(obj =>
                    {
                        TransferSumWindow.DialogResult = true;

                    }, (obj) => SumToTransfer != 0 && AccountNumberToTransfer != 0));
            }
        }
    }
}
