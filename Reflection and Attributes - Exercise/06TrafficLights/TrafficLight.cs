namespace _06TrafficLights
{
    using System;

    public class TrafficLight
    {

        internal enum Signal
        {
            Red = 0,
            Green = 1,
            Yellow = 2
        }


        private Signal signal;

        public string TogleSignal(string color)
        {
            var colors = Enum.GetValues(typeof(Signal));

            Signal currentSignal;
            if (!Enum.TryParse(color, out currentSignal)) throw new ArgumentException("Invalid color!");

            int index = ((int)currentSignal + 1) % colors.Length;
            currentSignal = (Signal)colors.GetValue(index);

            return currentSignal.ToString();
        }
    }
}