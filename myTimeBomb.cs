using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Windows.Forms;

namespace TimeBomb
{
    class myTimeBomb
    {
        Timer startTimer = new Timer();
        /*This timer would start when the time completes for starting up.*/

        Timer stopTimer = new Timer();
        /*This timer would start when the process starts and is used for killing the process.*/

        Process myProgram = new Process();
        /*This object of process would be for the program that has to be started and killed.*/

        void TimerSet(Timer timer, int time)
        {
            timer.Interval = time*1000;
            timer.Enabled = true;
        }
        public void checkTime(int hour, int minute, int seconds)
        {
            myProgram.StartInfo.FileName = "cmd.exe";
            startTimer.Tick += startTimer_Tick;
            stopTimer.Tick += stopTimer_Tick;

            DateTime _dateTime = DateTime.Now;
            int time = (_dateTime.Hour) * 60 * 60 + (_dateTime.Minute) * 60 + (_dateTime.Second);
            int givenTime = (hour * 60 * 60) +( minute * 60) + seconds;
            if (time<givenTime)
            {
                TimerSet(startTimer, givenTime - time);
                //If the time has not come yet, then the timer for starting
                //the process would have the interval of the remaining time needed.

                TimerSet(stopTimer, givenTime * 3);
                //givenTime*3 is the interval for the stop timer.
                //This means that it should stop after givenTime*3 hours.
            }
            else if (time>givenTime && time<givenTime*3)
                //givenTime*3 here means that if you want the timer to stop after your given time plus three more hours.
            {
                startProcess(myProgram);
                //Process Start
                TimerSet(stopTimer, time - (givenTime * 3));
                //The stopTimer's interval would be the difference of 3 times the
                //givenTime (the time at which the process must stop) and the current time.
            }
        }

        void stopTimer_Tick(object sender, EventArgs e)
        {
            killProcess(myProgram);
            Timer timer = (Timer)sender;
            timer.Enabled = false;
        }
        void startTimer_Tick(object sender, EventArgs e)
        {
            startProcess(myProgram);
            Timer timer = (Timer)sender;
            timer.Enabled = false;
        }
        void startProcess(Process program)
        {
            program.Start();
        }
        void killProcess(Process program)
        {
            program.Kill();
        }
    }
}
