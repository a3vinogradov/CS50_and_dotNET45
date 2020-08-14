﻿using System;

namespace Wrox.ProCSharp.Delegates
{
    public class CarInfoEventArgs : EventArgs
    {
        public CarInfoEventArgs(string car)
        {
            this.Car = car;
        }

        public string Car { get; private set; }
    }

    public class CarDealer
    {
        public event EventHandler<CarInfoEventArgs> NewCarInfo;

        public void NewCar(string car)
        {
            Console.WriteLine("CarDealer, new car {0}", car);
            //RaiseNewCarInfo(car);
            NewCarInfo(this, new CarInfoEventArgs(car));
        }
        // Вероятно, в C#8 проверка делегата на null встроенная. типа поглощения nullable ??
        //protected virtual void RaiseNewCarInfo(string car)
        //{
        //  EventHandler<CarInfoEventArgs> newCarInfo = NewCarInfo;
        //  if (newCarInfo != null)
        //  {
        //    newCarInfo(this, new CarInfoEventArgs(car));
        //  }
        //}
    }
}
