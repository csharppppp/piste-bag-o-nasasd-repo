﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Parking
{
    public partial class Parkout : Form
    {

        public event EventHandler ParkingRecordAdded;
        public event EventHandler Parking;
        private string palteNum, Type;
        public Parkout()
        {
            InitializeComponent();


        }




        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Parkout_Load(object sender, EventArgs e)
        {
            var parkingRecordsManager = ParkingRecordsManager.Instance;
            var allParkingRecords = parkingRecordsManager.GetAllParkingRecords();

            for (int i = allParkingRecords.Count - 1; i >= 0; i--)
            {
                var record = allParkingRecords[i];
                if (record.Status != "Cleared")
                {
                    ParkOutList POL = new ParkOutList(flowPanelVH, this, setAmt, setStatus);
                    POL.UpdateLabels(record);
                    POL.ParkingRecordAdded += ParkOutList_ParkingRecordAdded;
                    listOfVehicle.Controls.Add(POL);
                }
            }



        }
        private void ParkOutList_ParkingRecordAdded(object sender, EventArgs e)
        {

            Parking?.Invoke(this, EventArgs.Empty);
            this.Close();
        }
        private void parkOutList1_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void listOfVehicle_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel1_Paint_1(object sender, PaintEventArgs e)
        {

        }

        public void getVHparkOutnType(string paltenum, string type)
        {
            palteNum = paltenum;
            Type = type;
        }
       


        private void button4_Click(object sender, EventArgs e)
        {
           

            var parkingRecordsManager = ParkingRecordsManager.Instance;
            var allParkingRecords = parkingRecordsManager.GetAllParkingRecords();      
            for (int i = allParkingRecords.Count - 1; i >= 0; i--)
            {
                var record = allParkingRecords[i];
                if (record.Status != "Cleared" && record.PlateNumber == palteNum)
                {
                    ParkingHistoyRecord carDetails = new ParkingHistoyRecord(record.PlateNumber, record.Type, record.Model, record.Driver, record.Phone,
                    record.ArrivalDate, record.ArrivalTime, parkOutDate.Value.ToString("MM/dd/yyyy"), parkOutTime.Value.ToString("hh:mm:ss tt"), setTIME, setHOURS);
                    var parkingHistoryRecords = ParkingRecordsManager.Instance;
                    parkingRecordsManager.AddParkingHistoryRecord(carDetails);
              

                    double total = double.Parse(setAmt.Text);
                    double amount = double.Parse(enterAmt.Text);

                    if (amount > total)
                    {
                        record.Status = "Cleared";
                        setChange.Text = amount - total + "";
                        setStatus.Text = "Succesfuly paid the amount";
                        setStatus.ForeColor = Color.GreenYellow;
                    }
                    else {
                        MessageBox.Show("Insufficient amount", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }



                    Parking?.Invoke(this, EventArgs.Empty);
                  
                }
            }
        }






        private void button2_Click_1(object sender, EventArgs e)
        {
            Parking?.Invoke(this, EventArgs.Empty);
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            listOfVehicle.Controls.Clear();
            var parkingRecordsManager = ParkingRecordsManager.Instance;
            var allParkingRecords = parkingRecordsManager.GetAllParkingRecords();
            bool foundRecord = false;
            int count = 0;
            for (int i = allParkingRecords.Count - 1; i >= 0; i--)
            {
                var record = allParkingRecords[i];
                if (record.Status != "Cleared")
                {
                    ParkOutList POL = new ParkOutList(flowPanelVH, this, setAmt, setStatus);
                    POL.UpdateLabels(record);
                    POL.ParkingRecordAdded += ParkOutList_ParkingRecordAdded;
                    listOfVehicle.Controls.Add(POL);
                    count++;
                }
            }

            if (count == 0)
            {
                Label noResultsLabel = new Label();
                noResultsLabel.Text = "No results found.";
                listOfVehicle.Controls.Add(noResultsLabel);
            }

            flowPanelVH.Controls.Clear();
            setAmt.Text = "";
            setStatus.Text = "";
            setChange.Text = "";
            enterAmt.Text = "";
            searchVHTxt.Text = "";
            setHours.Text = "";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            listOfVehicle.Controls.Clear();
            var parkingRecordsManager = ParkingRecordsManager.Instance;
            var allParkingRecords = parkingRecordsManager.GetAllParkingRecords();
            bool foundRecord = false;
            for (int i = allParkingRecords.Count - 1; i >= 0; i--)
            {
                var record = allParkingRecords[i];
                if (record.Status != "Cleared")
                {
                    if (record.PlateNumber.Contains(searchVHTxt.Text))
                    {
                        ParkOutList POL = new ParkOutList(flowPanelVH, this, setAmt, setStatus);
                        POL.UpdateLabels(record);
                        POL.ParkingRecordAdded += ParkOutList_ParkingRecordAdded;
                        listOfVehicle.Controls.Add(POL);
                        foundRecord = true;
                    }
                }

            }
            if (!foundRecord)
            {
                Label noResultsLabel = new Label();
                noResultsLabel.Text = "No results found.";
                listOfVehicle.Controls.Add(noResultsLabel);
            }
        }

        private void seeAll_Click(object sender, EventArgs e)
        {
            listOfVehicle.Controls.Clear();
            var parkingRecordsManager = ParkingRecordsManager.Instance;
            var allParkingRecords = parkingRecordsManager.GetAllParkingRecords();
            for (int i = allParkingRecords.Count - 1; i >= 0; i--)
            {
                var record = allParkingRecords[i];
                if (record.Status != "Cleared")
                {
                    ParkOutList POL = new ParkOutList(flowPanelVH, this, setAmt, setStatus);
                    POL.UpdateLabels(record);
                    POL.ParkingRecordAdded += ParkOutList_ParkingRecordAdded;
                    listOfVehicle.Controls.Add(POL);
                }
            }
        }

        private void change_Click(object sender, EventArgs e)
        {

        }


        double setHOURS,setTIME;
        
        private void button5_Click(object sender, EventArgs e)
        {
            //here to compute time/amt


            var parkingRecordsManager = ParkingRecordsManager.Instance;
            var allParkingRecords = parkingRecordsManager.GetAllParkingRecords();

            for (int i = allParkingRecords.Count - 1; i >= 0; i--)
            {
                var record = allParkingRecords[i];
                if (record.PlateNumber == palteNum)
                {           
                        if (record.Status != "Cleared")
                        {

                        double flagDown = 0;
                        double succeedingAmt = 0;
                        var vehiclemanger = VehicleManger.Instance;
                        var VPM = vehiclemanger.GetVPM();
                        foreach (var vpm in VPM)
                        {
                            if (vpm.vehicleType == Type)
                            {
                                flagDown = vpm.flagDown;
                                succeedingAmt = vpm.additionalAmtPerHour;
                            }

                        }
                        // arrival dateTime
                        string arrivalDate = record.ArrivalDate;
                        string arrvalTime = record.ArrivalTime;
                        string arrivalDateTime = arrivalDate + " " + arrvalTime;
                        DateTime parkin = DateTime.Parse(arrivalDateTime);


                  
                        // Park out time
                        DateTime selectedDate = parkOutDate.Value;
                        DateTime selectedTime = parkOutTime.Value;

                        string formattedDateTime = selectedDate.ToString("MM/dd/yyyy");
                        string time = selectedTime.ToString("hh:mm:ss tt");
                        string parkout = formattedDateTime + " " + time;
                        DateTime toPARKOUT = DateTime.Parse(parkout);


                        TimeSpan duration = toPARKOUT -  parkin;

                        double HOURS = 0;
                        int day = 0;
                        int year = toPARKOUT.Year - parkin.Year;

                        if (year >= 1)
                        {
                            day = (int)duration.TotalDays % 365;
                            HOURS = (duration.TotalHours - year * 24 * 365) % 24;
                            Console.WriteLine("Parking Time: " + year + " Year/s " + day + " Day/s " + " and " + HOURS.ToString("0.00") + " Hour/s");
                        }
                        else if (duration.TotalHours > 23)
                        {
                            day = (int)duration.TotalDays;
                            HOURS = duration.TotalHours % 24;
                            Console.WriteLine("Parking Time: " + day + " Day/s " + " and " + HOURS.ToString("0.00") + " Hour/s");
                        }
                        else
                        {
                            Console.WriteLine("Parking Time: " + duration.Hours.ToString("0.00") + " Hour/s");
                        }

                        HOURS = duration.TotalHours;
                        double amount = flagDown;
                        double totalAmount = 0;

                        if (HOURS >1)
                        {
                            totalAmount = flagDown + succeedingAmt * (HOURS - 1);
                        }
                        else
                        {
                            totalAmount = amount;
                        }





                        double hours = Math.Round(HOURS, 2);
                        double amt = Math.Round(totalAmount, 2);

                        if (toPARKOUT >= parkin)
                        {
                            setHOURS = amt;
                            setTIME = hours;
                            setAmt.Text = amt + "";
                            setHours.Text = hours + "";
                            invalidT.Text = "";
                            invalidD.Text = "";
                        }
                        else
                        {
                            //  MessageBox.Show("Invalid date/time", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            DateTime p1 = DateTime.Parse(arrivalDate);
                            DateTime p2 = DateTime.Parse(arrvalTime);

                            if (parkOutDate.Value < p1 && parkOutTime.Value < p2)
                            {
                                invalidD.Text = "Invalid Date!";
                                invalidT.Text = "Invalid Time!";
                            }
                            else if (parkOutDate.Value < p1)
                                invalidD.Text = "Invalid Date!";
                            else if (parkOutTime.Value < p2)
                            {
                                invalidT.Text = "Invalid Time!";
                            }
                           
                             
                        }






                    }            
                }
            }
            





        }
    }
}


